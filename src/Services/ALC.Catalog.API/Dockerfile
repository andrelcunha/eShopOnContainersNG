FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN mkdir -p /src/Services/ALC.Catalog.API
COPY ["/src/Services/ALC.Catalog.API/ALC.Catalog.API.csproj", "/src/Services/ALC.Catalog.API/"]

RUN mkdir -p /src/BuildingBlocks/Core/ALC.Core
COPY ["/src/BuildingBlocks/Core/ALC.Core/ALC.Core.csproj","/src/BuildingBlocks/Core/ALC.Core/"]
COPY /src/BuildingBlocks/Core/ALC.Core/ /src/BuildingBlocks/Core/ALC.Core/

WORKDIR /src
RUN dotnet restore "/src/Services/ALC.Catalog.API/ALC.Catalog.API.csproj"
WORKDIR "/src/Services/ALC.Catalog.API"
COPY /src/Services/ALC.Catalog.API .
RUN dotnet build "ALC.Catalog.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ALC.Catalog.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ALC.Catalog.API.dll"]
