
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
   public enum Gender
    {
        Male,
        Female
    }
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { set; get; }
        public int Age { set; get; }
        public string? QrCode { set; get; }
        public Gender Gender { set; get; }

        [ForeignKey("Subscription")]
        public int SubscriptionID { get; set; }
        public virtual Subscription Subscription { get; set; }

        public double Paid { get; set; }
        public double Price { get; set; } = 0.0;

        public int NumberOfAttendance { set; get; } = 0;
        public bool Seen { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? LastAttendDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int DisplayOrderID { get; set; } = 1;


        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual List<TraineeDateAttend> TraineeDateAttends { get; set; }


    }
}
