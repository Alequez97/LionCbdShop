using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.TelegramBot.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LionCbdShop.TelegramBot.Commands
{
    public class WebAppCommand : ITelegramCommand
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public WebAppCommand(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task SendResponseAsync(Update update)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                var webAppData = update.Message.WebAppData.Data;

                var createOrderRequest = JsonConvert.DeserializeObject<CreateOrderRequest>(webAppData);

                await _telegramBotClient.SendTextMessageAsync(
                    chatId,
                    $"Your order is received and is ready to be completed",
                    ParseMode.MarkdownV2
                );
            }
            catch (Exception e)
            {
                await _telegramBotClient.SendTextMessageAsync(
                    chatId,
                    $"Error while processing your request",
                    ParseMode.MarkdownV2
                );
            }
        }

        public bool IsResponsibleForUpdate(Update update)
        {
            return update.Message?.Type == MessageType.WebAppData;
        }
    }
}
