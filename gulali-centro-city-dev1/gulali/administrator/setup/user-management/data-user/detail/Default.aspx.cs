using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class _admin_setup_usr_management_detail_view : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string user_get { get { return App.GetStr(this, "user"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "view");
        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/user-management/data-user/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            //try
            //{
            int number;
            bool result = Int32.TryParse(Cf.StrSql(user_get), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(user_get)))
                {
                    DataTable exist = Db.Rs("select User_ID from TBL_User where User_ID = '" + user_get + "' and User_ID != 1");
                    if (exist.Rows.Count > 0)
                    {
                        ddl_groupCMSPrev();

                        DataTable rsb = Db.Rs("select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_ID != 1 order by Employee_Full_Name asc");

                        if (rsb.Rows.Count > 0)
                        {
                            for (int i = 0; i < rsb.Rows.Count; i++)
                            {
                                ddl_Employee_Name.Items.Add(new ListItem(rsb.Rows[i]["Employee_Full_Name"].ToString(), rsb.Rows[i]["Employee_ID"].ToString()));
                            }
                        }

                        DataTable rs = Db.Rs("select User_Email, User_Name, u.Employee_ID, u.Role_ID, u.User_Authorized, User_IsAdmin from TBL_User u join TBL_Employee e on u.Employee_ID = e.Employee_ID where u.User_ID = '" + Cf.StrSql(user_get) + "'");
                        if (rs.Rows.Count > 0)
                        {
                            username.Text = rs.Rows[0]["User_Name"].ToString();
                            ddl_groupCMS.SelectedValue = rs.Rows[0]["Role_ID"].ToString();
                            ddl_Employee_Name.SelectedValue = rs.Rows[0]["Employee_ID"].ToString();

                            if (rs.Rows[0]["User_Authorized"].ToString() == "Y") { cb_block.Checked = false; } else { cb_block.Checked = true; }
                            if (rs.Rows[0]["User_IsAdmin"].ToString() == "2") { div_isAdmin.Visible = true; isAdmin.Checked = true; }

                            //}
                            //catch (Exception ) { }
                        }
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void ddl_groupCMSPrev()
    {
        string where = "Role_ID not in ('1', '2')";
        if (id_get == "2") { where = "Role_ID = '" + Cf.StrSql(id_get) + "'"; }
        DataTable rsa = Db.Rs("select * from TBL_Role where " + where + "  order by Role_ID asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_groupCMS.Items.Add(new ListItem(rsa.Rows[i]["Role_Name"].ToString(), rsa.Rows[i]["Role_ID"].ToString()));
            }
        }
    }
}