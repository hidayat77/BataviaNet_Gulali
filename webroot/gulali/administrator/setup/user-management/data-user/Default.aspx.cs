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

public partial class _admin_setup_usr_management_detail : System.Web.UI.Page
{
    protected string mode_get { get { return App.GetStr(this, "mode"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/user-management/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        link_add.Text = "<a class=\"btn btn-primary\" href=\"add/\">Tambah</a>";

        if (!string.IsNullOrEmpty(mode_get))
        {
            note.Text = "<div style=\"background-color: #10c469; text-align: center;\"><h4 class=\"page-title\">Updated Success!</h4></div>";
        }

        fill("1", pagesum.Text, search_text.Text);
    }

    protected void fill(string num, string sum, string search)
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
                            + "<th style=\"width:5%;text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody><tbody>");

        string sql_where = " where 1=1";
        if (search != "")
        {
            sql_where += " and Employee_Full_Name like '%" + search + "%' ";
        }

        DataTable rs = Db.Rs("select User_ID, hr.Role_ID, User_Name, Role_Name, User_Email, Employee_Full_Name, User_Authorized, User_IsAdmin from TBL_User hu join TBL_Employee he on hu.Employee_ID = he.Employee_ID join TBL_Role hr on hu.Role_ID = hr.Role_ID " + sql_where + " and User_ID != 1 order by Employee_Full_Name asc");

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
                    string id = rs.Rows[i]["User_ID"].ToString(), status_log = "Inactive", isAdmin_val = "No";
                    if (rs.Rows[i]["User_Authorized"].ToString() == "Y") { status_log = "Active"; }
                    if (rs.Rows[i]["User_IsAdmin"].ToString() == "2") { isAdmin_val = "Yes"; }

                    x.Append("<tr>"
                                + "<td valign=\"top\" width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                + "<td valign=\"top\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                                + "<td>" + rs.Rows[i]["User_Name"].ToString() + "</td>"
                                + "<td valign=\"top\">" + rs.Rows[i]["Role_Name"].ToString() + "</td>"
                                + "<td valign=\"top\">" + isAdmin_val + "</td>"
                                + "<td valign=\"top\">" + status_log + "</td>"
                                + "<td class=\"actions\" style=\"text-align: center;width:20%;\">"
                                    + "<a href=\"detail/?user=" + id + "\" style=\"padding-right:10px;\"><i class=\"fa fa-eye\"></i></a>"
                                    + "<a href=\"edit/?user=" + id + "\" style=\"padding-right:10px;\"><i class=\"fa fa-pencil\"></i></a>"
                                    + "<a onClick=\"confirmdel('delete/?user=" + id + "');\" style=\"padding-right:10px;cursor:pointer;\"><i class=\"fa fa-trash-o\"></i></a>"
                                + "</td>"
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