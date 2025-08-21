# =========================
# Etapa 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o .csproj e restaura as dependências
COPY ["src/Venice.Orders.Api/Venice.Orders.Api.csproj", "Venice.Orders.Api/"]
RUN dotnet restore "Venice.Orders.Api/Venice.Orders.Api.csproj"

# Copia todo o código-fonte
COPY ./src/Venice.Orders.Api/ Venice.Orders.Api/
WORKDIR "/src/Venice.Orders.Api"

# Build do projeto em modo Release
RUN dotnet build "Venice.Orders.Api.csproj" -c Release -o /app/build

# =========================
# Etapa 2: Publish
# =========================
FROM build AS publish
RUN dotnet publish "Venice.Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Etapa 3: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expõe a porta padrão da API
EXPOSE 5000

# Entry point
ENTRYPOINT ["dotnet", "Venice.Orders.Api.dll"]
