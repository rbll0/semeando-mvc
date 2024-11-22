# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copiar arquivos do projeto
COPY . ./

# Restaurar dependências
RUN dotnet restore

# Build do projeto
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copiar os arquivos gerados na etapa de build
COPY --from=build-env /App/out .

# Comando de entrada - Substitua "Semeando.Mvc.dll" pelo nome correto
ENTRYPOINT ["dotnet", "semeando-mvc.dll"]
