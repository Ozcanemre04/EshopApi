using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Infrastructure.Data;

namespace Eshop.Infrastructure.Repository
{
    public class BaseRepository
    {
        public readonly AppDbContext _appDbContext;
        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}