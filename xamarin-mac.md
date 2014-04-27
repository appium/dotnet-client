Steps to run tests on a mac using xamarin studio
=========================

* Install xamarin studio from - http://xamarin.com/studio
* Git clone appium repo
* From appium repo do ./reset --dev  (this step is needed for sample ios app)
* Navigate to /sample-code/examples/dotnet/AppiumDriverDemo
* type open AppiumDriverDemo.sln this should open the solution in xamarin studio
* Install xamarin nuget package manager following steps from https://github.com/mrward/monodevelop-nuget-addin
* Now open references in AppiumDriverDemo project  and click add references
* Search for newtonsoft, webdriver, appium and nunit packages, click manage and install
* Try building the project, if you get the message run your unit tests, you are done, otherwise close xamarin and try again
* Now start appium in another console
* Go to run -> run unit tests.
* You should see a unit test runner window and your tests should start on ios simulator
* If this is the first time you are running appium ios tests you may have to give permissions
* Appium server should also give you some logs
