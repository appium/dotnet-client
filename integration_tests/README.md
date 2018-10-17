# Appium.Net Integration Tests

## Running tests locally (local service)

- Ensure node.js is installed
- Install appium server instance via npm
- `cp env.json.sample env.json`
- Update `env.json` set DEV=true
- Run the tests in NUnit.

## Running tests on remote appium server

- `cp env.json.sample env.json`
- Update `env.json` set isRemoteServer=true
- Update 'env.json' set remoteAppiumServerUri i.e. remoteAppiumServerUri="http://10.200.1.2:4723/wd/hub"
- Run the tests in NUnit.
