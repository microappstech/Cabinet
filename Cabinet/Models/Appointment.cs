using System;
using System.ComponentModel.DataAnnotations.Schema;
using Cabinet.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabinet.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateAppointement { get; set;}
        public bool Annuled {get; set;}
        public bool Passed {get; set;}
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient {get; set;}
        public Doctor Doctor { get; set;}
    }
}
