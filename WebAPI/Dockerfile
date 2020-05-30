#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebAPI/homework1.csproj", "WebAPI/"]
RUN dotnet restore "WebAPI/homework1.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "homework1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "homework1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "homework1.dll"]