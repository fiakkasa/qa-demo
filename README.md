# Commands

## Unit Tests

`dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./coverage.cobertura.xml'; dotnet reportgenerator -reports:./qa-demo.Tests/coverage.cobertura.xml -targetdir:./qa-demo.Tests/TestResults -reporttypes:Html`

## Load Tests

- Start the project: `dotnet run -c Release ./qa-demo/qa-demo.csproj`
- Load Tests: `dotnet run -c Release --project ./qa-demo.LoadTests/qa-demo.LoadTests.csproj -u http://localhost:5148 --reportdir ./qa-demo.LoadTests/TestResults`

## References

- https://html-agility-pack.net
- https://nbomber.com
- https://github.com/commandlineparser/commandline