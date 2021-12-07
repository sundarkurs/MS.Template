using System;
using System.Collections.Generic;

namespace WSA.Microservice.Template.Domain.Models
{
    public partial class Template
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
