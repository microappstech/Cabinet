﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cabinet.Models
{
    public class Doctor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialite  { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Photo { get; set; }
        public string PhoneNumber{ get; set; }
        [NotMapped] public string Email { get; set; }
        [NotMapped] public string Password { get; set; }
        public string CIN { get; set; }

        //public ICollection<Appointment> Appointments { get; set; }
    }
}
