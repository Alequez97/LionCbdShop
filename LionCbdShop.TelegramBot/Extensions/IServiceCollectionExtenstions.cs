using LionCbdShop.TelegramBot.Commands;
using LionCbdShop.TelegramBot.Commands.Payment;
using LionCbdShop.TelegramBot.Interfaces;
using LionCbdShop.TelegramBot.Services;

namespace LionCbdShop.TelegramBot.Extensions;

public static class IServiceCollectionExtenstions
{
    public static IServiceCollection AddTelegramCommandServices(this IServiceCollection services)
    {
        //Common commands
        services.AddTransient<UnknownCommand>();
        services.AddTransient<ITelegramCommand, StartCommand>();

        //Payment commands
        services.AddTransient<ITelegramCommand, InvoiceCommand>();
        services.AddTransient<ITelegramCommand, PreCheckoutQueryCommand>();
        services.AddTransient<ITelegramCommand, SuccessfulPaymentCommand>();

        //Command services
        services.AddTransient<ITelegramCommandResolver, TelegramCommandResolver>();
        services.AddTransient<TelegramUpdateExecutor>();

        return services;
    }
}
