FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5010
ENV ASPNETCORE_ENVIRONMENT=Development
# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Eshop.Api/Eshop.Api.csproj", "Eshop.Api/"]
COPY ["Eshop.Application/Eshop.Application.csproj", "Eshop.Application/"]
COPY ["Eshop.Domain/Eshop.Domain.csproj", "Eshop.Domain/"]
COPY ["Eshop.Infrastructure/Eshop.Infrastructure.csproj", "Eshop.Infrastructure/"]
RUN dotnet restore "Eshop.Api/Eshop.Api.csproj"
COPY . .
WORKDIR "/src/Eshop.Api"
RUN dotnet build "Eshop.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Eshop.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Api.dll"]
