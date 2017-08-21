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

public partial class _setup_payroll_lembur : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "view");

        tab_komponen.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/payroll-component/\">Komponen</a>";
        tab_ptkp.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/ptkp/\">PTKP</a>";
        tab_progressive_tax.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/progressive-tax/\">Progressive Tax</a>";
        tab_parameter.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/parameter/\">Parameter</a>";
        tab_lembur.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/lembur/\">Lembur</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack) { 
            tbl_Lembur_HariKerja(); 
            tbl_Lembur_HariLibur(); 
        }
    }

    public void tbl_Lembur_HariKerja()
    {
        StringBuilder x = new StringBuilder();

        DataTable rs_category = Db.Rs2("select Rumus_Category_ID, Rumus_Category_Title from TBL_Rumus_Lembur where Rumus_Category_Status = 0");
        if (rs_category.Rows.Count > 0)
        {
            for (int i = 0; i < rs_category.Rows.Count; i++)
            {
                x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                  + "<thead>"
                      + " <tr role=\"row\">"
                          + "<th colspan=\"3\" style=\"text-align:center;\">" + rs_category.Rows[i]["Rumus_Category_Title"].ToString() + "</th>"
                      + "</tr>"
                      + " <tr role=\"row\">"
                          + "<th style=\"text-align:center;\">No</th>"
                          + "<th>Keterangan</th>"
                          + "<th style=\"text-align:center;\">Aksi</th>"
                      + "</tr>"
                  + "</thead>"
                  + "<tbody>");
                string id = rs_category.Rows[i]["Rumus_Category_ID"].ToString();

                DataTable rs_List = Db.Rs2("select Rumus_List_ID, Rumus_List, Rumus_List_Description from TBL_Rumus_lembur_List where Rumus_Category_ID = " + id + "");
                if (rs_List.Rows.Count > 0)
                {
                    for (int ii = 0; ii < rs_List.Rows.Count; ii++)
                    {
                        string id_rumus = rs_List.Rows[ii]["Rumus_List_ID"].ToString();
                        x.Append(
                                "<tr>"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (ii + 1) + "</td>"
                                    + "<td><b>" + rs_List.Rows[ii]["Rumus_List"].ToString() + "</b> ( " + rs_List.Rows[ii]["Rumus_List_Description"].ToString() + " )</td>"
                                    + "<td class=\"actions\" style=\"text-align:center;\">"
                                        + "<a href=\"edit/?id=" + id_rumus + "\"><i class=\"fa fa-pencil\"></i></a>"
                                    + "</td>"
                                );
                    }
                }
            }
            x.Append("</tbody></table>");
            table_hari_kerja.Text = x.ToString();
        }
        else { table_hari_kerja.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>"; }
    }

    public void tbl_Lembur_HariLibur()
    {
        StringBuilder x = new StringBuilder();

        DataTable rs_category = Db.Rs2("select Rumus_Category_ID, Rumus_Category_Title from TBL_Rumus_Lembur where Rumus_Category_Status = 1");
        if (rs_category.Rows.Count > 0)
        {
            for (int i = 0; i < rs_category.Rows.Count; i++)
            {
                x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                  + "<thead>"
                      + " <tr role=\"row\">"
                          + "<th colspan=\"3\" style=\"text-align:center;\">" + rs_category.Rows[i]["Rumus_Category_Title"].ToString() + "</th>"
                      + "</tr>"
                      + " <tr role=\"row\">"
                          + "<th style=\"text-align:center;\">No</th>"
                          + "<th>Keterangan</th>"
                          + "<th style=\"text-align:center;\">Aksi</th>"
                      + "</tr>"
                  + "</thead>"
                  + "<tbody>");
                string id = rs_category.Rows[i]["Rumus_Category_ID"].ToString();

                DataTable rs_List = Db.Rs2("select Rumus_List_ID, Rumus_List, Rumus_List_Description from TBL_Rumus_lembur_List where Rumus_Category_ID = " + id + "");
                if (rs_List.Rows.Count > 0)
                {
                    for (int ii = 0; ii < rs_List.Rows.Count; ii++)
                    {
                        string id_rumus = rs_List.Rows[ii]["Rumus_List_ID"].ToString();
                        x.Append(
                                "<tr>"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (ii + 1) + "</td>"
                                    + "<td><b>" + rs_List.Rows[ii]["Rumus_List"].ToString() + "</b> ( " + rs_List.Rows[ii]["Rumus_List_Description"].ToString() + " )</td>"
                                    + "<td class=\"actions\" style=\"text-align:center;\">"
                                        + "<a href=\"edit/?id=" + id_rumus + "\"><i class=\"fa fa-pencil\"></i></a>"
                                    + "</td>"
                                );
                    }
                }
            }
            x.Append("</tbody></table>");
            table_hari_libur.Text = x.ToString();
        }
        else { table_hari_libur.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>"; }
    }
}