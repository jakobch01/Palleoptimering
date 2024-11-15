# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official SDK image for build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Palleoptimering/Palleoptimering.csproj", "Palleoptimering/"]
RUN dotnet restore "Palleoptimering/Palleoptimering.csproj"
COPY . .
WORKDIR "/src/Palleoptimering"
RUN dotnet build "Palleoptimering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Palleoptimering.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Palleoptimering.dll"]
