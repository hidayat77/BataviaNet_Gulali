using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class _Default_Login : System.Web.UI.Page
{
    protected string link_get { get { return App.GetStr(this, "lnk"); } }
    protected string cklink { get { return App.GetStr(this, "cklink"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable content = Db.Rs("Select General_Content from TBL_General where General_Area = 'Logo'");
            logo.Text = "<img src=\"/assets/images/general/" + content.Rows[0]["General_Content"].ToString() + "\" alt=\"logo gulali forgot password\" style=\"width: 185px; height: auto;\" />";

            if (Cf.StrSql(App.UserID) != "" && App.Username != "")
            {
                string path = "";
                if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path = Param.Path_User; }
                Response.Redirect(path + "dashboard/");
            }
            if (!string.IsNullOrEmpty(link_get))
            {
                check_link();
            }
            StringBuilder x = new StringBuilder();
            if (cklink.Equals("54920"))
            {
                div_reset.Visible = false;
                div_notif_email.Visible = false;
                x.Append("<div class=\"m-t-40 card-box\">"
                           + "<div class=\"text-center\">"
                               + "<h4 class=\"text-uppercase font-bold m-b-0\">Link Anda Kadaluarsa</h4>"
                           + "</div>"
                           + "<div class=\"panel-body text-center\">"
                               + "<i class=\"fa fa-clock-o\" aria-hidden=\"true\" style=\"font-size:50px; color:#4fa4f8;\"></i>"
                               + "<p class=\"text-muted font-13 m-t-20\">Silahkan<a href=\"FPassword.aspx\" class=\"text-primary m-l-5\"><b>Register</b></a> kembali, atau hubungi Admin</p>"
                           + "</div>"
                       + "</div>");

                alert.Text = x.ToString();
            }
            else if (cklink.Equals("86920"))
            {
                div_reset.Visible = false;
                div_notif_email.Visible = false;
                x.Append("<div class=\"m-t-40 card-box\">"
                           + "<div class=\"text-center\">"
                               + "<h4 class=\"text-uppercase font-bold m-b-0\">Link Anda Telah Digunakan</h4>"
                           + "</div>"
                           + "<div class=\"panel-body text-center\">"
                               + "<i class=\"fa fa-check-square\" aria-hidden=\"true\" style=\"font-size:50px; color:#5fbeaa;\"></i>"
                               + "<p class=\"text-muted font-13 m-t-20\">Silahkan<a href=\"FPassword.aspx\" class=\"text-primary m-l-5\"><b>Register</b></a> kembali Untuk mendapatkan Link Baru</p>"
                           + "</div>"
                       + "</div>");

                alert.Text = x.ToString();
            }
            else if (cklink.Equals("62920"))
            {
                div_reset.Visible = false;
                div_notif_email.Visible = false;
                x.Append("<div class=\"m-t-40 card-box\">"
                           + "<div class=\"text-center\">"
                               + "<h4 class=\"text-uppercase font-bold m-b-0\">Link Tidak Ditemukan</h4>"
                           + "</div>"
                           + "<div class=\"panel-body text-center\">"
                               + "<i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\" style=\"font-size:50px; color:#f9c851;\"></i>"
                               + "<p class=\"text-muted font-13 m-t-20\">Silahkan<a href=\"FPassword.aspx\" class=\"text-primary m-l-5\"><b>Register</b></a> Kembali, atau hubungi Admin</p>"
                           + "</div>"
                       + "</div>");
                alert.Text = x.ToString();
            }
        }
    }

    protected void BtnSendEmail(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!Page.IsValid) { return; }
            else
            {
                DataTable cek_signup = Db.Rs("Select User_ID, Employee_ID, User_Name, User_Password from TBL_User where User_Name = '" + (Cf.StrSql(username.Text)).Trim() + "'");

                if (cek_signup.Rows.Count > 0)
                {
                    DateTime expiredDate = DateTime.Now.Date.AddDays(1);
                    string expiredDateStr = Convert.ToString(expiredDate.ToString("yyyy-MM-dd"));

                    string salt = "Gulali2017ForgetPassword";
                    string token = username.Text.Trim() + DateTime.Now + cek_signup.Rows[0]["User_Password"].ToString() + cek_signup.Rows[0]["User_id"].ToString() + salt;

                    DataTable rs_activation = Db.Rs("Select CONVERT(VARCHAR(32), HashBytes('MD5', '" + token + "'), 2) as MD5_activation_link");
                    string reset_activation_link = rs_activation.Rows[0]["MD5_activation_link"].ToString();

                    string update = "update TBL_User set User_ForgetPass_Link = '" + Cf.StrSql(reset_activation_link) + "', User_ForgetPass_Link_ExpiredDate = '" + expiredDateStr + "', User_ForgetPass_Link_Status = '1' where User_ID = '" + cek_signup.Rows[0]["User_ID"].ToString() + "'";

                    Db.Execute(update);

                    DataTable employee = Db.Rs("Select Employee_Company_Email from TBL_Employee where Employee_ID = '" + cek_signup.Rows[0]["Employee_ID"].ToString() + "' ");
                    string email_employee = employee.Rows[0]["Employee_Company_Email"].ToString();
                    label_email.Text = email_employee;

                    div_reset.Visible = false;
                    div_notif_email.Visible = true;
                    SendMail(Cf.StrSql(reset_activation_link), Cf.StrSql(email_employee), expiredDateStr);
                } else { Js.Alert(this, "Username Tidak Terdaftar"); }
            }
        }
    }

    protected void SendMail(string activation_link, string email, string expired_date)
    {
        try
        {
            DataTable RS1 = Db.Rs("Select Employee_Full_Name, Employee_Company_Email from TBL_Employee where Employee_Company_Email = '" + email + "'");
            string full_name = RS1.Rows[0]["Employee_Full_Name"].ToString();

            string txtTo = email;
            string txtSubject = "[Gulali - HRIS] – Reset Password";
            string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;font-size:14px;\">"
                                + "<tr style=\"background:none;\">"
                                    + "<td><font style=\"font-weight:bold;font-size:16px;\">Dear Mr/Mrs." + full_name + ",</font><br/></td>"
                                + "</tr>"
                                + "<tr style=\"background:none;\">"
                                    + "<td>Kami menerima pengajuan untuk reset password Anda untuk account " + email + " pada E-Recruitment Daihatsu Indonesia. <br/></td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td>Untuk mereset password Anda, silahkan klik link dibawah ini : <br/> <br/><td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td style=\"padding-left:30px;\">" + Param.linkSSL + "://" + HttpContext.Current.Request.Url.Authority + "/FPassword.aspx?lnk=" + activation_link + "<br>" + expired_date + " <br/><br/></td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td>Terimakasih. <br/> Panitia Rekrutmen & Seleksi <br/> PT. Astra Daihatu Motor</td>"
                                + "</tr>"
                                + "<tr><td>--------------------------</td></tr>"
                                + "<tr style=\"background:none;\">"
                                    + "<td><font  style=\"font-weight:bold;font-size:16px;\">Dear Mr/Mrs." + full_name + ",</font><br/></td>"
                                + "</tr>"
                                + "<tr style=\"background:none;\">"
                                    + "<td>We received  a request  to reset your password for your E-Recruitment Daihatsu Indonesia account " + email + " . <br/></td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td>To reset your password, please click the following link : <br/> <br/><td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td style=\"padding-left:30px;\">" + Param.linkSSL + "://" + HttpContext.Current.Request.Url.Authority + "/FPassword.aspx?lnk=" + activation_link + "<br>" + expired_date + " <br/><br/></td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td>Thank you. <br/> Recruitment and Selection Team <br/> PT. Astra Daihatu Motor</td>"
                                + "</tr>"
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
            try
            {
                smtp.Send(xx);
            }
            catch (Exception ex)
            { ex.Message.ToString(); }
        }
        catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error send email')", true); }
    }

    protected void check_link()
    {
        //email.Enabled = false;
        DataTable cek_link = Db.Rs("Select * from TBL_User where User_ForgetPass_Link = '" + Cf.StrSql(link_get) + "'");
        if (cek_link.Rows.Count > 0)
        {
            string status_link = cek_link.Rows[0]["User_ForgetPass_Link_status"].ToString();
            if (status_link.Equals("True"))
            {
                if (Convert.ToDateTime(cek_link.Rows[0]["User_ForgetPass_Link_ExpiredDate"].ToString()) >= DateTime.Now)
                {
                    div_forget_link.Visible = true;
                    div_reset.Visible = false;
                    username_link.Text = cek_link.Rows[0]["User_Name"].ToString();
                }
                else
                { //Link Kadaluarsa
                    Response.Redirect("FPassword.aspx?cklink=54920");
                }
            }
            else { Response.Redirect("FPassword.aspx?cklink=86920"); }
        }
        else
        { //Link Tidak Ada
            Response.Redirect("FPassword.aspx?cklink=62920");
        }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            if (div_reset.Visible == true)
            {
                x = Fv.isComplete(username) ? x : false;
            }
            if (div_forget_link.Visible == true)
            {
                x = Fv.isComplete(new_password) ? x : false;
                x = Fv.isComplete(confirm_new_password) ? x : false;
            }
            if (new_password.Text != confirm_new_password.Text)
            { x = false; note.Text = "<span style=\"color: red; font-weight:bold;\">Wrong Confirm New Password.</span>"; }
            return x;
        }
    }

    protected void BtnReset(object sender, EventArgs e)
    {
        if (valid)
        {
            string pass_new = Cf.StrSql(new_password.Text);
            string encryptionPassword = Param.encryptionPassword;
            string encrypted_new = Crypto.Encrypt(pass_new, encryptionPassword);

            if (new_password.Text == confirm_new_password.Text)
            {
                string update = "update TBL_User "
                    + "set User_LastModified = getdate(), User_Password = '" + encrypted_new + "', User_ForgetPass_Link_Status = '0' where User_ID = '2'";
                Db.Execute(update);

                this.Session.Abandon();
                string path = "";
                if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path = Param.Path_User; }
                Response.Redirect(path + "dashboard/");
            }
            else { Js.Alert(this, "Invalid New Password or Confirm Password!"); }
        }
    }

    protected void Back_Click(object sender, EventArgs e) { Response.Redirect("/gulali/sign-up/"); }
}