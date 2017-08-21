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
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Globalization;

public partial class _admin_laporan_cuti : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string from_date { get { return App.GetStr(this, "from"); } }
    protected string to_date { get { return App.GetStr(this, "to"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Cuti", "view");
        if (!IsPostBack)
        {
            tab_riwayat.Text = "<a href=\"" + Param.Path_Admin + "laporan/cuti/\">Riwayat Cuti</a>";
            tab_sisa_cuti.Text = "<a href=\"" + Param.Path_Admin + "laporan/cuti/sisa-cuti/\">Sisa Cuti</a>";

            fill("1", pagesum.Text);
            ddl_directSpvPrev();

            if (!string.IsNullOrEmpty(id_get) || !string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
            {
                from.Text = Convert.ToDateTime(from_date).ToString("dd-MM-yyyy");
                to.Text = Convert.ToDateTime(to_date).ToString("dd-MM-yyyy");
                ddl_employee.SelectedValue = id_get;
            }
            else
            {
                from.Text = Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year);
                to.Text = Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year);
            } updatePanelTable.Update();
        }
    }

    protected void ddl_directSpvPrev()
    {
        DataTable rsa = Db.Rs("select * from TBL_Employee where Employee_ID != 1 order by Employee_Full_Name asc");

        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_employee.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }

    protected void BtnGo(object sender, EventArgs e)
    {
        DateTime From = Convert.ToDateTime(from.Text);
        DateTime To = Convert.ToDateTime(to.Text);
        string From_Conv = From.ToString("yyyy-MM-dd").ToString();
        string To_Conv = To.ToString("yyyy-MM-dd").ToString();

        Response.Redirect(Param.Path_Admin + "laporan/cuti/?id=" + ddl_employee.SelectedValue + "&from=" + From_Conv + "&to=" + To_Conv + "");
    }

    protected void fill(string num, string sum)
    {
        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Nama</th>"
                            + "<th>Type of Leave</th>"
                            + "<th>Periode</th>"
                            + "<th>Status</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        if (!string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
        {
            DateTime From = Convert.ToDateTime(from_date);
            DateTime To = Convert.ToDateTime(to_date);
            string From_Conv = From.ToString("yyyy-MM-dd").ToString();
            string To_Conv = To.ToString("yyyy-MM-dd").ToString();

            string new_filter;

            if (id_get == "0") { new_filter = ""; } else { new_filter = " and l.Employee_ID = " + id_get + " "; }

            DataTable rs = Db.Rs("select e.Employee_ID, e.Employee_Full_Name, l.Leave_ID, l.Type_ID, b.Type_Desc, l.Leave_Half, convert(varchar,l.Leave_RequestDateFrom,107) as Leave_RequestDateFrom, convert(varchar,l.Leave_RequestDateTo,107) as Leave_RequestDateTo, convert(varchar,l.Leave_RequestDate_HalfDay,107) as Leave_RequestDate_HalfDay, l.Leave_StatusLeave from TBL_Leave l join TBL_Leave_Type b on l.Type_ID = b.Type_ID join TBL_Employee e on l.Employee_ID = e.Employee_ID where ((Leave_RequestDateFrom between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59' and Leave_RequestDateTo between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') or Leave_RequestDate_HalfDay between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') and (l.Leave_StatusLeave = 2 or l.Leave_StatusLeave = 3) " + new_filter + " order by Leave_RequestDate_HalfDay, Leave_RequestDateFrom asc");

            lable.Text = "";
            table.Visible = true;
            if (rs.Rows.Count > 0)
            {
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
                if (d < rs.Rows.Count)
                { dari = d + 1; if (d == 0) dari = 1; }
                if (ea < rs.Rows.Count) { sampai = ea; }
                else { sampai = rs.Rows.Count; }
                if (dari > 0)
                {
                    for (int i = dari - 1; i < sampai; i++)
                    {
                        string classes = "odd", status = "", color = "", decision_full_name = "", decision_log_date = "", decision_jam = "";
                        if (i % 2 == 0) { classes = "even"; }

                        DataTable decision_lm = Db.Rs("Select convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'LM' and Leave_ID = " + rs.Rows[i]["Leave_ID"].ToString() + " ");

                        x.Append("<tr class=\"" + classes + "\">"
                                        + "<td valign=\"top\" width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                        + "<td valign=\"top\" width=\"20%\" style=\"text-align:left;\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                                        + "<td valign=\"top\" width=\"15%\">" + rs.Rows[i]["Type_Desc"].ToString() + "</td>");

                        if (rs.Rows[i]["Leave_Half"].ToString() != "0")
                        {
                            string day = "";
                            if (rs.Rows[i]["Leave_Half"].ToString() == "1") { day += "Pagi"; } else if (rs.Rows[i]["Leave_Half"].ToString() == "2") { day += "Siang"; }
                            x.Append("<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Leave_RequestDate_HalfDay"].ToString() + " - " + day);
                        }
                        else
                        {
                            x.Append("<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Leave_RequestDateFrom"].ToString() + " -  " + rs.Rows[i]["Leave_RequestDateTo"].ToString() + "</td>");
                        }

                        x.Append("<td valign=\"top\" style=\"width:35%;text-align:center;\"><a href=\"" + Param.Path_Admin + "kalender/cuti/detail/detail-leave/?id=" + rs.Rows[i]["Employee_ID"].ToString() + "&ln=" + rs.Rows[i]["Leave_ID"].ToString() + "\">");

                        if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2") { status = "Reviewed by HRD"; color = "lime"; }
                        else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "3") { status = "Reject by HRD"; color = "red"; }

                        decision_full_name = decision_lm.Rows[0]["Employee_Full_Name"].ToString();
                        decision_log_date = decision_lm.Rows[0]["Log_Date"].ToString();
                        decision_jam = decision_lm.Rows[0]["jam"].ToString();

                        x.Append("<font style=\"color:" + color + ";\">" + status + " (" + decision_full_name + " - " + decision_log_date + " - " + decision_jam + ")</font>");
                        x.Append("</a></td></tr>");

                    }
                    x.Append("</tbody></table>");
                    table.Text = x.ToString();
                    updatePanelTable.Update();
                }
            }
            else
            {
                lable.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
                table.Visible = false;
            }
        }
    }

    protected void previous2_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text);
    }

    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        fill(a.ToString(), pagesum.Text);
    }

    protected void next_Click(object sender, EventArgs e)
    {
        try
        {
            int a = Cf.Int(pagenum.Text);
            a++;
            if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
            fill(a.ToString(), pagesum.Text);
        }
        catch (Exception) { Response.Redirect("/gulali/page/404.aspx"); }
    }

    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text);
    }

    protected void page_enter(object sender, EventArgs e)
    {
        try
        {
            int a = Cf.Int(pagenum.Text);
            if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
            fill(a.ToString(), pagesum.Text);
        }
        catch (Exception) { Response.Redirect("/gulali/page/404.aspx"); }
    }

    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(id_get) || !string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
        {
            PlaceHolder report = new PlaceHolder();
            StringBuilder x = new StringBuilder();

            x.Append("<table>"
                        + "<thead>"
                            + " <tr>"
                                + "<th style=\"background:#59a9f8;color:white;\">No</th>"
                                + "<th style=\"background:#59a9f8;color:white;\">Nama</th>"
                                + "<th style=\"background:#59a9f8;color:white;\">Type of Leave</th>"
                                + "<th style=\"background:#59a9f8;color:white;\">Periode</th>"
                                + "<th style=\"background:#59a9f8;color:white;\">Status</th>"
                            + "</tr>"
                        + "</thead>");

            DateTime From = Convert.ToDateTime(from_date);
            DateTime To = Convert.ToDateTime(to_date);
            string From_Conv = From.ToString("yyyy-MM-dd").ToString();
            string To_Conv = To.ToString("yyyy-MM-dd").ToString();

            string before = "", after = "";
            try { App.LogHistory("Generate : Laporan > Cuti", before, after, "Dari tanggal : " + From_Conv + " 00:01 s/d " + To_Conv + " 23:59"); }
            catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

            string new_filter;

            if (id_get == "0") { new_filter = ""; } else { new_filter = " and l.Employee_ID=" + id_get + " "; }
            DataTable rs = Db.Rs("select e.Employee_Full_Name, l.Leave_ID, l.Type_ID, b.Type_Desc, l.Leave_Half, convert(varchar,l.Leave_RequestDateFrom,107) as Leave_RequestDateFrom, convert(varchar,l.Leave_RequestDateTo,107) as Leave_RequestDateTo, convert(varchar,l.Leave_RequestDate_HalfDay,107) as Leave_RequestDate_HalfDay, l.Leave_StatusLeave from TBL_Leave l join TBL_Leave_Type b on l.Type_ID = b.Type_ID join TBL_Employee e on l.Employee_ID = e.Employee_ID where ((Leave_RequestDateFrom between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59' and Leave_RequestDateTo between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') or Leave_RequestDate_HalfDay between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') and (l.Leave_StatusLeave = 2 or l.Leave_StatusLeave = 3) " + new_filter + " order by Leave_RequestDate_HalfDay, Leave_RequestDateFrom asc");

            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string classes = "odd", status = "", color = "", decision_full_name = "", decision_log_date = "", decision_jam = "";
                    if (i % 2 == 0) { classes = "even"; }

                    DataTable decision_lm = Db.Rs("Select convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join TBL_Leave_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'LM' and Leave_ID = " + rs.Rows[i]["Leave_ID"].ToString() + " ");

                    x.Append("<tr class=\"" + classes + "\">"
                                    + "<td valign=\"top\" width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td valign=\"top\" width=\"20%\" style=\"text-align:left;\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                                    + "<td valign=\"top\" width=\"15%\">" + rs.Rows[i]["Type_Desc"].ToString() + "</td>");

                    if (rs.Rows[i]["Leave_Half"].ToString() != "0")
                    {
                        string day = "";
                        if (rs.Rows[i]["Leave_Half"].ToString() == "1") { day += "Pagi"; } else if (rs.Rows[i]["Leave_Half"].ToString() == "2") { day += "Siang"; }
                        x.Append("<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Leave_RequestDate_HalfDay"].ToString() + " - " + day);
                    }
                    else
                    {
                        x.Append("<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Leave_RequestDateFrom"].ToString() + " -  " + rs.Rows[i]["Leave_RequestDateTo"].ToString() + "</td>");
                    }

                    x.Append("<td valign=\"top\" style=\"width:35%;text-align:center;\">");

                    if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "2") { status = "Reviewed by HRD"; color = "lime"; }
                    else if (rs.Rows[i]["Leave_StatusLeave"].ToString() == "3") { status = "Reject by HRD"; color = "red"; }

                    decision_full_name = decision_lm.Rows[0]["Employee_Full_Name"].ToString();
                    decision_log_date = decision_lm.Rows[0]["Log_Date"].ToString();
                    decision_jam = decision_lm.Rows[0]["jam"].ToString();

                    x.Append("<font style=\"color:" + color + ";\">" + status + " (" + decision_full_name + " - " + decision_log_date + " - " + decision_jam + ")</font>");
                    x.Append("</td></tr>");

                }
                x.Append("</table>");
                table.Text = x.ToString();
            }

            report.Controls.Add(table);
            string filename = "Laporan Cuti";
            Rpt.ToExcel(this, report, filename);
        }
    }
}