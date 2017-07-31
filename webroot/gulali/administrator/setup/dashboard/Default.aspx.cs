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

public partial class _admin_setup_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Dashboard", "view");
        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        preview_dash1(); preview_dash2(); preview_dash3();
    }

    protected void preview_dash1()
    {
        StringBuilder a = new StringBuilder();
        a.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Dashboard</th>"
                            + "<th style=\"text-align:center;\">Status</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        DataTable dash1 = Db.Rs("Select Dashboard_ID, Dashboard_Name, Dashboard_Desc, Dashboard_Status, Dashboard_User from TBL_Dashboard where Dashboard_User = '1'");
        if (dash1.Rows.Count > 0)
        {
            for (int i = 0; i < dash1.Rows.Count; i++)
            {
                string id = dash1.Rows[i]["Dashboard_Name"].ToString();

                a.Append("<tr>"
                        + "<td style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                        + "<td>"
                            + "<b>" + dash1.Rows[i]["Dashboard_Name"].ToString() + "</b>"
                            + "<br/>" + dash1.Rows[i]["Dashboard_Desc"].ToString()
                        + "</td>"
                        + "<td style=\"text-align:center;\">");

                if (dash1.Rows[i]["Dashboard_Status"].ToString() == "1") { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/ checked=\"checked\">"); }
                else { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/>"); }
                a.Append("</td></tr>");

            }
        }
        a.Append("</table>");
        dashboard_staff.Text = a.ToString();
    }

    protected void preview_dash2()
    {
        StringBuilder a = new StringBuilder();
        a.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Dashboard</th>"
                            + "<th style=\"text-align:center;\">Status</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        DataTable dash1 = Db.Rs("Select Dashboard_ID, Dashboard_Name, Dashboard_Desc, Dashboard_Status, Dashboard_User from TBL_Dashboard where Dashboard_User = '2'");
        if (dash1.Rows.Count > 0)
        {
            for (int i = 0; i < dash1.Rows.Count; i++)
            {
                string id = dash1.Rows[i]["Dashboard_Name"].ToString();

                a.Append("<tr>"
                        + "<td style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                        + "<td>"
                            + "<b>" + dash1.Rows[i]["Dashboard_Name"].ToString() + "</b>"
                            + "<br/>" + dash1.Rows[i]["Dashboard_Desc"].ToString()                           
                        + "</td>"
                        + "<td style=\"text-align:center;\">");

                if (dash1.Rows[i]["Dashboard_Status"].ToString() == "1") { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/ checked=\"checked\">"); }
                else { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/>"); }
                a.Append("</td></tr>");

            }
        }
        a.Append("</table>");
        dashboard_supervisor.Text = a.ToString();
    }

    protected void preview_dash3()
    {
        StringBuilder a = new StringBuilder();
        a.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Dashboard</th>"
                            + "<th style=\"text-align:center;\">Status</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        DataTable dash1 = Db.Rs("Select Dashboard_ID, Dashboard_Name, Dashboard_Desc, Dashboard_Status, Dashboard_User from TBL_Dashboard where Dashboard_User = '3'");
        if (dash1.Rows.Count > 0)
        {
            for (int i = 0; i < dash1.Rows.Count; i++)
            {
                string id = dash1.Rows[i]["Dashboard_Name"].ToString();

                a.Append("<tr>"
                        + "<td style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                        + "<td>"
                            + "<b>" + dash1.Rows[i]["Dashboard_Name"].ToString() + "</b>"
                            + "<br/>" + dash1.Rows[i]["Dashboard_Desc"].ToString()
                        + "</td>"
                        + "<td style=\"text-align:center;\">");

                if (dash1.Rows[i]["Dashboard_Status"].ToString() == "1") { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/ checked=\"checked\">"); }
                else { a.Append("<input name=\"" + dash1.Rows[i]["Dashboard_ID"].ToString().Replace(' ', '_') + "[0]\" type=\"checkbox\"/>"); }
                a.Append("</td></tr>");

            }
        }
        a.Append("</table>");
        dashboard_direksi.Text = a.ToString();
    }

    protected void update_module(string field, string Dashboard_ID, string p_allowed)
    {
        if (p_allowed == "on") { p_allowed = "1"; } else { p_allowed = "0"; }

        string sql = "UPDATE TBL_Dashboard SET "
            + "[" + field + "] = '" + p_allowed + "' "
            + " WHERE Dashboard_ID = '" + Dashboard_ID + "'";
        Db.Execute(sql);
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        string Dashboard_ID, p_allowed;
        string[] fld = { "Dashboard_Status" };
        var req = HttpContext.Current;
        DataTable rs = Db.Rs("Select * from TBL_Dashboard");
        foreach (DataRow row in rs.Rows) // Loop over the rows.
        {
            Dashboard_ID = row["Dashboard_ID"].ToString();
            DataTable cekmodul = Db.Rs("Select * from TBL_Dashboard where Dashboard_ID = '" + Dashboard_ID + "'");
            if (cekmodul.Rows.Count > 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    p_allowed = req.Request[Dashboard_ID.Replace(' ', '_') + "[" + i + "]"];
                    update_module(fld[i], Dashboard_ID, p_allowed);
                }
            }
        }
        Response.Redirect(Param.Path_Admin + "setup/dashboard/");
    }
}