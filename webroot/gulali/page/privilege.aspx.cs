using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;

public partial class _notfound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        if (!IsPostBack)
        {
            string path = "";
            if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path = Param.Path_User; }
            link.Text = "<a class=\"btn btn-success waves-effect waves-light\" href=\"" + path + "/dashboard\">Kembali ke Dashboard</a>";
        }
    }
}
