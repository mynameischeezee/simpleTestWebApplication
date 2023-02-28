FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["movieStorage.Registration/movieStorage.Registration.csproj", "movieStorage.Registration/"]
RUN dotnet restore "movieStorage.Registration/movieStorage.Registration.csproj"
COPY . .
WORKDIR "/src/movieStorage.Registration"
RUN dotnet build "movieStorage.Registration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "movieStorage.Registration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "movieStorage.Registration.dll"]
