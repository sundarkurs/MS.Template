using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSA.Microservice.Template.Integration.Tests.Mock
{
    public static class TodoMock
    {
        public static List<Domain.Entities.Todo> Todos = new List<Domain.Entities.Todo>
        {
            new Domain.Entities.Todo{ Id = 1, Title= "Meetup" , Description= "Meeting up"},
            new Domain.Entities.Todo{ Id = 2, Title= "Party" , Description= "Birthday party"},
            new Domain.Entities.Todo{ Id = 3, Title= "Team meeting" , Description= "Status meeting"}
        };
    }
}
