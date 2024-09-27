FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY *.csproj ./WeatherThirdParty/
RUN dotnet restore ./WeatherThirdParty/WeatherThirdParty.csproj

# Copy the rest of the application code
COPY . ./WeatherThirdParty/

# Build the application in Release mode
RUN dotnet publish ./WeatherThirdParty/WeatherThirdParty.csproj -c Release -o /app/publish

# Stage 2: Use a lightweight runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/publish .

# Expose port 80 for the web app
EXPOSE 3000
ENV HTTP_PORTS=3000
ENV HTTPS_PORTS=8443

# Run the app
ENTRYPOINT ["dotnet", "WeatherThirdParty.dll"]
