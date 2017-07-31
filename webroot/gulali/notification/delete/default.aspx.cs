using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;

public partial class _notification_delete : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                App.CekPageUser(this);
                Db.Execute("delete from TBL_Notification where Notif_ID = '" + id + "'");
                Response.Redirect("/gulali/notification/");
            }
            catch (Exception) { }
        }
    }
}