#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["e-Res/e-Res.csproj", "e-Res/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Common/Infractruture.Common.csproj", "Common/"]
COPY ["Common.Dtos/Common.Dtos.csproj", "Common.Dtos/"]
RUN dotnet restore "e-Res/e-Res.csproj"
COPY . .
WORKDIR "/src/e-Res"
RUN dotnet build "e-Res.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "e-Res.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "e-Res.dll"]