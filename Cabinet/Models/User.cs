using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cabinet.Models
{
    public class User:IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public string FullName { get; set; }
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        [NotMapped] public string ConfirmPassword { get; set; }
        [NotMapped] public string Password { get; set; }
        [NotMapped] public string Email{ get; set; }
        [NotMapped] public string Specialite { get; set; }
        [NotMapped] public string TypeUser { get; set; }
        public bool IsConnected { get; set; }
        public string Photo { get; set; }
        
        [NotMapped] public IEnumerable<string> RoleNames { get; set; }

    }
}
