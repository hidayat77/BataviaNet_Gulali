using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class _user_keuangan_pinjaman : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Keuangan >> Pinjaman", "view");

        tab_pinjaman.Text = "<a href=\"" + Param.Path_User + "personal-data/keuangan/pinjaman/\">Pinjaman</a>";
        tab_slip_gaji.Text = "<a href=\"" + Param.Path_User + "personal-data/keuangan/slip-gaji/\">Slip Gaji</a>";
    }
}