using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using trifenix.connect.interfaces;

namespace trifenix.connect.email
{
    /// <summary>
    /// Clase email, encargada de la emisión de correos.
    /// </summary>
    public class Email : IEmail {

        private MailMessage Mail;
        private SmtpClient SmtpServer;

        /// <summary>
        /// Constructor del correo
        /// </summary>
        /// <param name="sender">emisor</param>
        /// <param name="password">clave</param>
        public Email(string sender, string password) {
            InitSMTPServer(sender,password);
        }


        /// <summary>
        /// Envía correo, ojo, debería ser parámetrizado
        /// </summary>
        /// <param name="sender">emisor</param>
        /// <param name="password">clave</param>
        private void InitSMTPServer(string sender, string password) {
            Mail = new MailMessage();
            Mail.From = new MailAddress("aresa.notificaciones@gmail.com", "Aresa");
            Mail.IsBodyHtml = true;
            SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Credentials = new System.Net.NetworkCredential(sender,password);
            SmtpServer.EnableSsl = true;
        }


        /// <summary>
        /// Envío de email
        /// </summary>
        /// <param name="mails">correo</param>
        /// <param name="subject">sujeto</param>
        /// <param name="htmlBody">cuerpo</param>
        public void SendEmail(List<string> mails, string subject, string htmlBody) {
            Mail.Subject = subject;
            var receivers = mails.Select(mail => new MailAddress(mail)).ToList();
            receivers.ForEach(receiver => Mail.To.Add(receiver));
            Mail.Body = htmlBody;
            SmtpServer.Send(Mail);
        }

    }
}