# Stage 1: Build the Vue.js frontend
FROM node:18 AS frontend-build
WORKDIR /app
COPY ["package.json", "package-lock.json", "./"]
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["api/api.csproj", "api/"]
RUN dotnet restore "api/api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "api.csproj" -c Release -o /app/build

# Stage 3: Publish the .NET application
FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Stage 4: Final stage - Combine frontend and backend
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=frontend-build /app/api/wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "api.dll"]