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
    protected string user { get { return App.GetStr(this, "user"); } }
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string cn { get { return App.GetStr(this, "cn"); } }
    protected string notif { get { return App.GetStr(this, "notif"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Cuti", "view");

        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(cn), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(cn)))
                {
                    DataTable exist_leave = Db.Rs("Select Leave_ID from TBL_Leave where Leave_ID = " + cn + "");
                    if (exist_leave.Rows.Count > 0)
                    {
                        if (notif != null && notif != "")
                        {
                            DataTable check_employee_login = Db.Rs("Select Notif_Employee_ID from TBL_Notification where Notif_ID = '" + Cf.StrSql(notif) + "'");
                            if (check_employee_login.Rows[0]["Notif_Employee_ID"].ToString() == App.Employee_ID)
                            {
                                string update = "update TBL_Notification set Notif_StatusPage = '1' where Notif_ID = '" + notif + "'";
                                Db.Execute(update);
                            }
                        }
                        detail_leave();
                        detail_bio_leave();
                    }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void ddl_type_prev()
    {
        DataTable rsa = Db.Rs("select Type_Desc, Type_ID from TBL_Leave_Type order by Type_Order asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_type.Items.Add(new ListItem(rsa.Rows[i]["Type_Desc"].ToString(), rsa.Rows[i]["Type_ID"].ToString()));
            }
        }
    }

    protected void ddl_replacement_prev()
    {
        DataTable rsa = Db.Rs("select Employee_Full_Name, Employee_ID from TBL_Employee order by Employee_Full_Name asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_replacement_staff.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }

    protected void detail_bio_leave()
    {
        try
        {
            DataTable rs = Db.Rs("select he.Employee_ID, Employee_Full_Name, Employee_Sum_LeaveBalance, Department_Name, Division_Name, Employee_Photo from TBL_Employee he left join TBL_Department hdep on he.Department_ID = hdep.Department_ID left join TBL_Division hdiv on he.Division_ID = hdiv.Division_ID  where Employee_ID = '" + Cf.StrSql(id) + "'");
            full_name.Text = rs.Rows[0]["Employee_Full_Name"].ToString();
            department.Text = rs.Rows[0]["Department_Name"].ToString();
            division.Text = rs.Rows[0]["Division_Name"].ToString();
            leave_balanced.Text = rs.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
        }
        catch (Exception) { }
    }

    protected void detail_leave()
    {
        DataTable rs = Db.Rs("Select ReplacementStaff_id, Leave_Half, CONVERT(VARCHAR(11),hl.Leave_RequestDate_HalfDay ,105) as Leave_RequestDate_HalfDayView, CONVERT(VARCHAR(11),hl.Leave_RequestDateFrom ,105) as Leave_RequestDateFrom, CONVERT(VARCHAR(11),hl.Leave_RequestDateTo ,105) as Leave_RequestDateTo, hl.Leave_StatusLeave, hl.Type_ID, e.Employee_Full_Name, hl.Leave_Form, hl.Leave_Remarks from TBL_Leave hl join TBL_Employee e on hl.Employee_ID = e.Employee_ID where hl.Leave_ID = " + cn + "");

        if (rs.Rows.Count > 0)
        {
            ddl_type_prev();
            ddl_replacement_prev();

            ddl_replacement_staff.SelectedValue = rs.Rows[0]["ReplacementStaff_id"].ToString();
            txtremarks.Text = rs.Rows[0]["Leave_Remarks"].ToString();

            if (rs.Rows[0]["Leave_Half"].ToString() != "0")
            {
                div_date.Visible = false;
                div_date_cb.Visible = true;
                cbhalf.Checked = true;
                rbpagi.Visible = true;
                rbsiang.Visible = true;
                lbpagi.Visible = true;
                rbsiang.Visible = true;
                lbsiang.Visible = true;
                to_cb.Text = rs.Rows[0]["Leave_RequestDate_HalfDayView"].ToString();

                if (rs.Rows[0]["Leave_Half"].ToString() == "1") { rbpagi.Checked = true; rbsiang.Checked = false; }
                else { rbpagi.Checked = false; rbsiang.Checked = true; }
            }
            else
            {
                from.Text = rs.Rows[0]["Leave_RequestDateFrom"].ToString();
                to.Text = rs.Rows[0]["Leave_RequestDateTo"].ToString();
            }
            ddl_type.SelectedValue = rs.Rows[0]["Type_ID"].ToString();

            string medical_doc = "<b>-</b>";
            if (!string.IsNullOrEmpty(rs.Rows[0]["Leave_Form"].ToString()))
            {
                div_upload_doc.Visible = true;
                medical_doc = "<i class=\"fa fa-folder-open-o\"></i>  <a href=\"/file-upload/leave/" + rs.Rows[0]["Leave_Form"].ToString() + "\">" + rs.Rows[0]["Leave_Form"].ToString() + "</a>";
            }
            medical.Text = medical_doc;

            DataTable rsb = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = " + cn + " and Log_DecisionUserStatus = 'SPV' and Log_StatusLeave != '2'");
            DataTable rsc = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = " + cn + " and Log_DecisionUserStatus = 'SPV'");
            DataTable rsd = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = " + cn + " and Log_DecisionUserStatus = 'LM'");

            string status = "";
            if (rsb.Rows.Count > 0)
            {
                if (rsb.Rows[0]["Log_StatusLeave"].ToString() == "0") { status = "<font style=\"color:green;\">Approved by "; } else if (rsb.Rows[0]["Log_StatusLeave"].ToString() == "1") { status = "<font style=\"color:red;\">Rejected by "; } else { status = "<font style=\"color:red;\">Canceled by "; }

                if (!string.IsNullOrEmpty(rsb.Rows[0]["Log_DecisionUserStatus"].ToString()))
                {
                    div_spv.Visible = true;
                    div_spv_remarks.Visible = true;
                    h4_spv.Visible = true;
                    h4_spv_remarks.Visible = true;
                    spv.Text = "<b>" + status + rsb.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    spv_remarks.Text = rsb.Rows[0]["Log_Remarks"].ToString();
                }
            }
            else
            {
                div_approve.Visible = true;
                div_reject.Visible = true;
                div_spvremarks.Visible = true;
            }

            if (rsd.Rows.Count > 0)
            {
                string wording = "";
                if (rsd.Rows[0]["Log_StatusLeave"].ToString() == "0") { wording = "<font style=\"color:green;\">Reviewed by "; } else if (rsd.Rows[0]["Log_StatusLeave"].ToString() == "1") { wording = "<font style=\"color:red;\">Rejected by "; } else { wording = "<font style=\"color:red;\">Canceled"; }

                div_lm.Visible = true;
                div_lm_remarks.Visible = true;
                h4_lm.Visible = true;
                h4_lm_remarks.Visible = true;
                lm.Text = "<b>" + wording + rsd.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                lm_remarks.Text = rsd.Rows[0]["Log_Remarks"].ToString();
            }
        }
    }

    protected void BtnApprove(object sender, EventArgs e)
    {
        //insert ke table logLeave -> Ket : untuk history leave
        string insert = "insert into TBL_Leave_Log (Log_DecisionBy, Log_DecisionUserStatus, Log_Remarks, Log_DateCreate, Leave_ID, Log_StatusLeave)"
                + "values('" + Cf.StrSql(App.Employee_ID) + "', 'SPV',  '" + Cf.StrSql(spvremarks.Text) + "' , getdate()," + cn + ", '0')";
        Db.Execute(insert);

        //update status -> Approve by spv

        string update = "update TBL_Leave set Leave_StatusLeave = 4 where Leave_ID = " + cn + "";
        Db.Execute(update);

        //insert notif ke user yang melakukan create leave request
        DataTable select_notif = Db.Rs("Select l.Employee_ID, e.Employee_Full_Name, e.Employee_Company_Email from TBL_Leave l join TBL_Employee e on l.Employee_ID = e.Employee_ID where Leave_ID = '" + cn + "'");
        DataTable check_max_notif = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");
        string insert_notif = "insert into TBL_Notification (Notif_Link, Notif_Employee_ID,  Notif_StatusPage, Notif_DateCreate, Notif_UserCreate, Notif_Remarks, Leave_ID)"
                        + "values('" + Param.Path_User + "personal-data/kalender/cuti/detail/?id=" + cn + "&notif=" + check_max_notif.Rows[0]["maksi"].ToString() + "', '" + select_notif.Rows[0]["Employee_ID"].ToString() + "', '0', getdate(), '" + App.Employee_ID + "', 'Approve Leave Request (Supervisor -> User)','" + cn + "')";
        Db.Execute(insert_notif);


        DataTable spv = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = '" + Cf.StrSql(App.Employee_ID) + "'");
        DataTable cekleavemonitor = Db.Rs("Select u.Employee_ID,e.Employee_Company_Email from TBL_User u join TBL_Employee e on u.Employee_ID=e.Employee_ID join TBL_Role hr on u.Role_ID = hr.Role_ID where User_IsAdmin = '2' or e.Employee_ID = 1");
        DataTable check_max_notif2 = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");

        int leavemonitor = 0;
        for (int a = 0; a < cekleavemonitor.Rows.Count; a++)
        {
            int max = Convert.ToInt32(check_max_notif2.Rows[0]["maksi"].ToString()) + a;

            leavemonitor = Convert.ToInt32(cekleavemonitor.Rows[a]["Employee_ID"]);
            //insert notif ke user dengan user_id = leave monitor ke table TBL_Notif
            string insert_notif2 = "insert into TBL_Notification (Notif_Link, Notif_Status, Notif_StatusPage, Notif_DateCreate, Notif_UserCreate, Notif_Remarks, Notif_Employee_ID, Leave_ID)"
                            + "values('" + Param.Path_Admin + "kalender/cuti/detail/detail-leave/?id=" + select_notif.Rows[0]["Employee_ID"].ToString() + "&ln=" + cn + "&notif=" + max + "', '0', '0', getdate(), '" + App.Employee_ID + "', 'Approve Leave Request (Supervisor -> Leave Monitor)','" + leavemonitor + "','" + cn + "')";
            Db.Execute(insert_notif2);
        }

        //send mail
        try
        {
            string txtTo = select_notif.Rows[0]["Employee_Company_Email"].ToString();
            string txtSubject = "[HRIS] - Leave Request";
            string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                + "<tr><td>" + spv.Rows[0]["Employee_Full_Name"].ToString() + " Has Been Approved " + select_notif.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request</td></tr>"
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
            try { smtp.Send(xx); }
            catch (Exception ex) { ex.Message.ToString(); }

        }
        catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error send email')", true); }

        //kirim email ke leave monitor
        for (int a = 0; a < cekleavemonitor.Rows.Count; a++)
        {
            try
            {
                string txtTo = cekleavemonitor.Rows[0]["Employee_Company_Email"].ToString();
                string txtSubject = "[HRIS] - Leave Request";
                string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                    + "<tr><td>" + spv.Rows[0]["Employee_Full_Name"].ToString() + " Has Been Approved " + select_notif.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request</td></tr>"
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
                try { smtp.Send(xx); }
                catch (Exception ex) { ex.Message.ToString(); }

            }
            catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error send email')", true); }
        }
        Response.Redirect(Param.Path_User + "department/kalender/cuti/detail/?id=" + Cf.StrSql(id) + "");
    }

    protected void BtnReject(object sender, EventArgs e)
    {
        //insert ke table logLeave -> Ket : untuk history leave
        string insert = "insert into TBL_Leave_Log (Log_DecisionBy, Log_DecisionUserStatus, Log_Remarks, Log_DateCreate, Leave_ID, Log_StatusLeave)"
                + "values('" + Cf.StrSql(App.Employee_ID) + "', 'SPV',  '" + Cf.StrSql(spvremarks.Text) + "' , getdate()," + cn + ", '1')";
        Db.Execute(insert);

        //update status -> Reject by spv

        string update = "update TBL_Leave set Leave_StatusLeave = 5 where Leave_ID = " + cn + "";
        Db.Execute(update);

        //insert notif ke user yang melakukan create leave request
        DataTable select_notif = Db.Rs("Select l.Employee_ID, e.Employee_Company_Email from TBL_Leave l join TBL_Employee e on l.Employee_ID = e.Employee_ID where Leave_ID = '" + cn + "'");
        DataTable check_max_notif = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");
        string insert_notif = "insert into TBL_Notification (Notif_Link, Notif_Employee_ID,  Notif_StatusPage, Notif_DateCreate, Notif_UserCreate, Notif_Remarks, Leave_ID)"
                        + "values('" + Param.Path_User + "personal-data/kalender/cuti/detail/?id=" + cn + "&notif=" + check_max_notif.Rows[0]["maksi"].ToString() + "', '" + select_notif.Rows[0]["Employee_ID"].ToString() + "', '0', getdate(), '" + App.Employee_ID + "', 'Reject Leave Request (Supervisor -> User)','" + cn + "')";
        Db.Execute(insert_notif);

        //kirim email ke user
        DataTable spv = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = '" + Cf.StrSql(App.Employee_ID) + "'");

        try
        {
            //send mail
            string txtTo = select_notif.Rows[0]["Employee_Company_Email"].ToString();
            string txtSubject = "[HRIS] - Leave Request";
            string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                        + "<tr><td>" + spv.Rows[0]["Employee_Full_Name"].ToString() + " Has Been Rejected " + select_notif.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request</td></tr>"
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
            try { smtp.Send(xx); }
            catch (Exception ex) { ex.Message.ToString(); }

        }
        catch { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Error send email')", true); }

        Response.Redirect(Param.Path_User + "department/kalender/cuti/detail/?id=" + Cf.StrSql(id) + "");
    }

    private void hitung()
    {
        DataTable cektype = Db.Rs("Select Type_ID from TBL_Leave where Leave_ID= '" + cn + "'");
        if (cektype.Rows[0]["Type_ID"].ToString() == "1")
        {
            //hitung leave balance
            decimal annualapproved = 0;
            DataTable approval = Db.Rs("select Employee_LeaveBalance from TBL_Employee where Employee_ID = '" + id + "'");
            decimal annual = Convert.ToDecimal(approval.Rows[0]["Employee_LeaveBalance"]);
            DataTable cekleave = Db.Rs("select * from TBL_Leave where Employee_ID='" + id + "' and Leave_ID= '" + cn + "'");
            if (cekleave.Rows[0]["Leave_Half"].ToString() == "1")
            {
                annualapproved += 1 * (1 / 2);
            }
            else
            {
                annualapproved += 1;
            }

            annual = annual - annualapproved;
            string update = "Update TBL_Employee set Employee_LeaveBalance = '" + annual + "'";
            Db.Execute(update);

        }
        else if (cektype.Rows[0]["Type_ID"].ToString() == "8")
        {
            //sickleave
            DataTable sisasickleave = Db.Rs("select Employee_SickLeave from TBL_Employee where Employee_ID='" + id + "'");
            decimal sisa = Convert.ToDecimal(sisasickleave.Rows[0]["Employee_SickLeave"]);
            DataTable cekrequest = Db.Rs("select * from TBL_Leave where Employee_ID = " + id + " and Leave_ID = '" + cn + "'");
            decimal jumlahrequest = 0;
            if (cekrequest.Rows[0]["Leave_Half"].ToString() != "0")
            {
                jumlahrequest += 1 * (1 / 2);
            }
            else
            {
                jumlahrequest += 1;
            }
            sisa = sisa - jumlahrequest;

            if (sisa < 0)
            {
                DataTable cekannual = Db.Rs("select Employee_LeaveBalance from TBL_Employee where Employee_ID='" + id + "'");
                decimal leavebalance = Convert.ToDecimal(cekannual.Rows[0]["Employee_LeaveBalance"]);
                DataTable cekunpaid = Db.Rs("select Employee_UnpaidLeave from TBL_Employee where Employee_ID = '" + id + "'");
                decimal unpaid;
                if (string.IsNullOrEmpty(cekunpaid.Rows[0]["Employee_UnpaidLeave"].ToString()))
                {
                    unpaid = 0;
                }
                else
                {
                    unpaid = Convert.ToDecimal(cekunpaid.Rows[0]["Employee_UnpaidLeave"]);
                }
                if (leavebalance > 0)
                {
                    leavebalance = leavebalance + sisa;
                    if (leavebalance < 0)
                    {
                        unpaid = unpaid - leavebalance;
                        string update = "Update TBL_Employee set Employee_SickLeave='0', Employee_LeaveBalance ='0', Employee_UnpaidLeave='" + unpaid + "' where Employee_ID = '" + id + "'";
                        Db.Execute(update);
                    }
                    else
                    {
                        string update = "Update TBL_Employee set Employee_SickLeave='0', Employee_LeaveBalance ='" + leavebalance + "' where Employee_ID = '" + id + "'";
                        Db.Execute(update);
                    }
                }
                else if (leavebalance <= 0)
                {
                    unpaid = unpaid - sisa;
                    string update = "Update TBL_Employee set Employee_SickLeave='0', Employee_UnpaidLeave = '" + unpaid + "' where Employee_ID = '" + id + "'";
                    Db.Execute(update);
                }
            }
            else
            {
                string update = "Update TBL_Employee set Employee_SickLeave = '" + sisa + "' where Employee_ID = '" + id + "'";
                Db.Execute(update);
            }

        }
    }

    protected void BtnBack(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_User + "department/kalender/cuti/detail/?id=" + Cf.StrSql(id) + "");
    }
}