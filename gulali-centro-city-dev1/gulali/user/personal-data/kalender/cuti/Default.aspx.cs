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

public partial class _user_kalender_cuti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Cuti", "view");

        tab_cuti.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/cuti/\">Cuti</a>";
        tab_lembur.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/lembur/\">Lembur</a>";
        tab_kehadiran.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/kehadiran/\">Kehadiran</a>";

        preview("1", pagesum.Text);
    }

    public void preview(string num, string sum)
    {
        DataTable rs = Db.Rs("select convert(varchar,Leave_RequestDateFrom,107) as Leave_RequestDateFrom, convert(varchar,Leave_RequestDateTo,107) as Leave_RequestDateTo, convert(varchar,Leave_RequestDate_HalfDay,107) as Leave_RequestDate_HalfDay, Type_Desc, Employee_ID, Leave_ID, Leave_Half, Leave_StatusLeave from TBL_Leave a join TBL_Leave_Type b on a.Type_ID = b.Type_ID where Employee_ID = '" + App.Employee_ID + "' order by Leave_ID desc");
        if (rs.Rows.Count > 0)
        {
            StringBuilder x = new StringBuilder();
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                        + "<thead>"
                            + " <tr role=\"row\">"
                                + "<th style=\"text-align:center;\">No</th>"
                                + "<th>Type of Leave</th>"
                                + "<th>Periode</th>"
                                + "<th style=\"text-align:center;\">Status</th>"
                            + "</tr>"
                        + "</thead>"
                    + "<tbody>");


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
                    string id = rs.Rows[i]["Employee_ID"].ToString();
                    string leave_id = rs.Rows[i]["Leave_ID"].ToString();

                    DataTable decision_spv = Db.Rs("Select  convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'SPV' and Leave_ID = " + leave_id + " ");
                    DataTable decision_lm = Db.Rs("Select convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'LM' and Leave_ID = " + leave_id + " ");

                    string classes = "odd", status = "", color = "", decision_full_name = "", decision_log_date = "", decision_jam = "";
                    if (i % 2 == 0) { classes = "even"; }

                    x.Append("<tr class=\"" + classes + "\">"
                                + "<td style=\"text-align:center;width:5%\">" + (i + 1).ToString() + "</td>"
                                + "<td style=\"width:15%\">" + rs.Rows[i]["Type_Desc"].ToString() + "</td>");
                    if (rs.Rows[i]["Leave_Half"].ToString() != "0")
                    {
                        string day = "";
                        if (rs.Rows[i]["Leave_Half"].ToString() == "1") { day += "Pagi"; } else if (rs.Rows[i]["Leave_Half"].ToString() == "2") { day += "Siang"; }
                        x.Append("<td style=\"width:20%\">" + rs.Rows[i]["Leave_RequestDate_HalfDay"].ToString() + " - " + day +
                        "<td style=\"width:35%;text-align:center;\"><a href=\"detail?id=" + leave_id + "\">");
                    }
                    else
                    {
                        x.Append("<td style=\"width:20%\">" + rs.Rows[i]["Leave_RequestDateFrom"].ToString() + " -  " + rs.Rows[i]["Leave_RequestDateTo"].ToString() + "</td>"
                            + "<td style=\"width:35%;text-align:center;\"><a style=\"\" href=\"detail?id=" + leave_id + "\">");
                    }

                    if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "0")
                    {
                        status = "Pending"; color = "";
                        x.Append("Pending");
                    }
                    else
                    {
                        if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2") { status = "Reviewed by HRD"; color = "lime"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "3") { status = "Reject by HRD"; color = "red"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "4") { status = "Approved by Supervisor"; color = "green"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "5") { status = "Reject by Supervisor"; color = "red"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "6") { status = "Canceled by HRD"; color = "lime"; }

                        if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "3" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "6")
                        {
                            if (decision_lm.Rows.Count > 0)
                            {
                                decision_full_name = decision_lm.Rows[0]["Employee_Full_Name"].ToString();
                                decision_log_date = decision_lm.Rows[0]["Log_Date"].ToString();
                                decision_jam = decision_lm.Rows[0]["jam"].ToString();
                            }
                        }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "4" || rs.Rows[i]["Leave_StatusLeave"].ToString() == "5")
                        {
                            if (decision_spv.Rows.Count > 0)
                            {
                                decision_full_name = decision_spv.Rows[0]["Employee_Full_Name"].ToString();
                                decision_log_date = decision_spv.Rows[0]["Log_Date"].ToString();
                                decision_jam = decision_spv.Rows[0]["jam"].ToString();
                            }
                        }

                        x.Append("<font style=\"color:" + color + ";\">" + status + " (" + decision_full_name + " - " + decision_log_date + " - " + decision_jam + ")</font>");
                    }
                    x.Append("</a></td></tr>");
                }
            }

            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else { note.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>"; }
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