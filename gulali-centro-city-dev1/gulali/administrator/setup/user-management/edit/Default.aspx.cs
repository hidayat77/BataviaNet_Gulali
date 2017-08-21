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

public partial class _admin_setup_usr_management_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "update");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/user-management/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

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

            DataTable rs = Db.Rs("Select Menu_Name, hp.Menu_ID, Menu_isAdmin, Privilege_View, Privilege_Insert, Privilege_Update, Privilege_Delete from TBL_Privilege hp , TBL_Menu hm where hm.Menu_ID = hp.Menu_ID and hp.Role_ID = '" + Cf.StrSql(id_get) + "' order by Menu_isAdmin asc");

            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string id = rs.Rows[i]["Menu_Name"].ToString(), isAdmin = "Admin";
                    if (rs.Rows[i]["Menu_isAdmin"].ToString() == "3") { isAdmin = "User"; }

                    x.Append(" <tr>"
                            + "<td style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                            + "<td>" + rs.Rows[i]["Menu_Name"].ToString() + "</td>"
                            + "<td style=\"text-align:center;\">" + isAdmin + "</td>"
                            + "<td style=\"text-align:center;\">");

                    if (rs.Rows[i]["Privilege_View"].ToString() == "1") { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/ checked=\"checked\">"); }
                    else { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/>"); }
                    x.Append("</td>"
                            + "<td style=\"text-align:center;\">");

                    if (rs.Rows[i]["Privilege_Insert"].ToString() == "1") { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[1]\" type=\"checkbox\"/ checked=\"checked\">"); }
                    else { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[1]\" type=\"checkbox\"/>"); }
                    x.Append("</td>"
                            + "<td style=\"text-align:center;\">");

                    if (rs.Rows[i]["Privilege_Update"].ToString() == "1") { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[2]\" type=\"checkbox\"/ checked=\"checked\">"); }
                    else { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[2]\" type=\"checkbox\"/>"); }
                    x.Append("</td>"
                            + "<td style=\"text-align:center;\">");

                    if (rs.Rows[i]["Privilege_Delete"].ToString() == "1") { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[3]\" type=\"checkbox\"/ checked=\"checked\">"); }
                    else { x.Append("<input name=\"" + rs.Rows[i]["Menu_ID"].ToString().Replace(' ', '_') + "[3]\" type=\"checkbox\"/>"); }
                    x.Append("</td></tr>");

                }
            }
            x.Append("</table>");
            table.Text = x.ToString();
            //}
            //catch (Exception ) { }
        }
    }

    protected void update_module(string field, string Menu_ID, string p_allowed)
    {
        //try
        //{
        if (p_allowed == "on") { p_allowed = "1"; } else { p_allowed = "0"; }

        string sql = "UPDATE TBL_Privilege SET "
            + "[" + field + "] = '" + p_allowed + "' "
            + " WHERE Menu_ID = '" + Menu_ID + "' and Role_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute(sql);
        //}
        //catch (Exception ) { }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        //try
        //{

        string Menu_ID, p_allowed;
        string[] fld = { "Privilege_View", "Privilege_Insert", "Privilege_Update", "Privilege_Delete" };
        var req = HttpContext.Current;
        DataTable rs = Db.Rs("Select * from TBL_Menu");
        foreach (DataRow row in rs.Rows) // Loop over the rows.
        {
            Menu_ID = row["Menu_ID"].ToString();
            DataTable cekmodul = Db.Rs("Select * from TBL_Privilege where Role_ID = '" + Cf.StrSql(id_get) + "' and Menu_ID = '" + Menu_ID + "'");
            if (cekmodul.Rows.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    p_allowed = req.Request[Menu_ID.Replace(' ', '_') + "[" + i + "]"];
                    update_module(fld[i], Menu_ID, p_allowed);
                }
            }
            else
            {
                Db.Execute("Insert into TBL_Privilege(Menu_ID, Role_ID, Privilege_View, Privilege_Insert, Privilege_Update, Privilege_Delete)"
                            + "Values('" + Menu_ID + "', '" + Cf.StrSql(id_get) + "', 0, 0, 0, 0)");
            }
        }
        Response.Redirect(Param.Path_Admin + "setup/user-management/");
        //}
        //catch (Exception ) { }
    }
}