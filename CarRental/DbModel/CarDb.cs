using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarRental.DbModel
{
    public class CarDb
    {
        [Key]
        public string Registration { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public virtual List<RentalDb> Rental { get; set; }
    }
}
