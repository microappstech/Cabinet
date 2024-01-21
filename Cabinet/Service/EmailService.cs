using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Cabinet.Models;
using System.Net;


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
            
            string fromEmail = config["Username"] ;
            string toEmail = user.Email;
            //string appPassword = "ynva cult rsoq miiy";// config["Password"];
            string appPassword = config["Password"];
            using (var message2 = new System.Net.Mail.MailMessage(fromEmail, toEmail))
            {
                message2.Subject = "Reset password";
                message2.Body = $"Hey Mr {user.Email} " +
                $"Your new Password : <h1> {Password} </h1>";

                using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    client.Port = 587;
                    
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(fromEmail, appPassword);
                    client.EnableSsl = true;

                    try
                    {
                        client.Send(message2);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return true;
        }
    }
}