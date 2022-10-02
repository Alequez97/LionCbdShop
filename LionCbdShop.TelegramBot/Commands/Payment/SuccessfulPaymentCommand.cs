using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LionCbdShop.TelegramBot.Commands.Payment;

public class SuccessfulPaymentCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public SuccessfulPaymentCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        await _telegramBotClient.SendTextMessageAsync(
            chatId,
            "Thank you for purchase",
            ParseMode.MarkdownV2
        );
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.Message?.Type == MessageType.SuccessfulPayment;
    }
}
