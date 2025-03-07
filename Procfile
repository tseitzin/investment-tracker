release: NODE_ENV=production npm install && npm run build && cd api && dotnet restore
web: cd api && dotnet publish -c Release -o out && cd out && dotnet api.dll --urls http://+:$PORT