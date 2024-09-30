# Utiliza la imagen oficial de .NET 8 como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establece el directorio de trabajo en /app
WORKDIR /app

# restore
COPY src/*.csproj ./src/
RUN dotnet restore ./src/ExpenseTrackerGroup3.csproj

# release
COPY src/ ./src/
RUN dotnet publish ./src/ExpenseTrackerGroup3.csproj -c Release -o /out

# Usa la imagen de runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Establece el puerto 8080 como el puerto de exposición de la aplicación
EXPOSE 8080

# Establece el comando de inicio de la aplicación como "dotnet run"
CMD ["dotnet", "ExpenseTrackerGroup3.dll"]

