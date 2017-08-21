using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;

public partial class _admin_setup_usr_management_detail_delete : System.Web.UI.Page
{
    protected string user_get { get { return App.GetStr(this, "user"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "delete");
        if (!IsPostBack)
        {
            //try
            //{
            string delete = "delete from TBL_User where User_ID = '" + Cf.StrSql(user_get) + "'";
            if (user_get != "1") { Db.Execute(delete); }

            Response.Redirect(Param.Path_Admin + "setup/user-management/data-user/");
            //}catch(Exception ){}
        }
    }
}