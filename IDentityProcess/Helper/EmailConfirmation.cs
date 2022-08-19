using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IDentityProcess.Helper
{
    public static class EmailConfirmation
    {
        public static void SendMail(string link,string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            mail.To.Add(email);
            mail.Subject = "";
            mail.Body = "<h2>Email adresini doğrulamak için linke tıklayın<h2/> <hr/>";
            mail.Body += $"<a href='{link}'> email doğrulama linki<a/>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential();
            smtpClient.Send(mail);
        }
    }
}
