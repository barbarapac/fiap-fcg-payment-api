# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./src/Fiap.FCG.Payment.WebApi/Fiap.FCG.Payment.WebApi.csproj
RUN dotnet publish ./src/Fiap.FCG.Payment.WebApi/Fiap.FCG.Payment.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Fiap.FCG.Payment.WebApi.dll"]
