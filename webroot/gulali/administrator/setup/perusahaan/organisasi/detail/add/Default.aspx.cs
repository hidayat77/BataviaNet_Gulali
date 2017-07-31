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

public partial class _admin_setup_organisasi_detail_add : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "insert");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/detail/?id=" + Cf.StrSql(id_get) + "\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id_get)))
                {
                    DataTable exist = Db.Rs("select * from TBL_Department where Department_ID = '" + id_get + "'");
                    if (exist.Rows.Count < 1)
                    {
                        Response.Redirect("/gulali/page/404.aspx");
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
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(txtGroupname) ? x : false;
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                DataTable chn = Db.Rs("Select * from TBL_Division where Division_name = '" + Cf.StrSql(txtGroupname.Text) + "'");
                if (chn.Rows.Count > 0) { return; }
                else
                {
                    string insert = "Insert into TBL_Division(Division_Name, Department_ID) values('" + Cf.StrSql(txtGroupname.Text) + "', '" + Cf.StrSql(id_get) + "')";
                    Db.Execute(insert);
                    Response.Redirect(Param.Path_Admin + "setup/perusahaan/organisasi/detail/?id=" + Cf.StrSql(id_get) + "");
                }
            }
        }
    }
}