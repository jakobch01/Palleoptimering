FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#Install required libraries for Kerberos
RUN apt-get update && apt-get install -y --no-install-recommends \
    libkrb5-3 \
    libgssapi-krb5-2 \
    krb5-user \
    && rm -rf /var/lib/apt/lists/*

#Set up the application user and group
ARG APP_UID=1000
ARG APP_GID=1000
RUN groupadd -g ${APP_GID} appgroup && \
    useradd -u ${APP_UID} -g appgroup -m appuser

#Create Data Protection Keys directory with correct permissions
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && \
    chown -R ${APP_UID}:${APP_GID} /home/app/.aspnet/DataProtection-Keys

USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Palleoptimering.csproj", "./"]
RUN dotnet restore "Palleoptimering.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Palleoptimering.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Palleoptimering.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Palleoptimering.dll"]