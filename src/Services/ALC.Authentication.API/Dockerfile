# Use the official .NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN mkdir -p /src/Services/ALC.Authentication.API
COPY ["/src/Services/ALC.Authentication.API/ALC.Authentication.API.csproj", "/src/Services/ALC.Authentication.API/"]
RUN dotnet restore "/src/Services/ALC.Authentication.API/ALC.Authentication.API.csproj"
WORKDIR "/src/Services/ALC.Authentication.API"
COPY /src/Services/ALC.Authentication.API .
RUN dotnet build "ALC.Authentication.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ALC.Authentication.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ALC.Authentication.API.dll"]
