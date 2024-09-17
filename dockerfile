# Utiliza la imagen oficial de .NET 8 como base
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Establece el directorio de trabajo en /app
WORKDIR /app

# Copia el código fuente de la aplicación en el directorio de trabajo
COPY *.csproj ./

COPY . .
# Restaura las dependencias del proyecto
RUN dotnet build -c Release

# Establece el puerto 8080 como el puerto de exposición de la aplicación
EXPOSE 8080

# Establece el comando de inicio de la aplicación como "dotnet run"
CMD ["dotnet", "run",]

