using System.Net;
using System.Net.Mail;

namespace ProyectoEjemploAPI.Utilities
{
    public class EmailService
    {
        private SmtpClient cliente;
        private MailMessage email;
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "eysonesteban1998@gmail.com";//correo desde el que se envia
        private string Pass = "pyimcyiizojozukn";//contraseña de aplicacion
        private bool EnabledSSL = true;

        public EmailService()
        {
            cliente = new SmtpClient()
            {
                Host = Host,
                Port = Port,
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(User, Pass)

            };
        }

        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtml = false)
        {
            email = new MailMessage(User, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtml;  
            cliente.Send(email);
        }
    }
}
