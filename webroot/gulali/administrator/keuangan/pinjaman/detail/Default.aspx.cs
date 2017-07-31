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
        App.ProtectPageGulali(this, "Keuangan >> Pinjaman", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "keuangan/pinjaman/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            combo_employee();

            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id)))
                {
                    DataTable exist = Db.Rs2("select Pinjaman_ID from TBL_Pinjaman where Pinjaman_ID = '" + id + "'");
                    if (exist.Rows.Count > 0)
                    {
                        view(id);
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }

    }

    protected void view (string id)
    {
        DataTable view_data = Db.Rs2("select Pinjaman_ID, Employee_ID, Pinjaman_Lama_Bulan, Pinjaman_DateFrom, Pinjaman_DateTo, Pinjaman_Pokok, Pinjaman_Pokok_Terbilang, Pinjaman_Angsuran_Perbulan, Pinjaman_Keperluan, Pinjaman_CreateById, Pinjaman_DateCreate, Pinjaman_DateUpdate from TBL_Pinjaman where Pinjaman_ID = '" + id + "'");

        ddl_employee.SelectedValue = view_data.Rows[0]["Employee_ID"].ToString();
        ddl_cicilan_bulan.SelectedValue = view_data.Rows[0]["Pinjaman_Lama_Bulan"].ToString();

        tanggal_mulai.Text = view_data.Rows[0]["Pinjaman_DateFrom"].ToString();
        tanggal_selesai.Text = view_data.Rows[0]["Pinjaman_DateTo"].ToString();
        txt_pinjaman.Text = view_data.Rows[0]["Pinjaman_Pokok"].ToString();
        txt_terbilang.Text = view_data.Rows[0]["Pinjaman_Pokok_Terbilang"].ToString();

        txt_jumlah_perbulan.Text = view_data.Rows[0]["Pinjaman_Angsuran_Perbulan"].ToString();
        txt_keperluan.Text = view_data.Rows[0]["Pinjaman_Keperluan"].ToString();
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
}