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

public partial class _admin_setup_organisasi_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "update");
        if (!IsPostBack)
        {
            href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                DataTable dba = Db.Rs("Select * from TBL_Department where Department_ID = " + Cf.StrSql(id_get) + "");
                if (dba.Rows.Count > 0)
                {
                    txtGroupname.Text = dba.Rows[0]["Department_Name"].ToString();
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        string update = "Update TBL_Department set Department_Name='" + txtGroupname.Text + "' where Department_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute(update);
        Response.Redirect("../");
    }
}