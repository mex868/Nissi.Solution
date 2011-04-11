using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Nissi.Util
{
    public static class SendEmail
    {
        /// Método de envio de email
        /// </summary>
        /// <param name="server">Servidor smtp</param>
        /// <param name="arquivo">nome e caminho do arquivo</param>
        /// <param name="titulo">titulo do email</param>
        public static void enviarEmailAnexo(string server, string emailEnviar, string titulo, StringBuilder corpo)
        {
            string[] enviarEmails = emailEnviar.Split(';');
            //Cria a Mensagem
            MailMessage msge = new MailMessage();
            msge.From = new MailAddress("nfe@nissimetal.com.br");
            for (int i = 0; i < enviarEmails.Length; i++)
            {
                if (!string.IsNullOrEmpty(enviarEmails[i]))
                    msge.To.Add(enviarEmails[i]);
            }
            msge.Subject = titulo;
            if (corpo.ToString().Contains("html"))
                msge.IsBodyHtml = true;
            msge.Body = corpo.ToString();
            //Envia
            SmtpClient client = new SmtpClient(server, 25);
            client.Credentials = new NetworkCredential("nfe@nissimetal.com.br", "nissi1973");
            client.Send(msge);
        }

        /// <summary>
        /// Método de envio de email
        /// </summary>
        /// <param name="server">Servidor smtp</param>
        /// <param name="emailEnviar"></param>
        /// <param name="arquivo">nome e caminho do arquivo</param>
        /// <param name="titulo">titulo do email</param>
        public static void enviarEmailAnexo(string server, string emailEnviar, string arquivo, string titulo, StringBuilder corpo)
        {
            string[] enviarEmails = emailEnviar.Split(';');
            //Cria a Mensagem
            MailMessage msge = new MailMessage();
            msge.From = new MailAddress("nfe@nissimetal.com.br");
            for (int i = 0; i < enviarEmails.Length; i++)
            {
                if (!string.IsNullOrEmpty(enviarEmails[i]))
                    msge.To.Add(enviarEmails[i]);
            }
            msge.Subject = titulo;
            if (corpo.ToString().Contains("html"))
                msge.IsBodyHtml = true;
            msge.Body = corpo.ToString();
            //Cria o anexo
            Attachment att = new Attachment(arquivo);
            //Adiciona a mensagem
            msge.Attachments.Add(att);
            //Envia
            SmtpClient client = new SmtpClient(server, 25);
            client.Credentials = new NetworkCredential("nfe@nissimetal.com.br", "nissi1973");
            client.Send(msge);
        }
    }
}
