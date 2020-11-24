using PathSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
