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

public partial class _admin_payroll_component_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "insert");
        if (!IsPostBack)
        {
            txtJamMasuk.Text = Convert.ToString("hh:mm").ToString();
            txtJamKeluar.Text = Convert.ToString(DateTime.Now.TimeOfDay);
            
        }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(txtKode) ? x : false;
            return x;
        }
    }

    protected void Submit1_Click(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                //DataTable chn = Db.Rs2("Select * from TBL_Kehadiran_Role where Kehadiran_Role_Code = '" + Cf.StrSql(txtKode.Text) + "', Kehadiran_Role_Nama = '" + Cf.StrSql(txtNama.Text) + "'");
                //if (chn.Rows.Count > 0)
                //{
                //    return;
                //}
                //else
                //{
                string insert = "Insert into TBL_Schedule_Role (Schedule_Role_Code, Schedule_Role_Nama, Schedule_Role_Status, Schedule_Role_Toleransi_Telat, Schedule_Role_Durasi_Istirahat,Schedule_Role_UpdateBy,Schedule_Role_CreateDate,Schedule_Role_JamMasuk, Schedule_Role_JamKeluar)"
                        + "values('" + Cf.StrSql(txtKode.Text) + "','" + Cf.StrSql(txtNama.Text) + "',0,'" + Cf.StrSql(txtToleransiTelat.Text) + "','" + Cf.StrSql(txtDurasi.Text) + "','" + Cf.StrSql(App.Employee_ID) + "',getdate(),'" + Cf.StrSql(txtJamMasuk.Text) + "','" + Cf.StrSql(txtJamKeluar.Text) + "')";
                    Db.Execute(insert);
                    
                    Response.Redirect(Param.Path_Admin + "setup/kehadiran/fulltime/");
                

                /*Start Here :)*/
                //string insert_flag = "insert into TBL_Payroll_Flag(Flag_Month, Flag_Year, Flag_ForMonth, Flag_TotalGaji, Flag_DateCreate)"
                //   + "values('" + Cf.StrSql(To_Conv_Month) + "', '" + Cf.StrSql(To_Conv_Year) + "',  '" + Cf.StrSql(ddl_bulan.SelectedValue) + "', 0, getdate())";
                //Db.Execute2(insert_flag);
                /*End Here :)*/


            }
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/kehadiran/fulltime/");
    }
}