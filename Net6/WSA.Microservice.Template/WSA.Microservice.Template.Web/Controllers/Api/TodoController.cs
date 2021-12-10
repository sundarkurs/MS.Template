using Microsoft.AspNetCore.Mvc;
using WSA.Microservice.Template.Application.Commands.Todo;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Queries.Todo;

namespace WSA.Microservice.Template.Web.Controllers.Api
{
    public class TodoController : BaseApiController
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllTodos.Query()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await Mediator.Send(new GetTodo.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TodoRequest todo)
        {
            var response = await Mediator.Send(new CreateTodo.Command { Todo = todo });
            return Ok(response);
        }
    }
}
