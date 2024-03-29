﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BasketService.Application.Commands;
using OnlineStore.BasketService.Application.Queries;
using OnlineStore.BasketService.Application.Responses;
using System.Net;

namespace OnlineStore.BasketService.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("basket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);

            return Ok(basket);
        }

        [HttpPost("basket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var basket = await _mediator.Send(createShoppingCartCommand);

            return Ok(basket);
        }

        [HttpDelete]
        [Route("basket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
        {
            var query = new DeleteBasketByUserNameQuery(userName);

            return Ok(await _mediator.Send(query));
        }
    }
}
