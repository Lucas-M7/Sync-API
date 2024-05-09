# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia e restaura o projeto
COPY *.csproj ./
RUN dotnet restore

# Copia todo o resto e constrói
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Porta HTTP
EXPOSE 5057

# Porta HTTPS
EXPOSE 7043

ENTRYPOINT ["dotnet", "SyncAPI.dll"]
