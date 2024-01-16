using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Cabinet.Models;

namespace Cabinet.Service
{
    public class EmailService 
    {
        private readonly SmtpClient _smtpClient;
        public EmailService(IConfiguration iConfiguration){
            
        }
        public async Task<bool> sendMailResset(User user, string Password)
        {
            var message = new MimeMessage();  
            message.From.Add(new MailboxAddress("Karli Nader", "karli16@ethereal.email"));  
            message.To.Add(new MailboxAddress(user.FullName, user.Email));  
            message.Subject = "Reset Password";

            message.Body = new TextPart("plain")
            {
                Text = $"Hey Mr {user.Email} " +
                $"Your new Password : <h1> {Password} </h1>"
            };
            using var client = new SmtpClient();
            client.Connect( "smtp.ethereal.email", 587, false);
            client.Authenticate("karli16@ethereal.email", "dd3MszcGSaq9V6c9NP");
            client.Send(message);
            client.Disconnect(true);
            return true;
        }
    }
}