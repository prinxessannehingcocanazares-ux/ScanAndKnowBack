# Use .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first for layer caching
COPY ["ScanToKnow2/ScanToKnow2.csproj", "ScanToKnowAPI/"]
COPY ["ScanToKnowBusiness/ScanToKnowBusiness.csproj", "ScanToKnowBusiness/"]
COPY ["ScanToKnowDataAccess/ScanToKnowDataAccess.csproj", "ScanToKnowDataAccess/"]

# Restore packages
RUN dotnet restore "ScanToKnow2/ScanToKnow2.csproj"

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish "ScanToKnow2/ScanToKnow2.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ScanToKnow2.dll"]