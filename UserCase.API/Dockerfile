FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UserCase.API/UserCase.API.csproj", "UserCase.API/"]
RUN dotnet restore "UserCase.API/UserCase.API.csproj"
COPY . .
WORKDIR "/src/UserCase.API"
RUN dotnet build "UserCase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserCase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserCase.API.dll"]
