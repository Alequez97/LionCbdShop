#!/bin/bash

sudo systemctl stop TelegramBot

git pull
rm -rf /srv/LionCbdShop.TelegramBot
mkdir /srv/LionCbdShop.TelegramBot
chown root /srv/LionCbdShop.TelegramBot
dotnet publish -c Release -o /srv/LionCbdShop.TelegramBot

sudo cp TelegramBot.service /etc/systemd/system/TelegramBot.service
sudo systemctl daemon-reload
echo "run sudo systemctl start TelegramBot to start service"