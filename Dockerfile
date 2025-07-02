FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base 
WORKDIR /app 
EXPOSE 5000 
ENV ASPNETCORE_URLS=http://+:5000 
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src 
COPY ["Mindy&Tzippy project/Mindy&Tzippy project.csproj", "./"] 
COPY ["Mindy&Tzippy project/Mindy&Tzippy project.csproj", "./"] 
RUN dotnet restore 
RUN dotnet restore 
RUN dotnet publish -c Release -o /app/publish 
 
 
 
COPY --from=build /app/publish . 
ENTRYPOINT ["dotnet", "Mindy&Tzippy project.dll"] 
