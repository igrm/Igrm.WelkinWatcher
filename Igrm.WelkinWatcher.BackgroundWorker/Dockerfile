FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Igrm.WelkinWatcher.BackgroundWorker/Igrm.WelkinWatcher.BackgroundWorker.csproj", "Igrm.WelkinWatcher.BackgroundWorker/"]
RUN dotnet restore "Igrm.WelkinWatcher.BackgroundWorker/Igrm.WelkinWatcher.BackgroundWorker.csproj"
COPY . .
WORKDIR "/src/Igrm.WelkinWatcher.BackgroundWorker"
RUN dotnet build "Igrm.WelkinWatcher.BackgroundWorker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Igrm.WelkinWatcher.BackgroundWorker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Igrm.WelkinWatcher.BackgroundWorker.dll"]