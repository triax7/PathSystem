using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Models;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository
{
    class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
