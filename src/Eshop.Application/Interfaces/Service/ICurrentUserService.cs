using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Interfaces.Service
{
    public interface ICurrentUserService
    {
      string UserId { get; }
      bool IsAuthenticated { get; }
    }
}