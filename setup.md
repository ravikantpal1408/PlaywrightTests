✅ 3. Restore NuGet packages
In the project root:

bash
Copy
Edit
dotnet restore
✅ 4. Install Playwright tools
bash
Copy
Edit

# Installs Playwright and browser binaries

npx playwright install
If npx doesn't work, install Playwright CLI:

bash
Copy
Edit
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
✅ 5. Build the project
bash
Copy
Edit
dotnet build

then install run below command
npx playwiright install

playwright install

then

dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI

for linux based system
export PATH="$PATH:$HOME/.dotnet/tools"

livingdoc --help

for linux
