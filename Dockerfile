# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project files
COPY DoughManager.Api/DoughManager.Api.csproj DoughManager.Api/
COPY DoughManager.Data/DoughManager.Data.csproj DoughManager.Data/
COPY DoughManager.Services/DoughManager.Services.csproj DoughManager.Services/

# Restore dependencies
RUN dotnet restore DoughManager.Api/DoughManager.Api.csproj

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet publish DoughManager.Api/DoughManager.Api.csproj -c Release -o out

# Use the official .NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the build output
COPY --from=build /app/out .

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "DoughManager.Api.dll"]