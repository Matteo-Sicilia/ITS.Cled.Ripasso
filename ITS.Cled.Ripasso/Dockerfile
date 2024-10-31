FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ITS.Cled.Ripasso/ITS.Cled.Ripasso.csproj", "ITS.Cled.Ripasso/"]
RUN dotnet restore "ITS.Cled.Ripasso/ITS.Cled.Ripasso.csproj"
COPY . .
WORKDIR "/src/ITS.Cled.Ripasso"
RUN dotnet build "ITS.Cled.Ripasso.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ITS.Cled.Ripasso.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITS.Cled.Ripasso.dll"]
