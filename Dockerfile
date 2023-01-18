# env variables
#ENV <NAME> <DEFAULTVALUE>

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["S3-Api-indi.csproj", "."]
RUN dotnet restore "./S3-Api-indi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "S3-Api-indi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "S3-Api-indi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "S3-Api-indi.dll"]