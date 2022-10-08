namespace LionCbdShop.TelegramBot.Models;

public class WebAppCommandData
{
    public string CustomerUsername { get; set; }

    public List<WebAppCommandCartItems> CartItems { get; set; }

    public double TotalPrice
    {
        get
        {
            var result = 0d;

            CartItems.ForEach(cartItem =>
            {
                result += cartItem.OriginalPrice * cartItem.Quantity;
            });

            return result;
        }
    }
}

public class WebAppCommandCartItems
{
    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public double OriginalPrice { get; set; }

    public int Quantity { get; set; }
}
