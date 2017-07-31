﻿using System;
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

public partial class _setup_payroll_ptkp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "view");

        tab_komponen.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/payroll-component/\">Komponen</a>";
        tab_ptkp.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/ptkp/\">PTKP</a>";
        tab_progressive_tax.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/progressive-tax/\">Progressive Tax</a>";
        tab_parameter.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/parameter/\">Parameter</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack) { tbl_ptkp(); }

    }

    public void tbl_ptkp()
    {
        StringBuilder x = new StringBuilder();

        DataTable rsa = Db.Rs2("select * from TBL_PTKP order By PTKP_Status");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%;\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Keterangan</th>"
                            + "<th>Nilai</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                string id = rsa.Rows[i]["PTKP_ID"].ToString(), val = "";

                if (!string.IsNullOrEmpty(rsa.Rows[i]["PTKP_Value"].ToString()))
                {
                    decimal txtsalary = Convert.ToDecimal(rsa.Rows[i]["PTKP_Value"].ToString());
                    val = (txtsalary).ToString("#,##0");
                }

                x.Append(
                        "<tr>"
                            + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1) + "</td>"
                            + "<td><b>" + rsa.Rows[i]["PTKP_Status"].ToString() + "</b> (" + rsa.Rows[i]["PTKP_Description"].ToString() + ")</td>"
                            + "<td style=\"text-align:center;\">" + val + "</td>"
                            + "<td class=\"actions\" style=\"text-align:center;\">"
                                + "<a href=\"edit/?id=" + id + "\"><i class=\"fa fa-pencil\"></i></a>"
                            + "</td>"
                        );
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {
            table.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
        }
    }
}