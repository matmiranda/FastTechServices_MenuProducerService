FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MenuProducerService.Api/MenuProducerService.Api.csproj", "MenuProducerService.Api/"]
COPY ["MenuProducerService.Application/MenuProducerService.Application.csproj", "MenuProducerService.Application/"]
COPY ["MenuProducerService.Domain/MenuProducerService.Domain.csproj", "MenuProducerService.Domain/"]
COPY ["MenuProducerService.Infrastructure/MenuProducerService.Infrastructure.csproj", "MenuProducerService.Infrastructure/"]
RUN dotnet restore "MenuProducerService.Api/MenuProducerService.Api.csproj"
COPY . .
WORKDIR "/src/MenuProducerService.Api"
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "MenuProducerService.Api.dll"]
