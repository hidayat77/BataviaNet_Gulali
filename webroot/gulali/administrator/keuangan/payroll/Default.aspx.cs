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

public partial class _admin_keuangan_payroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Keuangan >> Payroll", "view");

        if (!IsPostBack)
        {
            string id = "1";
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                //DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + id + "");
                //nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();
                fill("1", pagesum.Text, search_text.Text);
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();

        string sql_where = "";
        if (search != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "search_month")
            { sql_where += " where datename(month,dateadd(month, Flag_Month - 1, 0)) like '%" + search + "%'"; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "search_year")
            { sql_where += " where Flag_Year like '%" + search + "%'"; }
        }

        //DataTable rs = Db.Rs("select convert(varchar,Contract_StartPeriode,107) as Contract_StartPeriode, convert(varchar,Contract_EndPeriode,107) as Contract_EndPeriode, * from TBL_Employee_Contract join TBL_Position t on t.Position_ID = Contract_Title " + sql_where + " order by Contract_ID asc");

        DataTable rs = Db.Rs2("select distinct pf.Flag_ID, Flag_Month, datename(month,dateadd(month, Flag_Month - 1, 0)) as 'Flag_Month_Name', Flag_Year from TBL_Payroll_Flag pf join TBL_Payroll p on pf.Flag_ID = p.Flag_ID " + sql_where + " order by Flag_Year desc, Flag_Month desc");

        if (rs.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Periode</th>"
                            + "<th style=\"text-align:center;\">Action</th>"
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
            if (d < rs.Rows.Count) { dari = d + 1; if (d == 0) dari = 1; }
            if (ea < rs.Rows.Count) { sampai = ea; }
            else { sampai = rs.Rows.Count; }
            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {
                    string Flag_ID = rs.Rows[i]["Flag_ID"].ToString();

                    x.Append("<tr>"
                                + "<td style=\"text-align:center;\" width=\"5%\">" + (i + 1).ToString() + "</td>"
                                + "<td valign=\"top\" width=\"70%\">" + rs.Rows[i]["Flag_Month_Name"].ToString() + " " + rs.Rows[i]["Flag_Year"].ToString() + "</td>"
                                + "<td class=\"actions\" style=\"text-align: center;width:25%;\">"
                            + "<a href=\"detail/?month=" + rs.Rows[i]["Flag_Month"].ToString() + "&year=" + rs.Rows[i]["Flag_Year"].ToString() +"\"padding-right:20px;\"><i class=\"fa fa-eye\"></i></a>"
                            + "<a onClick=\"confirmdel('delete/?id=" + Flag_ID + "');\" style=\"padding-right:20px;\"><i class=\"fa fa-trash-o\" style=\"color:#ff5b5b;cursor:pointer;\"></i></a>"
                        + "</td>"
                    + "</tr>");
                }
                x.Append("</tbody></table>");

                table.Text = x.ToString();
                lable.Text = "";
                table.Visible = true;
            }
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Not Found</i></h3></div>";
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