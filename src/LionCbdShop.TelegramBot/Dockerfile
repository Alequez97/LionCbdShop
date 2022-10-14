#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LionCbdShop.TelegramBot/LionCbdShop.TelegramBot.csproj", "LionCbdShop.TelegramBot/"]
RUN dotnet restore "LionCbdShop.TelegramBot/LionCbdShop.TelegramBot.csproj"
COPY . .
WORKDIR "/src/LionCbdShop.TelegramBot"
RUN dotnet build "LionCbdShop.TelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LionCbdShop.TelegramBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LionCbdShop.TelegramBot.dll"]