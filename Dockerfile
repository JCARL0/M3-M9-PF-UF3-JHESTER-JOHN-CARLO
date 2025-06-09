FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
WORKDIR /app
COPY ./publish ./
ENTRYPOINT ["dotnet", "YourApp.dll"]
