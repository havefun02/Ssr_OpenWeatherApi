FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY WeatherThirdParty/*.csproj ./WeatherThirdParty/
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application in Release mode
RUN dotnet build --configuration Release --output /app/build

# Publish the app to a folder for deployment
RUN dotnet publish --configuration Release --output /app/publish

# Stage 2: Use a lightweight runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish .

# Expose port 80 for the web app
EXPOSE 3000

# Run the app
ENTRYPOINT ["dotnet", "WeatherThirdParty.dll"]
