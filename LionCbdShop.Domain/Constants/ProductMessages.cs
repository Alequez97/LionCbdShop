namespace LionCbdShop.Domain.Constants;

public static class ProductMessages
{
    public static class Common
    {
        public static string NotFound(string id)
        {
            return $"Product with id {id} not found";
        }
    }

    public static class Get
    {
        public const string Error = "Unable to load product data!";
    }

    public static class Create
    {
        public const string Success = "New product was added!";

        public const string Error = "Can't add new product. Try again later";
    }

    public static class Update
    {
        public const string Success = "Product was updated!";

        public const string Error = "Can't perform product update. Try again later";
    }

    public static class Delete
    {
        public const string Success = "Product was deleted!";

        public const string Error = "Can't perform product deletion. Try again later";
    }
}
