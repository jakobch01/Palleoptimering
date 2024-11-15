# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file first to restore dependencies
COPY Palleoptimering/Palleoptimering.csproj Palleoptimering/
RUN dotnet restore "Palleoptimering/Palleoptimering.csproj"

# Copy the rest of the source code
COPY . . 

# Set the working directory to the application folder
WORKDIR "/src/Palleoptimering"

# Build the application
RUN dotnet build "Palleoptimering.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Palleoptimering.csproj" -c Release -o /app/publish

# Copy the published app to the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Palleoptimering.dll"]
