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

public partial class _admin_setup_organisasi_detail : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(id_get)))
                {
                    DataTable exist = Db.Rs("select * from TBL_Department where Department_ID = '" + id_get + "'");
                    if (exist.Rows.Count > 0)
                    {
                        department.Text = exist.Rows[0]["Department_Name"].ToString();
                        tbl_division();
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    public void tbl_division()
    {
        add.Text = "<a href=\"add/?id=" + id_get + "\" class=\"btn btn-primary\">Tambah Divisi</a>";

        StringBuilder x = new StringBuilder();

        DataTable rsa = Db.Rs("select * from TBL_Division where Department_ID = '" + Cf.StrSql(id_get) + "' order By Division_Name");
        if (rsa.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:80%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Nama Divisi</th>"
                            + "<th style=\"text-align:center;width:25%;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                string id = rsa.Rows[i]["Division_ID"].ToString();
                x.Append(
                        "<tr>"
                            + "<td style=\"text-align:center;width:5%;\">" + (i + 1) + "</td>"
                            + "<td>" + rsa.Rows[i]["Division_Name"].ToString() + "</td>"
                            + "<td style=\"text-align:center;\">"
                                        + "<a href=\"edit/?id=" + id_get + "&cn=" + id + "\" title=\"edit\" style=\"padding-right:10px;\"><i class=\"fa fa-pencil\"></i></a>"
										+ "<a onClick=\"confirmdel('delete/?id=" + id + "&cn=" + id_get + "');\" title=\"delete\" style=\"cursor:pointer;\"><i class=\"fa fa-trash\"></i></a>"
                            + "</td>"
                        + "</tr>");
            }
            x.Append("</tbody></table>");
            table.Text = x.ToString();
        }
        else
        {
            note.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
        }
        table.Text = x.ToString();
    }
}