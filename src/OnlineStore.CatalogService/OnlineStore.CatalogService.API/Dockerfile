#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineStore.CatalogService/OnlineStore.CatalogService.API/OnlineStore.CatalogService.API.csproj", "src/OnlineStore.CatalogService/OnlineStore.CatalogService.API/"]
COPY ["src/OnlineStore.CatalogService/OnlineStore.CatalogService.Application/OnlineStore.CatalogService.Application.csproj", "src/OnlineStore.CatalogService/OnlineStore.CatalogService.Application/"]
COPY ["src/OnlineStore.CatalogService/OnlineStore.CatalogService.Domian/OnlineStore.CatalogService.Domian.csproj", "src/OnlineStore.CatalogService/OnlineStore.CatalogService.Domain/"]
COPY ["src/OnlineStore.CatalogService/OnlineStore.CatalogService.Infrastructure/OnlineStore.CatalogService.Infrastructure.csproj", "src/OnlineStore.CatalogService/OnlineStore.CatalogService.Infrastructure/"]
RUN dotnet restore "src/OnlineStore.CatalogService/OnlineStore.CatalogService.API/OnlineStore.CatalogService.API.csproj"
COPY . .
WORKDIR "/src/src/OnlineStore.CatalogService/OnlineStore.CatalogService.API"
RUN dotnet build "OnlineStore.CatalogService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStore.CatalogService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.CatalogService.API.dll"]