using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Globalization;

public partial class _log_aktifitas_detail : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        if (!IsPostBack)
        {
            link_back.Text = "<a href=\"/gulali/log-activity/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
            fill();
        }
    }

    protected void fill()
    {
        //check int
        int number;
        bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
        if (result)
        {
            if (Fv.cekInt(Cf.StrSql(id_get)))
            {
                string where = "";
                if (App.UserIsAdmin == "1") { where = ""; }
                else if (App.UserIsAdmin == "2") { where = " and b.User_IsAdmin != 1"; }
                else { where = " and Log_UserID = '" + App.UserID + "'"; }

                DataTable list = Db.Rs("Select Log_Date, User_Name, Log_IP, Log_Hostname, Log_Activity, Log_Desc, Log_UserRole, Log_Before, Log_After, Log_Employee_Name from TBL_Log_History a join TBL_User b on a.Log_UserID = b.User_ID where a.Log_ID = '" + Cf.StrSql(id_get) + "' " + where + "");
                if (list.Rows.Count > 0)
                {
                    txt_date.Text = DateTime.Parse(list.Rows[0]["Log_Date"].ToString()).ToString("dd-MMM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    txt_nama_karyawan.Text = list.Rows[0]["Log_Employee_Name"].ToString();
                    txt_username.Text = list.Rows[0]["User_Name"].ToString();
                    txt_IP.Text = list.Rows[0]["Log_IP"].ToString();
                    txt_Host.Text = list.Rows[0]["Log_Hostname"].ToString();
                    txt_Activity.Text = list.Rows[0]["Log_Activity"].ToString();
                    txt_Description.Text = list.Rows[0]["Log_Desc"].ToString();
                    txt_role.Text = list.Rows[0]["Log_UserRole"].ToString();

                    if (!string.IsNullOrEmpty(list.Rows[0]["Log_Before"].ToString())) { before.Text = list.Rows[0]["Log_Before"].ToString(); }
                    else { td_head_before.Visible = false; td_before.Visible = false; }

                    if (!string.IsNullOrEmpty(list.Rows[0]["Log_After"].ToString())) { after.Text = list.Rows[0]["Log_After"].ToString(); }
                    else { td_head_after.Visible = false; td_after.Visible = false; }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
        else { Response.Redirect("/gulali/page/404.aspx"); }
    }
}