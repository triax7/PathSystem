using System;
using System.Collections.Generic;
using System.Text;

namespace PathSystem.BLL.DTOs.Routing
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RouteDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
