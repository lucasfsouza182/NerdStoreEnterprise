using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Clients.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Clients.API.Controllers
{
    public class ClientsController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new CreateClientCommand(Guid.NewGuid(), "Lucas", "teste@teste.com", "30314299076"));

            return CustomResponse(result);
        }
    }
}
