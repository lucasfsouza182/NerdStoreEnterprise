using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Customers.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Customers.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new CreateCustomerCommand(Guid.NewGuid(), "Lucas", "teste@teste.com", "30314299076"));

            return CustomResponse(result);
        }
    }
}
