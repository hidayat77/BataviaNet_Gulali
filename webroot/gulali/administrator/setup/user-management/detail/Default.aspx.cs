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
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/user-management/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        fill();
    }

    protected void fill()
    {
        DataTable exist = Db.Rs("select Role_Name from TBL_Role where Role_ID = '" + Cf.StrSql(id_get) + "' and Role_ID != 1");
        if (exist.Rows.Count > 0)
        {
            username.Text = exist.Rows[0]["Role_Name"].ToString();

            StringBuilder x = new StringBuilder();
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                        + "<thead>"
                            + " <tr role=\"row\">"
                                + "<th style=\"text-align:center;\">No</th>"
                                + "<th>Menu</th>"
                                + "<th style=\"text-align:center;\">Halaman</th>"
                                + "<th style=\"text-align:center;\">View</th>"
                                + "<th style=\"text-align:center;\">Insert</th>"
                                + "<th style=\"text-align:center;\">Update</th>"
                                + "<th style=\"text-align:center;\">Delete</th>"
                            + "</tr>"
                        + "</thead>"
                        + "<tbody>");

            DataTable rs = Db.Rs("Select Menu_Name, Privilege_View, Privilege_Insert, Privilege_Update, Privilege_Delete, Menu_isAdmin from TBL_Privilege hp, TBL_Menu hm where hm.Menu_ID = hp.Menu_ID and hp.Role_ID = '" + Cf.StrSql(id_get) + "' order by Menu_isAdmin asc");

            lable.Text = "";
            table.Visible = true;
            if (rs.Rows.Count > 0)
            {
                for (int a = 0; a < rs.Rows.Count; a++)
                {
                    string priv_view = "<i class=\"fa fa-times-rectangle-o\" style=\"color:red;\"></i>", priv_insert = "<i class=\"fa fa-times-rectangle-o\" style=\"color:red;\"></i>", priv_update = "<i class=\"fa fa-times-rectangle-o\" style=\"color:red;\"></i>", priv_delete = "<i class=\"fa fa-times-rectangle-o\" style=\"color:red;\"></i>", isAdmin = "Admin";
                    
                    if (rs.Rows[a]["Menu_isAdmin"].ToString() == "3") { isAdmin = "User"; }
                    if (rs.Rows[a]["Privilege_View"].ToString() == "1") { priv_view = "<i class=\"fa fa-check-square-o\" style=\"color:green;\"></i>"; }
                    if (rs.Rows[a]["Privilege_Insert"].ToString() == "1") { priv_insert = "<i class=\"fa fa-check-square-o\" style=\"color:green;\"></i>"; }
                    if (rs.Rows[a]["Privilege_Update"].ToString() == "1") { priv_update = "<i class=\"fa fa-check-square-o\" style=\"color:green;\"></i>"; }
                    if (rs.Rows[a]["Privilege_Delete"].ToString() == "1") { priv_delete = "<i class=\"fa fa-check-square-o\" style=\"color:green;\"></i>"; }

                    x.Append("<tr>"
                                + "<td align=\"center\">" + (a + 1).ToString() + "</td>"
                                + "<td>" + rs.Rows[a]["Menu_Name"].ToString() + "</td>"
                                + "<td style=\"text-align:center;\">" + isAdmin + "</td>"
                                + "<td style=\"text-align:center;\">" + priv_view + "</td>"
                                + "<td style=\"text-align:center;\">" + priv_insert + "</td>"
                                + "<td style=\"text-align:center;\">" + priv_update + "</td>"
                                + "<td style=\"text-align:center;\">" + priv_delete + "</td>"
                                + "</tr>");
                }

                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
            else
            {
                lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
                table.Visible = false;
            }
        }
        else { Response.Redirect("/gulali/page/404.aspx"); }
    }
}