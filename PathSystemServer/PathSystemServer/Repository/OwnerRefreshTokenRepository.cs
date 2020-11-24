using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Models;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository
{
    public class OwnerRefreshTokenRepository : BaseRepository<OwnerRefreshToken>, IOwnerRefreshTokenRepository
    {
        public OwnerRefreshTokenRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
