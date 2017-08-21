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
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Payroll", "Update");

        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id)))
                {
                    show();
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    public void show()
    {
        DataTable chn = Db.Rs2("Select Schedule_Role_Code, Schedule_Role_Nama, Schedule_Role_Status, Schedule_Role_Toleransi_Telat, Schedule_Role_Durasi_Istirahat, Schedule_Role_JamMasuk, Schedule_Role_JamKeluar from TBL_Schedule_Role where Schedule_Role_Status = 1 and Schedule_Role_ID = '" + Cf.Int(id) + "'");
        if (chn.Rows.Count > 0)
        {
            DateTime start = Convert.ToDateTime(chn.Rows[0]["Schedule_Role_JamMasuk"].ToString());
            DateTime end = Convert.ToDateTime(chn.Rows[0]["Schedule_Role_JamKeluar"].ToString());

            string mulai = start.ToString("HH:mm");
            string selesai = end.ToString("HH:mm");

            Kode_Kehadiran.Text = chn.Rows[0]["Schedule_Role_Code"].ToString();
            nama_kehadiran.Text = chn.Rows[0]["Schedule_Role_Nama"].ToString();
            toleransi_telat.Text = chn.Rows[0]["Schedule_Role_Toleransi_Telat"].ToString();
            Jumlah_jam_istirahat.Text = chn.Rows[0]["Schedule_Role_Durasi_Istirahat"].ToString();
            jam_mulai.Text = mulai.ToString();
            jam_selesai.Text = selesai.ToString();
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

    protected void BtnUpdate(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                //DataTable chn = Db.Rs2("Select Kehadiran_Role_Code, Kehadiran_Role_Nama, Kehadiran_Role_Status from TBL_Kehadiran_Role where Kehadiran_Role_Status = 1 AND (Kehadiran_Role_Code = '" + Cf.StrSql(Kode_Kehadiran.Text) + "' or Kehadiran_Role_Nama = '" + Cf.StrSql(nama_kehadiran.Text) + "')");
                //if (chn.Rows.Count > 0)
                //{
                //    return;
                //}
                //else
                //{
                string update = "update TBL_Schedule_Role set Schedule_Role_Code = '" + Cf.StrSql(Kode_Kehadiran.Text) + "', Schedule_Role_Nama = '" + Cf.StrSql(nama_kehadiran.Text) + "', Schedule_Role_Toleransi_Telat = '" + Cf.StrSql(toleransi_telat.Text) + "', Schedule_Role_Durasi_Istirahat = '" + Cf.StrSql(Jumlah_jam_istirahat.Text) + "', Schedule_Role_JamMasuk = '" + Cf.StrSql(jam_mulai.Text) + "', Schedule_Role_JamKeluar = '" + Cf.StrSql(jam_selesai.Text) + "' where Schedule_Role_ID = '" + Cf.Int(id) + "'";

                    //string insert = "Insert into TBL_Kehadiran_Role(Kehadiran_Role_Code, Kehadiran_Role_Nama, Kehadiran_Role_Status, Kehadiran_Role_Toleransi_Telat, Kehadiran_Role_Durasi_Istirahat, kehadiran_Role_UpdateBy, Kehadiran_Role_CreateDate, kehadiran_Role_JamMasuk, Kehadiran_Role_JamKeluar) values('" + Cf.StrSql(Kode_Kehadiran.Text) + "','" + Cf.StrSql(nama_kehadiran.Text) + "','1','" + Cf.StrSql(toleransi_telat.Text) + "','" + Cf.StrSql(Jumlah_jam_istirahat.Text) + "','" + App.Employee_ID + "',getdate(),'" + Cf.StrSql(jam_mulai.Text) + "','" + Cf.StrSql(jam_selesai.Text) + "')";
                    Db.Execute2(update);

                    Response.Redirect(Param.Path_Admin + "setup/kehadiran/shift/");
                //}
            }
        }
    }

    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect(Param.Path_Admin + "/setup/kehadiran/shift/"); }
}