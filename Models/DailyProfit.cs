using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class DailyProfit
    {
       public int Id { get; set; }
        public double Price { get; set; }

        public double TraineePrice { get; set; } = 0.0;
        public double ProductPrice { get; set; } = 0.0;
        public double BillPrice { get; set; } = 0.0;
        public DateTime ProfitDate { get; set; }
        public bool Done { get; set; } = false;
    }
}
