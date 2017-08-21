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

public partial class _admin_laporan_cuti_balance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Cuti", "view");
        
        tab_riwayat.Text = "<a href=\"" + Param.Path_Admin + "laporan/cuti/\">Riwayat Cuti</a>";
        tab_sisa_cuti.Text = "<a href=\"" + Param.Path_Admin + "laporan/cuti/sisa-cuti/\">Sisa Cuti</a>";
        if (!IsPostBack)
        {
            fill("1", pagesum.Text, search_text.Text);
        }
    }

    protected void fill(string num, string sum, string search)
    {
        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Nama</th>"
                            + "<th>Departemen</th>"
                            + "<th>Divisi</th>"
                            + "<th style=\"text-align:center;\">Sisa Cuti</th>"
                        + "</tr>"
                    + "</thead>"
                    + "</tbody>");

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

        DataTable rs = Db.Rs("select a.Employee_ID, a.Employee_Sum_LeaveBalance, a.Employee_IDCard_Number, a.Employee_Full_Name, Employee_Last_Name, a.Employee_Company_Email, a.Employee_Personal_Email, b.Division_Name, c.Department_Name from TBL_Employee a left join TBL_Division b on a.Division_ID = b.Division_ID left join TBL_Department c on c.Department_ID = a.Department_ID  " + sql_where + " and Employee_ID != 1 order by Employee_Inactive,Employee_Full_Name asc ");

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

                    x.Append("<tr>"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td width=\"35%\">" + Cf.Upper(rs.Rows[i]["Employee_Full_Name"].ToString()) + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Department_Name"].ToString() + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Division_Name"].ToString() + "</td>"
                                    + "<td style=\"text-align:center;\">" + rs.Rows[i]["Employee_Sum_LeaveBalance"].ToString() + "</td>"
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

    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        string sql_where = "";
        if (search_text.Text != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Full_Name") { sql_where += " and A.Employee_Full_Name like '%" + search_text.Text + "%' or A.Employee_First_Name like '%" + search_text.Text + "%' or A.Employee_Middle_Name like '%" + search_text.Text + "%' or A.Employee_Last_Name like '%" + search_text.Text + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Address") { sql_where += " and A.Employee_Domicile_Address like '%" + search_text.Text + "%' or A.Employee_IDCard_Address like '%" + search_text.Text + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Department_Name")
            {
                sql_where += " and B.Department_Name like '%" + search_text.Text + "%' ";
            }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Division_Name")
            {
                sql_where += " and C.Division_Name like '%" + search_text.Text + "%' ";
            }
        }
        string before = "", after = "";
        try { App.LogHistory("Generate : Laporan > Karyawan", before, after, sql_where); }
        catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

        string query = "Select "
            + " A.Employee_NIK as 'NIK' ,"
            + " A.Employee_Full_Name as 'Nama',"
            + " B.Department_Name as 'Departemen',"
            + " C.Division_Name as 'Divisi', "
            + " A.Employee_Sum_LeaveBalance as 'Sisa Cuti'"
            + " from TBL_Employee A"
            + " left join TBL_Department B on A.Department_ID = B.Department_ID"
            + " left join TBL_Division C on A.Division_ID = C.Division_ID"
            + " left join TBL_Employee_Payroll I on A.Employee_ID = I.Employee_ID where 1=1 "
            + " " + sql_where + " and A.Employee_ID != 1 order by A.Employee_Full_Name asc";

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);

                        //Set Name of DataTables.
                        ds.Tables[0].TableName = "Sisa Cuti";

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                //Add DataTable as Worksheet.
                                wb.Worksheets.Add(dt);
                            }
                            //Export the Excel file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            //Response.ContentType = " application/vnd.ms-excel";
                            Response.AddHeader("content-disposition", "attachment;filename=Sisa Cuti " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }
            }
        }
    }
}