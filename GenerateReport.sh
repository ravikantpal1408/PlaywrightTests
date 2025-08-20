#!/bin/bash

dotnet tool install --global Microsoft.Playwright.CLI

sleep 30

dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI

sleep 30

playwright install

sleep 30

dotnet add package Microsoft.Playwright

sleep 30

dotnet clean

sleep 5

dotnet build --no-restore --configuration Debug

sleep 5

dotnet test --no-build --logger:"console;verbosity=detailed"
# dotnet test --filter "TestCategory=Dashboard" --no-build --logger:"console;verbosity=detailed"

# Wait for test results to be generated
sleep 2

# Generate LivingDoc HTML report
livingdoc test-assembly bin/Debug/net9.0/PlaywrightTests.dll \
  -t bin/Debug/net9.0/TestExecution.json \
  -o TestReport.html

# Open the report in your default browser
open TestReport.html