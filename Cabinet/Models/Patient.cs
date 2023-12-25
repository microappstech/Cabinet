using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Cabinet.Models;

namespace Cabinet.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public ICollection<Appointment> Appointments {get;set;}
        
    }
}
