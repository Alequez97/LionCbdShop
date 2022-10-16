using LionCbdShop.TelegramBot.Extensions;
using LionCbdShop.TelegramBot.Services;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;

namespace LionCbdShop.TelegramBot;

public class TelegramBot : BackgroundService
{
    private readonly string _webAppUrl;

    private ITelegramBotClient _telegramBotClient;
    private TelegramUpdateExecutor _telegramUpdateExecutor;

    public TelegramBot(
        IConfiguration configuration,
        ITelegramBotClient telegramBotClient,
        TelegramUpdateExecutor telegramUpdateExecutor
    )
    {
        _webAppUrl = configuration["Telegram:WebAppUrl"];
        _telegramBotClient = telegramBotClient;
        _telegramUpdateExecutor = telegramUpdateExecutor;
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

        // await _telegramBotClient.SetWebAppInChatMenuButton("Shop", _webAppUrl);

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
        await _telegramUpdateExecutor.ExecuteAsync(update);
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