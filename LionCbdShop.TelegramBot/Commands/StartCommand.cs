using LionCbdShop.TelegramBot.Constants;
using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace LionCbdShop.TelegramBot.Commands
{
    public class StartCommand : ITelegramCommand
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public StartCommand(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task SendResponseAsync(Update update)
        {
            var chatId = update.Message.Chat.Id;

            await _telegramBotClient.SendTextMessageAsync(
                chatId,
                "Welcome in Royal MMXXI shop",
                ParseMode.MarkdownV2
            );
        }

        public bool IsResponsibleForUpdate(Update update)
        {
            return update.Message != null && update.Message.Text.Contains(CommandNames.Start);
        }
    }
}
