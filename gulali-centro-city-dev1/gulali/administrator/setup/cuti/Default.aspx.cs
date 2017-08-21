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

        fill("1", pagesum.Text, search_text.Text);
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;vertical-align: middle;\">No</th>"
                            + "<th style=\"vertical-align: middle;\">Tipe</th>"
                            + "<th style=\"text-align:center;vertical-align: middle;\">Jumlah</th>"
                            + "<th style=\"text-align:center;vertical-align: middle;\">Lama Bekerja<br/> *bulan</th>"
                            + "<th style=\"text-align:center;vertical-align: middle;\">Order</th>"
                            + "<th style=\"text-align:center;vertical-align: middle;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

        string sql_where = " where 1 = 1";
        if (search != "") { sql_where += " and Type_Desc like '%" + search + "%' "; }

        DataTable rs = Db.Rs("select * from TBL_Leave_Type " + sql_where + " order by Type_Order asc");

        lable.Text = "";
        table.Visible = true;
        if (rs.Rows.Count > 0)
        {
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0") { pagenum.Text = num; }
                else { pagenum.Text = of.ToString(); }
                pagesum.Text = sum;
            }
            int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
            int per = Cf.Int(pagesum.Text);
            int ke = Cf.Int(pagenum.Text);
            c = ke - 1;
            d = c * per;
            ea = ke * per;

            count_page.Text = of.ToString();
            if (d < rs.Rows.Count) { dari = d + 1; if (d == 0) dari = 1; }
            if (ea < rs.Rows.Count) { sampai = ea; }
            else { sampai = rs.Rows.Count; }
            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {

                    string classes = "odd";
                    if (i % 2 == 0) { classes = "even"; }

                    x.Append("<tr class=\"" + classes + "\">"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td style=\"width:40%;\">" + rs.Rows[i]["Type_Desc"].ToString() + "</td>"
                                    + "<td style=\"width:10%;text-align:center;\">" + rs.Rows[i]["Type_Value"].ToString() + "</td>"
                                    + "<td style=\"width:10%;text-align:center;\">" + rs.Rows[i]["Type_Minimal_Working"].ToString() + "</td>"
                                    + "<td style=\"width:10%;text-align:center;\">"
                                        + "<select onchange=\"changeStatus('" + rs.Rows[i]["Type_ID"].ToString() + "','" + rs.Rows[i]["Type_Order"].ToString() + "',this.value);\">");

                                            for (int j = 1; j <= sampai; j++)
                                            {
                                                string selected = "";
                                                if (rs.Rows[i]["Type_Order"].ToString() == j.ToString())
                                                    selected = "selected=\"true\"";
                                                else
                                                    selected = "";

                                                x.Append("<option " + selected + ">" + j + "</option>");
                                            }
                                            x.Append("</select>"
                                    + "</td>"

                                    + "<td class=\"actions\" style=\"text-align: center;width:20%;\">"
                                        + "<a href=\"edit/?id=" + rs.Rows[i]["Type_ID"].ToString() + "\" style=\"padding-right:10px;\"><i class=\"fa fa-pencil\"></i></a>"
                                        + "<a onClick=\"confirmdel('delete/?id=" + rs.Rows[i]["Type_ID"].ToString() + "');\" style=\"padding-right:20px;cursor:pointer;\"><i class=\"fa fa-trash-o\"></i></a>"
                                    + "</td></tr>");

                }
                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            table.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous2_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        //if (a > 1)
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text, search_text.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
}