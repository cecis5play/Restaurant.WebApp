# Използваме официалния .NET SDK за билдване
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish Restaurant.WebApp.csproj -c Release -o /out

# Използваме по-лек runtime образ за стартиране
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENTRYPOINT ["dotnet", "Restaurant.WebApp.dll"]