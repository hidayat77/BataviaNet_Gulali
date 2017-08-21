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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class _data_karyawan_add : System.Web.UI.Page
{
    protected string recent { get { return App.GetStr(this, "recent"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Data Karyawan", "insert");

        if (!IsPostBack)
        {

        }
    }
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(Kode_Kehadiran) ? x : false;
            x = Fv.isComplete(nama_kehadiran) ? x : false;
            x = Fv.isComplete(toleransi_telat) ? x : false;
            x = Fv.isComplete(Jumlah_jam_istirahat) ? x : false;
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
                DataTable chn = Db.Rs2("Select Schedule_Role_Code, Schedule_Role_Nama, Schedule_Role_Status from TBL_Schedule_Role where Schedule_Role_Status = 1 AND (Schedule_Role_Code = '" + Cf.StrSql(Kode_Kehadiran.Text) + "' or Schedule_Role_Nama = '" + Cf.StrSql(nama_kehadiran.Text) + "')");
                if (chn.Rows.Count > 0)
                {
                    return;
                }
                else
                {
                    string insert = "Insert into TBL_Schedule_Role(Schedule_Role_Code, Schedule_Role_Nama, Schedule_Role_Status, Schedule_Role_Toleransi_Telat, Schedule_Role_Durasi_Istirahat, Schedule_Role_UpdateBy, Schedule_Role_CreateDate, Schedule_Role_JamMasuk, Schedule_Role_JamKeluar) values('" + Cf.StrSql(Kode_Kehadiran.Text) + "','" + Cf.StrSql(nama_kehadiran.Text) + "','1','" + Cf.StrSql(toleransi_telat.Text) + "','" + Cf.StrSql(Jumlah_jam_istirahat.Text) + "','" + App.Employee_ID + "',getdate(),'" + Cf.StrSql(jam_mulai.Text) + "','" + Cf.StrSql(jam_selesai.Text) + "')";
                    Db.Execute2(insert);

                    Response.Redirect(Param.Path_Admin + "setup/kehadiran/shift/");
                }
            }
        }
    }

    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect(Param.Path_Admin + "/setup/kehadiran/shift/"); }
}