#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8088

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Kafka/Kafka.csproj", "Kafka/"]
RUN dotnet restore "Kafka/Kafka.csproj"
COPY . .
WORKDIR "/src/Kafka"
RUN dotnet build "Kafka.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka.dll"]