using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSA.Microservice.Template.Application.Commands.Todo.Create;
using WSA.Microservice.Template.Application.Commands.Todo.Update;
using WSA.Microservice.Template.Application.Common.DTO;

namespace WSA.Microservice.Template.Application.Tests.Mock
{
    public static class TodoMock
    {
        public static CreateTodoCommand GetCreateTodoCommand(TodoRequest todo)
        {
            return new CreateTodoCommand
            {
                Todo = todo
            };
        }

        public static UpdateTodoCommand GetUpdateTodoCommand(int id, TodoRequest todo)
        {
            return new UpdateTodoCommand
            {
                Id = id,
                Todo = todo
            };
        }

        public static Domain.Entities.Todo TodoEntity = new Domain.Entities.Todo()
        {
            Id = 1,
            Title = "Meet up",
            Description = "Meet up on Jan 1st"
        };

        public static TodoRequest TodoModel = new TodoRequest()
        {
            Title = "Meet up",
            Description = "Meet up on Jan 1st"
        };

        public static TodoRequest UpdateTodoModel = new TodoRequest()
        {
            Title = "Meet up",
            Description = "Meet up on Jan 2nd"
        };
    }
}
