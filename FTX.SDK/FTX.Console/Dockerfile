#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["FTX.Console/FTXConsole.csproj", "FTX.Console/"]
RUN dotnet restore "FTX.Console/FTXConsole.csproj"
COPY . .
WORKDIR "/src/FTX.Console"
RUN dotnet build "FTXConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FTXConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FTXConsole.dll"]