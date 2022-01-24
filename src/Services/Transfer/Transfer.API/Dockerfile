#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Transfer/Transfer.API/Transfer.API.csproj", "src/Services/Transfer/Transfer.API/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/Services/Transfer/Transfer.API/Transfer.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Transfer/Transfer.API"
RUN dotnet build "Transfer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transfer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transfer.API.dll"]