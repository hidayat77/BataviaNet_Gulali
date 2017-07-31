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

public partial class _data_karyawan_history : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Data Karyawan", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id_get) + "");
        nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();

        href_kontrak.Text = "<a href=\"kontrak/?id=" + Cf.StrSql(id_get) + "\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;\">Kontrak</a>";
        href_surat_peringatan.Text = "<a href=\"surat-peringatan/?id=" + Cf.StrSql(id_get) + "\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 10px; color: white !important;\">Surat Peringatan</a>";
        href_pinjaman.Text = "<a href=\"pinjaman/\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;\">Pinjaman</a>";
        href_gaji.Text = "<a href=\"slip-gaji/?id=" + Cf.StrSql(id_get) + "\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;\">Slip Gaji</a>";
        href_exit_clearance.Text = "<a href=\"exit-clearance/\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;\">Exit Clearance</a>";
    }
}