# Usa a imagem oficial do .NET SDK para compilar e publicar a API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY SistemaAPI/*.csproj ./SistemaAPI/
WORKDIR /app/SistemaAPI
RUN dotnet restore

# Copia o código-fonte e publica a aplicação
COPY . .
RUN dotnet publish -c Release -o /publish

# Usa uma imagem mais leve para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

# Expõe a porta onde a API rodará
EXPOSE 5198
CMD ["dotnet", "SistemaAPI.dll"]
