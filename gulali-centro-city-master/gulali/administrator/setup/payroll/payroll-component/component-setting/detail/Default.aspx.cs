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

public partial class _admin_component_setting_detail : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "view");
        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                DataTable dba = Db.Rs2("Select * from TBL_MasterComponent where Component_ID = " + Cf.StrSql(id_get) + " and component_id not in ('18', '19', '20', '29', '30', '31')");
                if (dba.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_name"].ToString()))
                    {
                        txtComponentname.Text = dba.Rows[0]["component_name"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_code"].ToString()))
                    {
                        txtcode.Text = dba.Rows[0]["component_code"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_kind"].ToString()))
                    {
                        txtkind.Text = dba.Rows[0]["component_kind"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_type"].ToString()))
                    {
                        txttype.Text = dba.Rows[0]["component_type"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("/gulali/page/404.aspx");
                }
            }
            else
            {
                Response.Redirect("/gulali/page/404.aspx");
            }
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
    }
}