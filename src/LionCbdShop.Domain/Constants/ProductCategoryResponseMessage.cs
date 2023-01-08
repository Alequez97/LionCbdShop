namespace LionCbdShop.Domain.Constants;

public class ProductCategoryResponseMessage
{
    public static string CantDeleteBecauseOfExistingReference() => $"Can't delete because some products belongs to this product category";
}