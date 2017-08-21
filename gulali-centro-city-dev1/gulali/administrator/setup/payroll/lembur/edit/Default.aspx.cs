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

public partial class _setup_payroll_lembur_edit : System.Web.UI.Page
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
                DataTable dba = Db.Rs2("select Rumus_Category_Title, B.Rumus_List, B.Rumus_List_Jam, B.Rumus_List_Bulan, B.Rumus_List_Description from TBL_Rumus_Lembur A left join TBL_Rumus_lembur_List B on A.Rumus_Category_ID=B.Rumus_Category_ID where B.Rumus_List_ID = " + Cf.StrSql(id_get) + "");
                if (dba.Rows.Count > 0)
                {
                    txttitle.Text = dba.Rows[0]["Rumus_Category_Title"].ToString();
                    txtlist.Text = dba.Rows[0]["Rumus_List"].ToString();
                    txtjam.Text = dba.Rows[0]["Rumus_List_Jam"].ToString();
                    txtbulan.Text = dba.Rows[0]["Rumus_List_Bulan"].ToString();
                    txtDesc.Text = dba.Rows[0]["Rumus_List_Description"].ToString();
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
        string update = "Update TBL_Rumus_lembur_List set Rumus_List ='" + txtlist.Text + "', Rumus_List_Jam ='" + txtjam.Text + "', Rumus_List_Bulan ='" + txtbulan.Text + "', Rumus_List_Description ='" + txtDesc.Text + "', Rumus_List_UpdateBy ='" + App.Employee_ID + "', Rumus_List_UpdateDate = getdate() where Rumus_List_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute2(update);
        Response.Redirect(Param.Path_Admin + "setup/payroll/lembur/");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/lembur/");
    }
}