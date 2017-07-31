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

public partial class _setup_payroll_ptkp_edit : System.Web.UI.Page
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
                DataTable dba = Db.Rs2("Select * from TBL_PTKP where PTKP_ID = " + Cf.StrSql(id_get) + "");
                if (dba.Rows.Count > 0)
                {
                    decimal PTKP_Value = 0;
                    if (!string.IsNullOrEmpty(dba.Rows[0]["PTKP_Value"].ToString()))
                    {
                        PTKP_Value = Convert.ToDecimal(dba.Rows[0]["PTKP_Value"].ToString());
                    }

                    txtStatus.Text = dba.Rows[0]["PTKP_Status"].ToString();
                    txtDesc.Text = dba.Rows[0]["PTKP_Description"].ToString();
                    txtValue.Text = (PTKP_Value).ToString("#,##0");
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        string update = "Update TBL_PTKP set PTKP_Value ='" + Convert.ToDecimal(txtValue.Text) + "', PTKP_UpdateBy = '" + App.Employee_ID + "', PTKP_UpdateDate = getdate() where PTKP_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute2(update);
        Response.Redirect(Param.Path_Admin + "setup/payroll/ptkp/");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/ptkp/");
    }
}