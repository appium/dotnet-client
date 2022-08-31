# Appium.Net Integration Tests

## Running tests locally (local service)

- Ensure node.js is installed
- Install appium server instance via npm
- `cp env.json.sample env.json`
- Update `env.json` set DEV=true
- Run the tests in NUnit.

## Running tests on remote appium server

- `cp env.json.sample env.json`
- Update `env.json` by setting isRemoteAppiumServer=true
- Ensure env.json File Properties is as followed: <br>
 A. 'Build action' property is set to 'None' <br>
 B. 'Copy to Output Directory' property is either set to 'Copy always' or 'Copy if newer' <br>
- Set remoteAppiumServerUri i.e. remoteAppiumServerUri="http://10.200.1.2:4723/wd/hub"
- Run the tests in NUnit.
