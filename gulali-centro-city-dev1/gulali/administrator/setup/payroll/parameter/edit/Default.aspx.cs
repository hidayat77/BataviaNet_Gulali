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

public partial class _setup_payroll_parameter_edit : System.Web.UI.Page
{
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
                DataTable dba = Db.Rs2("Select * from TBL_Parameter where Param_ID = " + Cf.StrSql(id_get) + "");
                if (dba.Rows.Count > 0)
                {
                    txtDesc.Text = dba.Rows[0]["Param_Description"].ToString();
                    txtValue.Text = dba.Rows[0]["Param_Value"].ToString();
                    txtRemarks.Text = dba.Rows[0]["Param_Remarks"].ToString();

                    if (dba.Rows[0]["Param_Remarks"].ToString() == "Deduction" || dba.Rows[0]["Param_Remarks"].ToString() == "Regular Income")
                    {
                        tr_choose.Visible = true;
                        if (dba.Rows[0]["Param_Choose"].ToString() == "1")
                        {
                            cb_choose.Checked = true;
                        }
                    }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        string update_choose = ", Param_Choose = '0' ", val = "Param_Value = '0', ";
        DataTable dba = Db.Rs2("Select * from TBL_Parameter where Param_ID = " + Cf.StrSql(id_get) + "");
        if (dba.Rows[0]["Param_Remarks"].ToString() == "Deduction" || dba.Rows[0]["Param_Remarks"].ToString() == "Regular Income")
        {
            if (cb_choose.Checked == true)
            {
                update_choose = ", Param_Choose = '1' ";
            }
        }

        if (!string.IsNullOrEmpty(txtValue.Text))
        {
            val = "Param_Value = '" + (txtValue.Text).Replace(",", ".") + "',";
        }

        string update = "Update TBL_Parameter set " + val + " Param_UpdateBy = '" + App.Employee_ID + "', Param_UpdateDate = getdate() " + update_choose + " where Param_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute2(update);
        Response.Redirect(Param.Path_Admin + "setup/payroll/parameter/");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/parameter/");
    }
}