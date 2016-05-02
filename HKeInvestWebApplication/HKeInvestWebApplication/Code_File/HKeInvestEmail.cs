using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System;

namespace HKeInvestWebApplication
{
    public class HKeInvestEmail
    {
        public void sendEmail(string address, string subject, string body)
        {
            // Create an instance of MailMessage named mail.
            MailMessage mail = new MailMessage();

            // Create an instance of SmtpClient named emailServer.
            // Set the mail server to use as "smtp.ust.hk".
            SmtpClient emailServer = new SmtpClient("smtp.cse.ust.hk");
            // Set the sender (From), receiver (To), subject and 
            // message body fields of the mail message.
            mail.From = new MailAddress("comp3111_team107@cse.ust.hk",
                 "HKeInvest(Team107)");
            mail.To.Add(address);
            mail.Subject = subject;
            mail.Body = body;
            // Specify login credentials

            try {
                // Send the message.
                emailServer.Send(mail);
                MessageBox.Show("Sent!");
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
            


        }
    }
}