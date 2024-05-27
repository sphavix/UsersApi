FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AcountsApi.csproj", "AcountsApi/"]
RUN dotnet restore "AcountsApi.csproj"
COPY . .
WORKDIR "/src/AcountsApi"
RUN dotnet build "AcountsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcountsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcountsApi.dll"]