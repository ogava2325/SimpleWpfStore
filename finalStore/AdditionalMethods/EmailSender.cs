using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalStore
{
    public static class EmailSender
    {
        public static async Task Execute(string toEmail, string toName, string subject, string content)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("applicationforstore@gmail.com", "Store");
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = "test";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
