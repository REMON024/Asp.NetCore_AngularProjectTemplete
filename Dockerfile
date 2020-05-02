FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["NybSys.API/NybSys.API.csproj", "NybSys.API/"]
COPY ["NybSys.AuditLog.DAL/NybSys.AuditLog.DAL.csproj", "NybSys.AuditLog.DAL/"]
COPY ["NybSys.Models/NybSys.Models.csproj", "NybSys.Models/"]
COPY ["NybSys.Common/NybSys.Common.csproj", "NybSys.Common/"]
COPY ["NybSys.Session.BLL/NybSys.Session.BLL.csproj", "NybSys.Session.BLL/"]
COPY ["NybSys.Session.DAL/NybSys.Session.DAL.csproj", "NybSys.Session.DAL/"]
COPY ["NybSys.AuditLog.BLL/NybSys.AuditLog.BLL.csproj", "NybSys.AuditLog.BLL/"]
COPY ["NybSys.DAL/NybSys.DAL.csproj", "NybSys.DAL/"]
COPY ["NybSys.Auth.BLL/NybSys.Auth.BLL.csproj", "NybSys.Auth.BLL/"]
COPY ["NybSys.UnitOfWork/NybSys.UnitOfWork.csproj", "NybSys.UnitOfWork/"]
COPY ["NybSys.Repository/NybSys.Repository.csproj", "NybSys.Repository/"]
RUN dotnet restore "NybSys.API/NybSys.API.csproj"
COPY . .
WORKDIR "/src/NybSys.API"
RUN dotnet build "NybSys.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NybSys.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "NybSys.API.dll"]