# Usar una imagen base oficial de .NET para el SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos de proyecto y restaurar dependencias
COPY *.sln .
COPY PermissionsWeb.Api/*.csproj ./PermissionsWeb.Api/
COPY PermissionsWeb.Application/*.csproj ./PermissionsWeb.Application/
COPY PermissionsWeb.Data/*.csproj ./PermissionsWeb.Data/
COPY PermissionsWeb.Domain/*.csproj ./PermissionsWeb.Domain/
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . .
WORKDIR /app/PermissionsWeb.Api
RUN dotnet publish -c Release -o out

# Usar una imagen base oficial de .NET para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/PermissionsWeb.Api/out .

# Exponer el puerto que tu API usa
EXPOSE 80
EXPOSE 443

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "PermissionsWeb.Api.dll"]
