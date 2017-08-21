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
using System.Data.SqlClient;
using ClosedXML.Excel;

public partial class _admin_component_setting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/payroll-component/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            fill("1", pagesum.Text, search_text.Text);
        }
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();

        string sql_where = " where 1=1 and status_delete = 1";
        if (search != "")
        {
            sql_where += " and (component_code like '%" + search + "%' or component_name like '%" + search + "%' ) ";
        }

        DataTable rs = Db.Rs2("select * from TBL_MasterComponent " + sql_where + " and component_id not in ('18', '19', '20', '29', '30', '31') order by component_type desc");
        if (rs.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Code</th>"
                            + "<th>Component Name</th>"
                            + "<th>Component Type</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "</tbody>");

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
                    string id = rs.Rows[i]["component_id"].ToString(), type = rs.Rows[i]["component_type"].ToString(), mode = "";

                    if (type == "Regular Income") { mode = "1"; }
                    else if (type == "Irregular Income") { mode = "2"; }
                    else if (type == "Deduction") { mode = "3"; }

                    x.Append("<tr>"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td width=\"35%\">" + rs.Rows[i]["component_code"].ToString() + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["component_name"].ToString() + "</td>"
                                    + "<td style=\"width:20%\">" + type + "</td>"
                                    + "<td class=\"actions\" style=\"text-align: center;width:20%;\">"
                                        + "<a href=\"edit/?mode=" + mode + "&id=" + id + "\"><i class=\"fa fa-pencil\" title=\"edit\"></i></a>"
                                        + "<a href=\"detail/?id=" + id + "\" ><i class=\"fa fa-user\" title=\"detail\"></i></a>"
                                        + "<a onClick=\"confirmdel('delete/?id=" + id + "');\" style=\"cursor:pointer;\" title=\"delete\"><i class=\"fa fa-trash\"></i></a>"
                                    + "</td>"
                                + "</tr>");

                }
                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
        }
        else
        {
            lable.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
            table.Visible = false;
        }
    }


    protected void btn_search_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
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