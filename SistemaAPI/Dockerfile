# Estágio Base (para o ambiente de produção)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5198

# Estágio de Build (para compilar e gerar o código)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SistemaAPI/SistemaAPI.csproj", "SistemaAPI/"]
RUN dotnet restore "./SistemaAPI/SistemaAPI.csproj"
COPY . .
WORKDIR "/src/SistemaAPI"
RUN dotnet build "./SistemaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de Publicação (para gerar o código pronto para produção)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SistemaAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio Final (para o ambiente de execução)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SistemaAPI.dll"]
