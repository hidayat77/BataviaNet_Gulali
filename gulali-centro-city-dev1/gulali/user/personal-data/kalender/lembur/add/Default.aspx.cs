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
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Lembur", "insert");

        //link_href.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/lembur/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            Cancel.OnClientClick = string.Format("return confirm('Apakah Anda Yakin? Jika anda keluar maka data akan Hilang');", "kodeee");
            //ddl_employee_prev();
            //ddl_type_prev();
            preview("1", pagesum.Text);
        }
    }

    public void preview(string num, string sum)
    {

        //preview table list lembur
        DataTable rs = Db.Rs2("select List_ID, Employee_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai from TBL_Lembur_List_Temp where Employee_ID = '" + App.Employee_ID + "' order by List_Tanggal asc");

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
                                    + "<td class=\"actions\" style=\"text-align: center;width:8%;\">"
                                         + "<a onClick=\"confirmdel('delete_list/?id=" + id + "');\"><i class=\"fa fa-trash-o\" style=\"color:#ff5b5b;cursor:pointer;\"></i></a>"
                                    + "</td>"
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

    protected void ddl_day_show(int tahun, int bulan)
    {
        //Dropdown Tanggal Pada bulan dan tahun terpilih
        int days = DateTime.DaysInMonth(tahun, bulan);
        for (int day = 1; day <= days; day++)
        {
            //yield return new DateTime(tahun, bulan, day);
            string hari = Convert.ToString(day);
            ddl_day.Items.Add(new ListItem(hari, hari));
        }
    }

    protected void date_periode_TextChanged(object sender, EventArgs e)
    {
        DateTime Periode = Convert.ToDateTime(date_periode.Text);
        int tahun = Convert.ToInt32(Periode.ToString("yyyy").ToString());
        int bulan = Convert.ToInt32(Periode.ToString("MM").ToString());
        ddl_day_show(tahun, bulan);
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isTgl(date_periode) ? x : false;
            if (string.IsNullOrEmpty(date_periode.Text))
            {
                note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                + "<h4 class=\"page-title\">Lengkapi Data Anda!</h4>"
                            + "</div>";
            }
            return x;
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!Page.IsValid) { return; }
            else
            {
                //convert date
                DateTime Periode = Convert.ToDateTime(date_periode.Text);
                string bulan = Periode.ToString("yyyy-MM-dd").ToString();

                DataTable cek_lembur_periode = Db.Rs2("Select Lembur_DatePeriode, Employee_ID from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' And Lembur_DatePeriode = '" + Cf.StrSql(bulan) + "'");

                if (cek_lembur_periode.Rows.Count < 1)
                {
                    string insert = "Insert into TBL_Lembur (Lembur_DatePeriode, Lembur_DateCreate, Employee_ID, Lembur_Remarks, Lembur_Status)"
                                        + "values('" + Cf.StrSql(bulan) + "', getdate(),'" + App.Employee_ID + "','" + Cf.StrSql(txt_desc.Text) + "','0')";
                    Db.Execute2(insert);
                    
                    //select max lembur_ID
                    DataTable cek_max_id = Db.Rs2("Select MAX(Lembur_ID) as 'max_id' from TBL_Lembur");


                    //Select List Lembur
                    string year = Periode.ToString("yyyy").ToString();
                    string month = Periode.ToString("MM").ToString();

                    DataTable list_lembur = Db.Rs2("select List_ID from TBL_Lembur_List where YEAR(List_Tanggal) = '"+year+"' and MONTH(List_Tanggal) = '"+month+"' and Employee_ID = '" + App.Employee_ID + "'");

                    //DataTable cek_lembur_periode = Db.Rs2("Select Lembur_DatePeriode, Employee_ID from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' And Lembur_DatePeriode = '" + Cf.StrSql(bulan) + "'");

                    //string mulai = tanggal + " " + jam_mulai.Text;
                    //string selesai = tanggal + " " + jam_selesai.Text;

                    if (list_lembur.Rows.Count < 1)
                    {
                        //string insert_list_lembur = "Insert into TBL_Lembur_List (Employee_ID, Lembur_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate)"
                        //                    + "values('" + App.Employee_ID + "','" + Cf.StrSql(cek_max_id.Rows[0]["max_id"].ToString() + "','" + Cf.StrSql(txt_desk_lembur.Text) + "','" + Cf.StrSql(tanggal) + "','" + Cf.StrSql(mulai) + "','" + Cf.StrSql(selesai) + "',getdate())";

                        string insert_list_lembur = "INSERT INTO TBL_Lembur_List (List_Deskripsi_Lembur, Employee_ID, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate) SELECT List_Deskripsi_Lembur, Employee_ID, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate FROM TBL_Lembur_List_Temp where Employee_ID = '"+App.Employee_ID+"'";

                        Db.Execute2(insert_list_lembur);

                        string del_lembur_list_temp = "delete from TBL_Lembur_List_Temp where Employee_ID = '" + App.Employee_ID + "'";
                        Db.Execute2(del_lembur_list_temp);

                        //show table
                        note.Text = "";
                        create_notif_lembur();
                    }
                    else
                    {
                        note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                 + "<h4 class=\"page-title\">List Lembur " + date_periode.Text + " Sudah Ada!</h4>"
                             + "</div>"; return;
                    }
                }
                else
                {
                    note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                             + "<h4 class=\"page-title\">Lembur Periode " + date_periode.Text + " Sudah Ada!</h4>"
                         + "</div>"; return;
                }
            }
        }
    }

    protected void create_notif_lembur()
    {
        DataTable check_max_notif = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");

        string notif;
        if (string.IsNullOrEmpty(check_max_notif.Rows[0]["maksi"].ToString())) { notif = "1"; }
        else { notif = check_max_notif.Rows[0]["maksi"].ToString(); }

        DataTable max_lembur = Db.Rs2("Select max(Lembur_ID) as Lembur_ID from TBL_Lembur");

        string max_lembur_get;
        if (string.IsNullOrEmpty(max_lembur.Rows[0]["Lembur_ID"].ToString())) { max_lembur_get = "1"; }
        else { max_lembur_get = max_lembur.Rows[0]["Lembur_ID"].ToString(); }

        //insert notif ke supervisor employee yang melakukan create leave request
        //select spv bersangkutan
        DataTable check_max_notif2 = Db.Rs("Select max(Notif_ID)+1 as maksi from TBL_Notification");

        DataTable spv = Db.Rs2("select he.Employee_ID, he.Employee_Full_Name, he.Employee_DirectSpv, Year(Lembur_DatePeriode) as 'year', month(Lembur_DatePeriode) as 'Month' from TBL_Lembur hl join " + Param.Db + ".dbo.TBL_Employee he on hl.Employee_ID = he.Employee_ID where Lembur_ID = '" + Cf.StrSql(max_lembur_get) + "'");

        string year = spv.Rows[0]["year"].ToString();
        string month = spv.Rows[0]["month"].ToString();

        string insert_notif_spv = "insert into TBL_Notification (Notif_Link, Notif_Employee_ID,  Notif_StatusPage, Notif_DateCreate, Notif_UserCreate, Notif_Remarks, Leave_ID, Lembur_ID)"
                        + "values('" + Param.Path_User + "department/kalender/lembur/detail/detail-lembur/?id=" + App.Employee_ID + "&cn=" + max_lembur_get + "&year=" + Cf.Int(year) + "&month=" + Cf.Int(month) + "&notif=" + check_max_notif2.Rows[0]["maksi"].ToString() + "', '" + spv.Rows[0]["Employee_DirectSpv"].ToString() + "', '0', getdate(), '" + Cf.StrSql(App.Employee_ID) + "', 'Create Lembur Request(User -> Supervisor)','0','" + max_lembur_get + "')";
        Db.Execute(insert_notif_spv);

        try
        {
            //send mail
            DataTable rs2 = Db.Rs("select * from TBL_MailConfig where mail_config_id ='1'");
            DataTable emailSPV = Db.Rs("select Employee_CompanyEmail from TBL_Employee where Employee_ID = '" + spv.Rows[0]["Employee_DirectSpv"] + "' ");
            DataTable leavemonitor = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = '" + Cf.StrSql(App.Employee_ID) + "'");
            string txtFrom = rs2.Rows[0]["mail_config_from"].ToString();
            string txtSMTPServer = rs2.Rows[0]["mail_config_smtp"].ToString();
            string txtTo = emailSPV.Rows[0]["Employee_CompanyEmail"].ToString();
            string txtSubject = "Leave Request";
            string txtBody = "<table cellpadding=5 cellspacing=0 border=0 style=\"font-family:arial;\">"
                                + "<tr><td>" + leavemonitor.Rows[0]["Employee_Full_Name"].ToString() + " Reviewed Leave Request(HRD -> Supervisor)</td></tr>"
                                + "<tr><td>" + Param.Domain + "/?id=1 </td></tr>"
                            + "</table>";

            string SMTPServer = txtSMTPServer;
            string SMTPUser = txtFrom;
            string SMTPPass = Param.SMTPPass;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SMTPServer);
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(SMTPUser);
            mail.To.Add(txtTo);
            mail.Subject = txtSubject;
            mail.Body = txtBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPass);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        catch (Exception) { Response.Redirect("../"); }

        Response.Redirect("../");
    }

    protected void Submit_rows_Click(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!Page.IsValid) { return; }
            else
            {
                //convert date
                DateTime Periode = Convert.ToDateTime(date_periode.Text);
                string bulan = Periode.ToString("yyyy-MM-dd").ToString();
                string tanggal = Periode.ToString("yyyy-MM").ToString() + " - " + ddl_day.SelectedValue;


                DataTable list_lembur_temp = Db.Rs2("select List_ID, Employee_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai from TBL_Lembur_List_Temp where Employee_ID = '" + App.Employee_ID + "' and List_Tanggal = '" + Cf.StrSql(tanggal) + "' order by List_Tanggal asc");

                //DataTable cek_lembur_periode = Db.Rs2("Select Lembur_DatePeriode, Employee_ID from TBL_Lembur where Employee_ID = '" + App.Employee_ID + "' And Lembur_DatePeriode = '" + Cf.StrSql(bulan) + "'");

                string mulai = tanggal + " " + jam_mulai.Text;
                string selesai = tanggal + " " + jam_selesai.Text;

                if (list_lembur_temp.Rows.Count < 1)
                {
                    string insert = "Insert into TBL_Lembur_List_Temp (Employee_ID, List_Deskripsi_Lembur, List_Tanggal, List_JamMulai, List_JamSelesai, List_DateCreate)"
                                        + "values('" + App.Employee_ID + "','" + Cf.StrSql(txt_desk_lembur.Text) + "','" + Cf.StrSql(tanggal) + "','" + Cf.StrSql(mulai) + "','" + Cf.StrSql(selesai) + "',getdate())";
                    Db.Execute2(insert);
                    note.Text = "";
                    //show table
                    date_periode.Enabled = false;
                    preview("1", pagesum.Text);
                }
                else
                {
                    note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                             + "<h4 class=\"page-title\">List Lembur "+ddl_day.SelectedValue+"-"+ date_periode.Text + " Sudah Ada!</h4>"
                         + "</div>"; return;
                }
            }
        }
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(date_periode.Text))
        {
            DateTime Periode = Convert.ToDateTime(date_periode.Text);
            string year = Periode.ToString("yyyy").ToString();
            string month = Periode.ToString("MM").ToString();

            //string tanggal = Periode.ToString("yyyy-MM").ToString() + " - " + ddl_day.SelectedValue;

            string del_lembur_list = "delete from TBL_Lembur_List_Temp where YEAR(List_Tanggal) = '" + year + "' and MONTH(List_Tanggal) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "'";
            Db.Execute2(del_lembur_list);

            //string del_lembur = "delete from TBL_Lembur where YEAR(Lembur_DatePeriode) = '" + year + "' and MONTH(Lembur_DatePeriode) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "'";
            //Db.Execute2(del_lembur);
            Response.Redirect("../");
        }
        else
        {
            Response.Redirect("../");
        }
    }
    protected void jam_selesai_TextChanged(object sender, EventArgs e)
    {
        DateTime start = Convert.ToDateTime(jam_mulai.Text);
        DateTime end = Convert.ToDateTime(jam_selesai.Text);

        TimeSpan diff = end - start;
        double hours = diff.TotalHours;
        jam_total.Text = hours.ToString();
    }
}