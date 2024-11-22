# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copiar arquivos do projeto
COPY . ./
# Restaurar dependências
RUN dotnet restore
# Publicar o aplicativo
RUN dotnet publish -c Release -o out

# Etapa de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Diretório para armazenar chaves de proteção de dados
VOLUME /App/keys

# Copiar artefatos publicados
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "semeando-mvc.dll"]
