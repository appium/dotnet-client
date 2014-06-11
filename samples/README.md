#appium-dotnet-driver samples

This sample uses the nunit test framework.

## Run locally

### App download (DEFAULT)

- Start appium.
- Run the tests in nunit.

### DEV

- Clone appium.
- Build dev version of appium `./reset.sh --android --ios --dev --hardcore`
- `cp samples/env.json.sample samples/env.json`
- Update `samples/env.json` set DEV=true
- Start appium: `node .`
- Run the tests in nunit.

## RUN ON Sauce Labs

- `cp samples/env.json.sample samples/env.json`
- Update `samples/env.json` set SAUCE=true, and configure your sauce credentials.
- Run the tests in nunit.
