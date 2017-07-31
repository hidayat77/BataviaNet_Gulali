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

public partial class _admin_laporan_user_management : System.Web.UI.Page
{
    protected string mode_get { get { return App.GetStr(this, "mode"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.ExportExcel_New);
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Laporan >> User Management", "view");

        if (!IsPostBack)
        {
            ddl_DepartmentPrev(); ddl_DivisionPrev(); ddl_RolePrev();
        }

        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!string.IsNullOrEmpty(mode_get))
        {
            note.Text = "<div style=\"background-color: #10c469; text-align: center;\"><h4 class=\"page-title\">Updated Success!</h4></div>";
        }

        fill("1", pagesum.Text);
    }

    protected void ddl_DepartmentPrev()
    {
        DataTable rsa = Db.Rs("Select Department_ID, Department_Name from TBL_Department order by Department_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_department.Items.Add(new ListItem(rsa.Rows[i]["Department_Name"].ToString(), rsa.Rows[i]["Department_ID"].ToString()));
        }
    }

    protected void ddl_DivisionPrev()
    {
        DataTable rsa = Db.Rs("Select Division_ID, Division_Name from TBL_division order by Division_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_division.Items.Add(new ListItem(rsa.Rows[i]["Division_Name"].ToString(), rsa.Rows[i]["Division_ID"].ToString()));
        }
    }

    protected void ddl_RolePrev()
    {
        DataTable rsa = Db.Rs("Select Role_ID, Role_Name from TBL_Role where role_id != 1 order by Role_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_role.Items.Add(new ListItem(rsa.Rows[i]["Role_Name"].ToString(), rsa.Rows[i]["Role_ID"].ToString()));
        }
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_division.Visible = true;
        ddl_division.Items.Clear();
        DataTable selectDivision = Db.Rs("Select * From TBL_Division Where Department_ID = " + ddl_department.SelectedValue);
        if (selectDivision.Rows.Count > 0)
        {
            for (int i = 0; i < selectDivision.Rows.Count; i++)
            {
                ddl_division.Items.Add(new ListItem(selectDivision.Rows[i]["Division_Name"].ToString(), selectDivision.Rows[i]["Division_ID"].ToString()));
            }
        }
    }

    protected void fill(string num, string sum)
    {
        StringBuilder x = new StringBuilder();
        StringBuilder modals = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th style=\"\">Nama Karyawan</th>"
                            + "<th style=\"width:15%;\">Username</th>"
                            + "<th style=\"width:15%;\">Role</th>"
                            + "<th style=\"width:10%;\">Is Admin ?</th>"
                            + "<th style=\"width:10%;\">Status User</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody><tbody>");

        string dep = ddl_department.SelectedValue;
        string div = ddl_division.SelectedValue;
        string role = ddl_role.SelectedValue;

        string sql_where = " where 1=1";
        if (!string.IsNullOrEmpty(dep)) { sql_where += " and he.Department_ID = '" + dep + "'"; }
        if (!string.IsNullOrEmpty(div)) { sql_where += " and he.Division_ID = '" + div + "'"; }
        if (!string.IsNullOrEmpty(role)) { sql_where += " and hr.Role_ID = '" + role + "'"; }

        DataTable rs = Db.Rs("select hu.Employee_ID, he.Department_ID, he.Division_ID, User_ID, hr.Role_ID, User_Name, Role_Name, User_Email, Employee_Full_Name, User_Authorized, User_IsAdmin from TBL_User hu join TBL_Employee he on hu.Employee_ID = he.Employee_ID join TBL_Role hr on hu.Role_ID = hr.Role_ID " + sql_where + " and User_ID != 1 order by Employee_Full_Name asc");

        lable.Text = "";
        table.Visible = true;
        if (rs.Rows.Count > 0)
        {
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0")
                { pagenum.Text = num; }
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
                    string id_emp = rs.Rows[i]["Employee_ID"].ToString();
                    string id = rs.Rows[i]["User_ID"].ToString(), status_log = "Inactive", isAdmin_val = "No";
                    if (rs.Rows[i]["User_Authorized"].ToString() == "Y") { status_log = "Active"; }
                    if (rs.Rows[i]["User_IsAdmin"].ToString() == "2") { isAdmin_val = "Yes"; }

                    x.Append("<tr>"
                                + "<td valign=\"top\" width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                + "<td valign=\"top\"><a href=\"/gulali/administrator/data-karyawan/detail/?id=" + id_emp + "&mode=3\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</a></td>"
                                + "<td>" + rs.Rows[i]["User_Name"].ToString() + "</td>"
                                + "<td valign=\"top\">" + rs.Rows[i]["Role_Name"].ToString() + "</td>"
                                + "<td valign=\"top\">" + isAdmin_val + "</td>"
                                + "<td valign=\"top\">" + status_log + "</td>"
                            + "</tr>");

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text);
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
        int a = Cf.Int(pagenum.Text);
        //if (a > 1)
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text);
    }

    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        string sql_where = " where 1=1";
        if (!string.IsNullOrEmpty(ddl_department.SelectedValue)) { sql_where += " and he.Department_ID = '" + ddl_department.SelectedValue + "'"; }
        if (!string.IsNullOrEmpty(ddl_division.SelectedValue)) { sql_where += " and he.Division_ID = '" + ddl_division.SelectedValue + "'"; }
        if (!string.IsNullOrEmpty(ddl_role.SelectedValue)) { sql_where += " and hr.Role_ID = '" + ddl_role.SelectedValue + "'"; }


        string before = "", after = "";
        try { App.LogHistory("Generate : Laporan > User Management", before, after, sql_where); }
        catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

        string query = "Select "
            + " he.Employee_Full_Name as 'Employee Name' ,"
            + " hu.User_Name as 'User Name' ,"
            + " hu.User_Email as 'Email' ,"
            + " IIF(hu.User_Authorized = 'Y','Active','') as 'Status User',"
            + " hu.User_LastLogin as 'Last Login',"
            + " hu.User_LastIP as 'Last IP',"
            + " hu.User_LastHostName as 'Last Host Name', "
            + " hu.User_TglBlokir as 'Tanggal Blokir',"
            + " hu.User_CountLogin as 'Count Login',"
            + " hu.User_CountSalahPass as 'Count Salah Pass',"
            + " hu.User_LastModified as 'Last Modified',"
            + " hu.User_TglCreate as 'Tanggal Create',"
            + " hr.Role_Name as 'Role',"
            + " IIF(hu.User_IsAdmin = 1, 'Superadmin',"
            + " IIF(hu.User_IsAdmin = 2, 'Admin',"
            + " IIF(hu.User_IsAdmin = 3, 'User', ''))) as 'Is Admin' "
            + " from TBL_User hu"
            + " join TBL_Role hr on hu.Role_ID = hr.Role_ID"
            + " join TBL_Employee he on hu.Employee_ID = he.Employee_ID "
            + " " + sql_where + " and hu.User_ID != 1 order by he.Employee_Full_Name asc";

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
                        ds.Tables[0].TableName = "User Management";

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
                            Response.AddHeader("content-disposition", "attachment;filename=User Management " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
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