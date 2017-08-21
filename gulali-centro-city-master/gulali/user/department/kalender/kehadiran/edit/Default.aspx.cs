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
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Kehadiran", "Update");

        link_href.Text = "<a href=\"" + Param.Path_User + "department/kalender/kehadiran/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id)))
                {
                    ddl_jadwal_show(); 
                    show();
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void ddl_jadwal_SelectedTextChanged(object sender, EventArgs e)
    {
        if (ddl_jadwal.SelectedValue == "")
        {
            txt_masuk.Text = "";
            txt_keluar.Text = "";
        }
        else
        {
            DataTable rsa = Db.Rs("Select Schedule_Role_ID, Schedule_Role_JamMasuk, Schedule_Role_JamKeluar from TBL_Schedule_Role where Schedule_Role_Status = '1' and Schedule_Role_ID = '" + Cf.Int(ddl_jadwal.SelectedValue) + "' order by Schedule_Role_Nama asc");

            txt_masuk.Text = rsa.Rows[0]["Schedule_Role_JamMasuk"].ToString();
            txt_keluar.Text = rsa.Rows[0]["Schedule_Role_JamKeluar"].ToString();
        }

        //show ddl_employee_Replace
        DataTable chn = Db.Rs("Select Schedule_ID, Schedule_Role_ID, Schedule_Date from TBL_Schedule_Kalendar where Schedule_ID = '" + Cf.Int(id) + "'");

        string id_shift = chn.Rows[0]["Schedule_Role_ID"].ToString();

        DateTime tgl = Convert.ToDateTime(chn.Rows[0]["Schedule_Date"].ToString());
        string date_replace = tgl.ToString("yyyy-MM-dd");

        if (id_shift.Equals(ddl_jadwal.SelectedValue) || (ddl_jadwal.SelectedValue.Equals("")))
        {
            div_employe_replace.Visible = false;
            ddl_employee.Items.Clear();
        }
        else
        {
            div_employe_replace.Visible = true ;
            ddl_employee_replace_show(date_replace);
        }

    }
    protected void ddl_employee_replace_show(string date_replace)
    {
        DataTable rsa = Db.Rs("Select distinct B.Employee_ID, B.Employee_Full_Name from TBL_Schedule_Kalendar A left join TBL_Employee B on A.Employee_ID=B.Employee_ID where B.Employee_DirectSpv = '" + App.Employee_ID + "' and Schedule_Role_ID = '" + Cf.Int(ddl_jadwal.SelectedValue) + "' and Schedule_Date ='" + date_replace + "' order by B.Employee_Full_Name asc");

        ddl_employee.Items.Clear();
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_employee.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
        }
    }
    protected void ddl_jadwal_show()
    {
        DataTable rsa = Db.Rs("Select Schedule_Role_ID, Schedule_Role_Nama from TBL_Schedule_Role where Schedule_Role_Status = '1' order by Schedule_Role_Nama asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_jadwal.Items.Add(new ListItem(rsa.Rows[i]["Schedule_Role_Nama"].ToString(), rsa.Rows[i]["Schedule_Role_ID"].ToString()));
        }
    }
    protected void show()
    {
        DataTable chn = Db.Rs("Select Schedule_ID, B.Employee_Full_Name, Schedule_Role_ID, Schedule_Date from TBL_Schedule_Kalendar A left join TBL_Employee B on A.Employee_ID=B.Employee_ID where Schedule_ID = '" + Cf.Int(id) + "'");
        
        if (chn.Rows.Count > 0)
        {
            Nama_karyawan.Text = chn.Rows[0]["Employee_Full_Name"].ToString();
            ddl_jadwal.SelectedValue = chn.Rows[0]["Schedule_Role_ID"].ToString();

            DateTime tgl = Convert.ToDateTime(chn.Rows[0]["Schedule_Date"].ToString());
            txt_tanggal.Text = tgl.ToString("dd MMMM yyyy");

            //DateTime start = Convert.ToDateTime(chn.Rows[0]["Schedule_Role_JamMasuk"].ToString());
            //DateTime end = Convert.ToDateTime(chn.Rows[0]["Schedule_Role_JamKeluar"].ToString());

            //string mulai = start.ToString("HH:mm");
            //string selesai = end.ToString("HH:mm");

            //Kode_Kehadiran.Text = chn.Rows[0]["Schedule_Role_Code"].ToString();
            //nama_kehadiran.Text = chn.Rows[0]["Schedule_Role_Nama"].ToString();
            //toleransi_telat.Text = chn.Rows[0]["Schedule_Role_Toleransi_Telat"].ToString();
            //Jumlah_jam_istirahat.Text = chn.Rows[0]["Schedule_Role_Durasi_Istirahat"].ToString();
            //jam_mulai.Text = mulai.ToString();
            //jam_selesai.Text = selesai.ToString();
        }
    }
    protected bool valid
    {
        get
        {
            bool x = true;
            //x = Fv.isComplete(Kode_Kehadiran) ? x : false;
            //x = Fv.isComplete(nama_kehadiran) ? x : false;
            //x = Fv.isComplete(toleransi_telat) ? x : false;
            //x = Fv.isComplete(Jumlah_jam_istirahat) ? x : false;
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
                DataTable chn = Db.Rs("Select Schedule_ID, B.Employee_Full_Name, Schedule_Role_ID, Schedule_Date, A.Employee_ID from TBL_Schedule_Kalendar A left join TBL_Employee B on A.Employee_ID=B.Employee_ID where Schedule_ID = '" + Cf.Int(id) + "'");
                DateTime tgl = Convert.ToDateTime(chn.Rows[0]["Schedule_Date"].ToString());

                //string history_update = Cf.Int(ddl_employee.SelectedValue) + " -> " + Cf.Int(chn.Rows[0]["Employee_ID"].ToString());
                string history_update = Cf.Int(chn.Rows[0]["Employee_ID"].ToString()) + " -> " + Cf.Int(ddl_employee.SelectedValue);

                string update_employee_schedule = "update TBL_Schedule_Kalendar set Schedule_Role_ID = '" + Cf.Int(ddl_jadwal.SelectedValue) + "', Schedule_Replace_ID = '" + Cf.Int(ddl_employee.SelectedValue) + "', Schedule_History_Update = '"+history_update+"', Schedule_UpdateDate = getdate(), Schedule_UpdateBy = '" + App.Employee_ID + "' where Schedule_ID = '" + Cf.Int(id) + "'";

                string update_employee_schedule_replacement = "update TBL_Schedule_Kalendar set Schedule_Role_ID = '" + chn.Rows[0]["Schedule_Role_ID"].ToString() + "', Schedule_Replace_ID = '" + Cf.Int(chn.Rows[0]["Employee_ID"].ToString()) + "', Schedule_History_Update = '" + history_update + "', Schedule_UpdateDate = getdate(), Schedule_UpdateBy = '" + App.Employee_ID + "' where Employee_ID = '" + Cf.Int(ddl_employee.SelectedValue) + "' and Schedule_Date = '" + tgl.ToString("yyyy-MM-dd") + "'";

                Db.Execute(update_employee_schedule);
                Db.Execute(update_employee_schedule_replacement);

                Response.Redirect(Param.Path_User + "department/kalender/kehadiran/");
                //}
            }
        }
    }

    protected void BtnCancel(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_User + "department/kalender/kehadiran/");
    } 
}