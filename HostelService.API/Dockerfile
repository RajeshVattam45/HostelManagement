FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY HostelService.API/HostelService.API.csproj HostelService.API/
COPY HostelService.Application/HostelService.Application.csproj HostelService.Application/
COPY HostelService.Domain/HostelService.Domain.csproj HostelService.Domain/
COPY HostelService.Infrastructure/HostelService.Infrastructure.csproj HostelService.Infrastructure/

RUN dotnet restore "HostelService.API/HostelService.API.csproj"

COPY . .
WORKDIR "/src/HostelService.API"
RUN dotnet build "HostelService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HostelService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HostelService.API.dll"]
