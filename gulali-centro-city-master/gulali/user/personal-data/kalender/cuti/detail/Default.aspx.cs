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
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string notif { get { return App.GetStr(this, "notif"); } }
    protected string rep { get { return App.GetStr(this, "rep"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Cuti", "view");

        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id)))
                {
                    DataTable exist = Db.Rs("select * from TBL_Leave where Leave_ID = '" + id + "' and Employee_ID = '" + App.Employee_ID + "'");
                    if (exist.Rows.Count > 0)
                    {
                        if (notif != null && notif != "")
                        {
                            DataTable check_employee_login = Db.Rs("Select Notif_Employee_ID from TBL_Notification where Notif_ID = '" + Cf.StrSql(notif) + "'");
                            if (check_employee_login.Rows[0]["Notif_Employee_ID"].ToString() == App.Employee_ID)
                            {
                                Db.Execute("update TBL_Notification set Notif_StatusPage = '1' where Notif_ID = '" + notif + "'");
                                detail_leave();
                            }
                            else { Response.Redirect("/gulali/page/404.aspx"); }
                        }
                        else { detail_leave(); }
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                    detail_bio_leave();
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
            DataTable rs = Db.Rs("select he.Employee_ID, Employee_Full_Name, Employee_Sum_LeaveBalance, Department_Name, Division_Name, Employee_Photo from TBL_Employee he left join TBL_Department hdep on he.Department_ID = hdep.Department_ID left join TBL_Division hdiv on he.Division_ID = hdiv.Division_ID  where Employee_ID = '" + App.Employee_ID + "'");
            full_name.Text = rs.Rows[0]["Employee_Full_Name"].ToString();
            department.Text = rs.Rows[0]["Department_Name"].ToString();
            division.Text = rs.Rows[0]["Division_Name"].ToString();
            leave_balanced.Text = rs.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
        }
        catch (Exception) { }
    }

    protected void detail_leave()
    {
        ddl_type_prev();
        ddl_replacement_prev();

        DataTable rs = Db.Rs("Select l.flag, CONVERT(VARCHAR(11),l.Leave_RequestDate_HalfDay ,105) as Leave_RequestDate_HalfDayView, CONVERT(VARCHAR(11),l.Leave_RequestDateFrom ,105) as Leave_RequestDateFrom, CONVERT(VARCHAR(11),l.Leave_RequestDate_HalfDay ,105) as Leave_RequestDate_HalfDay, CONVERT(VARCHAR(11),l.Leave_RequestDateTo ,105) as Leave_RequestDateTo,e.Employee_Full_Name, Leave_Remarks, Leave_Half, Leave_Form, Leave_StatusLeave, Type_ID, e.Employee_ID, ReplacementStaff_id from TBL_Leave l join TBL_Employee e on l.Employee_ID=e.Employee_ID where Leave_ID = " + id + "");

        if (rs.Rows.Count > 0)
        {
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

            string medical_doc = "<b>-</b>";
            if (!string.IsNullOrEmpty(rs.Rows[0]["Leave_Form"].ToString()))
            {
                medical_doc = "<i class=\"fa fa-folder-open-o\"></i>  <a href=\"/file-upload/leave/" + rs.Rows[0]["Leave_Form"].ToString() + "\">" + rs.Rows[0]["Leave_Form"].ToString() + "</a>";
            }
            medical.Text = medical_doc;

            div_upload_doc.Visible = true;

            DataTable log_leave_mon = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = '" + Cf.StrSql(id) + "' and Log_DecisionUserStatus = 'LM' and Log_StatusLeave != '2'");
            DataTable log_leave_spv = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = '" + Cf.StrSql(id) + "' and Log_DecisionUserStatus = 'SPV'");
            DataTable log_leave_cancel = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = " + Cf.StrSql(id) + " and Log_DecisionUserStatus = 'LM' and Log_StatusLeave = '2'");
            DataTable rsd = Db.Rs("Select l.Log_StatusLeave, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Leave_Log l join TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Leave_ID = " + Cf.StrSql(id) + " and Log_DecisionUserStatus = 'LM'");

            string status = "";
            if (log_leave_mon.Rows.Count > 0)
            {
                if (log_leave_mon.Rows[0]["Log_StatusLeave"].ToString() == "0") { status = "<font style=\"color:#10c469;\">Reviewed by "; } else if (log_leave_mon.Rows[0]["Log_StatusLeave"].ToString() == "1") { status = "<font style=\"color:red;\">Rejected by "; } else { status = "<font style=\"color:red;\">Canceled by "; }

                if (!string.IsNullOrEmpty(log_leave_mon.Rows[0]["Log_DecisionUserStatus"].ToString()))
                {
                    div_lm.Visible = true;
                    div_lm_remarks.Visible = true;
                    h4_lm.Visible = true;
                    h4_lm_remarks.Visible = true;
                    lm.Text = "<b>" + status + log_leave_mon.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    lm_remarks.Text = log_leave_mon.Rows[0]["Log_Remarks"].ToString();
                }
            }

            if (log_leave_spv.Rows.Count > 0)
            {
                if (log_leave_spv.Rows[0]["Log_StatusLeave"].ToString() == "0") { status = "<font style=\"color:#10c469;\">Approved by "; } else { status = "<font style=\"color:red;\">Rejected by "; }

                if (!string.IsNullOrEmpty(log_leave_spv.Rows[0]["Log_DecisionUserStatus"].ToString()))
                {
                    div_spv.Visible = true;
                    div_spv_remarks.Visible = true;
                    h4_spv.Visible = true;
                    h4_spv_remarks.Visible = true;
                    spv.Text = "<b>" + status + log_leave_spv.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    spv_remarks.Text = log_leave_spv.Rows[0]["Log_Remarks"].ToString();
                }
            }
            if (rs.Rows[0]["Leave_StatusLeave"].ToString() == "6")
            {
                if (log_leave_spv.Rows.Count == 0)
                {
                    div_cancel.Visible = false;
                    div_cancel_remarks.Visible = false;
                    h4_cancel.Visible = false;
                    h4_cancel_remarks.Visible = false;
                    div_lm.Visible = true;
                    div_lm_remarks.Visible = true;
                    h4_lm.Visible = true;
                    h4_lm_remarks.Visible = true;
                    lm.Text = "<b>Canceled by " + rsd.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    lm_remarks.Text = rsd.Rows[0]["Log_Remarks"].ToString();
                }
                else
                {
                    div_cancel.Visible = true;
                    div_cancel_remarks.Visible = true;
                    h4_cancel.Visible = true;
                    h4_cancel_remarks.Visible = true;
                    cancel.Text = "<b>Canceled by " + log_leave_cancel.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    cancel_remarks.Text = log_leave_cancel.Rows[0]["Log_Remarks"].ToString();
                }
            }

            ddl_type.SelectedValue = rs.Rows[0]["Type_ID"].ToString();
            ddl_replacement_staff.SelectedValue = rs.Rows[0]["ReplacementStaff_id"].ToString();
        }
    }

    protected void BtnCancel(object sender, EventArgs e)
    {
        Response.Redirect( Param.Path_User + "personal-data/kalender/cuti/");
    }
}