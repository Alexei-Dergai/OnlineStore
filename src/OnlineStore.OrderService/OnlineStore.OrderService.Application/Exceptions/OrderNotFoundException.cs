namespace OnlineStore.OrderService.Application.Exceptions
{
    [Serializable]
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() { }

        public OrderNotFoundException(string message)
            : base(message) { }

        public OrderNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
