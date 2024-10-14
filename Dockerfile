#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CoreDomain/BloodDonation.Services.Authorization.API/BloodDonation.Services.Donations.API.csproj", "CoreDomain/BloodDonation.Services.Authorization.API/"]
COPY ["Shared/Shared.Core/Shared.Domain.csproj", "Shared/Shared.Core/"]
COPY ["Shared/Shared.Infra/Shared.Infra.csproj", "Shared/Shared.Infra/"]
COPY ["CoreDomain/BloodDonation.Services.Authorization.Application/BloodDonation.Services.Donations.Application.csproj", "CoreDomain/BloodDonation.Services.Authorization.Application/"]
COPY ["CoreDomain/BloodDonation.Services.Authorization.Core/BloodDonation.Services.Donations.Domain.csproj", "CoreDomain/BloodDonation.Services.Authorization.Core/"]
COPY ["CoreDomain/BloodDonation.Services.Authorization.Infra/BloodDonation.Services.Donations.Infra.csproj", "CoreDomain/BloodDonation.Services.Authorization.Infra/"]
RUN dotnet restore "./CoreDomain/BloodDonation.Services.Authorization.API/BloodDonation.Services.Donations.API.csproj"
COPY . .
WORKDIR "/src/CoreDomain/BloodDonation.Services.Authorization.API"
RUN dotnet build "./BloodDonation.Services.Donations.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BloodDonation.Services.Donations.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodDonation.Services.Donations.API.dll"]