using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Interfaces;

public interface ITelegramCommandResolver
{
    ITelegramCommand Resolve(Update update);
}