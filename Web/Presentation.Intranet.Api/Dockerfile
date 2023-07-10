#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/Presentation.Intranet.Api/Presentation.Intranet.Api.csproj", "Web/Presentation.Intranet.Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.Migration/Infrastructure.Migration.csproj", "Infrastructure.Migration/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Web/Presentation.Intranet.Api/Presentation.Intranet.Api.csproj"
COPY . .
WORKDIR "/src/Web/Presentation.Intranet.Api"
RUN dotnet build "Presentation.Intranet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.Intranet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.Intranet.Api.dll"]