using MediatR;

namespace OnlineStore.BasketService.Application.Queries
{
    public class DeleteBasketByUserNameQuery : IRequest
    {
        public string UserName { get; set; }

        public DeleteBasketByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
