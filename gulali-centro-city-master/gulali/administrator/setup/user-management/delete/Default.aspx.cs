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

public partial class _admin_setup_usr_management_delete : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "delete");
        if (!IsPostBack)
        {
            try
            {
                string deletepriv = "delete from TBL_Privilege where Role_ID = '" + Cf.StrSql(id) + "'";
                string delete = "delete from TBL_Role where Role_ID = '" + Cf.StrSql(id) + "'";

                if (id != "2") { Db.Execute(deletepriv); Db.Execute(delete); }

                Response.Redirect("../");
            }
            catch (Exception) { }
        }
    }
}