using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class _user_kalender_cuti_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void BtnSendEmail(object sender, EventArgs e)
    {
        //try
        //{
        string txtTo = "devitapramulya@gmail.com";
        string txtSubject = "Leave Request";
        string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                    + "<tr><td>" + Param.Domain + "/?id=1 </td></tr>"
                                + "</table>";

        // Server Gmail
        string smtp_user = Param.SMTPUser;
        string smtp_pass = Param.SMTPPass;
        string email_user = txtTo;
        string subject = txtSubject;
        string body = txtBody;
        MailMessage xx = new MailMessage();
        xx.Body = body;
        xx.Sender = new MailAddress(smtp_user);
        xx.From = new MailAddress(smtp_user);
        xx.To.Add(email_user);
        xx.Subject = subject;
        xx.IsBodyHtml = true;
        var smtp = new System.Net.Mail.SmtpClient();
        {
            smtp.Host = Param.SMTPServer;
            smtp.Port = Convert.ToInt32(Param.SSL_Port);
            smtp.EnableSsl = Convert.ToBoolean(Param.Enable_SSL);
            smtp.UseDefaultCredentials = Convert.ToBoolean(Param.Default_Credentials);
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(smtp_user, smtp_pass);

            smtp.Timeout = Convert.ToInt32(Param.SMTP_Timeout);
        }
        smtp.Send(xx);
        //try { smtp.Send(xx); }
        //catch (Exception ex) { ex.Message.ToString(); }

        //} catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error send email')", true); } }
    }
}