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

public partial class _master_type_cuti_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    public static string before = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Cuti", "update");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack)
        {
            try
            {
                DataTable rs = Db.Rs("select * from TBL_Leave_Type where Type_ID = '" + Cf.StrSql(id_get) + "'");
                type.Text = rs.Rows[0]["Type_Desc"].ToString();
                value.Text = rs.Rows[0]["Type_Value"].ToString();
                long_working.Text = rs.Rows[0]["Type_Minimal_Working"].ToString();

                before = "Tipe : " + rs.Rows[0]["Type_Desc"].ToString() + " <br/> Jumlah : " + rs.Rows[0]["Type_Value"].ToString() + " <br/> Lama Bekerja : " + rs.Rows[0]["Type_Minimal_Working"].ToString() + " ";
            }
            catch (Exception ) { }
        }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(type) ? x : false;
            x = Fv.isInt(value) ? x : false;
            x = Fv.isInt(long_working) ? x : false;
            if (string.IsNullOrEmpty(type.Text) || string.IsNullOrEmpty(value.Text) || string.IsNullOrEmpty(long_working.Text))
            {
                note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                + "<h4 class=\"page-title\">Lengkapi Data Anda!</h4>"
                            + "</div>";
            }
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
                string before = "", after = "Tipe : " + type.Text + " <br/> Jumlah : " + value.Text + " <br/> Lama Bekerja : " + long_working.Text + " ";
                try { App.LogHistory("Tambah : Master Tipe Cuti ", before, after, ""); }
                catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

                string update = "update TBL_Leave_Type set Type_Desc = '" + Cf.StrSql(type.Text) + "', Type_Value = '" + Cf.StrSql(value.Text) + "', Type_Minimal_Working = '" + Cf.StrSql(long_working.Text) + "' where Type_ID = '" + Cf.StrSql(id_get) + "'";
                Db.Execute(update);
                Response.Redirect(Param.Path_Admin + "setup/cuti/");
            }
        }
    }
}