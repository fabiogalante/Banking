FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Processing/Processing.API/Processing.API.csproj", "Processing.API/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["src/Services/Transfer/Transfer.Infra/Processing.Infra.csproj", "Transfer.Infra/"]
COPY ["src/Services/Transfer/Transfer.Domain/Processing.Domain.csproj", "Transfer.Domain/"]
COPY ["src/Services/Processing/Processing.Application/Processing.Application.csproj", "Processing.Application/"]
COPY ["src/BuildingBlocks/Account.Service/Account.Service.csproj", "Account.Service/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "Common.Logging/"]
RUN dotnet restore "src/Services/Processing/Processing.API/Processing.API.csproj"
COPY . .
WORKDIR "/src/Processing.API"
RUN dotnet build "Processing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Processing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Processing.API.dll"]
