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

public partial class _admin_data_karyawan_history : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string cn { get { return App.GetStr(this, "cn"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Kontrak", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/history/kontrak/?id=" + id + "\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                fill();
                DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id) + "");
                nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();
            }
        }
    }
    protected void fill()
    {
        DataTable rs = Db.Rs("select ht.Position_Name, convert(varchar,Contract_StartPeriode,105) as Contract_StartPeriode, convert(varchar,Contract_EndPeriode,105) as Contract_EndPeriode, Contract_Title, hc.Employee_ID, remarks, Contract_FileUpload, Contract_Remarks from TBL_Employee_Contract hc join TBL_Position ht on hc.Contract_Title = ht.Position_ID join TBL_Employee he on he.Employee_ID = hc.Contract_UserCreate where hc.Employee_ID = " + id + " and Contract_ID = " + cn + "");

        if (rs.Rows.Count > 0)
        {
            //convert datetime
            string Join_Conv = rs.Rows[0]["Contract_StartPeriode"].ToString();
            string Resign_Conv = rs.Rows[0]["Contract_EndPeriode"].ToString();

            DateTime Join = Convert.ToDateTime(Join_Conv);
            DateTime Resign = Convert.ToDateTime(Resign_Conv);
            
            contract_start.Text = Join.ToString("dd MMMM yyyy");
            contract_end.Text = Resign.ToString("dd MMMM yyyy");
            title_lable.Text = rs.Rows[0]["Position_Name"].ToString();
            if (!string.IsNullOrEmpty(rs.Rows[0]["Contract_FileUpload"].ToString()))
            {
                file.Text = "<a href=\"/file-upload/kontrak/" + rs.Rows[0]["Contract_FileUpload"].ToString() + "\" target=\"_blank\">" + rs.Rows[0]["Contract_FileUpload"].ToString() + "</a>";
            }
            else
            {
                file.Text = "<font style=\"color:red;\">Empty</font>";
            }
            remarks.Text = rs.Rows[0]["Contract_Remarks"].ToString();
        }
    }
}