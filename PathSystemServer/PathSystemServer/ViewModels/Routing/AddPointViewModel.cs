using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathSystemServer.ViewModels.Routing
{
    public class AddPointViewModel
    {
        [Required]
        public int RouteId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
    }
}
