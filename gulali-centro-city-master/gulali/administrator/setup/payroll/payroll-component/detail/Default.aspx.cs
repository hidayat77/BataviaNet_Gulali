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
        App.ProtectPageGulali(this, "Setup >> Payroll", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/payroll-component/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id_get)))
                {
                    tbl_component_list(); tbl_people_list();
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    public void tbl_people_list()
    {
        StringBuilder x = new StringBuilder();
        DataTable rsa = Db.Rs("select r.Admin_Role,h.Employee_Full_Name, h.employee_id from TBL_Employee h left join TBL_Employee_Payroll b on h.Employee_ID = b.Payroll_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role r on b.Payroll_Group=r.role_id where r.Admin_Role is not null and r.role_id='" + Cf.StrSql(id_get) + "'");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Employee " + Cf.UpperFirst(rsa.Rows[0]["admin_role"].ToString()) + "</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                string id = rsa.Rows[i]["Employee_id"].ToString();
                x.Append("<tr>"
                            + "<td style=\"text-align:center;\">" + (i + 1) + "</td>"
                            + "<td>" + rsa.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                        + "</tr>");
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else { note2.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>"; }
    }

    public void tbl_component_list()
    {
        StringBuilder x = new StringBuilder();

        DataTable data_component = Db.Rs2("select component_type, component_name, privilege_value from TBL_Payroll_Role a join TBL_Payroll_Privilege b on a.Admin_Role = b.Admin_Role join TBL_MasterComponent c on b.Component_ID = c.component_id where a.Admin_Role is not null and role_id='" + Cf.StrSql(id_get) + "' and b.Privilege_Choose = '1' order by component_type asc");

        if (data_component.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"border: 1px solid #dddddd;\" >"
                        + "<thead>"
                                + " <tr role=\"row\">"
                                    + "<th style=\"text-align:center;\">No</th>"
                                    + "<th>Type</th>"
                                    + "<th>Name</th>"
                                    + "<th style=\"text-align:center;\">Value</th>"

                                + "</tr>"
                        + "</thead>"
                        + "<tbody>");

            for (int i = 0; i < data_component.Rows.Count; i++)
            {
                x.Append("<tr>"
                             + "<td style=\"text-align:center;vertical-align: middle;\" class=\"field_name\">" + (i + 1).ToString() + "</td>"
                             + "<td style=\"vertical-align: middle;\" class=\"field_name\"><strong>" + data_component.Rows[i]["component_type"].ToString() + "</strong></td>"
                             + "<td style=\"vertical-align: middle;\" class=\"field_name\"><strong>" + data_component.Rows[i]["component_name"].ToString() + "</strong></td>"
                             + "<td style=\"text-align:center;\">" + data_component.Rows[i]["privilege_value"].ToString() + "</td>"
                         + "</tr>");
            }

            x.Append("</tbody></table>");

            list_component.Text = x.ToString();
        }
        else { note1.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>"; }
    }
}