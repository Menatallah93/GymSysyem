using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class TraineeDateAttend
    {
        public int ID { get; set; }
        public DateTime Attend { get; set; }

        [ForeignKey("Trainee")]
        public int TraineeID { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
