# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file first (note the nested CustomersAPI folders)
COPY CustomersAPI/CustomersAPI/CustomersAPI.csproj CustomersAPI/CustomersAPI/
RUN dotnet restore "CustomersAPI/CustomersAPI/CustomersAPI.csproj"

# Now copy the rest of the source code
COPY . ./
RUN dotnet publish "CustomersAPI/CustomersAPI/CustomersAPI.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7081

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "CustomersAPI.dll"]

