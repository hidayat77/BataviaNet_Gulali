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

public partial class _admin_data_karyawan : System.Web.UI.Page
{
    protected string mode_get { get { return App.GetStr(this, "mode"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Data Karyawan", "view");

        if (!string.IsNullOrEmpty(mode_get))
        {
            link_active.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/\">";
            link_inactive.Text = "<a data-toggle=\"tab\" href=\"inactive\">";
            class_inactive.Text = "class=\"active\"";
        }
        else
        {
            link_active.Text = "<a data-toggle=\"tab\" href=\"#active\">";
            link_inactive.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/?mode=2\">";
            class_active.Text = "class=\"active\"";
        }

        fill("1", pagesum.Text, search_text.Text);
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Nama</th>"
                            + "<th>Departemen</th>"
                            + "<th>Divisi</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        string sql_where = " where 1=1";
        if (search != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Full_Name") { sql_where += " and Employee_Full_Name like '%" + search + "%' or Employee_First_Name like '%" + search + "%' or Employee_Middle_Name like '%" + search + "%' or Employee_Last_Name like '%" + search + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Address") { sql_where += " and Employee_Domicile_Address like '%" + search + "%' or Employee_IDCard_Address like '%" + search + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Department_Name")
            {
                sql_where += " and c.Department_Name like '%" + search + "%' ";
            }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Division_Name")
            {
                sql_where += " and b.Division_Name like '%" + search + "%' ";
            }
        }

        string que = "";
        if (!string.IsNullOrEmpty(mode_get))
        { que = " and Employee_Inactive = '2'"; }
        else { que = " and Employee_Inactive = '1'"; }

        DataTable rs = Db.Rs("select a.Employee_ID, a.Employee_Inactive, a.Employee_IDCard_Number, a.Employee_Full_Name, Employee_Last_Name, a.Employee_Company_Email, a.Employee_Personal_Email, b.Division_Name, c.Department_Name from TBL_Employee a left join TBL_Division b on a.Division_ID = b.Division_ID left join TBL_Department c on c.Department_ID = a.Department_ID " + sql_where + " " + que + " and Employee_ID != 1 order by Employee_Inactive,Employee_Full_Name asc");

        lable.Text = "";
        table.Visible = true;
        if (rs.Rows.Count > 0)
        {
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0")
                {
                    pagenum.Text = num;
                }
                else
                {
                    pagenum.Text = of.ToString();
                }
                pagesum.Text = sum;
            }
            else
            {
            }
            int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
            int per = Cf.Int(pagesum.Text);
            int ke = Cf.Int(pagenum.Text);
            c = ke - 1;
            d = c * per;
            ea = ke * per;

            count_page.Text = of.ToString();
            if (d < rs.Rows.Count)
            {
                dari = d + 1;
                if (d == 0) dari = 1;
            }
            if (ea < rs.Rows.Count)
            {
                sampai = ea;
            }
            else
            {
                sampai = rs.Rows.Count;
            }
            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {
                    string id = rs.Rows[i]["Employee_ID"].ToString();

                    string link_edit = "", classes = "odd";
                    if (mode_get == "2") { link_edit = "mode=2&"; }

                    if (i % 2 == 0) { classes = "even"; }

                    x.Append("<tr class=\"" + classes + "\">"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td width=\"35%\">" + Cf.Upper(rs.Rows[i]["Employee_Full_Name"].ToString()) + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Department_Name"].ToString() + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Division_Name"].ToString() + "</td>"
                                    + "<td class=\"actions\" style=\"text-align: center;width:20%;\">"
                                        + "<a href=\"detail/?" + link_edit + "id=" + id + "\" style=\"padding-right:20px;\"><i class=\"fa fa-eye\"></i></a>"
                                        + "<a href=\"edit/?" + link_edit + "id=" + id + "\" style=\"padding-right:20px;\"><i class=\"fa fa-pencil\"></i></a>"
                                        + "<a href=\"history/?" + link_edit + "id=" + id + "\">Riwayat</a>"
                                    + "</td>"
                                + "</tr>");

                }
                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            table.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous2_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        //if (a > 1)
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text, search_text.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
}