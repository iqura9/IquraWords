#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 443

ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.csproj ./

RUN dotnet restore "Server.csproj"
COPY . /src
RUN dotnet build "Server.csproj" -c Release -o /app/build

COPY Directory.Build.props .

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM build as final
WORKDIR /src/Server
ENTRYPOINT [ "dotnet", "watch", "run" ]


#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Server.dll"]