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
        App.ProtectPageGulali(this, "Data Karyawan >> Payroll", "View");

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

    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect(Param.Path_Admin + "/setup/kehadiran/shift/"); }
}