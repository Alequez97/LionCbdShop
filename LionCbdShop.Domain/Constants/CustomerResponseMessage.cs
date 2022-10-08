namespace LionCbdShop.Domain.Constants
{
    public static class CustomerResponseMessage
    {
        public static string Exists(string username) => $"Customer with username {username} already exists"; 
    }
}
