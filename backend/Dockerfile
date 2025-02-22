# Stage 1: Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7081

# Stage 2: Build and publish the .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "CustomersAPI.csproj"
RUN dotnet publish "CustomersAPI.csproj" -c Release -o /app/publish

# Stage 3: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "CustomersAPI.dll"]
