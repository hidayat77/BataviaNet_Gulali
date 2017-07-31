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

public partial class _user_kalender_cuti_detail : System.Web.UI.Page
{
    //protected string user { get { return App.GetStr(this, "user"); } }
    //protected string id { get { return App.GetStr(this, "id"); } }
    protected string cn { get { return App.GetStr(this, "cn"); } }
    protected string year { get { return App.GetStr(this, "year"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected string notif { get { return App.GetStr(this, "notif"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Cuti", "view");

        if (!IsPostBack)
        {
            int number;
            bool result = Int32.TryParse(Cf.StrSql(year), out number);
            bool result2 = Int32.TryParse(Cf.StrSql(month), out number);
            bool result3 = Int32.TryParse(Cf.StrSql(cn), out number);
            if (result && result2 && result3)
            {
                if (Fv.cekInt(Cf.StrSql(year)) && Fv.cekInt(Cf.StrSql(month)) && Fv.cekInt(Cf.StrSql(cn)))
                {
                    DataTable exist = Db.Rs2("select List_ID, Employee_ID from TBL_Lembur_List where YEAR(List_Tanggal) = '" + year + "' and MONTH(List_Tanggal) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "'");
                    if (exist.Rows.Count > 0)
                    {
                        if (notif != null && notif != "")
                        {
                            DataTable check_employee_login = Db.Rs("Select Notif_Employee_ID from TBL_Notification where Notif_ID = '" + Cf.StrSql(notif) + "'");
                            if (check_employee_login.Rows[0]["Notif_Employee_ID"].ToString() == App.Employee_ID)
                            {
                                string update = "update TBL_Notification set Notif_StatusPage = '1' where Notif_ID = '" + notif + "'";
                                Db.Execute(update);
                            }
                        }
                        preview("1", pagesum.Text);

                        //detail_leave();
                        detail_bio_leave();
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void detail_bio_leave()
    {
        try
        {
            DataTable rs = Db.Rs("select he.Employee_ID, Employee_Full_Name, Employee_Sum_LeaveBalance, Department_Name, Division_Name, Employee_Photo from TBL_Employee he left join TBL_Department hdep on he.Department_ID = hdep.Department_ID left join TBL_Division hdiv on he.Division_ID = hdiv.Division_ID  where Employee_ID = '" + App.Employee_ID + "'");
            full_name.Text = rs.Rows[0]["Employee_Full_Name"].ToString();
            department.Text = rs.Rows[0]["Department_Name"].ToString();
            division.Text = rs.Rows[0]["Division_Name"].ToString();

            DataTable total_lembur = Db.Rs2("SELECT Sum(DATEDIFF(hour, List_JamMulai ,List_JamSelesai)) as 'total_lembur' FROM TBL_Lembur_List where YEAR(List_Tanggal) = '" + year + "' and MONTH(List_Tanggal) = '" + month + "' and Employee_ID = '" + App.Employee_ID + "'");
            lembur_balanced.Text = total_lembur.Rows[0]["total_lembur"].ToString() +" Jam";
        }
        catch (Exception) { }
    }
    protected void BtnBack(object sender, EventArgs e)
    {
        Response.Redirect("../");
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

            DataTable rsb = Db.Rs2("Select l.Log_StatusLembur, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Lembur_Log l join "+ Param.Db + ".dbo.TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Lembur_ID = " + cn + " and Log_DecisionUserStatus = 'SPV' and Log_StatusLembur != '2'");
            DataTable rsc = Db.Rs2("Select l.Log_StatusLembur, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Lembur_Log l join " + Param.Db + ".dbo.TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Lembur_ID = " + cn + " and Log_DecisionUserStatus = 'SPV'");
            DataTable rsd = Db.Rs2("Select l.Log_StatusLembur, l.Log_Remarks, l.Log_DecisionUserStatus, e.Employee_Full_Name from TBL_Lembur_Log l join " + Param.Db + ".dbo.TBL_Employee e on l.Log_DecisionBy = e.Employee_ID where Lembur_ID = " + cn + " and Log_DecisionUserStatus = 'LM'");

            string status = "";
            if (rsb.Rows.Count > 0)
            {
                if (rsb.Rows[0]["Log_StatusLembur"].ToString() == "0") { status = "<font style=\"color:green;\">Approved by "; } else if (rsb.Rows[0]["Log_StatusLembur"].ToString() == "1") { status = "<font style=\"color:red;\">Rejected by "; } else { status = "<font style=\"color:red;\">Canceled by "; }

                if (!string.IsNullOrEmpty(rsb.Rows[0]["Log_DecisionUserStatus"].ToString()))
                {
                    div_spv.Visible = true;
                    div_spv_remarks.Visible = true;
                    h4_spv.Visible = true;
                    h4_spv_remarks.Visible = true;
                    spv.Text = "<b>" + status + rsb.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                    spv_remarks.Text = rsb.Rows[0]["Log_Remarks"].ToString();
                }
            }

            if (rsd.Rows.Count > 0)
            {
                string wording = "";
                if (rsd.Rows[0]["Log_StatusLembur"].ToString() == "0") { wording = "<font style=\"color:green;\">Reviewed by "; } else if (rsd.Rows[0]["Log_StatusLembur"].ToString() == "1") { wording = "<font style=\"color:red;\">Rejected by "; } else { wording = "<font style=\"color:red;\">Canceled"; }

                div_lm.Visible = true;
                div_lm_remarks.Visible = true;
                h4_lm.Visible = true;
                h4_lm_remarks.Visible = true;
                lm.Text = "<b>" + wording + rsd.Rows[0]["Employee_Full_Name"].ToString() + "</b>";
                lm_remarks.Text = rsd.Rows[0]["Log_Remarks"].ToString();
            }
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

}