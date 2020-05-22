#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["IngaiaInterview.WebApi/IngaiaInterview.WebApi.csproj", "IngaiaInterview.WebApi/"]
COPY ["IngaiaInterview.Application/IngaiaInterview.Application.csproj", "IngaiaInterview.Application/"]
RUN dotnet restore "IngaiaInterview.WebApi/IngaiaInterview.WebApi.csproj"
COPY . .
WORKDIR "/src/IngaiaInterview.WebApi"
RUN dotnet build "IngaiaInterview.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IngaiaInterview.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IngaiaInterview.WebApi.dll"]