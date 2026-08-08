# -------- BUILD --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔥 copiar TODO
COPY . .

# 🔥 restaurar solución completa
RUN dotnet restore "AtlasCommerce.Api/AtlasCommerce.Api.csproj"

# 🔥 publicar API
WORKDIR "/src/AtlasCommerce.Api"
RUN dotnet publish -c Release -o /app/publish

# -------- RUNTIME --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AtlasCommerce.Api.dll"]