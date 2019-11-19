FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["App2/App2.csproj", "App2/"]
RUN dotnet restore "App2/App2.csproj"
COPY . .
WORKDIR "/src/App2"
RUN dotnet build "App2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App2.dll"]