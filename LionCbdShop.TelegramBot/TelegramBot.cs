using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LionCbdShop.TelegramBot;

public class TelegramBot : BackgroundService
{
    private readonly string _webAppUrl;
    private readonly TelegramBotClient _telegramBotClient;

    public TelegramBot(IConfiguration configuration, TelegramBotClient telegramBotClient)
    {
        _webAppUrl = configuration["Telegram:WebAppUrl"];
        _telegramBotClient = telegramBotClient;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Telegram bot started...");
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };

        _telegramBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cancellationToken
        );
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Telegram bot stopped...");
        return base.StopAsync(cancellationToken);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } outMessage)
            return;

        // Only process text messages
        if (outMessage.Text is not { } messageText)
            return;

        var chatId = outMessage.Chat.Id;

        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        
        var inlineKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton("Buy electornic cigarette")
                {
                    WebApp = new WebAppInfo()
                    {
                        Url = _webAppUrl
                    }
                }
            }
        });

        await botClient.SendTextMessageAsync(
            chatId, 
            "Welcome in Royal MMXXI shop", 
            ParseMode.MarkdownV2, 
            replyMarkup: inlineKeyboard);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}