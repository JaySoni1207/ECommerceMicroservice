FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first for layer caching
COPY ["OrderService/OrderService.csproj", "OrderService/"]
COPY ["Ecommerce.Common/Ecommerce.Common.csproj", "Ecommerce.Common/"]

# Restore dependencies
RUN dotnet restore "OrderService/OrderService.csproj"

# Copy the rest of the source
COPY . .

# Build & publish
WORKDIR "/src/OrderService"
RUN dotnet publish -c Release -f net10.0 --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]