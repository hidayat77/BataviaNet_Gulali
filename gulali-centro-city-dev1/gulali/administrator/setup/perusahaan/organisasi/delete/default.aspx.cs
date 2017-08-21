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

public partial class _admin_setup_organisasi_delete : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "delete");
        if (!IsPostBack)
        {
            Db.Execute("Delete from TBL_Department where Department_ID = '" + Cf.StrSql(id) + "'");
            Response.Redirect("../");
        }
    }
}