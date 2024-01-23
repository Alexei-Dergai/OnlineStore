#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineStore.BasketService/OnlineStore.BasketService.API/OnlineStore.BasketService.API.csproj", "src/OnlineStore.BasketService/OnlineStore.BasketService.API/"]
COPY ["src/OnlineStore.BasketService/OnlineStore.BasketService.API/OnlineStore.BasketService.Application.csproj", "src/OnlineStore.BasketService/OnlineStore.BasketService.Application/"]
COPY ["src/OnlineStore.BasketService/OnlineStore.BasketService.API/OnlineStore.BasketService.Domain.csproj", "src/OnlineStore.BasketService/OnlineStore.BasketService.Domain/"]
COPY ["src/OnlineStore.BasketService/OnlineStore.BasketService.API/OnlineStore.BasketService.Infrastructure.csproj", "src/OnlineStore.BasketService/OnlineStore.BasketService.Infrastructure/"]
RUN dotnet restore "src/OnlineStore.BasketService/OnlineStore.BasketService.API/OnlineStore.BasketService.API.csproj"
COPY . .
WORKDIR "/src/src/OnlineStore.BasketService/OnlineStore.BasketService.API"
RUN dotnet build "OnlineStore.BasketService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStore.BasketService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.BasketService.API.dll"]