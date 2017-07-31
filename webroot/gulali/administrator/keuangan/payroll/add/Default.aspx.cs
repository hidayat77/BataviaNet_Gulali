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

public partial class _admin_keuangan_payroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Keuangan >> Payroll", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "keuangan/payroll/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            tanggal_mulai.Text = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            tanggal_selesai.Text = DateTime.Now.ToString("dd-MM-yyyy");
            jam_mulai.Text = Convert.ToString(0);
            jam_selesai.Text = Convert.ToString(DateTime.Now.TimeOfDay);
            ddl_bulan.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }
        //else { Response.Redirect("/gulali/page/404.aspx"); }
    }
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        tanggal_mulai.Attributes.Add("disabled", "disabled");
        tanggal_selesai.Attributes.Add("disabled", "disabled");
        jam_mulai.Attributes.Add("disabled", "disabled");
        jam_selesai.Attributes.Add("disabled", "disabled");
        txt_hari_kerja.Attributes.Add("disabled", "disabled");
        txt_bonus.Attributes.Add("disabled", "disabled");
        ddl_bulan.Attributes.Add("disabled", "disabled");
        fill();
    }
    protected void fill()
    {
        if (!string.IsNullOrEmpty(ddl_bulan.SelectedValue))
        {
            table.Visible = true;
            //Submit.Visible = false;
            //td_final.Visible = true;
            //from.Enabled = false;
            //to.Enabled = false;
            //ddlh.Enabled = false;
            //ddlh1.Enabled = false;
            //ddlm.Enabled = false;
            //ddlm1.Enabled = false;
            //ddl_month.Enabled = false;
            //ddl_month.SelectedValue = month;

            //DateTime From = Convert.ToDateTime(from_date);
            //DateTime To = Convert.ToDateTime(to_date);
            //DateTime From2 = Convert.ToDateTime(From);
            //DateTime To2 = Convert.ToDateTime(To).AddDays(1);

            //string From_Conv = From.ToString("yyyy-MM-dd hh:mm").ToString();
            //string To_Conv = To.ToString("yyyy-MM-dd hh:mm").ToString();

            //string From_Conv2 = From.ToString("dd-MM-yyyy").ToString();
            //string To_Conv2 = To.ToString("dd-MM-yyyy").ToString();
            //from.Text = From_Conv2;
            //to.Text = To_Conv2;

            //string To_Conv_Year = To.ToString("yyyy").ToString();
            //string To_Conv_Month = To.ToString("MM").ToString();


            /*
            //syntak dibawah untuk menhitung jumlah hari kerja(tanpa hari minggu, belum termasuk holiday)
            int totalDue = 0;
            int totalDue2 = 1;
            for (DateTime date = From2; date < To2; date = date.AddDays(1))
            {
                string nameOfTheDay = date.ToString("dddd", new System.Globalization.CultureInfo("en-GB")).ToLower();
                if (nameOfTheDay != "sunday" && nameOfTheDay != "saturday")
                {
                    totalDue += totalDue2;
                }
            }
            int total_hari_kerja = totalDue;

            //query untuk select dari HRIS_Attendance_Holiday, dari periode from s/d periode to ada holiday apa nggak(kecuali sabtu minggu)
            DataTable total_holiday = Db.Rs("select DATENAME(WEEKDAY, Holiday_Date) from HRIS_Attendance_Holiday where Holiday_Date between '" + From_Conv + "' and '" + To_Conv + "' and DATENAME(WEEKDAY, Holiday_Date) != 'Sunday' and DATENAME(WEEKDAY, Holiday_Date) != 'Saturday'");

            //total hari kerja yang sudah dipotong holiday
            int total_kerja_min_holiday = total_hari_kerja - total_holiday.Rows.Count;
            */
            //   DataTable employee_id = Db.Rs("SELECT distinct Employee_ID, he.Employee_BasicSalary, he.Employee_UnpaidLeave, he.Employee_NIK, he.Employee_Name, he.Employee_OthersAllowance, he.Employee_Jamsostek, he.Employee_LeaveBalance, he.Employee_SickLeave, he.Employee_UnpaidLeave FROM HRIS_Absen ha join HRIS_Employee he on Absen_Employee=Employee_Name where Absen_Date between '" + From_Conv + "' and '" + To_Conv + "'  order by Employee_ID asc");
            StringBuilder x = new StringBuilder();
            x.Append(" <table id=\"myTable01\" class=\"fancyTable\" width=\"100%\" style=\"\"> <thead> <tr>"
                     + "<th rowspan=\"3\" style=\"text-align:center;vertical-align:middle; width:30px;\">No </th>"
                     + "<th rowspan=\"3\" style=\"text-align:center;vertical-align:middle; width:90px; \">Group</th>"
                     + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;width:150px;\">Full Name</th>"
                     + "<th colspan=\"6\" style=\"text-align: center; vertical-align:middle;\">Regular Income</th>"
                     + "<th colspan=\"5\" style=\"text-align: center; vertical-align:middle;\"> Irregular Income</th>"
                     + "<th colspan=\"3\" style=\"text-align: center; vertical-align:middle;\"> Deduction</th>"
                     + " </tr><tr>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle;width:82px;\">Basic <br/> Salary</th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width:82px;\"> Allowance</th>"
                     + "<th colspan=\"4\" style=\"text-align: center; vertical-align:middle;\"> BPJS </th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width:75px;\"> THR  </th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle;width:75px;\">Overtime</th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle;width:75px;\">Bonus  </th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle;width:75px;\">Insentive</th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width:65px;\">BPJS <br>Pribadi</th>"
                     + "<th colspan=\"2\" style=\"text-align: center; vertical-align:middle;\"> BPJS </th>"
                     + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; \">Unpaid Leave</th>"
                     + " </tr><tr>"

                     + "<th style=\"text-align: center; vertical-align:middle;width:65px;\">Kesehatan</th>"
                     + "<th style=\"text-align: center; vertical-align:middle; width:65px;\">JKK</th>"
                     + "<th style=\"text-align: center; vertical-align:middle;  width:65px;\">JHT </th>"
                     + "<th style=\"text-align: center; vertical-align:middle;  width:65px;\">JKM</th>"
                     + "<th style=\"text-align: center; vertical-align:middle;width:76px;\">Kesehatan</th>"
                     + "<th style=\"text-align: center; vertical-align:middle;width:65px;\">JHT</th>"
                     + "</tr>"
                     + "</thead>");
            thead.Text = x.ToString();
            //DataTable employee_id = Db.Rs("SELECT distinct Employee_Religion, Employee_ID,pp.Admin_Role, he.Employee_Name, he.Employee_Last_Name, he.BPJS_Pribadi, he.salaryPPH, he.salaryBPJS, he.salaryBPJSTK, he.Employee_UnpaidLeave, he.Employee_NIK,  he.Employee_OthersAllowance, he.Employee_Jamsostek,he.PTKP_Status, he.Employee_LeaveBalance, he.Employee_SickLeave, he.Employee_UnpaidLeave, he.Employee_JoinDate, he.Employee_Division, he.BPJS_Pribadi_value   FROM  HRIS_Employee he left join HRIS_title t on he.title_id=t.title_id left join " + Param.DB4 + ".dbo.HRIS_Payroll_Role r on  he.Payroll_Group=r.role_id  left join " + Param.DB4 + ".dbo.HRIS_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role  where he.Payroll_Group is not null and pp.privilege_choose=1 and he.Employee_Authorized='A' and he.employee_id in ('2','3','4','5','6','7') order by  pp.admin_role,he.Employee_JoinDate,he.Employee_Division asc");
            ////and he.employee_id in ('1308','1304','1357','1352') 
            //for (int a = 0; a < employee_id.Rows.Count; a++)
            //{
            //    string id = employee_id.Rows[a]["Employee_ID"].ToString();
            //    decimal unpaidleave;
            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["Employee_UnpaidLeave"].ToString()))
            //    {
            //        unpaidleave = Convert.ToDecimal(employee_id.Rows[a]["Employee_UnpaidLeave"]);
            //    }
            //    else
            //    {
            //        unpaidleave = 0;
            //    }
            //    string encryptionPassword = "BatavianetICCTFHRISpass@word1";
            //    string id_emp = id;
            //    string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
            //    string original = Crypto.Decrypt(encrypted, encryptionPassword);

            //    double basicsalary = 0; double allowance = 0;
            //    double basicsalaryPPH = 0, basicsalaryBPJS = 0, basicsalaryBPJSTK = 0, value_BPJSPribadi = 0;

            //    /*History Salary*/
            //    basicsalary = HistorySalary(encrypted);

            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["salaryBPJS"].ToString()))
            //    {
            //        basicsalaryBPJS = Convert.ToDouble(employee_id.Rows[a]["salaryBPJS"]);
            //    }
            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["salaryBPJSTK"].ToString()))
            //    {
            //        basicsalaryBPJSTK = Convert.ToDouble(employee_id.Rows[a]["salaryBPJSTK"]);
            //    }
            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["BPJS_Pribadi_value"].ToString()))
            //    {
            //        value_BPJSPribadi = Convert.ToDouble(employee_id.Rows[a]["BPJS_Pribadi_value"]);
            //    }
            //    else { value_BPJSPribadi = 0; }
            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["salaryPPH"].ToString()))
            //    {
            //        basicsalaryPPH = Convert.ToDouble(employee_id.Rows[a]["salaryPPH"]);
            //    }
            //    else { basicsalaryPPH = 0; }


            //    if (!string.IsNullOrEmpty(employee_id.Rows[a]["Employee_OthersAllowance"].ToString()))
            //    {
            //        allowance = Convert.ToDouble(employee_id.Rows[a]["Employee_OthersAllowance"]);
            //    }
            //    else { allowance = 0; }
            //    string ptkpstatus = employee_id.Rows[a]["ptkp_status"].ToString();

            //    double jkm = 0, jht = 0, jhtPeg = 0, jkk = 0, bpjsKesehatan = 0, bpjsPegawai = 0, bpjsFix = 0;
            //    string bpjsPribadi = employee_id.Rows[a]["BPJS_Pribadi"].ToString();

            //    var result = PARAMETER_PAYROLL(bpjsPribadi, id, basicsalaryBPJS, basicsalaryBPJSTK);

            //    jkk = result.Item1;
            //    jkm = result.Item2;
            //    jht = result.Item3;
            //    bpjsKesehatan = result.Item4;
            //    bpjsPegawai = result.Item5;
            //    bpjsFix = result.Item6;


            //    double tunjangan = 0;
            //    tunjangan = Allowance(id);

            //    string joinDate = employee_id.Rows[a]["Employee_JoinDate"].ToString();
            //    string religion = employee_id.Rows[a]["Employee_Religion"].ToString();
            //    string year_employment = "", month_employment = "";
            //    double THR = 0, THRprorate = 0;




            //    int month_conv = Convert.ToInt32(Cf.StrSql(To_Conv_Month));
            //    var resultTHR = THRFunction(joinDate, religion, basicsalary, tunjangan, month_conv, To_Conv_Year);
            //    THRprorate = resultTHR.Item2;
            //    year_employment = resultTHR.Item3;
            //    month_employment = resultTHR.Item4;

            //    if (!string.IsNullOrEmpty(year_employment))
            //    {
            //        if (Convert.ToDouble(year_employment) < 1)
            //        {
            //            THR = THRprorate;
            //        }
            //        else { THR = resultTHR.Item1; }
            //    }


            //    double count = 0;
            //    Literal lit_a = new Literal();
            //    lit_a.ID = "Print_a_" + a + "";
            //    lit_a.Text = "<tbody>"
            //                + "<tr>"
            //                + "<td  valign=\"top\"  style=\"text-align:center;vertical-align:middle;\">" + (a + 1).ToString() + "</td>"
            //                + "<td valign=\"top\" style=\"padding-left:2px;vertical-align:middle;max-width:130px; min-width:130px;word-wrap:break-word;\" >" + Cf.UpperFirst(employee_id.Rows[a]["admin_role"].ToString()) + "</td>"
            //                + "<td  valign=\"top\"  style=\"padding-left:2px;vertical-align:middle;max-width:140px; min-width:140px;word-wrap:break-word;\">" + employee_id.Rows[a]["Employee_Name"].ToString() + " " + employee_id.Rows[a]["Employee_Last_Name"].ToString() + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right; vertical-align:middle; \">" + basicsalary.ToString("#,##0.00") + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + tunjangan.ToString("#,##0.00") + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + bpjsKesehatan.ToString("#,##0.00") + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jkk.ToString("#,##0.00") + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jht.ToString("#,##0.00") + "</td>"
            //                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jkm.ToString("#,##0.00") + "</td>"
            //    + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
            //    PH.Controls.Add(lit_a);

            //    TextBox txt_THR = new TextBox();
            //    txt_THR.ID = "txt_THR_" + (a + 1).ToString();
            //    txt_THR.Text = THR.ToString("#,##0.00");
            //    txt_THR.Width = 60;
            //    txt_THR.Height = 12;
            //    PH.Controls.Add(txt_THR);

            //    Literal lit_THR = new Literal();
            //    lit_THR.ID = "Print_THR_" + a + "";
            //    lit_THR.Text = "</td>"

            //    + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
            //    PH.Controls.Add(lit_THR);

            //    TextBox txt_overtime = new TextBox();
            //    txt_overtime.ID = "txt_overtime_" + (a + 1).ToString();
            //    txt_overtime.Text = count.ToString("#,##0.00");
            //    txt_overtime.Width = 60;
            //    txt_overtime.Height = 12;
            //    PH.Controls.Add(txt_overtime);

            //    Literal lit_overtime = new Literal();
            //    lit_overtime.ID = "Print_overtime_" + a + "";
            //    lit_overtime.Text = "</td>"

            //    + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
            //    PH.Controls.Add(lit_overtime);

            //    TextBox txt_bonus = new TextBox();
            //    txt_bonus.ID = "Txt_bonus_" + (a + 1).ToString();
            //    txt_bonus.Text = count.ToString("#,##0.00");
            //    txt_bonus.Width = 60;
            //    txt_bonus.Height = 12;
            //    PH.Controls.Add(txt_bonus);

            //    Literal lit_bonus = new Literal();
            //    lit_bonus.ID = "Print_bonus_" + a + "";
            //    lit_bonus.Text = "</td>"

            //    + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
            //    PH.Controls.Add(lit_bonus);

            //    TextBox txt_insentive = new TextBox();
            //    txt_insentive.ID = "txt_insentive_" + (a + 1).ToString();
            //    txt_insentive.Text = count.ToString("#,##0.00");
            //    txt_insentive.Width = 60;
            //    txt_insentive.Height = 12;
            //    PH.Controls.Add(txt_insentive);

            //    Literal lit_insentive = new Literal();
            //    lit_insentive.ID = "Print_insentive_" + a + "";
            //    lit_insentive.Text = "</td>"
            //    + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + value_BPJSPribadi.ToString("#,##0.00") + "</td>"
            //    + "<td valign=\"top\" style=\"text-align:right;width:78px; vertical-align:middle;\">" + bpjsPegawai.ToString("#,##0.00") + "</td>"
            //    + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle; \">" + jhtPeg.ToString("#,##0.00") + "</td>"

            //    + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">";
            //    PH.Controls.Add(lit_insentive);


            //    TextBox txt_unpaidleave = new TextBox();
            //    txt_unpaidleave.ID = "Txt_unpaidleave_" + (a + 1).ToString();
            //    txt_unpaidleave.Text = count.ToString("#,##0.00");
            //    txt_unpaidleave.Width = 60;
            //    txt_unpaidleave.Height = 12;
            //    PH.Controls.Add(txt_unpaidleave);

            //    Literal lit_unpaidleave = new Literal();
            //    lit_unpaidleave.ID = "Print_unpaidleave_" + a + "";
            //    lit_unpaidleave.Text = "</td>"

            //    + "</tr>"
            //    + "</tbody>";
            //    PH.Controls.Add(lit_unpaidleave);
            //}
        }
    }
}