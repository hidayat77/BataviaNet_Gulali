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

public partial class _user_exit_clearance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Data Pribadi >> Exit Clearance", "view");

        tab_data_pribadi.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/data-pribadi/\">Data Pribadi</a>";
        tab_kontrak.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/kontrak/\">Kontrak</a>";
        tab_surat_peringatan.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/surat-peringatan/\">Surat Peringatan</a>";
        tab_pinjaman.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/pinjaman/\">Pinjaman</a>";
        tab_exit_clearance.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/exit-clearance/\">Exit Clearance</a>";
    }
}