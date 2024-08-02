using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Model
{
    public class Train
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainId { get; set; }



        [Column(TypeName = "varchar(25)")]
        [Required(ErrorMessage = "Train name can not be empty")]
        [MinLength(5, ErrorMessage = "Train name can not be less than 5")]
        public string Name { get; set; }


        public string ArrivalTime { get; set; }


        public string DepartureTime { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "MM/DD/YYYY Format")]
        public DateTime ArrivalDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "MM/DD/YYYY Format")]
        public DateTime DepartureDate { get; set; }

        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }

        public double distance { get; set; }
        public bool isActive { get; set; }
        
        //public ICollection<Booking> bookings { get; set; }
        public ICollection<Seat> seats { get; set; }


    }
}