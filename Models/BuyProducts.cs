using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSysyemWpf.Models
{
    public class BuyProducts
    {
        public int ID { get; set; }


        public string Name { get; set; }

        public int Quantaty { get; set; }

        // total-> Cost 
        public int Price { get; set; }

        // price -> sale
        public int SaledPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Done { get; set; }  = false;

        public string QrCode { get; set; }


        [DefaultValue(false)]
        public bool IsDeleted { get; set; }


    }
}


