FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["server2.csproj", "./"]
RUN dotnet restore "server2.csproj"
COPY . .
RUN dotnet build "server2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "server2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server2.dll"]
