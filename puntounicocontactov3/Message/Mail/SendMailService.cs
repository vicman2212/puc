using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Message.Mail
{
    public class SendMailService:IMessageBuilder<MessageModel>
    {
        private string _SMTPServer;
        private string _SMTPPort;
        private string _SenderMail;
        private string _PasswordMail;
        private string _SenderName;
        public SendMailService() {
            IConfigurationBuilder builder = new ConfigurationBuilder();// Nos permite usar un archivo para importar datos
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));//Establece la ruta en la que se encuentra el json
            IConfigurationRoot build = builder.Build(); // Acceso al archivo "appsettings.json"
            //Acceder a los valores del configuration
            _SMTPServer = build["MailConfiguration:SMTPServer"];
            _SMTPPort = build["MailConfiguration:SMTPPort"];
            _SenderMail = build["MailConfiguration:SenderMail"];
            _PasswordMail = build["MailConfiguration:PasswordMail"];
            _SenderName = build["MailConfiguration:SenderName"];
        }

        public async Task<bool> SendAsync(MessageModel message)
        {
            bool flag = false;
            int portInt = Int16.Parse(_SMTPPort);
            try
            {
                SmtpClient smtpClient = new SmtpClient(_SMTPServer, portInt)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_SenderMail, _PasswordMail)
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_SenderMail, _SenderName),
                    Subject = message.Subject,
                    Body = message.BodyMessage,
                    IsBodyHtml = true,
                };
                //Recipiente Para:
                foreach (var item in message.To.AsQueryable())
                {
                    mailMessage.To.Add(item);
                }
                //Recipiente CCP:
                foreach (var item in message.CCP.AsQueryable())
                {
                    mailMessage.CC.Add(item);
                }
                // mailMessage.CC.Add("victor.rgarcia@firco.gob.mx");
                await smtpClient.SendMailAsync(mailMessage);
                flag = true;
            }
            catch (Exception e)
            {
                flag = true;
                Console.WriteLine(e);
            }
            return flag;
        }
    }
}
