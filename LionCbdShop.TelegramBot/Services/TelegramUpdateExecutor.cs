using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Services
{
    public class TelegramUpdateExecutor
    {
        private readonly ITelegramCommandResolver _commandResolver;

        public TelegramUpdateExecutor(ITelegramCommandResolver commandResolver)
        {
            _commandResolver = commandResolver;
        }

        public async Task ExecuteAsync(Update update)
        {
            if (update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;

            var command = _commandResolver.Resolve(update);

            await command.SendResponseAsync(update);
        }
    }
}
