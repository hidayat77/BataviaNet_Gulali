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

public partial class _user_kalender_cuti_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Cuti", "insert");

        href_cancel.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/cuti/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack)
        {
            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            ddl_employee_prev();
            ddl_type_prev();
            detail_leave();
        }
    }

    protected void ddl_employee_prev()
    {
        DataTable rsa = Db.Rs("select Employee_Full_Name, Employee_ID from TBL_Employee where Employee_ID != 1 and Employee_ID != '" + App.Employee_ID + "'order by Employee_Full_Name asc");

        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_employee.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
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

    protected void detail_leave()
    {
        try
        {
            DataTable rs = Db.Rs("select he.Employee_ID, Employee_Full_Name, Employee_Sum_LeaveBalance, Department_Name, Division_Name, Employee_Photo from TBL_Employee he left join TBL_Department hdep on he.Department_ID = hdep.Department_ID left join TBL_Division hdiv on he.Division_ID = hdiv.Division_ID where Employee_ID = '" + App.Employee_ID + "'");

            full_name.Text = rs.Rows[0]["Employee_Full_Name"].ToString();
            department.Text = rs.Rows[0]["Department_Name"].ToString();
            division.Text = rs.Rows[0]["Division_Name"].ToString();
            leave_balanced.Text = rs.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
        }
        catch (Exception) { }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isTgl(from) ? x : false;
            if (to.Visible == true) { x = Fv.isTgl(to) ? x : false; }
            if (string.IsNullOrEmpty(from.Text) || (to.Visible == true && string.IsNullOrEmpty(to.Text)))
            {
                note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                + "<h4 class=\"page-title\">Lengkapi Data Anda!</h4>"
                            + "</div>";
            }
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!Page.IsValid) { return; }
            else
            {
                //convert date
                DateTime From = Convert.ToDateTime(from.Text);
                string From_Conv = From.ToString("yyyy-MM-dd").ToString();
                string file_med = "";
                if (medical.HasFile)
                {
                    string Folder = "leave/";
                    file_med = uploadFile(Folder, medical);
                }

                string halfday, insert = "";
                if (rbpagi.Checked) { halfday = "1"; } else { halfday = "2"; }

                if (cbhalf.Checked == true)
                {
                    insert = "insert into TBL_Leave(Type_ID, ReplacementStaff_id, Leave_StatusLeave, Leave_StatusPage, Leave_RequestDateCreate, Employee_ID, Leave_RequestDate_HalfDay, Leave_Half, Leave_RequestDateFrom, Leave_RequestDateTo, Leave_Form, flag, Leave_Remarks)"
                            + "values('" + ddl_type.SelectedValue + "', '" + ddl_employee.SelectedValue + "','0', '0', getdate(), '" + App.Employee_ID + "', '" + From_Conv + "', '" + halfday + "', '', '','" + file_med + "','0','" + Cf.StrSql(txtremarks.Text) + "')";
                    Db.Execute(insert);
                    notif_leave();
                }
                else
                {
                    DateTime To = Convert.ToDateTime(to.Text);
                    String To_Conv = To.ToString("yyyy-MM-dd").ToString();
                    if (From <= To)
                    {
                        insert = "insert into TBL_Leave(Leave_RequestDateFrom, ReplacementStaff_id, Leave_RequestDateTo, Type_ID, Leave_StatusLeave, Leave_StatusPage, Leave_RequestDateCreate, Employee_ID, Leave_RequestDate_HalfDay, Leave_Half, Leave_Form, flag, Leave_Remarks)"
                                + "values('" + From_Conv + "', '" + ddl_employee.SelectedValue + "' ,'" + To_Conv + "' , '" + ddl_type.SelectedValue + "', '0', '0', getdate(), '" + App.Employee_ID + "', '', '','" + file_med + "','0','" + Cf.StrSql(txtremarks.Text) + "')";
                        Db.Execute(insert);
                        notif_leave();
                    }
                    else { Js.Alert(this, "Invalid Periode of Leave Request!"); }
                }
            }
        }
    }

    protected void notif_leave()
    {
        DataTable check_max_notif = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");

        string notif;
        if (string.IsNullOrEmpty(check_max_notif.Rows[0]["maksi"].ToString())) { notif = "1"; }
        else { notif = check_max_notif.Rows[0]["maksi"].ToString(); }

        DataTable max_leave = Db.Rs("Select max(Leave_ID) as Leave_ID from TBL_Leave");

        string max_leave_get;
        if (string.IsNullOrEmpty(max_leave.Rows[0]["Leave_ID"].ToString())) { max_leave_get = "1"; }
        else { max_leave_get = max_leave.Rows[0]["Leave_ID"].ToString(); }

        //insert notif ke supervisor employee yang melakukan create leave request
        //select spv bersangkutan
        DataTable check_max_notif2 = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");
        DataTable spv = Db.Rs("select he.Employee_ID, he.Employee_Full_Name, he.Employee_DirectSpv from TBL_Leave hl join TBL_Employee he on hl.Employee_ID = he.Employee_ID where Leave_ID = '" + Cf.StrSql(max_leave_get) + "'");
        string insert_notif_spv = "insert into TBL_Notification (Notif_Link, Notif_Employee_ID,  Notif_StatusPage, Notif_DateCreate, Notif_UserCreate, Notif_Remarks, Leave_ID)"
                        + "values('" + Param.Path_User + "department/kalender/cuti/detail/detail-leave/?id=" + App.Employee_ID + "&cn=" + max_leave_get + "&notif=" + check_max_notif2.Rows[0]["maksi"].ToString() + "', '" + spv.Rows[0]["Employee_DirectSpv"].ToString() + "', '0', getdate(), '" + Cf.StrSql(App.Employee_ID) + "', 'Create Leave Request(User -> Supervisor)','" + max_leave_get + "')";
        Db.Execute(insert_notif_spv);

        try
        {
            //send mail
            DataTable rs2 = Db.Rs("select * from TBL_MailConfig where mail_config_id ='1'");
            DataTable emailSPV = Db.Rs("select Employee_CompanyEmail from TBL_Employee where Employee_ID = '" + spv.Rows[0]["Employee_DirectSpv"] + "' ");
            DataTable leavemonitor = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = '" + Cf.StrSql(App.Employee_ID) + "'");
            string txtFrom = rs2.Rows[0]["mail_config_from"].ToString();
            string txtSMTPServer = rs2.Rows[0]["mail_config_smtp"].ToString();
            string txtTo = emailSPV.Rows[0]["Employee_CompanyEmail"].ToString();
            string txtSubject = "Leave Request";
            string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                + "<tr><td>" + leavemonitor.Rows[0]["Employee_Full_Name"].ToString() + " Reviewed Leave Request(HRD -> Supervisor)</td></tr>"
                                + "<tr><td>" + Param.Domain + "/?id=1 </td></tr>"
                            + "</table>";

            string SMTPServer = txtSMTPServer;
            string SMTPUser = txtFrom;
            string SMTPPass = Param.SMTPPass;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SMTPServer);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(SMTPUser);
            mail.To.Add(txtTo);
            mail.Subject = txtSubject;
            mail.Body = txtBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPass);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception) { Response.Redirect(Param.Path_User + "personal-data/kalender/cuti/"); }

        Response.Redirect(Param.Path_User + "personal-data/kalender/cuti/");
    }

    protected void cbhalf_CheckedChanged(object sender, EventArgs e)
    {
        rbpagi.Checked = false; rbsiang.Checked = false;
        if (cbhalf.Checked == true)
        {
            rbpagi.Visible = true; rbsiang.Visible = true;
            lbpagi.Visible = true; lbsiang.Visible = true;
            to.Visible = false; imgto.Visible = false;

            to.Text = ""; rbpagi.Checked = true;
        }
        else
        {
            rbpagi.Visible = false; rbsiang.Visible = false;
            lbpagi.Visible = false; lbsiang.Visible = false;
            to.Visible = true; imgto.Visible = true;
        }
    }
    // SO upload image
    protected string uploadFile(string Folder, FileUpload img)
    {
        string upload_file = "";
        try
        {
            string filename = Path.GetFileName(img.FileName);
            string dir = Param.PathFile + Folder;
            bool status = true;

            while (status)
            {
                status = FileExists(filename, Server.MapPath("~/file-upload/" + Folder));
                if (status)
                    filename = getRandomFileName() + System.IO.Path.GetExtension(img.PostedFile.FileName);
            }

            upload_file = filename;
            string large = dir + filename;

            img.PostedFile.SaveAs(large);
        }
        catch (Exception ex)
        {
            Js.Alert(this, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
        }
        return upload_file;
    }

    protected string uploadIncResize(int h, int w, string Folder, FileUpload img, bool resize)
    {
        string upload_file = "";
        try
        {
            if (App.check_Filemime(img))
            {
                string filename = Path.GetFileName(img.FileName);
                string dir = Param.PathFile + Folder;
                bool status = true;

                while (status)
                {
                    status = FileExists(filename, Server.MapPath("~/FileUpload/" + Folder));
                    if (status)
                        filename = getRandomFileName() + System.IO.Path.GetExtension(img.PostedFile.FileName);
                }

                upload_file = filename;

                string temp = dir + "(photo)" + filename;
                string large = dir + filename;

                img.PostedFile.SaveAs(temp);
                if (resize)
                {
                    Imgh.Crop(temp, large, h, w);
                }
                else
                {
                    img.PostedFile.SaveAs(large);
                }
                File.Delete(temp);
                Js.Alert(this, "Upload status: upload successfull.");
            }
            else
                error_file.Text = "<span style=\"color:red;\">Upload status: Only DOC, IMAGE, ZIP files are allowed!</span>";
        }
        catch (Exception ex)
        {
            Js.Alert(this, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
        }
        return upload_file;
    }

    protected string getRandomFileName()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        int newFile = 0;
        newFile = rand.Next(1, 9999999);

        return newFile.ToString();
    }

    protected bool FileExists(string filename, string path)
    {
        FileInfo imageFile = new FileInfo(path + filename);
        bool fileStatus = imageFile.Exists;
        return fileStatus;
    }
    // EO upload file
}