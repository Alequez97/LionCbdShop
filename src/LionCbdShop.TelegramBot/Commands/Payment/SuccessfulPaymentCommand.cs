using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Domain.Services;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LionCbdShop.TelegramBot.Commands.Payment;

public class SuccessfulPaymentCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IOrderService _orderService;

    public SuccessfulPaymentCommand(ITelegramBotClient telegramBotClient, IOrderService orderService)
    {
        _telegramBotClient = telegramBotClient;
        _orderService = orderService;
    }

    public async Task SendResponseAsync(Update update)
    {
        var chatId = update.Message.Chat.Id;

        var paidOrderNumber = update.Message.SuccessfulPayment.InvoicePayload;
        var updateOrderStatusRequest = new UpdateOrderStatusRequest()
        {
            OrderNumber = paidOrderNumber,
            Status = OrderStatus.Paid
        };
        await _orderService.UpdateOrderStatusAsync(updateOrderStatusRequest);

        await _telegramBotClient.SendTextMessageAsync(
            chatId,
            "Thank you for purchase",
            ParseMode.MarkdownV2
        );
    }

    public bool IsResponsibleForUpdate(Update update)
    {
        return update.Message?.Type == MessageType.SuccessfulPayment;
    }
}