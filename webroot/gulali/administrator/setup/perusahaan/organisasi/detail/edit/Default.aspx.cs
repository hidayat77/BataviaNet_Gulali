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

public partial class _admin_setup_organisasi_detail_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string cn { get { return App.GetStr(this, "cn"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "edit");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/detail/?id=" + Cf.StrSql(id_get) + "\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack) 
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            bool result2 = Int32.TryParse(Cf.StrSql(cn), out number);
            if (result && result2)
            {
                DataTable dba = Db.Rs("Select Division_Name from TBL_Division where Division_ID = " + Cf.Int(cn) + "");
                if (dba.Rows.Count > 0)
                {
                    txtGroupname.Text = dba.Rows[0]["Division_Name"].ToString();
                }
            } else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        DataTable chn = Db.Rs("Select * from TBL_Division where Division_name = '" + txtGroupname.Text + "'");
        if (txtGroupname.Text != "")
        {
            string update = "Update TBL_Division set Division_Name='" + txtGroupname.Text + "' where Division_ID = '" + Cf.Int(cn) + "'";
            Db.Execute(update);
            Response.Redirect(Param.Path_Admin + "setup/perusahaan/organisasi/detail/?id=" + Cf.StrSql(id_get) + "");
        } else { return; }
    }
}