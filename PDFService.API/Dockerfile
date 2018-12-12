FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
RUN ["apt-get", "update"]
RUN ["apt-get", "-y", "install", "libgdiplus"]
WORKDIR /app
EXPOSE 8080

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY PDFService.sln ./
COPY PDFService.API/PDFService.API.csproj PDFService.API/
COPY PDFService.Adapter/PDFService.Adapter.csproj PDFService.Adapter/
COPY PDFService.Entities/PDFService.Entities.csproj PDFService.Entities/
COPY PDFService.DTO/PDFService.DTO.csproj PDFService.DTO/
COPY PDFService.API.Messages/PDFService.API.Messages.csproj PDFService.API.Messages/
COPY PDFService.DI/PDFService.DI.csproj PDFService.DI/
COPY PDFService.Repository/PDFService.Repository.csproj PDFService.Repository/
COPY PDFService.Repository.MySQL/PDFService.Repository.MySQL.csproj PDFService.Repository.MySQL/
COPY PDFService.Services.Entity/PDFService.Services.Entity.csproj PDFService.Services.Entity/
COPY docker-compose.dcproj .
RUN dotnet restore PDFService.sln -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/PDFService.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish PDFService.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PDFService.API.dll"]
