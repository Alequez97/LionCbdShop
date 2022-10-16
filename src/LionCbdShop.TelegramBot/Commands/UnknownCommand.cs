using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LionCbdShop.TelegramBot.Commands;

public class UnknownCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public UnknownCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        await _telegramBotClient.SendTextMessageAsync(
            chatId,
            $"Unknown command was sent",
            ParseMode.MarkdownV2
        );
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return false;
    }
}