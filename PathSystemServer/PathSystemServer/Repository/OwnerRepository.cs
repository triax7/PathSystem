using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathSystemServer.Models;
using PathSystemServer.Repository.Interfaces;

namespace PathSystemServer.Repository
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationContext context)
            : base(context)
        {

        }
    }
}
