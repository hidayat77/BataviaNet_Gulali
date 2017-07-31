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

public partial class _hari_libur_nasional_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    public static string before = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Hari Libur", "update");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/hari-libur-nasional/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack)
        {
            try
            {
                DataTable rs = Db.Rs("select Holiday_List_Name, CONVERT(VARCHAR(11),Holiday_Date ,105) as Holiday_startDate from TBL_Holiday a join TBL_Holiday_List b on a.Holiday_List_ID = b.Holiday_List_ID where Holiday_ID = '" + Cf.StrSql(id_get) + "'");
                holiday.Text = rs.Rows[0]["Holiday_List_Name"].ToString();
                from.Text = rs.Rows[0]["Holiday_startDate"].ToString();

                before = "Hari Libur : " + rs.Rows[0]["Holiday_List_Name"].ToString() + " <br/> Tanggal : " + rs.Rows[0]["Holiday_startDate"].ToString() + " ";
            }
            catch (Exception ex) { }
        }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isTgl(from) ? x : false;
            if (string.IsNullOrEmpty(from.Text))
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
                DateTime From = Convert.ToDateTime(from.Text);
                string From_Conv = From.ToString("yyyy-MM-dd").ToString();

                string after = "Hari Libur : " + holiday.Text + " <br/> Tanggal : " + From_Conv + " ";
                try { App.LogHistory("Edit : Hari Libur", before, after, ""); }
                catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

                string update = "update TBL_Holiday set Holiday_Date = '" + From_Conv + "' where Holiday_ID = '" + Cf.StrSql(id_get) + "'";
                Db.Execute(update);
                Response.Redirect(Param.Path_Admin + "setup/hari-libur-nasional/");
            }
        }
    }
}