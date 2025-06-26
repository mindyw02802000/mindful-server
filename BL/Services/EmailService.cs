using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using System.Collections.Generic;

namespace BL.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly string _adminEmail;


        public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword, string adminEmail)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _adminEmail = adminEmail;
        }

        public async Task SendOrderConfirmationEmailAsync(BlOrder order)
        {
            try
            {
                // שליחת אימייל ללקוח
                await SendCustomerConfirmationEmailAsync(order);

                // שליחת אימייל למנהל
                await SendAdminConfirmationEmailAsync(order);
            }
            catch (Exception ex)
            {
                // לוג השגיאה
                Console.WriteLine($"שגיאה בשליחת אימייל: {ex.Message}");
                throw;
            }
        }

        private async Task SendCustomerConfirmationEmailAsync(BlOrder order)
        {
            string subject = $"אישור הזמנה #{order.IdOrder} - {order.IdSchool}";
            string body = GenerateCustomerEmailBody(order);

            await SendEmailAsync(order.Email, subject, body);
        }

        private async Task SendAdminConfirmationEmailAsync(BlOrder order)
        {
            string subject = $"הזמנה חדשה #{order.IdOrder} מ-{order.IdSchool}";
            string body = GenerateAdminEmailBody(order, order.IdSchool.ToString(), order.Email);

            await SendEmailAsync(_adminEmail, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(_senderEmail, "מערכת הזמנות תלבושות");
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;

                    await client.SendMailAsync(message);
                }
            }
        }

        private string GenerateCustomerEmailBody(BlOrder order)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html dir=\"rtl\" lang=\"he\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\">");
            sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("  <title>אישור הזמנה</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }");
            sb.AppendLine("    .container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; }");
            sb.AppendLine("    .header { background-color: #1a1a2e; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }");
            sb.AppendLine("    .content { background-color: white; padding: 20px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }");
            sb.AppendLine("    .footer { text-align: center; margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; color: #777; font-size: 14px; }");
            sb.AppendLine("    h1, h2 { color: #1a1a2e; }");
            sb.AppendLine("    .highlight { color: #ff2e63; font-weight: bold; }");
            sb.AppendLine("    .details { margin: 20px 0; border: 1px solid #eee; padding: 15px; border-radius: 5px; background-color: #f9f9f9; }");
            sb.AppendLine("    .details-row { display: flex; justify-content: space-between; margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 10px; }");
            sb.AppendLine("    .details-row:last-child { border-bottom: none; margin-bottom: 0; padding-bottom: 0; }");
            sb.AppendLine("    .details-label { font-weight: bold; color: #555; }");
            sb.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 15px; }");
            sb.AppendLine("    th, td { padding: 10px; border: 1px solid #ddd; text-align: right; }");
            sb.AppendLine("    th { background-color: #f2f2f2; }");
            sb.AppendLine("    .total-row { font-weight: bold; background-color: #f2f2f2; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"container\">");
            sb.AppendLine("    <div class=\"header\">");
            sb.AppendLine("      <h1>אישור הזמנה</h1>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"content\">");
            sb.AppendLine($"      <h2>שלום {order.IdSchool},</h2>");
            sb.AppendLine("      <p>תודה שהזמנת אצלנו. פרטי ההזמנה שלך מפורטים להלן:</p>");

            sb.AppendLine("      <div class=\"details\">");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">מספר הזמנה:</span>");
            sb.AppendLine($"          <span>{order.IdOrder}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">תאריך הזמנה:</span>");
            sb.AppendLine($"          <span>{order.DateOfOrdder.ToString("dd/MM/yyyy")}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">תאריך אירוע:</span>");
            sb.AppendLine($"          <span>{order.DateOfEvent.ToString("dd/MM/yyyy")}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">איש קשר:</span>");
            sb.AppendLine($"          <span>{order.Contact}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">טלפון:</span>");
            sb.AppendLine($"          <span>{order.PhoneContact}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">כתובת אספקה:</span>");
            sb.AppendLine($"          <span>{order.ProvisionAddress}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            sb.AppendLine("      <h3>פריטים שהוזמנו:</h3>");
            sb.AppendLine("      <table>");
            sb.AppendLine("        <tr>");
            sb.AppendLine("          <th>דגם</th>");
            sb.AppendLine("          <th>מידה</th>");
            sb.AppendLine("          <th>כמות</th>");
            sb.AppendLine("          <th>מחיר</th>");
            sb.AppendLine("        </tr>");

            foreach (var item in order.DetailingOrders)
            {
                sb.AppendLine("        <tr>");
                sb.AppendLine($"          <td>{item.IdModel}</td>");
                sb.AppendLine($"          <td>{item.Size}</td>");
                sb.AppendLine($"          <td>{item.Count}</td>");
                sb.AppendLine($"          <td>יוצג בהמשך</td>"); // מחיר הפריט אינו מופיע במודל DetailingOrder
                sb.AppendLine("        </tr>");
            }

            sb.AppendLine("        <tr class=\"total-row\">");
            sb.AppendLine("          <td colspan=\"3\">סה\"כ לתשלום:</td>");
            sb.AppendLine($"          <td>₪{order.CostPrice}</td>");
            sb.AppendLine("        </tr>");
            sb.AppendLine("      </table>");

            sb.AppendLine("      <p>אנו מודים לך על הזמנתך ונשמח לעמוד לשירותך בכל עת.</p>");
            sb.AppendLine("      <p>בברכה,<br>צוות התלבושות</p>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"footer\">");
            sb.AppendLine("      <p>© 2023 מערכת הזמנות תלבושות. כל הזכויות שמורות.</p>");
            sb.AppendLine("      <p>אם יש לך שאלות, אנא צור קשר בטלפון: 03-1234567</p>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private string GenerateAdminEmailBody(BlOrder order, string schoolName, string customerEmail)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html dir=\"rtl\" lang=\"he\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\">");
            sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("  <title>הזמנה חדשה</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }");
            sb.AppendLine("    .container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; }");
            sb.AppendLine("    .header { background-color: #1a1a2e; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }");
            sb.AppendLine("    .content { background-color: white; padding: 20px; border-radius: 0 0 5px 5px; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }");
            sb.AppendLine("    .footer { text-align: center; margin-top: 20px; padding-top: 20px; border-top: 1px solid #eee; color: #777; font-size: 14px; }");
            sb.AppendLine("    h1, h2 { color: #1a1a2e; }");
            sb.AppendLine("    .highlight { color: #ff2e63; font-weight: bold; }");
            sb.AppendLine("    .details { margin: 20px 0; border: 1px solid #eee; padding: 15px; border-radius: 5px; background-color: #f9f9f9; }");
            sb.AppendLine("    .details-row { display: flex; justify-content: space-between; margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 10px; }");
            sb.AppendLine("    .details-row:last-child { border-bottom: none; margin-bottom: 0; padding-bottom: 0; }");
            sb.AppendLine("    .details-label { font-weight: bold; color: #555; }");
            sb.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 15px; }");
            sb.AppendLine("    th, td { padding: 10px; border: 1px solid #ddd; text-align: right; }");
            sb.AppendLine("    th { background-color: #f2f2f2; }");
            sb.AppendLine("    .total-row { font-weight: bold; background-color: #f2f2f2; }");
            sb.AppendLine("    .alert { background-color: #ffe6e6; border-left: 4px solid #ff2e63; padding: 10px; margin: 15px 0; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class=\"container\">");
            sb.AppendLine("    <div class=\"header\">");
            sb.AppendLine("      <h1>הזמנה חדשה התקבלה</h1>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"content\">");
            sb.AppendLine("      <div class=\"alert\">");
            sb.AppendLine($"        <p><strong>התקבלה הזמנה חדשה #{order.IdOrder} מ-{schoolName}</strong></p>");
            sb.AppendLine("      </div>");

            sb.AppendLine("      <h3>פרטי הלקוח:</h3>");
            sb.AppendLine("      <div class=\"details\">");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">שם בית הספר:</span>");
            sb.AppendLine($"          <span>{schoolName}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">איש קשר:</span>");
            sb.AppendLine($"          <span>{order.Contact}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">טלפון:</span>");
            sb.AppendLine($"          <span>{order.PhoneContact}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">דוא\"ל:</span>");
            sb.AppendLine($"          <span>{customerEmail}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">כתובת אספקה:</span>");
            sb.AppendLine($"          <span>{order.ProvisionAddress}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            sb.AppendLine("      <h3>פרטי ההזמנה:</h3>");
            sb.AppendLine("      <div class=\"details\">");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">מספר הזמנה:</span>");
            sb.AppendLine($"          <span>{order.IdOrder}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">תאריך הזמנה:</span>");
            sb.AppendLine($"          <span>{order.DateOfOrdder.ToString("dd/MM/yyyy HH:mm")}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"details-row\">");
            sb.AppendLine("          <span class=\"details-label\">תאריך אירוע:</span>");
            sb.AppendLine($"          <span>{order.DateOfEvent.ToString("dd/MM/yyyy")}</span>");
            sb.AppendLine("        </div>");
            sb.AppendLine("      </div>");

            sb.AppendLine("      <h3>פריטים שהוזמנו:</h3>");
            sb.AppendLine("      <table>");
            sb.AppendLine("        <tr>");
            sb.AppendLine("          <th>דגם</th>");
            sb.AppendLine("          <th>מידה</th>");
            sb.AppendLine("          <th>כמות</th>");
            sb.AppendLine("          <th>מחיר</th>");
            sb.AppendLine("        </tr>");

            foreach (var item in order.DetailingOrders)
            {
                sb.AppendLine("        <tr>");
                sb.AppendLine($"          <td>{item.IdModel}</td>");
                sb.AppendLine($"          <td>{item.Size}</td>");
                sb.AppendLine($"          <td>{item.Count}</td>");
                sb.AppendLine($"          <td>יוצג בהמשך</td>"); // מחיר הפריט אינו מופיע במודל DetailingOrder
                sb.AppendLine("        </tr>");
            }

            sb.AppendLine("        <tr class=\"total-row\">");
            sb.AppendLine("          <td colspan=\"3\">סה\"כ לתשלום:</td>");
            sb.AppendLine($"          <td>₪{order.CostPrice}</td>");
            sb.AppendLine("        </tr>");
            sb.AppendLine("      </table>");

            sb.AppendLine("      <p style=\"margin-top: 20px;\"><strong>שים לב:</strong> יש לטפל בהזמנה זו בהקדם האפשרי.</p>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"footer\">");
            sb.AppendLine("      <p>© 2023 מערכת הזמנות תלבושות. כל הזכויות שמורות.</p>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}