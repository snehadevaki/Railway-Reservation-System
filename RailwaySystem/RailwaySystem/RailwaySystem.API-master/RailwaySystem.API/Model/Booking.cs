using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Model
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [ForeignKey("TrainId")]
        public int? TrainId { get; set; }


        [ForeignKey("PassengerId")]
        public int? PassengerId { get; set; }

        public int UserId { get; set; }


        public string Classes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public double fare { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "MM/DD/YYYY Format")]
        public DateTime Date { get; set; }


        public string Status { get; set; }

        public int SeatNum { get; set; }
      
        public bool isActive { get; set; }
        public ICollection<Transaction> transactions { get; set; }
    }
}