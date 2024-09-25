FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["WeatherThirdParty.csproj", "./"]
RUN dotnet restore "./WeatherThirdParty.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src/."
RUN dotnet build "WeatherThirdParty.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "WeatherThirdParty.csproj" -c Release -o /app/publish

# Final stage, where we set up the runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherThirdParty.dll"]
