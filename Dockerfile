# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore

# Publish app to /out directory
RUN dotnet publish -c Release -o out

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/out ./

# Run the app
ENTRYPOINT ["dotnet", "PokeTCG.dll"]
