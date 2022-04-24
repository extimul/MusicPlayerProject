FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Core/Identity/MusicPlayer.API.Identity/MusicPlayer.API.Identity.csproj", "MusicPlayer.API.Identity/"]
RUN dotnet restore "src/Core/Identity/MusicPlayer.API.Identity/MusicPlayer.API.Identity.csproj"
COPY . .
WORKDIR "/src/MusicPlayer.API.Identity"
RUN dotnet build "MusicPlayer.API.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicPlayer.API.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicPlayer.API.Identity.dll"]
