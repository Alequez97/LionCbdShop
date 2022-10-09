using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Commands.Payment;

public class PreCheckoutQueryCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;

    public PreCheckoutQueryCommand(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.PreCheckoutQuery != null;
    }

    public async Task SendResponseAsync(Update update)
    {
        var preCheckoutQuery = update.PreCheckoutQuery;

        // Here data can be validated and desicion can be made if order can processed

        //await _telegramBotClient.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id);
    }
}
