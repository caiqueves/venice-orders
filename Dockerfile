# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os projetos
COPY ["src/Venice.Orders.Api/Venice.Orders.Api.csproj", "Venice.Orders.Api/"]
COPY ["src/Venice.Orders.Application/Venice.Orders.Application.csproj", "Venice.Orders.Application/"]
COPY ["src/Venice.Orders.Domain/Venice.Orders.Domain.csproj", "Venice.Orders.Domain/"]
COPY ["src/Venice.Orders.Redis/Venice.Orders.Redis.csproj", "Venice.Orders.Redis/"]
COPY ["src/Venice.Orders.RabbitMQ/Venice.Orders.RabbitMQ.csproj", "Venice.Orders.RabbitMQ/"]
COPY ["src/Venice.Orders.MongoDB/Venice.Orders.MongoDB.csproj", "Venice.Orders.MongoDB/"]
COPY ["src/Venice.Orders.SqlServer/Venice.Orders.SqlServer.csproj", "Venice.Orders.SqlServer/"]
COPY ["src/Venice.Orders.CrossCutting.IoC/Venice.Orders.CrossCutting.IoC.csproj", "Venice.Orders.CrossCutting.IoC/"]
COPY ["src/Venice.Orders.CrossCutting.Shareable/Venice.Orders.CrossCutting.Shareable.csproj", "Venice.Orders.CrossCutting.Shareable/"]

# Restaura dependências
RUN dotnet restore Venice.Orders.Api/Venice.Orders.Api.csproj

# Copia todo o código
COPY src/. .

# Build
RUN dotnet build Venice.Orders.Api/Venice.Orders.Api.csproj -c Release -o /app/build

# Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Venice.Orders.Api/Venice.Orders.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Expõe porta 5000 dentro do container
EXPOSE 5000

# Runtime final (corrigido)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Venice.Orders.Api.dll"]
