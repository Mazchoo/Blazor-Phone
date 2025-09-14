### Installation

1. Download dotnet (tested with version 9.0) from https://dotnet.microsoft.com/en-us/download/dotnet/9.0.
2. Clone the repository

In the terminal:
  * cd into ./Blazor-Phone
  * Run `dotnet build`
  * Run `dotnut run`

### Debugging

Install Visual Studio (Including ASP.NET and Web Tools)
In this case the following version was used:

```
Microsoft Visual Studio Community 2022 (64-bit) - Current
Version 17.13.6
```

Open the `./BlazorPhone/BlazorPhone.sln` and run the project in https mode, in the dropdown text to run button ensure that Microsoft Edge is selected as the Browser.
Breakpoints can be set in the .cs files and debugged live.

To Debug the tests open the `./BlazorPhone.Tests/BlazorPhone.Tests.sln` and set a breakpoint in a test. Right click the test and select "Debug Test".
