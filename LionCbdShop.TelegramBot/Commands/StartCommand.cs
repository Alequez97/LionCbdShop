using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Customers;
using LionCbdShop.Persistence.Entities;
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
    private readonly ICustomerService _profileService;
    private readonly IConfiguration _configuration;

    public StartCommand(
        ITelegramBotClient telegramBotClient, 
        ICustomerService profileService, 
        IConfiguration configuration)
    {
        _telegramBotClient = telegramBotClient;
        _profileService = profileService;
        _configuration = configuration;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        var createCustomerRequest = new CreateCustomerRequest()
        {
            Username = update.Message.From.Username,
            FirstName = update.Message.From.FirstName,
            LastName = update.Message.From.LastName,
            CustomerProvider = CustomerProvider.Telegram,
            IdInCustomerProviderSystem = update.Message.From.Id.ToString(),
        };
        var response = await _profileService.CreateAsync(createCustomerRequest);

        if (!response.IsSuccess)
        {
            //TODO: Log, that customer profile was not stored in database
        }

        var inlineKeyboard = new ReplyKeyboardMarkup(
                new KeyboardButton("Open shop")
                {
                    WebApp = new WebAppInfo()
                    {
                        Url = _configuration["Telegram:WebAppUrl"],
                    }
                }
            );

        await _telegramBotClient.SendTextMessageAsync(
            chatId,
            $"Welcome in Royal MMXXI shop",
            ParseMode.MarkdownV2,
            replyMarkup: inlineKeyboard
        );
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.Message?.Text?.Contains(CommandNames.Start) ?? false;
    }
}
