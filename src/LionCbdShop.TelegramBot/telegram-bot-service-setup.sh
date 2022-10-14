#!/bin/bash

mkdir /srv/LionCbdShop.TelegramBot
chown root /srv/LionCbdShop.TelegramBot
dotnet publish -c Release -o /srv/LionCbdShop.TelegramBot

sudo cp TelegramBot.service /etc/systemd/system/TelegramBot.service
sudo systemctl daemon-reload
sudo systemctl start TelegramBot
sudo systemctl status TelegramBot