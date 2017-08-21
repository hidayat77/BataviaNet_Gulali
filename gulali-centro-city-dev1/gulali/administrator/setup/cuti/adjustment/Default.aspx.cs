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

public partial class _admin_setup_cuti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Cuti", "view");

        tab_master.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/\">Master</a>";
        tab_adj.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/adjustment\">Adjustment</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        tbl_filter();
    }
    public void tbl_filter()
    {
        StringBuilder x = new StringBuilder();
        DataTable rsa = Db.Rs("select distinct YEAR(balance_create_date) as 'year_' from TBL_History_Leave_Balance order by year_ desc");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:50%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                //+ "<th width=\"5%\" style=\"text-align:center;\">No</th>"
                            + "<th width=\"30%\">Year</th>"
                            + "<th width=\"45%\">Month</th>"
                            + "<th width=\"20%\" style=\"text-align:center;\">Detail</th>"
                        + "</tr>"
                    + "</thead>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {

                string id = rsa.Rows[i]["year_"].ToString();
                x.Append(
                        "<tr>"
                    //+ "<td>" + (i+1) + "</td>"
                        );
                DataTable tb = Db.Rs("select distinct DATENAME(MONTH, balance_create_date) as 'month_', MONTH(balance_create_date) as 'month_order' from TBL_History_Leave_Balance where YEAR(balance_create_date) = '" + id + "' order by month_order desc");
                if (tb.Rows.Count > 0)
                {

                    int a = 0;
                    for (int b = 0; b < tb.Rows.Count; b++)
                    {
                        //string bulan;
                        //DateTime date1 = Convert.ToDateTime(rs.Rows[i]["organization_start_date"].ToString());
                        //bulan = date1.ToString("MM");
                        //bulan.ToString("MMMM dd");
                        if (a == 0)
                        {
                            x.Append("<td rowspan=" + tb.Rows.Count + "\" style=\"vertical-align: middle; text-align:center;\"><b>" + rsa.Rows[i]["year_"].ToString() + "</b></td>");
                        }
                        x.Append("<td>" + tb.Rows[b]["month_"].ToString() + "</td>"
                                + "<td style=\"text-align: center\">"
                                    //+ "<ul class=\"list-nostyle list-inline\">"
                                    //    + "<li>"
                                    //        + "<a href=\"?mode=1&yr=" + rsa.Rows[i]["year_"].ToString() + "&mn=" + tb.Rows[b]["month_order"].ToString() + "\" class=\"icon-arrow-right\" title=\"detail\">&nbsp;</a>"
                                    //    + "</li>"
                                    //+ "</ul>"
                                    + "<a href=\"detail/?month=" + tb.Rows[b]["month_order"].ToString() + "&year=" + rsa.Rows[i]["year_"].ToString() + "\"padding-right:20px;\"><i class=\"fa fa-eye\"></i></a>"
                                + "</td>"
                            + "</tr>"
                        );
                        a++;
                    }
                }
                else
                {
                    x.Append("<td style=\"vertical-align: middle\"><b>" + rsa.Rows[i]["department_Name"].ToString() + "</b></td>"
                            + "<td>No Division</td>"
                            + "<td style=\"text-align: center\">"
                            + "<ul class=\"list-nostyle list-inline\">"
                                + "<li>"
                                    + "<a href=\"?mode=1&yr=" + rsa.Rows[i]["year_"].ToString() + "\" class=\"icon-arrow-right\" title=\"detail\">&nbsp;</a>"
                                + "</li>"
                            + "</ul>"
                        + "</td>");
                }
            }
            x.Append("</table>");
            table.Text = x.ToString();
            lable.Text = "";
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
        }
    }
}