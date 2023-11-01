using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class Subscription
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public int? NumbersOfSessions { get; set; }
        public double Price { get; set; }
        public int NumberOfMonths { get; set; }


        [DefaultValue(false)]
        public bool Special { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public List<Trainee> Trainee { get; set; }


        // 3month -> startDate : 5/8   5/11  

        // 15 Sessison => startDate : 5/8  end: 5/9  15 session

        // 12  => star:5/8  end: 5/9     12 session

    }
}
