FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UI/Web/MusicPlayer.WebUI.Authorization/MusicPlayer.WebUI.Authorization.csproj", "MusicPlayer.WebUI.Authorization/"]
RUN dotnet restore "src/UI/Web/MusicPlayer.WebUI.Authorization/MusicPlayer.WebUI.Authorization.csproj"
COPY . .
WORKDIR "/src/MusicPlayer.WebUI.Authorization"
RUN dotnet build "MusicPlayer.WebUI.Authorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicPlayer.WebUI.Authorization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicPlayer.WebUI.Authorization.dll"]
