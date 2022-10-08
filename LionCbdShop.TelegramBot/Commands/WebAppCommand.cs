using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.TelegramBot.Constants;
using LionCbdShop.TelegramBot.Interfaces;
using LionCbdShop.TelegramBot.Models;
using LionCbdShop.TelegramBot.Services;
using Newtonsoft.Json;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LionCbdShop.TelegramBot.Commands
{
    public class WebAppCommand : ITelegramCommand
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IOrderService _orderService;
        private readonly EmojiProvider _emojiProvider;

        public WebAppCommand(
            ITelegramBotClient telegramBotClient, 
            IOrderService orderService, 
            EmojiProvider emojiProvider)
        {
            _telegramBotClient = telegramBotClient;
            _orderService = orderService;
            _emojiProvider = emojiProvider;
        }

        public async Task SendResponseAsync(Update update)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                var webAppData = update.Message.WebAppData.Data;
                var webAppCommandData = JsonConvert.DeserializeObject<WebAppCommandData>(webAppData);

                var createOrderRequest = new CreateOrderRequest()
                {
                    CustomerUsername = update.Message.Chat.Username,
                    CartItems = webAppCommandData.CartItems.Select(webAppDataCartItem => {
                            return new CartItem() { ProductId = webAppDataCartItem.ProductId, Quantity = webAppDataCartItem.Quantity };
                        }).ToList()
                };

                var response = await _orderService.CreateAsync(createOrderRequest);

                if (response.IsSuccess)
                {
                    await _telegramBotClient.SendTextMessageAsync(
                        chatId,
                        GetMessageFromOrderData(webAppCommandData),
                        ParseMode.MarkdownV2
                    );

                    return;
                }
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

        private string GetMessageFromOrderData(WebAppCommandData webAppCommandData)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Please review your order");

            foreach (var cartItem in webAppCommandData.CartItems)
            {
                stringBuilder.AppendLine($"{cartItem.ProductName} {_emojiProvider.GetEmoji(cartItem.ProductName)} x{cartItem.Quantity}");
            }

            stringBuilder.AppendLine($"Total price: {webAppCommandData.TotalPrice}");

            return stringBuilder.Replace(".", ",").ToString();
        }
    }
}
