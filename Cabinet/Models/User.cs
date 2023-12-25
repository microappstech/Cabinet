using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cabinet.Models
{
    public class User:IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        [NotMapped] public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public bool IsConnected { get; set; }
        public string Photo { get; set; }
    }
}
