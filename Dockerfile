# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./API/S3-Api-indi.csproj" --disable-parallel
RUN dotnet publish "./API/S3-Api-indi.csproj" -c release -o /app --no-restore

ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://+:80;https://+:5000
ENV ASPNETCORE_HTTPS_PORT 5000

WORKDIR /cert
RUN dotnet dev-certs https -ep certhttps.pfx -p Password123

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
COPY --from=build /cert ./app

WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
EXPOSE 80

ENTRYPOINT ["dotnet", "S3-Api-indi.dll"]