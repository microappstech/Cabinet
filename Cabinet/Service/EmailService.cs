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
        IConfiguration _configuration;
        public EmailService(IConfiguration iConfiguration){
            _configuration = iConfiguration;
        }
        public async Task<bool> sendMailResset(User user, string Password)
        {
            var config = _configuration.GetSection("STMPServer");
            var host = config["Host"];
            var message = new MimeMessage();  
            message.From.Add(new MailboxAddress(config["Name"], config["Username"]));  
            message.To.Add(new MailboxAddress(user.FullName, user.Email));  
            message.Subject = "Reset Password";

            message.Body = new TextPart("plain")
            {
                Text = $"Hey Mr {user.Email} " +
                $"Your new Password : <h1> {Password} </h1>"
            };
            using var client = new SmtpClient();
            client.Connect(host, Convert.ToInt32(config["Port"]), false);
            client.Authenticate(config["Username"], config["Password"]);
            var result = client.Send(message);
            client.Disconnect(true);
            return true;
        }
    }
}