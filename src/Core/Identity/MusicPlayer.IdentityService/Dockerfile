FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 44392

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Core/Identity/MusicPlayer.IdentityService/MusicPlayer.IdentityService.csproj", "src/Core/Identity/MusicPlayer.IdentityService/"]
COPY ["src/Shared/MusicPlayer.Base/MusicPlayer.Base.csproj", "src/Shared/MusicPlayer.Base/"]
COPY ["src/Core/Identity/MusicPlayer.IdentityService.Persistence/MusicPlayer.IdentityService.Persistence.csproj", "src/Core/Identity/MusicPlayer.IdentityService.Persistence/"]
COPY ["src/Core/Identity/MusicPlayer.IdentityService.Application/MusicPlayer.IdentityService.Application.csproj", "src/Core/Identity/MusicPlayer.IdentityService.Application/"]
COPY ["src/Core/Identity/MusicPlayer.IdentityService.Domain/MusicPlayer.IdentityService.Domain.csproj", "src/Core/Identity/MusicPlayer.IdentityService.Domain/"]
COPY ["src/Core/Base/MusicPlayer.API.Base/MusicPlayer.API.Base.csproj", "src/Core/Base/MusicPlayer.API.Base/"]
RUN dotnet restore "src/Core/Identity/MusicPlayer.IdentityService/MusicPlayer.IdentityService.csproj"
COPY . .
WORKDIR "/src/src/Core/Identity/MusicPlayer.IdentityService"
RUN dotnet build "MusicPlayer.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicPlayer.IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicPlayer.IdentityService.dll"]