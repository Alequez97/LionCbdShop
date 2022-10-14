using LionCbdShop.Domain.Extensions;
using LionCbdShop.Persistence;
using LionCbdShop.TelegramBot;
using LionCbdShop.TelegramBot.Extensions;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        
        services.Configure<HostOptions>(hostOptions =>
        {
            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });

        services.AddSingleton<ITelegramBotClient>(_ =>
        {
            var telegramToken = configuration!["Telegram:Token"];

            return new TelegramBotClient(telegramToken);
        });

        services.AddTelegramCommandServices();

        services.AddDomainLevelServices();
        
        services.AddDbContext<LionCbdShopDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Sql")), ServiceLifetime.Singleton);

        services.AddHostedService<TelegramBot>();
    })
    .Build();

// using (var scope = host.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<LionCbdShopDbContext>();
//     db.Database.Migrate();
// }

await host.RunAsync();