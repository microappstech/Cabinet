using System.ComponentModel.DataAnnotations.Schema;

namespace Cabinet.Models
{
    public class Infirmier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Photo { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string CIN { get; set; }
    }
}
