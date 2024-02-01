using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bool isSent = false;
            try
            {
                MailMessage message = new MailMessage();
                MailAddressCollection ma = new MailAddressCollection();
                //message.To.Add(BussinessAccessLayer.LoginEmailID);
                //message.Bcc.Add(BussinessAccessLayer.LoginEmailID);



                foreach (var item in txtToEmail.Text.Trim().TrimEnd(',').Split(','))
                {
                    message.To.Add(item);
                }
                if (!string.IsNullOrEmpty(txtCCEmail.Text))
                {
                    foreach (var item in txtCCEmail.Text.Trim().TrimEnd(',').Split(','))
                    {
                        message.CC.Add(item);
                    }
                }



                if (string.IsNullOrEmpty(txtFromEmail.Text))
                {
                    ma.Add(new MailAddress("test@gmail.com", "Name: Test"));
                    message.From = ma[0];
                    message.Sender = ma[0];
                    message.ReplyToList.Add(ma[0]);
                }
                else
                {
                    ma.Add(txtFromEmail.Text);
                    message.From = new MailAddress(txtFromEmail.Text);
                    message.Sender = new MailAddress(txtFromEmail.Text);
                    message.ReplyToList.Add(txtFromEmail.Text);
                }

                message.Subject = txtSubject.Text;
                message.Body = txtBody.Text;
                message.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient(txtHost.Text, Convert.ToInt32(txtPort.Text));
                try
                {
                    smtp.Send(message);
                    isSent = true;
                    MessageBox.Show("Success");
                }
                catch (SmtpException se)
                {
                    MessageBox.Show("SMTP Exc" + se.ToString());
                    //BussinessLayer.LogAppErrors("MailSender", "SendSMTPEMail", "", se.ToString(), "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed" + ex.ToString());
            }
        }
    }
}
