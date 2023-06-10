using System.ComponentModel.DataAnnotations;

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
