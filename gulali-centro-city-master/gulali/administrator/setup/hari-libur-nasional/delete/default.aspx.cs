using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;

public partial class _holiday_delete : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                App.CekPageUser(this);
                App.ProtectPageGulali(this, "Setup >> Hari Libur", "delete");

                string before = "ID Hari Libur : " + id + "", after = "";
                try { App.LogHistory("Hapus : Hari Libur", before, after, ""); }
                catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

                Db.Execute("delete from TBL_Holiday where Holiday_ID = '" + Cf.StrSql(id) + "'");
                Response.Redirect(Param.Path_Admin + "setup/hari-libur-nasional/");
            }
            catch (Exception) { }
        }
    }
}