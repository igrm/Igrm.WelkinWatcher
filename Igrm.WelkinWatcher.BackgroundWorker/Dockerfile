FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Igrm.WelkinWatcher.BackgroundWorker/Igrm.WelkinWatcher.BackgroundWorker.csproj", "Igrm.WelkinWatcher.BackgroundWorker/"]
RUN dotnet restore "Igrm.WelkinWatcher.BackgroundWorker/Igrm.WelkinWatcher.BackgroundWorker.csproj"
COPY . .
WORKDIR "/src/Igrm.WelkinWatcher.BackgroundWorker"
RUN dotnet build "Igrm.WelkinWatcher.BackgroundWorker.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "Igrm.WelkinWatcher.BackgroundWorker.csproj" -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Igrm.WelkinWatcher.BackgroundWorker.dll"]