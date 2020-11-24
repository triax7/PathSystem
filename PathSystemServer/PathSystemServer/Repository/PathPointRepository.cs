using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Models;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository
{
    public class PathPointRepository : BaseRepository<PathPoint>, IPathPointRepository
    {
        public PathPointRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
