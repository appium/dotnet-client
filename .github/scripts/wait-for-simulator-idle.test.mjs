import assert from 'node:assert/strict';
import {spawnSync} from 'node:child_process';
import {mkdtempSync, readFileSync, rmSync, writeFileSync} from 'node:fs';
import {tmpdir} from 'node:os';
import {delimiter, join} from 'node:path';
import {fileURLToPath} from 'node:url';
import {test} from 'node:test';

const script = fileURLToPath(new URL('./wait-for-simulator-idle.mjs', import.meta.url));
const udid = '11111111-2222-3333-4444-555555555555';

function snapshot(cpu) {
  return `100 1 0.0 /runtime/launchd_sim --udid ${udid}\n`
    + `101 100 ${cpu} /runtime/SpringBoard\n`
    + '200 1 0.0 /runtime/launchd_sim --udid another-simulator\n'
    + '201 200 99.0 /runtime/SpringBoard\n';
}

// Run the real CLI with deterministic ps output, without booting a simulator.
function run(snapshots, env = {}, args = [udid]) {
  const dir = mkdtempSync(join(tmpdir(), 'simulator-idle-test-'));
  try {
    const countPath = join(dir, 'count');
    writeFileSync(countPath, '0');
    writeFileSync(join(dir, 'ps'), `#!${process.execPath}
const fs = require('node:fs');
const path = ${JSON.stringify(countPath)};
const count = Number(fs.readFileSync(path, 'utf8'));
fs.writeFileSync(path, String(count + 1));
const snapshots = ${JSON.stringify(snapshots)};
process.stdout.write(snapshots[Math.min(count, snapshots.length - 1)]);
`, {mode: 0o755});
    const result = spawnSync(process.execPath, [script, ...args], {
      encoding: 'utf8',
      timeout: 10_000,
      env: {
        ...process.env,
        PATH: `${dir}${delimiter}${process.env.PATH}`,
        WAIT_SIM_IDLE_CPU_THRESHOLD: '20',
        WAIT_SIM_IDLE_CONSECUTIVE: '3',
        WAIT_SIM_IDLE_INTERVAL_MS: '1',
        WAIT_SIM_IDLE_MAX_WAIT_MS: '5000',
        ...env,
      },
    });
    assert.ifError(result.error);
    return {...result, samples: Number(readFileSync(countPath, 'utf8'))};
  } finally {
    rmSync(dir, {recursive: true, force: true});
  }
}

test('waits for consecutive low CPU samples from only the selected simulator', () => {
  const result = run([snapshot(50), snapshot(5), snapshot(5), snapshot(5)]);
  assert.equal(result.status, 0);
  assert.equal(result.samples, 4);
  assert.match(result.stdout, /Simulator settled/);
});

test('sums child CPU and resets the count when the total reaches the threshold', () => {
  const busy = snapshot(10) + '102 100 10.0 /runtime/another-service\n';
  const result = run([snapshot(5), snapshot(5), busy, snapshot(5), snapshot(5), snapshot(5)]);
  assert.equal(result.status, 0);
  assert.equal(result.samples, 6);
});

test('warns and proceeds when the wait budget is exhausted', () => {
  const result = run([snapshot(90)], {WAIT_SIM_IDLE_MAX_WAIT_MS: '0'});
  assert.equal(result.status, 0);
  assert.equal(result.samples, 1);
  assert.match(result.stderr, /::warning::.*did not settle.*proceeding anyway/);
});

test('warns and proceeds when the selected simulator process disappears', () => {
  const result = run([snapshot(50), '200 1 0.0 /runtime/launchd_sim --udid another-simulator\n']);
  assert.equal(result.status, 0);
  assert.equal(result.samples, 2);
  assert.match(result.stderr, /::warning::.*process tree disappeared/);
});

test('rejects a missing UDID before sampling processes', () => {
  const result = run([snapshot(0)], {}, []);
  assert.equal(result.status, 1);
  assert.equal(result.samples, 0);
  assert.match(result.stderr, /Usage:/);
});
