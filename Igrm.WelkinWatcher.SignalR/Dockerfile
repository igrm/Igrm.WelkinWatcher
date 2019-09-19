FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Igrm.WelkinWatcher.Hub/Igrm.WelkinWatcher.Hub.csproj", "Igrm.WelkinWatcher.Hub/"]
RUN dotnet restore "Igrm.WelkinWatcher.Hub/Igrm.WelkinWatcher.Hub.csproj"
COPY . .
WORKDIR "/src/Igrm.WelkinWatcher.Hub"
RUN dotnet build "Igrm.WelkinWatcher.Hub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Igrm.WelkinWatcher.Hub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Igrm.WelkinWatcher.Hub.dll"]