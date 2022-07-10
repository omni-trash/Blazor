#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# ASP.NET Core Runtime
# Daten müssen zuvor veröffentlicht worden sein.
# Erst dann sind alle nötigen Dateien vorhanden (zB wwwroot, web.config etc.)

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
# COPY source destination
# source is relative dir to current context
# destination is the WORKDIR (/app)
# alternative keep it empty and mount a host volume ... whatever
COPY publish .
#VOLUME [ "/data" ]
ENTRYPOINT ["dotnet", "Blazor.Server.dll"]
