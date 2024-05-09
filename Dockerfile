FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo do projeto e restaurar as dependências
COPY *.csproj .
RUN dotnet restore

# Copiar todo o código e compilar o projeto
COPY . .
RUN dotnet publish -c Release -o out

# Usar a imagem base do ASP.NET Core para execução
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expor a porta utilizada pela sua aplicação
EXPOSE 5057

# Definir o comando de inicialização da aplicação
ENTRYPOINT ["dotnet", "SyncAPI.dll"]
