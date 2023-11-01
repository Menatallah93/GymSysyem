using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class AddintionalBill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public bool IsDeleted { get; set; } = false;
        public bool IsOnDay { get; set; } = false;
    }
}
