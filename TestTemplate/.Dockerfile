# Sử dụng hình ảnh .NET ASP.NET làm base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Dùng SDK để build dự án
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestTemplate/TestTemplate.csproj", "TestTemplate/"]
RUN dotnet restore "TestTemplate/TestTemplate.csproj"
COPY . .
WORKDIR "/src/TestTemplate"
RUN dotnet build "TestTemplate.csproj" -c Release -o /app/build

# Publish ứng dụng
FROM build AS publish
RUN dotnet publish "TestTemplate.csproj" -c Release -o /app/publish

# Tạo container cuối cùng
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .# Sử dụng hình ảnh ASP.NET làm base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443  # Nếu chạy HTTPS

# Dùng SDK để build dự án
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file project trước để cache dependencies (tăng tốc build)
COPY TestTemplate/TestTemplate.csproj TestTemplate/
RUN dotnet restore "TestTemplate/TestTemplate.csproj"

# Copy toàn bộ source code
COPY . .

# Build ứng dụng
WORKDIR "/src/TestTemplate"
RUN dotnet build "TestTemplate.csproj" -c Release -o /app/build

# Publish ứng dụng
FROM build AS publish
RUN dotnet publish "TestTemplate.csproj" -c Release -o /app/publish

# Tạo container cuối cùng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "TestTemplate.dll"]
