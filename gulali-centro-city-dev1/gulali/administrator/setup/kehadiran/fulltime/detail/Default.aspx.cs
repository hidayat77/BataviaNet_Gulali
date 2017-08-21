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
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class _admin_payroll_component_detail : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Fulltime", "Detail");
        if (!IsPostBack)
        {
            link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/kehadiran/fulltime/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

            txtJamMasuk.Text = Convert.ToString("hh:mm").ToString();
            txtJamKeluar.Text = Convert.ToString(DateTime.Now.TimeOfDay);
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {

                DataTable rs_fulltime = Db.Rs("Select * from TBL_Schedule_Role"
            + " where Schedule_Role_ID ='" + Cf.Int(id_get) + "'");
                if (rs_fulltime.Rows.Count > 0)
                {
                    txtKode.Text = rs_fulltime.Rows[0]["Schedule_Role_Code"].ToString();
                    txtNama.Text = rs_fulltime.Rows[0]["Schedule_Role_Nama"].ToString();
                    txtToleransiTelat.Text = rs_fulltime.Rows[0]["Schedule_Role_Toleransi_Telat"].ToString();
                    txtDurasi.Text = rs_fulltime.Rows[0]["Schedule_Role_Durasi_Istirahat"].ToString();
                    txtJamMasuk.Text = rs_fulltime.Rows[0]["Schedule_Role_JamMasuk"].ToString();
                    txtJamKeluar.Text = rs_fulltime.Rows[0]["Schedule_Role_JamKeluar"].ToString();
                }
                else
                {
                    Response.Redirect("/notFound/404.aspx");
                }
            }
            else
            {
                Response.Redirect("/notFound/404.aspx");
            }
        }

    }

}