using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Interfaces;

public interface ITelegramCommand
{
    Task SendResponseAsync(Update update);

    bool IsResponsibleForUpdate(Update update);
}