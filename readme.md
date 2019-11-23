# Test Automation
- NetCore3
- NUnit
- Allure.NUnit
- Nlog

## TestData dir
- .runsettings file for dotnet test. Specify test search engine and
- .csv - test data files (search query;result index;expected result text)

## Run tests
- bing.bat - bing tests
- ddg.bat - duck duck go tests
- google.bat - google tests

## Tested on
```
dotnet --version 
3.0.100

allure --version
2.13.0
```

## Console examples
`dotnet test -s=.runsettings` - run tests with runnsettings file 

`allure serve %allure_dir%` - run allure report