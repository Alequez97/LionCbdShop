namespace LionCbdShop.Domain.Constants
{
    public static class CustomerMessages
    {
        public static class Create
        {
            public static string Success { get; private set; } = "Profile successfully created";

            public static string CustomerExists { get; private set; } = "Profile already exists";

            public static string Error(string logId)
            {
                return $"Can't create profile. See logs for more information. Log id: {logId}";
            }
        }
    }
}
