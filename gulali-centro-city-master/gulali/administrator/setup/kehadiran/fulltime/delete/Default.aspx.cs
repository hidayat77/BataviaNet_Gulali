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

public partial class _admin_payroll_component_delete : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        //App.ProtectAdmin(this, "Asset Management", "Delete");
        if (!IsPostBack)
        {
            //try
            //{

            string delete = "Delete from TBL_Schedule_Role where Schedule_Role_ID = '" + id + "'";
            Db.Execute(delete);

            Response.Redirect("/gulali/administrator/setup/kehadiran/fulltime/");
            //}catch(Exception ){}
        }
    }





    
}