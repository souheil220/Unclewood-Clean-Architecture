FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UnclewoodCleanArchitectur/UnclewoodCleanArchitectur.csproj", "UnclewoodCleanArchitectur/"]
COPY ["UnclewoodCleanArchitecture.Infrastructure/UnclewoodCleanArchitecture.Infrastructure.csproj", "UnclewoodCleanArchitecture.Infrastructure/"]
COPY ["UnclewoodCleanArchitecture.Application/UnclewoodCleanArchitecture.Application.csproj", "UnclewoodCleanArchitecture.Application/"]
COPY ["UnclewoodCleanArchitecture.Domain/UnclewoodCleanArchitecture.Domain.csproj", "UnclewoodCleanArchitecture.Domain/"]
COPY ["UnclewoodCleanArchitecture.Presentation/UnclewoodCleanArchitecture.Presentation.csproj", "UnclewoodCleanArchitecture.Presentation/"]
RUN dotnet restore "UnclewoodCleanArchitectur/UnclewoodCleanArchitectur.csproj"
COPY . .
WORKDIR "/src/UnclewoodCleanArchitectur"
RUN dotnet build "UnclewoodCleanArchitectur.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UnclewoodCleanArchitectur.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnclewoodCleanArchitectur.dll"]
