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

public partial class _admin_setup_posisi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "view");

        tab_perusahaan.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/perusahaan/\">Perusahaan</a>";
        tab_organisasi.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\">Organisasi</a>";
        tab_posisi.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/posisi/\">Posisi</a>";
        tab_peraturan_kantor.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/peraturan-kantor/\">Peraturan Kantor</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack) { preview(); }
    }

    public void preview()
    {
        StringBuilder x = new StringBuilder();
        DataTable rsa = Db.Rs("select Position_ID,Position_Name from TBL_Position order By Position_Name");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:80%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Posisi</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                x.Append(
                        "<tr>"
                            + "<td style=\"width:5%;text-align:center;\">" + (i + 1) + "</td>"
                            + "<td>" + rsa.Rows[i]["Position_Name"].ToString() + "</td>"
                            + "<td style=\"text-align:center;\">"
                                + "<a href=\"edit/?id=" + rsa.Rows[i]["Position_ID"].ToString() + "\" title=\"edit\" style=\"padding:10px;\"><i class=\"fa fa-pencil\"></i></a>"
                                + "<a onClick=\"confirmdel('delete/?id=" + rsa.Rows[i]["Position_ID"].ToString() + "');\" title=\"delete\" style=\"cursor:pointer;\"><i class=\"fa fa-trash\"></i></a>"
                            + "</td>"
                        );
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {

        }
        table.Text = x.ToString();
    }
}