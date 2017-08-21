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

public partial class _admin_setup_general : System.Web.UI.Page
{
    protected string tanggal { get { return App.GetStr(this, "date"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Kehadiran", "view");

        link_href.Text = "<a href=\"" + Param.Path_User + "department/kalender/kehadiran/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        //tab_fulltime.Text = "<a href=\"" + Param.Path_Admin + "setup/kehadiran/fulltime/\">Full Time</a>";
        //tab_shift.Text = "<a href=\"" + Param.Path_Admin + "setup/kehadiran/shift/\">Shifting</a>";

        if (!IsPostBack) 
        {
            //int number;
            //bool result = Int32.TryParse(Cf.StrSql(tanggal), out number);
            //if (result)
            //{
            //    if (Fv.cekInt(Cf.StrSql(tanggal)))
            //    {
                    DateTime tgl = Convert.ToDateTime(tanggal);
                    showtanggal.Text = tgl.ToString("dd MMMM yyyy");
                    fill("1", pagesum.Text, search_text.Text); 
            //    }
            //    else { Response.Redirect("/gulali/page/404.aspx"); }
            //}
            //else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void fill(string num, string sum, string search)
    {
        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                + "<thead>"
                    + " <tr role=\"row\">"
                        + "<th style=\"text-align:center;\">No</th>"
                        + "<th style=\"text-align:center;\">Nama</th>"
                        + "<th style=\"text-align:center;\">Status Shift</th>"
                        + "<th style=\"text-align:center;\">Aksi</th>"
                    + "</tr>"
                + "</thead>"
                + "<tbody>");

        string sql_where = "";
        if (search != "") { sql_where += " and (Schedule_Role_Code like '%" + search + "%' or Schedule_Role_Nama like '%" + search + "%')"; }

        //DataTable rs = Db.Rs2("select Schedule_Role_ID, Schedule_Role_Code, Schedule_Role_Nama, Schedule_Role_Status, Schedule_Role_JamMasuk, Schedule_Role_JamKeluar from TBL_Schedule_Role where Schedule_Role_Status = 1 " + sql_where + " order By Schedule_Role_ID");

        DateTime tgl = Convert.ToDateTime(tanggal);
        string tahun = tgl.ToString("yyyy");
        string bulan = tgl.ToString("MM");
        string hari = tgl.ToString("dd");

        DataTable rs = Db.Rs2("select A.Schedule_ID, B.Employee_Full_Name, C.Schedule_Role_Nama from TBL_Schedule_Kalendar A left join " + Param.Db + ".dbo.TBL_Employee B on A.Employee_ID = B.Employee_ID Left Join TBL_Schedule_Role C on A.Schedule_Role_ID = C.Schedule_Role_ID where year(Schedule_Date) = '" + tahun + "' and MONTH(Schedule_Date) = '" + bulan + "' and day(Schedule_Date) = '" + hari + "' and B.Employee_DirectSpv = '"+App.Employee_ID+"' order By C.Schedule_Role_Nama asc");

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
                    string id = rs.Rows[i]["Schedule_ID"].ToString();

                    //DateTime start = Convert.ToDateTime(rs.Rows[i]["Schedule_Role_JamMasuk"].ToString());
                    //DateTime end = Convert.ToDateTime(rs.Rows[i]["Schedule_Role_JamKeluar"].ToString());

                    //string jam_mulai = start.ToString("HH:mm");
                    //string jam_selesai = end.ToString("HH:mm");

                    DateTime sekarang = DateTime.Now.AddDays(-1);
                    DateTime tanggal_schedule = Convert.ToDateTime(tanggal);

                    string outputs = "";
                    string tanggal_id = tanggal_schedule.ToString("dd-MM-yy");

                    if (tanggal_schedule < sekarang) { outputs = ""; div_add.Visible = false; }
                    else {
                        div_add.Visible = true;
                        outputs = "<a href=\"edit/?id=" + id + "\" style=\"padding-right:10px;\"><i class=\"fa fa-pencil\"></i></a>"
                                + "<a onClick=\"confirmdel('delete/?id=" + id + "');\" style=\"cursor:pointer;\"><i class=\"fa fa-trash\"></i></a>";
                    }

                    x.Append(
                            "<tr>"
                                + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1) + "</td>"
                                + "<td style=\"text-align:center;\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</td>"
                                + "<td style=\"text-align:center;\">" + rs.Rows[i]["Schedule_Role_Nama"].ToString() + "</td>"
                                + "<td class=\"actions\" style=\"text-align:center;\">"
                                    + "<a href=\"detail/?id=" + id + "\" style=\"padding-right:10px;\"><i class=\"fa fa-eye\"></i></a>"
                                    + outputs + "</td>"
                            );
                }
                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
            else
            {
                lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
                table.Visible = false;
            }
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
        try
        {
            int a = Cf.Int(pagenum.Text);
            a++;
            if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
            fill(a.ToString(), pagesum.Text, search_text.Text);
        }
        catch (Exception) { Response.Redirect("/gulali/page/404.aspx"); }
    }

    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text, search_text.Text);
    }

    protected void page_enter(object sender, EventArgs e)
    {
        try
        {
            int a = Cf.Int(pagenum.Text);
            if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
            fill(a.ToString(), pagesum.Text, search_text.Text);
        }
        catch (Exception) { Response.Redirect("/gulali/page/404.aspx"); }
    }
}