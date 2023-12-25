using System;
using Cabinet.Models;
namespace Cabinet.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public DateTime? DateCreated {get; set;}
        public bool Annuled {get; set;}
        public bool Passed {get; set;}
        public Patient Patient {get; set;}
    }
}
