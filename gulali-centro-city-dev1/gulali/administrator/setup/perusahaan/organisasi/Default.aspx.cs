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

public partial class _admin_setup_organisasi : System.Web.UI.Page
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

        if (!IsPostBack) { tbl_division(); }
    }

    public void tbl_division()
    {
        StringBuilder x = new StringBuilder();

        DataTable rsa = Db.Rs("select Department_ID,Department_Name from TBL_Department order By Department_Name");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:70%\">"
                    + "<thead>"
                        + " <tr>"
                            + "<th style=\"text-align:center;width:5%;\">No</th>"
                            + "<th >Nama Departemen</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                string id = rsa.Rows[i]["Department_ID"].ToString();

                x.Append("<tr>"
                            + "<td style=\"text-align:center;\">" + (i + 1) + "</td>"
                            + "<td><a href=\"detail/?id=" + id + "\" title=\"detail\">" + rsa.Rows[i]["Department_Name"].ToString() + "</a></td>"
                            + "<td style=\"text-align:center;\">"
                                        + "<a href=\"edit/?id=" + id + "\" title=\"edit\" style=\"padding-right:10px;\"><i class=\"fa fa-pencil\"></i></a>");
                DataTable cek_divisi = Db.Rs("Select Department_ID from TBL_Division where Department_ID = '" + Cf.StrSql(id) + "'");
                if (cek_divisi.Rows.Count < 1)
                {
                    x.Append("<li><a onClick=\"confirmdel('delete/?id=" + id + "');\" title=\"delete\" style=\"cursor:pointer;\"><i class=\"fa fa-trash\"></i></a></li>");
                }
                x.Append("</ul>"
                            + "</td>");
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