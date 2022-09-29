namespace LionCbdShop.Domain;

public class Response
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}

public class Response<T> where T : class
{
    public T ResponseObject { get; set; }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}