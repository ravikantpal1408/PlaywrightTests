✅ Option 1: Run by File Path
From your project root (PlaywrightTests):

bash
Copy
Edit
dotnet test --filter "FullyQualifiedName~Dashboard" --no-build
This assumes your Dashboard.feature step definitions are in DashboardSteps.cs and the namespace is consistent.

✅ Option 2: Run using SpecFlow CLI (if installed)
If you have specflow CLI installed globally (or locally), you can do:

bash
Copy
Edit
specflow run Dashboard.feature
✅ Option 3: Run using VSTestCaseFilter (if you know scenario name)
bash
Copy
Edit
dotnet test --filter "TestCategory=Dashboard" --no-build
You'll need to tag your scenarios or feature like this:

gherkin
Copy
Edit
@Dashboard
Feature: OrangeHRM Dashboard
