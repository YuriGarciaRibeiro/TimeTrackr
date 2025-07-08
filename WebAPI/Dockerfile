# Use imagem oficial do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Copia tudo e restaura dependências
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"

# Copia o restante e faz o build
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebAPI.dll"]
