#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Control/Control.csproj", "Control/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["IdentityDataAccess/IdentityDataAccess.csproj", "IdentityDataAccess/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "Control/Control.csproj"
COPY . .
WORKDIR "/src/Control"
RUN dotnet build "Control.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Control.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Control.dll"]
