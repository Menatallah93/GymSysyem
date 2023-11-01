using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string EDitAndDeletPassword { set; get; }
        public string FinancialPassword { set; get; }

    }
}
