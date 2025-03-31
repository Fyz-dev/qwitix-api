# See the article at https://aka.ms/customizecontainer to learn how to customize a debug container and how Visual Studio uses this Dockerfile to create images for faster debugging.

# This stage is used when running from VS in fast mode (default for debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

ARG ASPNETCORE_ENVIRONMENT
ARG DatabaseSettings__ConnectionString
ARG JwtSettings__Issuer
ARG JwtSettings__Audience
ARG JwtSettings__Secret
ARG Authentication__Google__ClientId
ARG Authentication__Google__ClientSecret

ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV DatabaseSettings__ConnectionString=$DatabaseSettings__ConnectionString
ENV JwtSettings__Issuer=$JwtSettings__Issuer
ENV JwtSettings__Audience=$JwtSettings__Audience
ENV JwtSettings__Secret=$JwtSettings__Secret
ENV Authentication__Google__ClientId=$Authentication__Google__ClientId
ENV Authentication__Google__ClientSecret=$Authentication__Google__ClientSecret


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["qwitix-api/qwitix-api.csproj", "qwitix-api/"]
RUN dotnet restore "./qwitix-api/qwitix-api.csproj"
COPY . .
WORKDIR "/src/qwitix-api"
RUN dotnet build "./qwitix-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project that will be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./qwitix-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in normal mode (default when debug configuration is not used)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "qwitix-api.dll"]