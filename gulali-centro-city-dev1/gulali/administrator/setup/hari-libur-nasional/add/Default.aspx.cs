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

public partial class _hari_libur_nasional : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Hari Libur", "insert");

        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/hari-libur-nasional/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack) { ddl_holiday(); }
    }
    protected void ddl_holiday()
    {
        DataTable rsa = Db.Rs("Select * from TBL_Holiday_List order by Holiday_List_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            holiday.Items.Add(new ListItem(rsa.Rows[i]["Holiday_List_Name"].ToString(), rsa.Rows[i]["Holiday_List_ID"].ToString()));
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
            if (!Page.IsValid) { return; }
            else
            {
                DateTime From = Convert.ToDateTime(from.Text);
                string From_Conv = From.ToString("yyyy-MM-dd").ToString();

                string before = "", after = "ID Hari Libur : " + holiday.Text + " <br/> Tanggal : " + From_Conv + " ";
                try { App.LogHistory("Tambah : Hari Libur", before, after, ""); }
                catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

                Db.Execute("insert into TBL_Holiday(Holiday_List_ID, Holiday_Date) values ('" + Cf.StrSql(holiday.Text) + "','" + From_Conv + "')");
                Response.Redirect(Param.Path_Admin + "setup/hari-libur-nasional/");
            }
        }
    }
}