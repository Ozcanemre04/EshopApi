FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5010

ENV ASPNETCORE_URLS=http://+:5010
ENV ASPNETCORE_ENVIRONMENT=Development


FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Eshop.Api/Eshop.Api.csproj", "src/Eshop.Api/"]
COPY ["src/Eshop.Application/Eshop.Application.csproj", "src/Eshop.Application/"]
COPY ["src/Eshop.Domain/Eshop.Domain.csproj", "src/Eshop.Domain/"]
COPY ["src/Eshop.Infrastructure/Eshop.Infrastructure.csproj", "src/Eshop.Infrastructure/"]
RUN dotnet restore "src/Eshop.Api/Eshop.Api.csproj"
COPY . .
WORKDIR "/src/src/Eshop.Api"
RUN dotnet build "Eshop.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Eshop.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Api.dll"]
