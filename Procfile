release: NODE_ENV=production npm install && npm run build
web: cd api && dotnet publish -c Release && cd bin/Release/net8.0 && dotnet api.dll --urls http://+:$PORT