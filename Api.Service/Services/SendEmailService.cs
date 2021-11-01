using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Email;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;

namespace Api.Service.Services
{
    public class SendEmailService : ISendEmailSerivce
    {

        EmailConfiguration emailConfiguration = new EmailConfiguration();


        public async Task SendMail(SendEmailDto sendEmail)
        {
            try
            {
                using (var _mailConf = new MailMessage())
                {
                    _mailConf.From = new MailAddress(emailConfiguration.Email);
                    _mailConf.Subject = sendEmail.Subject;
                    _mailConf.To.Add(sendEmail.SendTo);
                    _mailConf.IsBodyHtml = true;
                    _mailConf.Body = $"<p> {sendEmail.BodyEmail} </p>";
                    _mailConf.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                    _mailConf.BodyEncoding = Encoding.GetEncoding("UTF-8");

                    // SMTP - Simple Mail Transfer Protocol
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential(emailConfiguration.Email, emailConfiguration.Password);
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Host = "smtp.office365.com";
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.Timeout = 20_000;

                        await smtpClient.SendMailAsync(_mailConf);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}