mkdir coverage

src\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe -register:user -filter:"+[UnitTestGenerator]* -[*.Tests]*" -excludebyattribute:*.ExcludeFromCodeCoverage*^ -mergebyhash -target:"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" -targetargs:"/testcontainer:src\UnitTestGenerator.Tests\bin\Debug\UnitTestGenerator.Tests.dll" -output:coverage\coverage.xml
@rem packages\OpenCover.4.6.166\tools\OpenCover.Console.exe -mergeoutput -register:user -excludebyattribute:*.ExcludeFromCodeCoverage*^ -target:"packages\NUnit.Runners.2.6.4\tools\nunit-console.exe" "-targetargs:\"ClassLibrary1Tests\bin\Debug\ClassLibrary1Tests.dll\" -noshadow" -filter:"-[*ClassLibrary1*]*" -output:coverage\coverage.xml

src\packages\ReportGenerator.2.3.2.0\tools\ReportGenerator.exe "-reports:coverage\coverage.xml" "-targetdir:coverage\report" "-historydir:coverage\history"

coverage\report\index.htm