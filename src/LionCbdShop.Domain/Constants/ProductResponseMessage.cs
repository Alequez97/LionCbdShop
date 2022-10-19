namespace LionCbdShop.Domain.Constants;

public static class ProductResponseMessage
{
    public static string CantDeleteBecauseOfExistingReference() => $"Can't delete because some orders contains this product";
}