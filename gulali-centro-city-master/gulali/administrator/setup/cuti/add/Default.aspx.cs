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

public partial class _master_type_cuti_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Cuti", "insert");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack) { }
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
            if (!Page.IsValid) { return; }
            else
            {
                string before = "", after = "Tipe : " + type.Text + " <br/> Jumlah : " + value.Text + " <br/> Lama Bekerja : " + long_working.Text + " ";
                try { App.LogHistory("Tambah : Master Tipe Cuti ", before, after, ""); }
                catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

                DataTable max = Db.Rs("select max(Type_Order) + 1 as max from TBL_Leave_Type");

                Db.Execute("insert into TBL_Leave_Type(Type_Desc, Type_Value, Type_Minimal_Working, Type_Order) values ('" + Cf.StrSql(type.Text) + "', '" + Cf.StrSql(value.Text) + "', '" + Cf.StrSql(long_working.Text) + "', '" + max.Rows[0]["max"].ToString() + "')");

                Response.Redirect(Param.Path_Admin + "setup/cuti/");
            }
        }
    }
}