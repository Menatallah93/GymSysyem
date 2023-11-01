using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class DailyTrainee
    {
        public int ID { get; set; }

        public int NumberOfTrainee { get; set; } = 0;

        public DateTime Date { get; set; }
        public bool Done { get; set; }=false;

        public double TotalIncome { get; set; }
    }
}
