FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 44393

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Core/API/LibraryAPI/MusicPlayer.API.Library/MusicPlayer.API.Library.csproj", "src/Core/API/LibraryAPI/MusicPlayer.API.Library/"]
COPY ["src/Shared/MusicPlayer.Base/MusicPlayer.Base.csproj", "src/Shared/MusicPlayer.Base/"]
COPY ["src/Core/Base/MusicPlayer.API.Base/MusicPlayer.API.Base.csproj", "src/Core/Base/MusicPlayer.API.Base/"]
RUN dotnet restore "src/Core/API/LibraryAPI/MusicPlayer.API.Library/MusicPlayer.API.Library.csproj"
COPY . .
WORKDIR "/src/src/Core/API/LibraryAPI/MusicPlayer.API.Library"
RUN dotnet build "MusicPlayer.API.Library.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicPlayer.API.Library.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicPlayer.API.Library.dll"]