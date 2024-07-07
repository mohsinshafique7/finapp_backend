using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finapp_backend.Dtos.Account
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Tokens { get; set; }
    }
}