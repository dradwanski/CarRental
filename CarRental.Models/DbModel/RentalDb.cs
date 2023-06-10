using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DbModel
{
    public class RentalDb
    {
        [Key]
        public int RentalId { get; set; }
        public virtual CarDb CarRegistration { get; set; }
        public virtual CustomerDb Customer { get; set; }
        public DateOnly RentalDate  { get; set; }
        public DateOnly ReturnDate  { get; set; }
        public int Fees { get; set; }
    }
}
