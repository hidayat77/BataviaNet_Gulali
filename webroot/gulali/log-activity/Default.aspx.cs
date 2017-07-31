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
using System.Globalization;

public partial class _log_aktifitas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        if (!IsPostBack)
        {
            preview("1", pagesum.Text);
        }
    }

    public void preview(string num, string sum)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>");
        if (App.UserIsAdmin != "3") { x.Append("<th style=\"width:15%;\">Username</th>"); }
        x.Append("<th style=\"width:15%;\">Role</th>");
        if (App.UserIsAdmin != "3") { x.Append("<th style=\"width:10%;\">is Admin ?</th>"); }

        x.Append("<th>Aktifitas</th>"
                + "<th>Tanggal</th>"
                + "<th style=\"width:10%;text-align:center;\">Aksi</th>"
            + "</tr>"
        + "</thead><tbody>");

        string where = "";
        if (App.UserIsAdmin == "1") { where = ""; }
        else if (App.UserIsAdmin == "2") { where = " where User_IsAdmin != 1"; }
        else { where = " where Log_UserID = '" + App.UserID + "'"; }

        DataTable rs = Db.Rs("SELECT Log_ID, Log_Activity, Log_UserID, Log_Username, Log_UserRole, User_IsAdmin, Log_Date FROM TBL_Log_History " + where + " order by Log_Date desc");
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
            if (d < rs.Rows.Count) { dari = d + 1; if (d == 0) dari = 1; }
            if (ea < rs.Rows.Count) { sampai = ea; }
            else { sampai = rs.Rows.Count; }

            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {
                    string id = rs.Rows[i]["Log_ID"].ToString(), isAdmin = "No";
                    if (rs.Rows[i]["User_IsAdmin"].ToString() == "1" || rs.Rows[i]["User_IsAdmin"].ToString() == "2") { isAdmin = "yes"; }
                    x.Append("<tr>"
                                + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>");
                                if (App.UserIsAdmin != "3") { x.Append("<td>" + rs.Rows[i]["Log_Username"].ToString() + "</td>");}
                                x.Append("<td>" + rs.Rows[i]["Log_UserRole"].ToString() + "</td>");
                                if (App.UserIsAdmin != "3") { x.Append("<td>" + isAdmin + "</td>");}
                                x.Append("<td>" + rs.Rows[i]["Log_Activity"].ToString() + "</td>"
                                //+ "<td>" + DateTime.Parse(rs.Rows[0]["Log_Date"].ToString()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + "</td>"
                                + "<td>" + rs.Rows[0]["Log_Date"].ToString() + "</td>"
                                + "<td style=\"text-align:center;\"><a href=\"detail/?id=" + id + "\"><i class=\"fa fa-eye\"></i></a></td>"
                            + "</tr>");
                }
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {
            lable.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
            table.Visible = false;
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text);
    }
}