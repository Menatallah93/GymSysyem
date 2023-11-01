using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class MonthlyProfit
    {
        public int ID { get; set; }

        public double MonthPrice { get; set; }

        public DateTime ProfitMonthlyDate { get; set; }

        public bool Refresh { get; set; } = false;


    }
}
