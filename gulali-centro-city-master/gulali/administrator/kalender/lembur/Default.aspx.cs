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

public partial class _admin_kalender_lembur : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Kalender >> Lembur", "view");

        if (!IsPostBack)
        {
            //DataTable checksupervisor = Db.Rs("Select Employee_ID, Employee_DirectSPV from TBL_Employee WHERE Employee_DirectSpv = '" + Cf.StrSql(App.Employee_ID) + "'");

            //if (checksupervisor.Rows.Count > 0) { preview("1", pagesum.Text, search_text.Text); }
            //else { Response.Redirect("/gulali/page/404.aspx"); }

            preview("1", pagesum.Text, search_text.Text);
        }
    }

    public void preview(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th width=\"40%\">Nama</th>"
                            + "<th style=\"text-align:center;\">Detail</th>"
                        + "</tr>"
                    + "</thead>");

        string where = "where a.Employee_ID != 1";
        if (search != "") { where += " and Employee_Full_Name like '%" + search + "%' "; }

        DataTable rs = Db.Rs("select distinct a.Employee_ID, Employee_Full_Name from TBL_Employee a join " + Param.Db2 + ".dbo.TBL_Lembur b on a.Employee_ID = b.Employee_ID " + where + " and Lembur_Status in ('2', '3', '4') order by Employee_Full_Name asc");
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
                    string id = rs.Rows[i]["Employee_ID"].ToString();

                    DataTable count_req = Db.Rs2("Select Lembur_ID from TBL_Lembur where Lembur_Status = 4 and Employee_ID = " + id + "");
                    DataTable count_create = Db.Rs2("Select Lembur_ID from TBL_Lembur where Lembur_Status not in ('0', '5') and Employee_ID= " + id + "");

                    x.Append("<tbody>"
                            + " <tr>"
                                + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                + "<td>" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                                + "<td style=\"text-align:center;\"><a href=\"detail/?id=" + id + "\" >"
                                + "<font style=\"color:green;\">" + count_create.Rows.Count + " Request (<font style=\"color:green;\"><font style=\"color:red;\">" + count_req.Rows.Count + "</font> Need Approval </font>)</font>"
                            + "</tr>"
                      + "</tbody>");
                }
            }
            x.Append("</table>");
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
        preview("1", pagesum.Text, search_text.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        preview(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        preview("0", pagesum.Text, search_text.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text, search_text.Text);
    }
}