using Microsoft.AspNetCore.Mvc;
using WSA.Microservice.Template.Application.Commands.Todo.Create;
using WSA.Microservice.Template.Application.Commands.Todo.Delete;
using WSA.Microservice.Template.Application.Commands.Todo.Update;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Web.Attributes;

namespace WSA.Microservice.Template.Web.Controllers.Api
{
    //[TokenAuthorize]
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
            var response = await Mediator.Send(new CreateTodoCommand { Todo = todo });
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TodoRequest todo)
        {
            var response = await Mediator.Send(new UpdateTodoCommand { Id = id, Todo = todo });
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await Mediator.Send(new DeleteTodoCommand { Id = id });
            return Ok(response);
        }
    }
}
