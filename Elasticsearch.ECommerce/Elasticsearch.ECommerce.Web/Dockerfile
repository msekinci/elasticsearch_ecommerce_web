FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Elasticsearch.ECommerce.Web/Elasticsearch.ECommerce.Web.csproj", "Elasticsearch.ECommerce.Web/"]
RUN dotnet restore "Elasticsearch.ECommerce.Web/Elasticsearch.ECommerce.Web.csproj"
COPY . .
WORKDIR "/src/Elasticsearch.ECommerce.Web"
RUN dotnet build "Elasticsearch.ECommerce.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elasticsearch.ECommerce.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elasticsearch.ECommerce.Web.dll"]
