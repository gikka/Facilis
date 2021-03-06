﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace Facilis.MVC
{
    public class EmailService
    {
        public static Task SendAsync(string assunto, string mensagem, List<string> destinatarios)
        {

            var msg = new MailMessage { From = new MailAddress("facilis.sge@gmail.com", "Facilis") };


            foreach (var item in destinatarios)
            {
                msg.Bcc.Add(new MailAddress(item));
            }

            msg.IsBodyHtml = true;
            msg.Subject = assunto;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mensagem, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mensagem, null, MediaTypeNames.Text.Html));

            using (var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587)))
            {
                smtpClient.UseDefaultCredentials = false;
                var credentials = new NetworkCredential("facilis.sge@gmail.com", "1qazxsw2cde");
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }

            return Task.FromResult(0);
        }
    }
}