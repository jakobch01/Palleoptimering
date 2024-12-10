# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Palleoptimering/Palleoptimering.csproj", "Palleoptimering/"]
RUN dotnet restore "Palleoptimering/Palleoptimering.csproj"
COPY . .
WORKDIR "/src/Palleoptimering"
RUN dotnet build "Palleoptimering.csproj" -c Release -o /app/build

# Publish the app to the output directory
FROM build AS publish
WORKDIR "/src/Palleoptimering"
RUN dotnet publish "Palleoptimering.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Palleoptimering.dll"]
