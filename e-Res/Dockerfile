FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5192
ENV ASPNETCORE_URLS=http://+:5192


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "e-Res/e-Res.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ADD ./e-Res/Uploads/Images ./Images


ENTRYPOINT ["dotnet", "e-Res.dll"]