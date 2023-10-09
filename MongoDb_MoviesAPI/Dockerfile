#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["MongoDb_MoviesAPI.csproj", "./"]
RUN dotnet restore "MongoDb_MoviesAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MongoDb_MoviesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MongoDb_MoviesAPI.csproj" -c Release -o /app/publish \
	-r alpine-x64 \
	--self-contained true \
	-p:PublishTrimmed=true \
	-p:PublishSingleFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./MongoDb_MoviesAPI"] 