using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace LionCbdShop.TelegramBot.Commands.Payment;

public class InvoiceCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public InvoiceCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.Message?.Text?.Contains("invoice") ?? false;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        var labeledPrices = new List<LabeledPrice>()
        {
            new LabeledPrice("Cherry x3", 799),
            new LabeledPrice("Free delivery", 0)
        };

        await _telegramBotClient.SendInvoiceAsync(
            chatId,
            "Order number #141535434",
            "Please receive your invoice with cool cherry cigarete",
            "what is payload?",
            "284685063:TEST:MDE3ZGQ2YzEwMjk5",
            "EUR",
            labeledPrices,
            needShippingAddress: true,
            needPhoneNumber: true,
            needName: true
        );
    }
}