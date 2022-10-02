using LionCbdShop.TelegramBot.Commands;
using LionCbdShop.TelegramBot.Interfaces;
using LionCbdShop.TelegramBot.Services;

namespace LionCbdShop.TelegramBot.Extensions
{
    public static class IServiceCollectionExtenstions
    {
        public static IServiceCollection AddTelegramCommandServices(this IServiceCollection services)
        {
            services.AddTransient<UnknownCommand>();
            services.AddTransient<ITelegramCommand, StartCommand>();

            services.AddTransient<ITelegramCommandResolver, TelegramCommandResolver>();

            services.AddTransient<TelegramUpdateExecutor>();

            return services;
        }
    }
}
