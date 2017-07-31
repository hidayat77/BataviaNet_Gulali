using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;

public partial class _user_kalender_department_cuti_detail : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string n { get { return App.GetStr(this, "n"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Cuti", "view");
        if (!IsPostBack)
        {
            link_back.Text = "<a href=\"" + Param.Path_Admin + "kalender/cuti/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

            DataTable exist = Db.Rs("Select Employee_ID from TBL_User where User_ID = '" + App.UserID + "' and Employee_ID = " + App.Employee_ID + "");
            if (exist.Rows.Count > 0)
            {
                DataTable exist_leave = Db.Rs("Select Leave_ID from TBL_Leave where Employee_ID = " + id + "");
                if (exist_leave.Rows.Count > 0)
                {
                    if (n == "1") { lable.Text = "<font style=\"color:red;\">Mail was not Sent</font>"; } else { lable.Text = ""; }
                    preview("1", pagesum.Text);
                }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }


    public void preview(string num, string sum)
    {
        DataTable rs = Db.Rs("select e.Employee_Full_Name, e.Employee_ID, Leave_ID, Leave_StatusLeave, Type_Desc, convert(varchar,Leave_RequestDate_HalfDay,107) as Leave_RequestDate_HalfDay, convert(varchar,Leave_RequestDateFrom,107) as Leave_RequestDateFrom, convert(varchar,Leave_RequestDateTo,107) as Leave_RequestDateTo, Leave_Half from TBL_Leave l join TBL_Employee e on l.Employee_ID = e.Employee_ID join TBL_Leave_Type t on l.Type_ID = t.Type_ID where l.Employee_ID = '" + id + "' and (Leave_StatusLeave = '4' or Leave_StatusLeave not in ('0', '5')) order by Leave_ID desc");

        if (rs.Rows.Count > 0)
        {
            employee_name.Text = "(" + rs.Rows[0]["Employee_Full_Name"].ToString() + ")";
            StringBuilder x = new StringBuilder();
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                        + "<thead>"
                            + " <tr role=\"row\">"
                                + "<th style=\"text-align:center;\">No</th>"
                                + "<th>Type of Leave</th>"
                                + "<th>Periode</th>"
                                + "<th style=\"text-align:center;\">Status</th>"
                            + "</tr>"
                        + "</thead><tbody>");
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0") { pagenum.Text = num; }
                else { pagenum.Text = of.ToString(); }
                pagesum.Text = sum;
            }

            int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
            int per = Cf.Int(pagesum.Text);
            int ke = Cf.Int(pagenum.Text);
            c = ke - 1;
            d = c * per;
            ea = ke * per;

            count_page.Text = of.ToString();
            if (d < rs.Rows.Count) { dari = d + 1; if (d == 0) dari = 1; }
            if (ea < rs.Rows.Count) { sampai = ea; }
            else { sampai = rs.Rows.Count; }

            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {
                    string id_employee = rs.Rows[i]["Employee_ID"].ToString();
                    string leave_id = rs.Rows[i]["Leave_ID"].ToString();

                    DataTable decision_spv = Db.Rs("Select convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'SPV' and Leave_ID = " + leave_id + " ");
                    DataTable decision_lm = Db.Rs("Select  convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'LM' and Leave_ID = " + leave_id + " ");

                    string classes = "odd", status = "", color = "", style = "", decision_full_name = "", decision_log_date = "", decision_jam = "";
                    if (i % 2 == 0) { classes = "even"; }
                    if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "4") { style = "style=\"color:#FF0000;font-weight:bold;\""; }
                    x.Append("<tr class=\"" + classes + "\" " + style + ">"
                                + "<td style=\"text-align:center;width:5%;\">" + (i + 1).ToString() + "</td>"
                                + "<td style=\"width:15%\">" + rs.Rows[i]["Type_Desc"].ToString() + "</td>");
                    if (rs.Rows[i]["Leave_Half"].ToString() != "0")
                    {
                        string day = "";
                        if (rs.Rows[i]["Leave_Half"].ToString() == "1") { day += "Pagi"; } else if (rs.Rows[i]["Leave_Half"].ToString() == "2") { day += "Siang"; }
                        x.Append("<td style=\"width:20%\">" + rs.Rows[i]["Leave_RequestDate_HalfDay"].ToString() + " - " + day +
                        "<td style=\"width:35%;text-align:center;\"><a href=\"detail-leave/?id=" + id + "&ln=" + leave_id + "\" >");
                    }
                    else
                    {
                        x.Append("<td style=\"width:20%\">" + rs.Rows[i]["Leave_RequestDateFrom"].ToString() + " -  " + rs.Rows[i]["Leave_RequestDateTo"].ToString() + "</td>"
                            + "<td style=\"width:35%;text-align:center;\"><a style=\"\" href=\"detail-leave/?id=" + id + "&ln=" + leave_id + "\">");
                    }

                    if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "0")
                    {
                        status = "Pending"; color = "";
                        x.Append("Pending");
                    }
                    else
                    {
                        if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2") { status = "Reviewed by HRD "; color = "lime"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "3") { status = "Reject by HRD "; color = "red"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "4") { status = "Approved by Supervisor "; color = "green"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "5") { status = "Reject by Supervisor "; color = "red"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "6") { status = "Canceled by HRD "; color = "lime"; }

                        if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "3" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "6")
                        {
                            decision_full_name = decision_lm.Rows[0]["Employee_Full_Name"].ToString();
                            decision_log_date = decision_lm.Rows[0]["Log_Date"].ToString();
                            decision_jam = decision_lm.Rows[0]["jam"].ToString();
                        }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "4" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "5")
                        {
                            decision_full_name = decision_spv.Rows[0]["Employee_Full_Name"].ToString();
                            decision_log_date = decision_spv.Rows[0]["Log_Date"].ToString();
                            decision_jam = decision_spv.Rows[0]["jam"].ToString();
                        }

                        x.Append("<font style=\"color:" + color + ";\">" + status + "(" + decision_full_name + " - " + decision_log_date + " - " + decision_jam + ")</font>");
                    }
                    x.Append("</a></td></tr>");

                }
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {
            lable.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
        }
    }

    protected void previous2_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        preview(a.ToString(), pagesum.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        preview("0", pagesum.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text);
    }
}