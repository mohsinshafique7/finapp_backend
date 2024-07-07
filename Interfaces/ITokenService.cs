using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finapp_backend.Models;

namespace finapp_backend.Interfaces
{
    public interface ITokenService
    {
        string createToken(AppUsers user);
    }
}