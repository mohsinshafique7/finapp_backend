using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finapp_backend.Helpers
{
    public class CommentQueryObject
    {
        public string? Symbol { get; set; } = null;
        public bool IsDescending { get; set; } = true;
    }
}