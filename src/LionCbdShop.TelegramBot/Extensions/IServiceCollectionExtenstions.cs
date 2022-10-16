using LionCbdShop.TelegramBot.Commands;
using LionCbdShop.TelegramBot.Commands.Groups;
using LionCbdShop.TelegramBot.Commands.Payment;
using LionCbdShop.TelegramBot.Interfaces;
using LionCbdShop.TelegramBot.Services;

namespace LionCbdShop.TelegramBot.Extensions;

public static class IServiceCollectionExtenstions
{
    public static IServiceCollection AddTelegramCommandServices(this IServiceCollection services)
    {
        //Common commands
        services.AddSingleton<UnknownCommand>();
        services.AddSingleton<ITelegramCommand, StartCommand>();
        services.AddSingleton<ITelegramCommand, WebAppCommand>();

        //Telegram groups commands
        services.AddSingleton<ITelegramCommand, MyChatMemberCommand>();
        services.AddSingleton<ITelegramCommand, ChatMemberAddedCommand>();
        services.AddSingleton<ITelegramCommand, ChatMemberLeftCommand>();

        //Payment commands
        services.AddSingleton<ITelegramCommand, InvoiceCommand>();
        services.AddSingleton<ITelegramCommand, PreCheckoutQueryCommand>();
        services.AddSingleton<ITelegramCommand, SuccessfulPaymentCommand>();

        //Command services
        services.AddSingleton<ITelegramCommandResolver, TelegramCommandResolver>();
        services.AddSingleton<TelegramUpdateExecutor>();

        services.AddSingleton<EmojiProvider>();

        return services;
    }
}