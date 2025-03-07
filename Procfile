release: NODE_ENV=production npm install && npm run build && cd api && dotnet restore
web: dotnet api/bin/Release/net8.0/api.dll --urls http://+:$PORT