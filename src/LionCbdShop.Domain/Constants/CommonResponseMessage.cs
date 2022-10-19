namespace LionCbdShop.Domain.Constants;

public static class CommonResponseMessage
{
    public static class Get
    {
        public static string NotFound(string entityName, Guid id) => $"{entityName} with id {id} not found.";

        public static string Error(string entityName) => $"Unable to load {entityName} data. Try again later.";
    }

    public static class Create
    {
        public static string Success(string entityName) => $"New {entityName} was successfuly added!";

        public static string Error(string entityName) => $"Can't add new {entityName}. Try again later";
    }

    public static class Update
    {
        public static string Success(string entityName) => $"{entityName} was successfuly updated!";

        public static string Error(string entityName) => $"Can't perform {entityName} update. Try again later";
    }

    public static class Delete
    {
        public static string Success(string entityName) => $"{entityName} was successfuly deleted!";
        
        public static string Error(string entityName) => $"Can't perform {entityName} deletion. Try again later";
    }
}
