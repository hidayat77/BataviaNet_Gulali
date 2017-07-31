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
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Slip Gaji", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/history/?id=" + id + "\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id) + "");
                nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();

                string encryptionPassword = Param.encryptionPassword;
                string id_emp = Cf.StrSql(id);
                string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                string original = Crypto.Decrypt(encrypted, encryptionPassword);

                combo_year(encrypted);
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void combo_year(string encrypted)
    {
        DataTable rs = Db.Rs2("select distinct YEAR(Payroll_PeriodTo) as 'year_' from TBL_Payroll where Payroll_EmployeeID='" + encrypted + "' order by year_ desc");
        DataTable max = Db.Rs2("select max(YEAR(Payroll_PeriodTo)) as 'year_' from TBL_Payroll where Payroll_EmployeeID='" + encrypted + "' order by year_ desc");
        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string cb = rs.Rows[i]["year_"].ToString();
                ddl_year.Items.Add(new ListItem(cb, cb));
            }
            ddl_year.SelectedValue = max.Rows[0]["year_"].ToString();
            Salary_prev(ddl_year.SelectedValue);
        }
        else
        {
            ddl_year.Visible = false;
            table.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
        }
    }

    protected void load_year(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_year.SelectedValue);
        if (a.Equals(0)) { table.Visible = false; }
        else
        {
            table.Visible = true;
            Salary_prev(ddl_year.SelectedValue);
        }
    }

    protected void Salary_prev(string cb)
    {
        string encryptionPassword = Param.encryptionPassword;
        string id_emp = Cf.StrSql(id);
        string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
        string original = Crypto.Decrypt(encrypted, encryptionPassword);

        DataTable rs = Db.Rs2("select DATENAME(MONTH,Payroll_PeriodTo) AS 'bulan_nama', MONTH(Payroll_PeriodTo) AS 'bulan_number', Payroll_ID from TBL_Payroll where Payroll_EmployeeID='" + encrypted + "' and YEAR(Payroll_PeriodTo) = '" + cb + "'");
        if (rs.Rows.Count > 0)
        {
            StringBuilder x = new StringBuilder();
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string id_pay = rs.Rows[i]["Payroll_ID"].ToString();
                string sbrng = rs.Rows[i]["bulan_number"].ToString();

                x.Append("<div class=\"col-md-2\">"
                                + "<div class=\"text-center card-box\">"
                                    + "<div>"
                                        + "<img src=\"/assets/images/icons/receipt.png\" class=\"img-circle thumb-xl img-thumbnail m-b-10\" alt=\"profile-image\">"
                                        + "<a href=\"detail/?id=" + id + "&month=" + sbrng + "&year=" + cb + "\" style=\"background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;\">" + rs.Rows[i]["bulan_nama"].ToString() + "</a>"
                                    + "</div>"
                                + "</div>"
                            + "</div>");
            }
            x.Append("</table>");
            table.Text = x.ToString();
        }
        else
        {
            table.Visible = true;
            table.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
        }
    }
}