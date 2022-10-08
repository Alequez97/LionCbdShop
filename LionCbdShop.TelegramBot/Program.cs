using LionCbdShop.Domain.Extensions;
using LionCbdShop.TelegramBot;
using LionCbdShop.TelegramBot.Extensions;
using Telegram.Bot;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });

        services.AddSingleton<ITelegramBotClient>(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var telegramToken = configuration!["Telegram:Token"];

            return new TelegramBotClient(telegramToken);
        });

        services.AddTelegramCommandServices();

        services.AddDomainLevelServices();
        
        services.AddHostedService<TelegramBot>();
    })
    .Build();

await host.RunAsync();