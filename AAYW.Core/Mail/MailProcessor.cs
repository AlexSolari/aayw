using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AAYW.Core.Extensions;
using System.Net.Mime;
using AAYW.Core.Dependecies;
using AAYW.Core.Data.Managers;
using AAYW.Core.Models.Bussines;
using System.Net;
using System.Security;

namespace AAYW.Core.Mail
{
    public class MailProcessor : AAYW.Core.Mail.IMailProcessor
    {
        public MailProcessor()
        {

        }

        private SmtpClient CreateClient()
        {
            var client = new SmtpClient();
            var websiteSettings = ((WebsiteSettingsManager)Resolver.GetInstance<IManager<WebsiteSettings>>()).GetSettings();

            client.EnableSsl = websiteSettings.MailEnableSsl;
            client.Host = websiteSettings.MailHost;
            client.Port = websiteSettings.MailPort;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(websiteSettings.MailUsername, websiteSettings.MailPassword);

            return client;
        }

        public void Send(string adress, string subject, string templateKey, Dictionary<string, string> replacements = null)
        {
            var websiteSettings = ((WebsiteSettingsManager)Resolver.GetInstance<IManager<WebsiteSettings>>()).GetSettings();
            using (var client = CreateClient())
            {
                var body = Resources.ResourceAccessor
                    .Instance.Get(templateKey);

                if (replacements != null)
                {
                    foreach (var tag in replacements)
                    {
                        body.Replace("[{0}]".FormatWith(tag.Key), tag.Value);
                    }
                }

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(websiteSettings.MailAdress);
                msg.To.Add(adress);
                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = body;
                ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                msg.AlternateViews.Add(alternate);
                msg.Priority = MailPriority.High;

                client.Send(msg);
            }
        }
    }
}
