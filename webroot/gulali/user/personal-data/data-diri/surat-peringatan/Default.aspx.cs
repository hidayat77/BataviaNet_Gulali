﻿using System;
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

public partial class _user_data_pribadi_surat_peringatan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Data Pribadi >> Surat Peringatan", "view");

        tab_data_pribadi.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/data-pribadi/\">Data Pribadi</a>";
        tab_kontrak.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/kontrak/\">Kontrak</a>";
        tab_surat_peringatan.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/surat-peringatan/\">Surat Peringatan</a>";
        tab_pinjaman.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/pinjaman/\">Pinjaman</a>";
        tab_exit_clearance.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/exit-clearance/\">Exit Clearance</a>";

        if (!IsPostBack) { fill("1", pagesum.Text, search_text.Text); }
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();

        string sql_where = " where A.Employee_ID = '" + App.Employee_ID + "'";
        if (search != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "Type of Warning Letter")
            { sql_where += " and A.Warning_Type like '%" + search + "%'"; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Remarks")
            { sql_where += " and A.Warning_Remarks like '%" + search + "%'"; }
        }

        DataTable rs = Db.Rs("select convert(varchar,Warning_CreateDate,106) as Warning_CreateDate, Warning_ID, B.Employee_Full_Name , Warning_Type,  Warning_Remarks from TBL_Employee_Warning A left join TBL_Employee B on b.Employee_ID = A.Employee_ID " + sql_where + " order by Warning_CreateDate asc");
        if (rs.Rows.Count > 0)
        {
            x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Jenis Peringatan</th>"
                            + "<th>Catatan</th>"
                            + "<th>Tanggal Dibuat</th>"
                            + "<th style=\"text-align:center;\">Aksi</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");

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
                    string id_contract = rs.Rows[i]["Warning_ID"].ToString();

                    x.Append("<tr>"
                                + "<td style=\"text-align:center;\" width=\"5%\">" + (i + 1).ToString() + "</td>"
                                + "<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Warning_Type"].ToString() + "</td>"
                                + "<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Warning_Remarks"].ToString() + "</td>"
                                + "<td valign=\"top\" width=\"20%\">" + rs.Rows[i]["Warning_CreateDate"].ToString() + "</td>"
                                + "<td class=\"actions\" style=\"text-align: center;width:20%;\">"
                            + "<a href=\"detail/?id=" + id_contract + "\" style=\"padding-right:20px;\"><i class=\"fa fa-eye\"></i></a>"
                        + "</td>"
                    + "</tr>");
                }
                x.Append("</tbody></table>");

                table.Text = x.ToString();
                lable.Text = "";
                table.Visible = true;
            }
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            table.Visible = false;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
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