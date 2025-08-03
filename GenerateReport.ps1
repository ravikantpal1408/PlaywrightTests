dotnet clean

Start-Sleep -Seconds 5

dotnet build --no-restore --configuration Debug

Start-Sleep -Seconds 5



dotnet test --no-build --logger:"console;verbosity=detailed"
# dotnet test --filter "TestCategory=Dashboard" --no-build --logger:"console;verbosity=detailed"

# Wait for test results to be generated
Start-Sleep -Seconds 2

# Generate LivingDoc HTML report
livingdoc test-assembly bin/Debug/net9.0/PlaywrightTests.dll `
  -t bin/Debug/net9.0/TestExecution.json `
  -o TestReport.html

# Open the report in your default browser
Start-Process TestReport.html
