FROM mcr.microsoft.com/dotnet/aspnet:6.0.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0.400 AS build
WORKDIR /src

COPY ["global.json", "/"]
COPY ["src/Directory.Build.props", "src/"]
COPY ["src/Directory.Packages.props", "src/"]
COPY ["src/Server/src/", "src/Server/src/"]

RUN dotnet restore "src/Server/src/Worker/Worker.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "src/Server/src/Worker/Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Server/src/Worker/Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TagIt.Server.Worker.dll"]