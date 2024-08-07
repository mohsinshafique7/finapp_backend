using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace finapp_backend.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUsers AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}