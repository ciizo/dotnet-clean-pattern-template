#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Application/Ciizo.Restful.Onion.Api/Ciizo.Restful.Onion.Api.csproj", "src/Application/Ciizo.Restful.Onion.Api/"]
RUN dotnet restore "./src/Application/Ciizo.Restful.Onion.Api/./Ciizo.Restful.Onion.Api.csproj"
COPY . .
WORKDIR "/src/src/Application/Ciizo.Restful.Onion.Api"
RUN dotnet build "./Ciizo.Restful.Onion.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ciizo.Restful.Onion.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ciizo.Restful.Onion.Api.dll"]