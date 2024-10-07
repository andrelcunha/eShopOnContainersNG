using ALC.Core.Mediator;
using ALC.Customers.API.Application.Commands;
using ALC.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ALC.Customers.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet ("customers")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new RegisterCustomerCommand(Guid.NewGuid(), 
                name: "Andre", 
                email: "deko81@gmail.com",
                cpf: "44604005028"));
            
            return CustomResponse(result);
        }
    }
}
