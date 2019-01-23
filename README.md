TFL Road Status
===============

This console application displays road conditions according to the TfL API available at: https://api.tfl.gov.uk

For a developer key please register here: https://api-portal.tfl.gov.uk

- We're using [Paket](https://fsprojects.github.io/Paket/) to manage dependencies

- And [FAKE](https://fake.build/) (F# Make) to build the executable

Build instructions
------------------

1. From a clean slate it's best to review the [FAKE getting started](https://fake.build/fake-gettingstarted.html) guide

2. Also, you'll need do [download](https://github.com/fsprojects/Paket/releases/tag/5.195.7) the latest **paket.bootstrapper.exe** into the .paket folder

3. Rename this newly downloaded file **paket.exe**

4. Finally, from the root folder run `fake run build.fsx --target Test`

That final step should have restored packages, built the solution and run the unit tests.
You may verify that this has worked by confirming that the file **RoadStats.exe** exists in the `/bin/release` folder. Please ignore errors from xUnit2 - the key thing is that no tests have failed.

Running the application
-----------------------
Update each **app.config** so that `AppId` and `Key` contain those values given to you following [registration](https://api-portal.tfl.gov.uk).

Running the application with a single argument, `RoadStatus.exe A2`, from the command line returns the road's condition.
In this example output is shown below for the [A2 road](https://www.londontraffic.org/a2/).

```
The status of the A2 is as follows
    Road Status is Serious
    Road Status Description is Serious Delays
```

When a non-existent road is queried, the [A101](https://www.londontraffic.org/a101/) for example, the response is as follows:

```
A101 is not a valid road
```

Notes
-----

Currently, the feature tests aren't being run as part of the build process. For now it's necessary to open the solution using Visual Studio and run them as part of the development process. A pull request with a fix so that these too are run as part of the build would be very welcome.