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

public partial class _setup_payroll_progressive_tax_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "update");
        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                DataTable dba = Db.Rs2("Select * from TBL_Progressive_Tax where Progressive_Tax_ID = " + Cf.StrSql(id_get) + "");
                if (dba.Rows.Count > 0)
                {
                    txtDesc.Text = dba.Rows[0]["progressive_tax_desc"].ToString();
                    txtValue.Text = dba.Rows[0]["progressive_tax_prosentase"].ToString();
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
    protected void Submit_Click(object sender, EventArgs e)
    {
        string update = "Update TBL_Progressive_tax set  progressive_tax_desc ='" + txtDesc.Text + "', progressive_tax_prosentase ='" + txtValue.Text + "' where progressive_tax_id = '" + Cf.StrSql(id_get) + "'";
        Db.Execute2(update);
        Response.Redirect(Param.Path_Admin + "setup/payroll/progressive-tax/");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/progressive-tax/");
    }
}