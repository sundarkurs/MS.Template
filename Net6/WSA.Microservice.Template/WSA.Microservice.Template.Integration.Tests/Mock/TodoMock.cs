using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSA.Microservice.Template.Application.Commands.Todo.Create;

namespace WSA.Microservice.Template.Integration.Tests.Mock
{
    public static class TodoMock
    {
        public static List<Domain.Entities.Todo> Todos = new List<Domain.Entities.Todo>
        {
            new Domain.Entities.Todo{ Id = 1, Title= "Meet up" , Description= "Meeting up"},
            new Domain.Entities.Todo{ Id = 2, Title= "Party" , Description= "Birthday party"},
            new Domain.Entities.Todo{ Id = 3, Title= "Team meeting" , Description= "Status meeting"}
        };

        public static Domain.Entities.Todo NewTodo = new Domain.Entities.Todo()
        {
            Id = 1,
            Title = "Meet up",
            Description = "Meet up description."
        };

        public static CreateTodoCommand CreateCommand(string title, string description)
        {
            return new CreateTodoCommand
            {
                Todo = new Application.Common.DTO.TodoRequest { Title = title, Description = description }
            };
        }
    }
}
