FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["testes-microservico-docker/testes-microservico-docker.csproj", "testes-microservico-docker/"]
RUN dotnet restore "testes-microservico-docker/testes-microservico-docker.csproj"
COPY . .
WORKDIR "/src/testes-microservico-docker"
RUN dotnet build "testes-microservico-docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "testes-microservico-docker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "testes-microservico-docker.dll"]
