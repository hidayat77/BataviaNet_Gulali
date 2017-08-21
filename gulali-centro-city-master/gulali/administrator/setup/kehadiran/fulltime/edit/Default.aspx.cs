using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Globalization;

public partial class _admin_payroll_component_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        //App.ProtectPageGulali(this, "Setup >> Fulltime", "update");
        if (!IsPostBack)
        {
            txtJamMasuk.Text = Convert.ToString("hh:mm").ToString();
            txtJamKeluar.Text = Convert.ToString(DateTime.Now.TimeOfDay);
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {

                DataTable rs_fulltime = Db.Rs("Select * from TBL_Schedule_Role"
            + " where Schedule_Role_ID ='" + Cf.Int(id_get) + "'");
                if (rs_fulltime.Rows.Count > 0)
                {
                    txtKode.Text = rs_fulltime.Rows[0]["Schedule_Role_Code"].ToString();
                    txtNama.Text = rs_fulltime.Rows[0]["Schedule_Role_Nama"].ToString();
                    txtToleransiTelat.Text = rs_fulltime.Rows[0]["Schedule_Role_Toleransi_Telat"].ToString();
                    txtDurasi.Text = rs_fulltime.Rows[0]["Schedule_Role_Durasi_Istirahat"].ToString();
                    txtJamMasuk.Text = rs_fulltime.Rows[0]["Schedule_Role_JamMasuk"].ToString();
                    txtJamKeluar.Text = rs_fulltime.Rows[0]["Schedule_Role_JamKeluar"].ToString();
                }
                else
                {
                    Response.Redirect("/notFound/404.aspx");
                }
            }
            else
            {
                Response.Redirect("/notFound/404.aspx");
            }
        }

    }


    protected void BtnSubmit(object sender, EventArgs e)
    {
        string update = "Update TBL_Schedule_Role set Schedule_Role_Code ='" + Cf.StrSql(txtKode.Text) + "', Schedule_Role_Nama = '" + Cf.StrSql(txtNama.Text) + "', Schedule_Role_Status = 0, Schedule_Role_Toleransi_Telat = '" + Cf.StrSql(txtToleransiTelat.Text) + "', Schedule_Role_Durasi_Istirahat = '" + Cf.StrSql(txtDurasi.Text) + "', Schedule_Role_UpdateBy = '" + Cf.StrSql(App.Employee_ID) + "', Schedule_Role_JamMasuk = '" + Cf.StrSql(txtJamMasuk.Text) + "' , Schedule_Role_JamKeluar = '" + Cf.StrSql(txtJamKeluar.Text) + "'where Schedule_Role_ID = '" + Cf.StrSql(id_get) + "'";
        Db.Execute(update);

        Response.Redirect("/gulali/administrator/setup/kehadiran/fulltime/");
        //Response.Redirect(update);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/kehadiran/fulltime/");
    }

}