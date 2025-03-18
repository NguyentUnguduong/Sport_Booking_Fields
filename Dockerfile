# Base image để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Image chứa SDK để build code
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file .csproj trước để cache dependencies
COPY TestTemplate/TestTemplate.csproj TestTemplate/
RUN dotnet restore "TestTemplate/TestTemplate.csproj"

# Copy toàn bộ source code vào container
COPY . .
WORKDIR "/src/TestTemplate"

# Build & Publish ứng dụng
RUN dotnet publish "TestTemplate.csproj" -c Release -o /app/publish --no-restore

# Final runtime container
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "TestTemplate.dll"]