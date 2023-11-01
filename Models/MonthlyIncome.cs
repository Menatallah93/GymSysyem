using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class MonthlyIncome
    {
        public int Id { get; set; }
        public double MonthlyPrice { get; set; }
        public DateTime ProfitMonthDate { get; set; }
        public bool Refresh { get; set; } = false;
    }
}