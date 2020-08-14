#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

LABEL maintainer="Darren <jesn2013@hotmail.com>"


FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Wechaty.Getting.Start/Wechaty.Getting.Start.csproj", "src/Wechaty.Getting.Start/"]
COPY ["src/Wechaty.Application.Contracts/Wechaty.Application.Contracts.csproj", "src/Wechaty.Application.Contracts/"]
COPY ["src/Wechaty.Domain.Shared/Wechaty.Domain.Shared.csproj", "src/Wechaty.Domain.Shared/"]
COPY ["src/Wechaty.Puppet.Model/Wechaty.Puppet.Model.csproj", "src/Wechaty.Puppet.Model/"]
COPY ["src/PlugIn/Wechaty.PlugIn.QRCodeTerminal/Wechaty.PlugIn.QRCodeTerminal.csproj", "src/PlugIn/Wechaty.PlugIn.QRCodeTerminal/"]
COPY ["src/Wechaty.Plugin.Base/Wechaty.Plugin.Base.csproj", "src/Wechaty.Plugin.Base/"]
COPY ["src/Wechaty.Application/Wechaty.Application.csproj", "src/Wechaty.Application/"]
COPY ["src/Wechaty.Puppet.Hostie/Wechaty.Puppet.Hostie.csproj", "src/Wechaty.Puppet.Hostie/"]
COPY ["src/Wechaty.Puppet.Contracts/Wechaty.Puppet.Contracts.csproj", "src/Wechaty.Puppet.Contracts/"]
COPY ["src/PlugIn/Wechaty.PlugIn.DingDong/Wechaty.PlugIn.DingDong.csproj", "src/PlugIn/Wechaty.PlugIn.DingDong/"]
RUN dotnet restore "src/Wechaty.Getting.Start/Wechaty.Getting.Start.csproj"
COPY . .
WORKDIR "/src/src/Wechaty.Getting.Start"
RUN dotnet build "Wechaty.Getting.Start.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wechaty.Getting.Start.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wechaty.Getting.Start.dll"]

LABEL \
  org.label-schema.license="Apache-2.0" \
  org.label-schema.build-date="$(date -u +'%Y-%m-%dT%H:%M:%SZ')" \
  org.label-schema.version="$DOCKER_TAG" \
  org.label-schema.docker.params="WECHATY_TOKEN=token token from https://www.chatie.io,  TZ='Asia/Shanghai' TimeZone"
