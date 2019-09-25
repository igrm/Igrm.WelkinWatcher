FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["Igrm.WelkinWatcher.InfoBoard/Igrm.WelkinWatcher.InfoBoard.csproj", "Igrm.WelkinWatcher.InfoBoard/"]
RUN dotnet restore "Igrm.WelkinWatcher.InfoBoard/Igrm.WelkinWatcher.InfoBoard.csproj"
COPY . .
WORKDIR "/src/Igrm.WelkinWatcher.InfoBoard"
RUN dotnet build "Igrm.WelkinWatcher.InfoBoard.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "Igrm.WelkinWatcher.InfoBoard.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Igrm.WelkinWatcher.InfoBoard.dll"]