using System;
using System.ComponentModel.DataAnnotations.Schema;
using Cabinet.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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
        //public int DoctorId { get; set; }
        public string Visit { get; set; }
        [NotMapped] public DateTime? Start { get { return DateAppointement; } set { } }
        [NotMapped] public DateTime? End { get { return DateAppointement.Value.AddHours(1); } set { } }
        public Patient Patient {get; set;}
        //public Doctor Doctor { get; set;}
    }
}
