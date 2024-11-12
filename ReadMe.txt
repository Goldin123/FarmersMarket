Database Migration

dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate --project FarmersMarket.Database --startup-project FarmersMarket

dotnet ef database update --project FarmersMarket.Database --startup-project FarmersMarket


GitHub Existing

git init
git add .
git commit -m "Add existing project files to Git"
git remote add origin https://github.com/cameronmcnz/example-website.git
git push -u -f origin master
