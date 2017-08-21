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

public partial class _admin_keuangan_pinjaman : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Keuangan >> Pinjaman", "Edit");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "keuangan/pinjaman/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            combo_employee();
        }

    }
    protected void combo_employee()
    {
        //EMPLOYEE
        ddl_employee.Items.Clear();
        DataTable rsa = Db.Rs("select * from TBL_Employee where Employee_ID != 1 order by Employee_Full_Name asc");
        if (rsa.Rows.Count > 0)
        {
            ddl_employee.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih Karyawan  --", "0"));
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_employee.Items.Add(new System.Web.UI.WebControls.ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int jumlah_bulan = Convert.ToInt32(ddl_cicilan_bulan.SelectedValue);

        DateTime date_start = DateTime.Now;
        string mulai = date_start.ToString("dd-MM-yyy");

        DateTime date_end = DateTime.Now.AddMonths(jumlah_bulan);
        string selesai = date_end.ToString("dd-MM-yyy");

        tanggal_mulai.Text = mulai;
        tanggal_selesai.Text = selesai;
    }
    protected void txt_pinjaman_Load(object sender, EventArgs e)
    {
        int nominal_pinjaman = Convert.ToInt32(txt_pinjaman.Text);
        int jumlah_bulan = Convert.ToInt32(ddl_cicilan_bulan.SelectedValue);

        int pembagi = nominal_pinjaman / jumlah_bulan;

        txt_jumlah_perbulan.Text = pembagi.ToString();
    }

    protected void save_Click(object sender, EventArgs e)
    {
        string id_karyawan = ddl_employee.SelectedValue;
        string cicilan_bulan = ddl_cicilan_bulan.SelectedValue;

        //convert date
        DateTime mulai = Convert.ToDateTime(tanggal_mulai.Text);
        DateTime selesai = Convert.ToDateTime(tanggal_selesai.Text);
        string tgl_mulai = mulai.ToString("yyyy-MM-dd").ToString();
        string tgl_selesai = selesai.ToString("yyyy-MM-dd").ToString();

        string jumlah_pinjaman = txt_pinjaman.Text;

        //jumlah cicilan
        int nominal_pinjaman = Convert.ToInt32(txt_pinjaman.Text);
        int jumlah_bulan = Convert.ToInt32(ddl_cicilan_bulan.SelectedValue);
        int pembagi = nominal_pinjaman / jumlah_bulan;
        string jumlah_cicilan = pembagi.ToString();

        string jumlah_terbilang = txt_terbilang.Text;
        string keperluan = txt_keperluan.Text;


        string insert = "Insert into TBL_Pinjaman (Employee_ID, Pinjaman_Lama_Bulan, Pinjaman_DateFrom, Pinjaman_DateTo, Pinjaman_Pokok, Pinjaman_Pokok_Terbilang, Pinjaman_Angsuran_Perbulan, Pinjaman_Keperluan, Pinjaman_CreateById, Pinjaman_DateCreate, Pinjaman_DateUpdate ) values('" + Cf.StrSql(id_karyawan) + "','" + Cf.Int(cicilan_bulan) + "','" + Cf.StrSql(tgl_mulai) + "','" + Cf.StrSql(tgl_selesai) + "','" + Cf.StrSql(jumlah_pinjaman) + "','" + Cf.StrSql(jumlah_terbilang) + "','" + Cf.StrSql(jumlah_cicilan) + "','" + Cf.StrSql(keperluan) + "','" + Cf.StrSql(App.Employee_ID) + "',getdate(),getdate())";

        Db.Execute2(insert);

        //update nomorperjanjian

        string tanggal1 = mulai.ToString("yyyy_MM_dd").ToString();
        string tanggal2 = selesai.ToString("yyyy_MM_dd").ToString();
      
        ////format nomor perjanjian = tanggal mulai + id karyawan + tanggal selesai
        string nomor_perjanjian = tanggal1 + "-" + id_karyawan + "-" + tanggal2;

        string update = "update TBL_Pinjaman set Pinjaman_No_Perjanjian = '" + Cf.StrSql(nomor_perjanjian) + "' where Employee_ID = '" + Cf.Int(id_karyawan) + "'";
            Db.Execute2(update);


            Response.Redirect("../");
    }
}