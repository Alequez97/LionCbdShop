using LionCbdShop.TelegramBot.Constants;
using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LionCbdShop.TelegramBot.Commands;

public class StartCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IConfiguration _configuration;

    public StartCommand(ITelegramBotClient telegramBotClient, IConfiguration configuration)
    {
        _telegramBotClient = telegramBotClient;
        _configuration = configuration;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        var inlineKeyboard = new ReplyKeyboardMarkup(
            new KeyboardButton("Open shop")
            {
                WebApp = new WebAppInfo()
                {
                    Url = _configuration["Telegram:WebAppUrl"]
                }
            }
        );

        await _telegramBotClient.SendTextMessageAsync(
            chatId,
            "Welcome in Royal MMXXI shop",
            ParseMode.MarkdownV2,
            replyMarkup: inlineKeyboard
        );
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.Message?.Text?.Contains(CommandNames.Start) ?? false;
    }
}
