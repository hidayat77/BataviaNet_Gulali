using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
using AjaxControlToolkit;

public partial class usercontrol_menu : System.Web.UI.UserControl
{
    protected string Submenu = "", Menu = "";
    public string moduleSub { get { return Submenu; } set { Submenu = value; } }
    public string moduleMenu { get { return Menu; } set { Menu = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {

        string path_dashboard = "";
        if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path_dashboard = Param.Path_Admin + "dashboard/"; } else if (App.UserIsAdmin == "3") { path_dashboard = Param.Path_User + "dashboard/"; }

        dashboard.Text = "<li id=\"dashboard\" class=\"" + (Menu == "dashboard" ? "active" : "") + "\">"
                        + "<a href=\"" + path_dashboard + "\"><i class=\"zmdi zmdi-home\"></i><span>Dashboard </span></a>"
                     + "</li>";

        if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2")
        {
            data_karyawan.Text = "<li id=\"data-karyawan\" class=\"" + (Menu == "data-karyawan" ? "active" : "") + "\">"
                                        + "<a href=\"" + Param.Path_Admin + "data-karyawan/\"><i class=\"zmdi zmdi-face\"></i><span>Data Karyawan </span></a>"
                                        // tidak digunakan, duplikat dengan yang ada di riwayat. akan dipindah ke laporan
                                        /*+ "<ul class=\"submenu\">"
                                            + "<li>"
                                                   + "<ul>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/data-karyawan/\">Data Karyawan</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/pinjaman/\">Pinjaman</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/gaji/\">Gaji</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/exit-clearance/\">Exit Clearance</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/kontrak/\">Kontrak</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/surat-peringatan/\">Surat Peringatan</a></li>"
                                                       + "<li><a href=\"" + Param.Path_Admin + "data-karyawan/slip-gaji/\">Slip Gaji</a></li>"
                                                   + "</ul>"
                                             + "</li>"
                                           + "</ul>"*/
                                       + "</li>";

            kalender.Text = "<li id=\"kalender\" class=\"has-submenu " + (Menu == "kalender" ? "active" : "") + "\">"
                              + "<a href=\"#\"><i class=\"zmdi zmdi-calendar-check\"></i><span>Kalender </span></a>"
                              + "<ul class=\"submenu\">"
                                + "<li><a href=\"" + Param.Path_Admin + "kalender/cuti/\">Cuti</a></li>"
                                + "<li><a href=\"" + Param.Path_Admin + "kalender/lembur/\">Lembur</a></li>"
                                + "<li><a href=\"" + Param.Path_Admin + "kalender/kehadiran/\">Kehadiran</a></li>"
                              + "</ul>"
                            + "</li>";

            keuangan.Text = "<li id=\"keuangan\" class=\"has-submenu " + (Menu == "keuangan" ? "active" : "") + "\">"
                                        + "<a href=\"#\"><i class=\"zmdi zmdi-money-box\"></i><span>Keuangan </span></a>"
                                        + "<ul class=\"submenu\">"
                                            + "<li><a href=\"" + Param.Path_Admin + "keuangan/pinjaman/\">Pinjaman</a></li>"
                                            + "<li><a href=\"" + Param.Path_Admin + "keuangan/payroll/\">Payroll</a></li>"
                                        + "</ul>"
                                     + "</li>";

            laporan.Text = "<li id=\"laporan\" class=\"" + (Menu == "laporan" ? "active" : "") + "\">"
                                + "<a href=\"" + Param.Path_Admin + "laporan/\"><i class=\"zmdi zmdi-file-text\"></i><span>Laporan </span></a>"
                              + "</li>";

            setup.Text = "<li id=\"setup\" class=\" " + (Menu == "setup" ? "active" : "") + "\">"
                                + "<a href=\"" + Param.Path_Admin + "setup/\"><i class=\"zmdi zmdi-settings\"></i><span>Pengaturan</span></a>"
                             + "</li>";
        }
        else
        {
            personal_data.Text = "<li id=\"personal_data\" class=\"has-submenu " + (Menu == "personal_data" ? "active" : "") + "\">"
                                        + "<a href=\"#\"><i class=\"zmdi zmdi-face\"></i><span>Data Pribadi </span></a>"
                                        + "<ul class=\"submenu\">"
                                            + "<li>"
                                                   + "<ul>"
                                                       + "<li><a href=\"" + Param.Path_User + "personal-data/data-diri/data-pribadi/\">Data Pribadi</a></li>"
                                                       + "<li><a href=\"" + Param.Path_User + "personal-data/kalender/cuti/\">Kalender</a></li>"
                                                       + "<li><a href=\"" + Param.Path_User + "personal-data/keuangan/pinjaman\">Keuangan</a></li>"
                                                   + "</ul>"
                                             + "</li>"
                                           + "</ul>"
                                       + "</li>";

            
            DataTable checksupervisor = Db.Rs("Select Employee_ID, Employee_DirectSPV from TBL_Employee WHERE Employee_DirectSpv = '" + Cf.StrSql(App.Employee_ID) + "'");
            if (checksupervisor.Rows.Count > 0)
            {
                department.Text = "<li id=\"department\" class=\"has-submenu " + (Menu == "department" ? "active" : "") + "\">"
                                  + "<a href=\"#\"><i class=\"zmdi zmdi-calendar-check\"></i><span>Department </span></a>"
                                  + "<ul class=\"submenu\">"
                                    + "<li><a href=\"" + Param.Path_User + "department/kalender/cuti/\">Kalender</a></li>"
                                    //+ "<li><a href=\"" + Param.Path_User + "department/keuangan/\">Keuangan</a></li>"
                                  + "</ul>"
                                + "</li>";
            }
        }

        if (!IsPostBack)
        {
        }
    }
}