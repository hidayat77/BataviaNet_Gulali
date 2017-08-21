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

public partial class _user_kalender_lembur : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Lembur", "view");

        tab_cuti.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/cuti/\">Cuti</a>";
        tab_lembur.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/lembur/\">Lembur</a>";
        tab_kehadiran.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/kehadiran/\">Kehadiran</a>";

        preview("1", pagesum.Text);
    }

    public void preview(string num, string sum)
    {
        DataTable rs = Db.Rs2("select Lembur_ID, Lembur_DatePeriode, Lembur_Status from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' order by Lembur_DatePeriode desc");

        if (rs.Rows.Count > 0)
        {
            //employee_name.Text = "(" + rs.Rows[0]["Employee_Full_Name"].ToString() + ")";
            StringBuilder x = new StringBuilder();
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                        + "<thead>"
                            + " <tr role=\"row\">"
                                + "<th style=\"text-align:center;\">No</th>"
                                + "<th>Periode</th>"
                                + "<th style=\"text-align:center;\">Status</th>"
                                + "<th style=\"text-align:center;\">Action</th>"
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
                    
                    //string id_employee = rs.Rows[i]["Employee_ID"].ToString();
                    string Lembur_ID = rs.Rows[i]["Lembur_ID"].ToString();

                    DataTable decision_spv = Db.Rs("Select convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join " + Param.Db2 + ".dbo.TBL_Lembur_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'SPV' and Lembur_ID = " + Lembur_ID + " ");
                    DataTable decision_lm = Db.Rs("Select  convert(varchar,Log_DateCreate,107) as Log_Date,convert(varchar,Log_DateCreate,108) as jam, he.Employee_Full_Name from TBL_Employee he join " + Param.Db2 + ".dbo.TBL_Lembur_Log hl on hl.Log_DecisionBy = he.Employee_ID where Log_DecisionUserStatus = 'LM' and Lembur_ID = " + Lembur_ID + " ");

                    string classes = "odd", status = "", color = "", style = "", decision_full_name = "", decision_log_date = "", decision_jam = "";
                    if (i % 2 == 0) { classes = "even"; }
                    x.Append("<tr class=\"" + classes + "\" " + style + ">"
                                + "<td style=\"text-align:center;width:5%;\">" + (i + 1).ToString() + "</td>");

                    //convert to month and date 
                    DateTime daydate = Convert.ToDateTime(rs.Rows[i]["Lembur_DatePeriode"].ToString());
                    string tanggal = daydate.ToString("MMMM yyyy");

                    string year = daydate.ToString("yyyy");
                    string month = daydate.ToString("MM");

                    x.Append("<td style=\"width:20%\">" + Cf.StrSql(tanggal) + "<td style=\"width:35%;text-align:center;\"><a href=\"detail-lembur?cn=" + Cf.Int(Lembur_ID) + "&year=" + Cf.Int(year) + "&month=" + Cf.Int(month) + "\" >");

                    if (rs.Rows[i]["Lembur_Status"].ToString() == "0")
                    {
                        status = "Pending"; color = "";
                        x.Append("Pending");
                    }
                    else
                    {
                        if (rs.Rows[i]["Lembur_Status"].ToString() == "2") { status = "Reviewed by HRD "; color = "lime"; }
                        else if (rs.Rows[i]["Lembur_Status"].ToString() == "3") { status = "Reject by HRD "; color = "red"; }
                        else if (rs.Rows[i]["Lembur_Status"].ToString() == "4") { status = "Approved by Supervisor "; color = "green"; }
                        else if (rs.Rows[i]["Lembur_Status"].ToString() == "5") { status = "Reject by Supervisor "; color = "red"; }
                        else if (rs.Rows[i]["Lembur_Status"].ToString() == "6") { status = "Canceled by HRD "; color = "lime"; }

                        if (rs.Rows[i]["Lembur_Status"].ToString() == "2" || rs.Rows[i]["Lembur_Status"].ToString() == "3" || rs.Rows[i]["Lembur_Status"].ToString() == "6")
                        {
                            decision_full_name = decision_lm.Rows[0]["Employee_Full_Name"].ToString();
                            decision_log_date = decision_lm.Rows[0]["Log_Date"].ToString();
                            decision_jam = decision_lm.Rows[0]["jam"].ToString();
                        }
                        else if (rs.Rows[i]["Lembur_Status"].ToString() == "4" || rs.Rows[i]["Lembur_Status"].ToString() == "5")
                        {
                            decision_full_name = decision_spv.Rows[0]["Employee_Full_Name"].ToString();
                            decision_log_date = decision_spv.Rows[0]["Log_Date"].ToString();
                            decision_jam = decision_spv.Rows[0]["jam"].ToString();
                        }

                        x.Append("<font style=\"color:" + color + ";\">" + status + "(" + decision_full_name + " - " + decision_log_date + " - " + decision_jam + ")</font>");
                    }
                    x.Append("</a></td>");

                    x.Append("<td class=\"actions\" style=\"text-align: center;width:8%;\">"
                                        + "<a href=\"detail/?year=" + year + "&month=" + month + "\" style=\"padding-right:10px;\"><i class=\"fa fa-eye\"></i></a>"
                                         + "<a onClick=\"confirmdel('delete_list/?id=" + Lembur_ID + "');\"><i class=\"fa fa-trash-o\" style=\"color:#ff5b5b;cursor:pointer;\"></i></a>"
                                    + "</td>");
                    x.Append("</tr>");
                }
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {
            note.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
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