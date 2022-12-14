using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.TelegramBot.Interfaces;
using LionCbdShop.TelegramBot.Models;
using LionCbdShop.TelegramBot.Services;
using Newtonsoft.Json;
using System.Text;
using Telegram.Bot.Types.Payments;

namespace LionCbdShop.TelegramBot.Commands
{
    public class WebAppCommand : ITelegramCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IOrderService _orderService;
        private readonly EmojiProvider _emojiProvider;

        public WebAppCommand(
            IConfiguration configuration,
            ITelegramBotClient telegramBotClient,
            IOrderService orderService,
            EmojiProvider emojiProvider)
        {
            _configuration = configuration;
            _telegramBotClient = telegramBotClient;
            _orderService = orderService;
            _emojiProvider = emojiProvider;
        }

        public async Task SendResponseAsync(Update update)
        {
            var chatId = update.Message.Chat.Id;

            try
            {
                var webAppSentData = JsonConvert.DeserializeObject<WebAppCommandData>(update.Message.WebAppData.Data);

                var createOrderRequest = new CreateOrderRequest()
                {
                    IdInCustomerProviderSystem = chatId.ToString(),
                    CustomerUsername = update.Message.Chat.Username ?? update.Message.Chat.FirstName,
                    CartItems = webAppSentData.CartItems.Select(webAppDataCartItem =>
                    {
                        return new CreateOrderRequestCartItem()
                        {
                            ProductId = webAppDataCartItem.ProductId,
                            ProductNameDuringOrderCreation = webAppDataCartItem.ProductName,
                            Quantity = webAppDataCartItem.Quantity
                        };
                    }).ToList()
                };

                var createOrderResponse = await _orderService.CreateAsync(createOrderRequest);

                if (createOrderResponse.IsSuccess)
                {
                    await _telegramBotClient.SendInvoiceAsync(
                        chatId,
                        $"Order number {createOrderResponse.ResponseObject.OrderNumber}",
                        "Best quality with Royal MMXXI",
                        createOrderResponse.ResponseObject.OrderNumber,
                        _configuration["PaymentGateway:Stripe"],
                        "EUR",
                        GetPaymentLabeledPricesFromOrderData(webAppSentData),
                        needShippingAddress: true,
                        needPhoneNumber: true,
                        needName: true
                    );

                    var updateOrderRequest = new UpdateOrderStatusRequest()
                    {
                        OrderNumber = createOrderResponse.ResponseObject.OrderNumber,
                        Status = OrderStatus.InvoiceSent
                    };
                    await _orderService.UpdateOrderStatusAsync(updateOrderRequest);

                    return;
                }
            }
            catch (Exception e)
            {
                await _telegramBotClient.SendTextMessageAsync(
                    chatId,
                    "Error while processing your request! Try again later or contact support",
                    ParseMode.MarkdownV2
                );
            }
        }

        public bool IsResponsibleForUpdate(Update update)
        {
            return update.Message?.Type == MessageType.WebAppData;
        }

        private IEnumerable<LabeledPrice> GetPaymentLabeledPricesFromOrderData(WebAppCommandData webAppCommandData,
            int deliveryPriceInCents = default)
        {
            foreach (var cartItem in webAppCommandData.CartItems)
            {
                yield return new LabeledPrice(
                    $"{cartItem.ProductName} {_emojiProvider.GetEmoji(cartItem.ProductName)} x{cartItem.Quantity}",
                    (int)(cartItem.TotalPrice * 100));
            }

            if (deliveryPriceInCents > 0)
            {
                yield return new LabeledPrice("Delivery", deliveryPriceInCents);
            }
            else
            {
                yield return new LabeledPrice("Free delivery", 0);
            }
        }

        private string GetMessageFromOrderData(WebAppCommandData webAppCommandData)
        {
            var stringBuilder = new StringBuilder();

            //stringBuilder.AppendLine("Please review your order");

            foreach (var cartItem in webAppCommandData.CartItems)
            {
                stringBuilder.AppendLine(
                    $"{cartItem.ProductName} {_emojiProvider.GetEmoji(cartItem.ProductName)} x{cartItem.Quantity}");
            }

            stringBuilder.AppendLine($"Total price: {webAppCommandData.TotalPrice}");

            return stringBuilder.Replace(".", ",").ToString();
        }
    }
}