using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.ViewModels.Routing
{
    public class GetOptimizedRouteViewModel
    {
        [Required]
        public int RouteId { get; set; }
        [Required]
        public int StartingPointId { get; set; }
    }
}
