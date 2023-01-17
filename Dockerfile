# env variables
#ENV <NAME> <DEFAULTVALUE>

# https
#WORKDIR /cert
#RUN dotnet dev-certs https -ep certhttps.pfx -p Password123

# Use the official Microsoft .NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

# Set the working directory
WORKDIR /app

# Copy the application and its dependencies into the container
COPY . .

# Restore the application's dependencies
RUN dotnet restore --source https://api.nuget.org/v3/index.json --source https://myget.org/F/myfeed/api/v3/index.json

# Build the application
RUN dotnet build --configuration Release

# Publish the application
RUN dotnet publish --configuration Release --output /app/out

EXPOSE 5000
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "S3-Api-Indi.dll"]

