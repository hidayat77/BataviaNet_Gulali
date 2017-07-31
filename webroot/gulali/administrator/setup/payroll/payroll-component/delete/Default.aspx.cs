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
        App.ProtectPageGulali(this, "Setup >> Payroll", "delete");
        if (!IsPostBack)
        {
            try
            {
                DataTable rsa = Db.Rs2("select role_id,admin_role from " + Param.Db2 + ".[dbo].[TBL_Payroll_Role] where  role_id='" + Cf.StrSql(id) + "' and admin_role not in('superadmin') order By role_id ");
                if (rsa.Rows.Count > 0)
                {
                    string delete = "Delete from " + Param.Db2 + ".[dbo].TBL_Payroll_role where role_id = '" + Cf.StrSql(id) + "'";
                    Db.Execute(delete);
                    string admin_role = rsa.Rows[0]["admin_role"].ToString();
                    string deletePriv = "Delete from  " + Param.Db2 + ".[dbo].TBL_Payroll_Privilege where admin_role= '" + admin_role + "'";
                    Db.Execute2(deletePriv);
                    Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/");
                }
            }
            catch (Exception) { }
        }
    }
}