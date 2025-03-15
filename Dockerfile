# Sử dụng Windows-based .NET Framework SDK 6.0
FROM mcr.microsoft.com/dotnet/framework/sdk:6.0 AS build
WORKDIR /src
COPY ["TestTemplate/TestTemplate.csproj", "TestTemplate/"]
RUN nuget restore "TestTemplate/TestTemplate.csproj"
COPY . .
RUN msbuild "TestTemplate/TestTemplate.csproj" /p:Configuration=Release /p:OutputPath=/app/build

# Stage 2: Publish
FROM build AS publish
RUN msbuild "TestTemplate/TestTemplate.csproj" /p:Configuration=Release /p:OutputPath=/app/publish

# Runtime Container (Windows-based)
FROM mcr.microsoft.com/dotnet/framework/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["TestTemplate.exe"]