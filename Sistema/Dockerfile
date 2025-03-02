# ----------------------------------
# Stage 1: Build
# ----------------------------------
  FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
  WORKDIR /src

  COPY ["Sistema.sln", "./"]
  COPY ["Sistema.API/Sistema.API.csproj", "Sistema.API/"]
  COPY ["Sistema.Communication/Sistema.Communication.csproj", "Sistema.Communication/"]
  COPY ["Sistema.Exception/Sistema.Exception.csproj", "Sistema.Exception/"]

  RUN dotnet restore "Sistema.API/Sistema.API.csproj"

  COPY . .

  WORKDIR "/src/Sistema.API"
  RUN dotnet build "Sistema.API.csproj" -c Release -o /app/build

  # ----------------------------------
  # Stage 2: Publish
  # ----------------------------------
  FROM build AS publish
  RUN dotnet publish "Sistema.API.csproj" -c Release -o /app/publish

  # ----------------------------------
  # Stage 3: Runtime
  # ----------------------------------
  FROM mcr.microsoft.com/dotnet/aspnet:9.0
  ENV ASPNETCORE_URLS=http://0.0.0.0:5130

  ENV ASPNETCORE_ENVIRONMENT=Production
  # ENV ASPNETCORE_ENVIRONMENT=DevelopmentProduction

  EXPOSE 5130
  WORKDIR /app

  # Copiar os arquivos publicados para a imagem final
  COPY --from=publish /app/publish .

  ENTRYPOINT ["dotnet", "Sistema.API.dll"]
