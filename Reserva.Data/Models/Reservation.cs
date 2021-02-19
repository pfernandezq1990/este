using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Reserva.Data.Models{
    public class Reservation {
        public int Id { get; set;}        

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd-MM", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }        

        [Range(1, 5)]
        [Required]
        public int Ranking { get; set; }

        [Required]
        public bool Favorite { get; set; }

        [Required]
        public int ContactId { get; set; }

        [JsonIgnore]
        public virtual Contact Contact { get; set; }

       
    }
}