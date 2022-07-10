#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build the App in Release Configuration with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Blazor.Server/Blazor.Server.csproj", "Blazor.Server/"]
COPY ["Blazor.Client/Blazor.Client.csproj", "Blazor.Client/"]
COPY ["Blazor.Shared/Blazor.Shared.csproj", "Blazor.Shared/"]
RUN dotnet restore "Blazor.Server/Blazor.Server.csproj"
COPY . .
WORKDIR "/src/Blazor.Server"
RUN dotnet build "Blazor.Server.csproj" -c Release -o /app/build

# Publisch the App
FROM build AS publish
RUN dotnet publish "Blazor.Server.csproj" -c Release -o /app/publish

# Create final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor.Server.dll"]