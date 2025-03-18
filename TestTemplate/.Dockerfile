# Sử dụng Windows Container vì đây là ASP.NET Framework (không phải .NET Core)
# Image chứa IIS + .NET Framework Runtime
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS base
WORKDIR /inetpub/wwwroot

# Image chứa đầy đủ SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /src

# Copy file project để tối ưu cache dependencies
COPY TestTemplate.csproj ./
RUN nuget restore TestTemplate.csproj

# Copy toàn bộ source code vào container
COPY . ./TestTemplate
WORKDIR /src/TestTemplate

# Build ứng dụng bằng MSBuild
RUN msbuild TestTemplate.csproj /p:Configuration=Release /p:OutputPath=c:\out

# Final runtime container dựa trên IIS + .NET Framework
FROM base AS final
WORKDIR /inetpub/wwwroot

# Copy kết quả build từ stage build sang container chạy chính
COPY --from=build C:/out .

# Mở cổng HTTP
EXPOSE 80

# Chạy IIS
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]