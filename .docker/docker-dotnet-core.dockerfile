FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CloudApp/CloudApp.csproj", "CloudApp/"]
COPY ["CloudApp.Core/CloudApp.Core.csproj", "CloudApp.Core/"]
COPY ["CloudApp.Infra/CloudApp.Infra.csproj", "CloudApp.Infra/"]
COPY ["CloudApp.Model/CloudApp.Model.csproj", "CloudApp.Model/"]
RUN dotnet restore "CloudApp\CloudApp.csproj"
COPY . .
WORKDIR "/src/CloudApp"
RUN dotnet build "CloudApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudApp.dll"]
