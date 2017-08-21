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

public partial class _admin_payroll_component_add : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "insert");
        if (!IsPostBack) { }
    }
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(txtGroupname) ? x : false;
            return x;
        }
    }

    protected void Submit1_Click(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                DataTable chn = Db.Rs2("Select * from TBL_Payroll_Role where admin_role = '" + Cf.StrSql(txtGroupname.Text) + "'");
                if (chn.Rows.Count > 0)
                {
                    return;
                }
                else
                {
                    string insert = "Insert into TBL_Payroll_Role(admin_role) values('" + Cf.StrSql(txtGroupname.Text) + "')";
                    Db.Execute2(insert);

                    DataTable allModule = Db.Rs2("Select * from TBL_MasterComponent ");

                    for (int i = 0; i < allModule.Rows.Count; i++)
                    {
                        string insertPrivilege = "insert into TBL_Payroll_Privilege(Component_id, Admin_Role, Privilege_choose, privilege_value)"
                        + "values('" + allModule.Rows[i]["component_id"].ToString() + "','" + Cf.StrSql(txtGroupname.Text) + "', '0', '0')";

                        Db.Execute2(insertPrivilege);
                    }

                    Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/");
                }
            }
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/");
    }
}