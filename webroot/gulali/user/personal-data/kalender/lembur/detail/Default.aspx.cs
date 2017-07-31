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

public partial class _user_kalender_cuti_add : System.Web.UI.Page
{
    protected string year { get { return App.GetStr(this, "year"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Lembur", "insert");

        //link_href.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/lembur/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            //Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            //ddl_employee_prev();
            //ddl_type_prev();

            int number;
            bool result = Int32.TryParse(Cf.StrSql(year), out number);
            bool result2 = Int32.TryParse(Cf.StrSql(month), out number);
            if (result && result2)
            {
                if (Fv.cekInt(Cf.StrSql(year)) && Fv.cekInt(Cf.StrSql(month)))
                {
                    DataTable exist = Db.Rs2("select List_ID, Employee_ID from TBL_Lembur_List where YEAR(List_Tanggal) = '"+year+"' and MONTH(List_Tanggal) = '"+month+"' and Employee_ID = '" + App.Employee_ID + "'");
                    if (exist.Rows.Count > 0)
                    {
                        preview("1", pagesum.Text);
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    public void preview(string num, string sum)
    {
        //preview table list lembur
        DataTable rs = Db.Rs2("select List_ID, Employee_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai from TBL_Lembur_List where YEAR(List_Tanggal) = '" + year + "' and MONTH(List_Tanggal) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "' order by List_Tanggal asc");

        DataTable tbl_lembur = Db.Rs2("select Lembur_DatePeriode, Lembur_DateCreate, Employee_ID, Lembur_Remarks from TBL_Lembur where YEAR(Lembur_DatePeriode) = '" + year + "' and MONTH(Lembur_DatePeriode) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "'");

        DateTime dt_periode = Convert.ToDateTime(tbl_lembur.Rows[0]["Lembur_DatePeriode"].ToString());
        date_periode.Text = dt_periode.ToString("MMMM-yyyy");
        txt_desc.Text = tbl_lembur.Rows[0]["Lembur_Remarks"].ToString();

        if (rs.Rows.Count > 0)
        {
            StringBuilder x = new StringBuilder();
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
                    string id = rs.Rows[i]["List_ID"].ToString();
                    DateTime daydate = Convert.ToDateTime(rs.Rows[i]["List_Tanggal"].ToString());
                    string tanggal = daydate.ToString("dd");

                    DateTime start = Convert.ToDateTime(rs.Rows[i]["List_JamMulai"].ToString());
                    DateTime end = Convert.ToDateTime(rs.Rows[i]["List_JamSelesai"].ToString());

                    string jam_mulai = start.ToString("HH:mm");
                    string jam_selesai = end.ToString("HH:mm");

                    var hours = (end - start).TotalHours;

                    //int sum_hours = Convert.ToInt32(jam_selesai) - Convert.ToInt32(jam_mulai);
                    string jam_total = hours.ToString();

                    x.Append("<tr role=\"row\">"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td width=\"50%\">" + Cf.Upper(rs.Rows[i]["List_Deskripsi_Lembur"].ToString()) + "</td>"
                                    + "<td style=\"width:7%; text-align:center;\">" + Cf.StrSql(tanggal) + "</td>"
                                    + "<td style=\"width:10%; text-align:center;\">" + Cf.StrSql(jam_mulai) + "</td>"
                                    + "<td style=\"width:10%; text-align:center;\">" + Cf.StrSql(jam_selesai) + "</td>"
                                    + "<td style=\"width:10%; text-align:center;\">" + Cf.StrSql(jam_total) + "</td>"
                                + "</tr>");
                }
            }

            //x.Append("</tbody></table>");
            table.Text = x.ToString();
            tbl_padding.Visible = true;
        }
        else { table.Text = ""; tbl_padding.Visible = false; }
    }

    protected void previous2_Click(object sender, EventArgs e)
    {
        preview("1", pagesum.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        preview(a.ToString(), pagesum.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        preview("0", pagesum.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        preview(a.ToString(), pagesum.Text);
    }
    //protected void Submit_Click(object sender, EventArgs e)
    //{
    //    if (valid)
    //    {
    //        if (!Page.IsValid) { return; }
    //        else
    //        {
    //            //convert date
    //            DateTime Periode = Convert.ToDateTime(date_periode.Text);
    //            string bulan = Periode.ToString("yyyy-MM-dd").ToString();

    //            DataTable cek_lembur_periode = Db.Rs2("Select Lembur_DatePeriode, Employee_ID from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' And Lembur_DatePeriode = '" + Cf.StrSql(bulan) + "'");

    //            if (cek_lembur_periode.Rows.Count < 1)
    //            {
    //                string insert = "Insert into TBL_Lembur (Lembur_DatePeriode, Lembur_DateCreate, Employee_ID, Lembur_Remarks)"
    //                                    + "values('" + Cf.StrSql(bulan) + "', getdate(),'" + App.Employee_ID + "','" + Cf.StrSql(txt_desc.Text) + "')";
    //                Db.Execute2(insert);
                    
    //                //select max lembur_ID
    //                DataTable cek_max_id = Db.Rs2("Select MAX(Lembur_ID) as 'max_id' from TBL_Lembur");


    //                //Select List Lembur
    //                string year = Periode.ToString("yyyy").ToString();
    //                string month = Periode.ToString("MM").ToString();

    //                DataTable list_lembur = Db.Rs2("select List_ID from TBL_Lembur_List where YEAR(List_Tanggal) = '"+year+"' and MONTH(List_Tanggal) = '"+month+"' and Employee_ID = '" + App.Employee_ID + "'");

    //                //DataTable cek_lembur_periode = Db.Rs2("Select Lembur_DatePeriode, Employee_ID from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' And Lembur_DatePeriode = '" + Cf.StrSql(bulan) + "'");

    //                //string mulai = tanggal + " " + jam_mulai.Text;
    //                //string selesai = tanggal + " " + jam_selesai.Text;

    //                if (list_lembur.Rows.Count < 1)
    //                {
    //                    //string insert_list_lembur = "Insert into TBL_Lembur_List (Employee_ID, Lembur_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate)"
    //                    //                    + "values('" + App.Employee_ID + "','" + Cf.StrSql(cek_max_id.Rows[0]["max_id"].ToString() + "','" + Cf.StrSql(txt_desk_lembur.Text) + "','" + Cf.StrSql(tanggal) + "','" + Cf.StrSql(mulai) + "','" + Cf.StrSql(selesai) + "',getdate())";

    //                    string insert_list_lembur = "INSERT INTO TBL_Lembur_List (List_Deskripsi_Lembur, Employee_ID, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate) SELECT List_Deskripsi_Lembur, Employee_ID, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate FROM TBL_Lembur_List_Temp where Employee_ID = '"+App.Employee_ID+"'";

    //                    Db.Execute2(insert_list_lembur);

    //                    string del_lembur_list_temp = "delete from TBL_Lembur_List_Temp where Employee_ID = '" + App.Employee_ID + "'";
    //                    Db.Execute2(del_lembur_list_temp);

    //                    //show table
    //                    note.Text = "";
    //                    Response.Redirect("../");
    //                }
    //                else
    //                {
    //                    note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
    //                             + "<h4 class=\"page-title\">List Lembur " + date_periode.Text + " Sudah Ada!</h4>"
    //                         + "</div>"; return;
    //                }
    //            }
    //            else
    //            {
    //                note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
    //                         + "<h4 class=\"page-title\">Lembur Periode " + date_periode.Text + " Sudah Ada!</h4>"
    //                     + "</div>"; return;
    //            }
    //        }
    //    }
    //}
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../");
    }
}