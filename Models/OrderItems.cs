using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class OrderItems
    {
        public int Id { get; set; }

        [ForeignKey("BuyProducts")]
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }

        public double Price { get; set; }
        public DateTime orderdateTime { get; set; }
        public bool Done { get; set; } = false;
        public virtual BuyProducts BuyProducts { get; set; }
    }
}
