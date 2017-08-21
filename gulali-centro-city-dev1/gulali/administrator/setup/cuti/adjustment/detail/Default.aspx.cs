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

public partial class _admin_setup_cuti : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string year { get { return App.GetStr(this, "year"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected string mode { get { return App.GetStr(this, "mode"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Cuti", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/adjustment/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (id != "")
        {
            detail_preview(id);
            div_table_show.Visible = false;
            div_detail_show.Visible = true;
        }
        else
        {
            div_table_show.Visible = true;
            div_detail_show.Visible = false;
            DataTable rs_bulan_nama = Db.Rs("select DateName( month , DateAdd( month , " + month + " , -1 ) ) as 'monthname' from TBL_History_Leave_Balance");
            periode_show.Text = rs_bulan_nama.Rows[0]["monthname"].ToString() + " " + year;

            preview("1", pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
        }
    }
    public void preview(string num, string sum, string dropdown, string text_box, string datefrom, string dateto)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;width:5%;\">No</th>"
                            + "<th style=\"text-align:center;width:15%;\">Employee Name</th>"
                            + "<th style=\"text-align:center;width:15%;\">Division</th>"
                            + "<th style=\"text-align:center;width:15%;\">Department</th>"
                            + "<th style=\"text-align:center;width:5%;\">Value</th>"
                            + "<th style=\"text-align:center;width:15%;\">Create By</th>"
                            + "<th style=\"text-align:center;width:15%;\">Date</th>"
                            + "<th style=\"text-align:center;width:10%;\">Status</th>"
                            + "<th style=\"text-align:center;width:5%;\">Action</th>"
                        + "</tr>"
                    + "</thead>");

        //string sql_where = " where 1=1";
        string sql_where;
        if (!string.IsNullOrEmpty(Cf.StrSql(month)))
        {
            sql_where = " where MONTH(balance_create_date) = '" + Cf.Int(month) + "' AND YEAR(balance_create_date) = '" + Cf.Int(year) + "'";
        }
        else
        {
            sql_where = " where YEAR(balance_create_date) = '" + Cf.Int(year) + "'";
        }
        //button search 
        if (dropdown == "1")
        {
            sql_where += " and (B.Employee_Full_Name like '%" + text_box + "%')";
            //sql_where += " and B.Employee_Last_Name like '%" + text_box + "%'";
        }
        else if (dropdown == "2")
        {
            sql_where += " and D.Division_Name like '%" + text_box + "%'";
        }
        else if (dropdown == "3")
        {
            sql_where += " and E.Department_Name like '%" + text_box + "%'";
        }
        else if (dropdown == "4")
        {
            sql_where += " and (C.Employee_Full_Name like '%" + text_box + "%')";
        }
        if (datefrom != "" && dateto != "")
        {
            sql_where += " and (A.balance_create_date BETWEEN '" + datefrom + " 00:00:00' and '" + dateto + " 23:59:59')";
            //sql_where += " and (A.balance_create_date BETWEEN '"+datefrom+"' and '"+dateto+"')";
        }
        if (radio_increase.Checked)
        {
            sql_where += " and A.balance_status = 1";
        }
        else if (radio_decrease.Checked)
        {
            sql_where += " and A.balance_status = 2";
        }
        else
        {
            sql_where += "";
        }

        DataTable rsa = Db.Rs("select A.balance_id as 'id', B.Employee_Full_Name as 'Name', A.balance_value as 'Value',"
            + " C.Employee_Full_Name as 'Create_By_Name', D.Division_Name as 'Div_name', E.Department_Name as 'Dep_name',"
            + " CONVERT(VARCHAR(11),A.balance_create_date,106) as 'Date', IIF(A.balance_status = 1, 'Penambahan', IIF(A.balance_status = 2, 'Pengurangan', '')) as 'Status'"
            + " from TBL_History_Leave_Balance A"
            + " left join TBL_Employee B on A.employee_id = B.Employee_ID"
            + " left join TBL_Employee C on A.Balance_Create_By = C.Employee_ID"
            + " left join TBL_Division D on A.division_id = D.Division_ID"
            + " left join TBL_Department E on A.department_id = E.Department_ID " + sql_where + " Order by A.balance_create_date desc");



        //DataTable rs = Db.Rs5("select distinct pf.Flag_ID, Flag_Month, Flag_Year from HRIS_Payroll_Flag pf join HRIS_Payroll p on pf.Flag_ID = p.Flag_ID " + sql_where + " order by Flag_Year desc, Flag_Month desc");

        if (rsa.Rows.Count > 0)
        {
            if (rsa.Rows.Count > 0)
            {
                double of = 0;
                if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
                {

                    of = Math.Ceiling(rsa.Rows.Count / System.Convert.ToDouble(sum));
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
                if (d < rsa.Rows.Count)
                {
                    dari = d + 1;
                    if (d == 0) dari = 1;
                }
                if (ea < rsa.Rows.Count)
                {
                    sampai = ea;
                }
                else
                {
                    sampai = rsa.Rows.Count;
                }
                if (dari > 0)
                {
                    for (int i = dari - 1; i < sampai; i++)
                    {
                        string id = rsa.Rows[i]["id"].ToString();

                        x.Append(
                                "<tr>"
                                    + "<td style=\"text-align:center;\">" + (i + 1) + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Name"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Div_name"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Dep_name"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Value"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Create_By_Name"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Date"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rsa.Rows[i]["Status"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">"
                            //+ "<ul class=\"list-nostyle list-inline\">"
                            //    + "<li>"
                            //        + "<a href=\"action/detail/?id=" + id + "&month="+month+"&year="+year+"\" class=\"icon-search\" title=\"Detail\">&nbsp;</a>"
                            //    + "</li>"
                            //+ "</ul>"
                            + "<a href=\"../detail/?id=" + id + "&month=" + month + "&year=" + year + "\" padding-right:20px;\"><i class=\"fa fa-eye\"></i></a>"
                                    + "</td>"
                                + "</tr>"
                                );

                    }
                    x.Append("</tbody>");
                    x.Append("</table>");
                    table.Visible = true;
                    div_pagging.Visible = true;
                    table.Text = x.ToString();
                    lable.Text = "";
                    //tb2.Visible = true;
                    //table2.Visible = true;
                    //lable_table.Text = "";
                    //updatePanelTable.Update();
                }
            }
        }
        else
        {
            //tb2.Visible = false;
            div_pagging.Visible = false;
            table.Visible = false;
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            //updatePanelTable.Update();
        }
    }
    protected void previous2_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        preview(a.ToString(), pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void next2_Click(object sender, EventArgs e)
    {
        preview("0", pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview("1", pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text, ddl_selected.SelectedValue, Search.Text, tanggal_mulai.Text, tanggal_selesai.Text);
    }

    protected void detail_preview(string id)
    {
        DataTable rs_employee = Db.Rs("select A.balance_id as 'id', B.Employee_Full_Name as 'Full_Name', A.balance_value as 'Value',"
            + " C.Employee_Full_Name as 'Create_By', D.Division_Name as 'Div_name', E.Department_Name as 'Dep_name', A.balance_remarks as 'remarks',"
            + " CONVERT(VARCHAR(11),A.balance_create_date,106) as 'Date', IIF(A.balance_status = 1, 'Increase', IIF(A.balance_status = 2, 'Decrease', '')) as 'Status'"
            + " from TBL_History_Leave_Balance A"
            + " left join TBL_Employee B on A.employee_id = B.Employee_ID"
            + " left join TBL_Employee C on A.Balance_Create_By = C.Employee_ID"
            + " left join TBL_Division D on A.Division_ID = D.Division_ID"
            + " left join TBL_Department E on A.Department_ID = E.Department_ID"
            + " where A.balance_id ='" + Cf.Int(id) + "'");
        if (rs_employee.Rows.Count > 0)
        {
            txt_employee.Text = rs_employee.Rows[0]["Full_Name"].ToString();
            txt_division.Text = rs_employee.Rows[0]["Div_name"].ToString();
            txt_department.Text = rs_employee.Rows[0]["Dep_name"].ToString();
            txt_value.Text = rs_employee.Rows[0]["Value"].ToString();
            txt_create_by.Text = rs_employee.Rows[0]["Create_By"].ToString();
            txt_date.Text = rs_employee.Rows[0]["Date"].ToString();
            txt_status.Text = rs_employee.Rows[0]["Status"].ToString();
            txt_desc.Text = rs_employee.Rows[0]["remarks"].ToString();
        }
        else
        {
            Response.Redirect("?month=" + month + "&year=" + year + "");
        }
    }
    protected void Button_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("?month=" + month + "&year=" + year + "");
    }
}