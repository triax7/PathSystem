using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.ViewModels.Routing
{
    public class RouteCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
