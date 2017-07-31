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

public partial class _admin_surat_peringatan_detail : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected string cn { get { return App.GetStr(this, "cn"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Surat Peringatan", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/history/surat-peringatan/?id=" + id + "\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id) + "");
                nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();
                fill();
            }
        }
    }
    protected void fill()
    {
        DataTable rs = Db.Rs("select Warning_Type, B.Employee_Full_Name as 'Employee_Create', Warning_Remarks, Warning_FileTermination from TBL_Employee_Warning A left join TBL_Employee B on A.Employee_UserCreate = B.Employee_ID where A.Employee_ID =  " + id + " and A.Warning_ID = " + cn + "");

        if (rs.Rows.Count > 0)
        {
            ddl_type_warning.SelectedValue = rs.Rows[0]["Warning_Type"].ToString();
            remarks.Text = rs.Rows[0]["Warning_Remarks"].ToString();
            createby.Text = rs.Rows[0]["Employee_Create"].ToString();

            if (!string.IsNullOrEmpty(rs.Rows[0]["Warning_FileTermination"].ToString()))
            {
                file.Text = "<a href=\"/file-upload/surat-peringatan/" + rs.Rows[0]["Warning_FileTermination"].ToString() + "\" target=\"_blank\">" + rs.Rows[0]["Warning_FileTermination"].ToString() + "</a>";
            }
            else
            {
                file.Text = "<font style=\"color:red;\">Empty</font>";
            }
        }
    }
}