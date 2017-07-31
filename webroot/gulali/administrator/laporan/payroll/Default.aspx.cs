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

using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using ClosedXML.Excel;
using System.Data.SqlClient;

public partial class _admin_laporan_payroll : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected string year { get { return App.GetStr(this, "year"); } }
    protected string date_from { get { return App.GetStr(this, "date_from"); } }
    protected string date_to { get { return App.GetStr(this, "date_to"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Payroll", "view");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        
        if (!string.IsNullOrEmpty(mode))
        {
            if (mode.Equals("2"))
            {
                link_perbulan.Text = "<a href=\"" + Param.Path_Admin + "laporan/payroll/\">";
                link_perperiode.Text = "<a data-toggle=\"tab\" href=\"inactive\">";
                class_perperiode.Text = "class=\"active\"";
                div_date_perperiode_from.Visible = true;
                div_date_perperiode_to.Visible = true;
                div_date_perbulan.Visible = false;
                if (!string.IsNullOrEmpty(date_from) && !string.IsNullOrEmpty(date_to))
                {
                    preview_periode();
                }
            }
            else
            {
                Response.Redirect("../");
            }
        }
        else
        {
            link_perbulan.Text = "<a data-toggle=\"tab\" href=\"#active\">";
            link_perperiode.Text = "<a href=\"" + Param.Path_Admin + "laporan/payroll/?mode=2\">";
            class_perbulan.Text = "class=\"active\"";
            div_date_perperiode_from.Visible = false;
            div_date_perperiode_to.Visible = false;
            div_date_perbulan.Visible = true;
        }

        if (!IsPostBack)
        {
            updatePanelTable.Update();
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(month), out number);
            if (result)
            {
                if (Fv.cekInt(Cf.StrSql(month)))
                {
                    bool result2 = Int32.TryParse(Cf.StrSql(year), out number);
                    if (result)
                    {
                        if (Fv.cekInt(Cf.StrSql(year)))
                        {
                            DataTable exist = Db.Rs2("select Flag_Month, DateName( month , DateAdd( month , " + Cf.StrSql(month) + " , -1 ) ) as monthname from TBL_Payroll_Flag where Flag_Month = '" + Cf.StrSql(month) + "' and Flag_Year = '" + Cf.StrSql(year) + "'");
                            if (exist.Rows.Count > 0)
                            {
                                preview("1", search_text.Text);
                                search_text.Focus();
                                DataTable fill = Db.Rs2("Select Flag_TotalGaji from TBL_Payroll_flag where Flag_Month = '" + Cf.StrSql(month) + "' and Flag_Year = '" + Cf.StrSql(year) + "'");
                                decimal total = Convert.ToDecimal(fill.Rows[0]["Flag_TotalGaji"]);
                            }
                            else
                            {
                                lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
                                table.Visible = false;
                                div_table.Visible = false;
                                div_download.Visible = false;
                                //Response.Redirect("/notFound/404.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("/notFound/404.aspx");
                        }
                    }
                    else
                    {
                       Response.Redirect("/notFound/404.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/notFound/404.aspx");
                }
            }
            else
            {
                //Response.Redirect("/notFound/404.aspx");
            }
        }
    }
    protected void date_perbulan_TextChanged(object sender, EventArgs e)
    {
        string a = date_perbulan.Text;
        if (a.Equals(""))
        {
            div_cari.Visible = false;
            div_download.Visible = false;
            //div_payslip.Visible = false;
        }
        else
        {
            div_cari.Visible = true;
            div_download.Visible = false;
            //div_payslip.Visible = false;
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(mode))
        {
            DateTime Date_from = Convert.ToDateTime(date_perperiode_from.Text);
            DateTime Date_to = Convert.ToDateTime(date_perperiode_to.Text);

            string mulai = Date_from.ToString("MM-yyyy").ToString();
            string selesai = Date_to.ToString("MM-yyyy").ToString();
            //string bulan_mulai = Date.ToString("MM").ToString();
            //string tahun_mulai = Date.ToString("yyyy").ToString();

            Response.Redirect("/gulali/administrator/laporan/payroll/?mode=2&date_from=" + mulai + "&date_to=" + selesai + "");
        }
        else
        {
            DateTime Date = Convert.ToDateTime(date_perbulan.Text);

            string bulan_mulai = Date.ToString("MM").ToString();
            string tahun_mulai = Date.ToString("yyyy").ToString();

            Response.Redirect("/gulali/administrator/laporan/payroll/?month=" + bulan_mulai + "&year=" + tahun_mulai + "");
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(mode))
        {
            export_periode();
        }
        else
        {
            exportexcelnew("1", search_text.Text);
        }
    }




    /////// REPORT SUMMARY PERBULAN ///////////
    private string header_content()
    {
        string x = "";
        x = "<table class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr> "
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">No</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center;vertical-align:middle;\">Group</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">Full Name</th>"
                          + "<th colspan=\"7\" style=\"text-align: center; vertical-align:middle;\">Regular Income</th>"
                          + "<th colspan=\"6\" style=\"text-align: center; vertical-align:middle;\">Irregular Income</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">Total Bruto</th>"
                          + "<th colspan=\"5\" style=\"text-align: center; vertical-align:middle;\">Deduction</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">Total Netto</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">PTKP</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">PKP</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">PPH Yearly</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">PPH Terpotong</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">PPH Monthly</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle;\">THP</th>"
                      + "</tr>"
                      + "<tr>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Basic Salary </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Allowance</th>"
                          + "<th colspan=\"4\" style=\"text-align: center; vertical-align:middle;\">BPJS  </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Total Regular Income</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">THR Prorated </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Overtime </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Bonus  </th> "
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Insentive</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">BPJS Pribadi </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Total Irregular Income</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Biaya Jabatan  </th>"
                          + "<th colspan=\"2\" style=\"text-align: center; vertical-align:middle; \">BPJS </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Unpaid Leave </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; width: 85px; \">Total Deduction</th>"
                      + "</tr>"
                      + "<tr>"

                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\"> Kesehatan </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\">JKK </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\">JHT </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\">JKM </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\">Kesehatan </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; width: 85px;\">JHT </th>"
                      + "</tr>"
                    + "</thead>";
        return x;
    }
    private static Tuple<decimal, decimal, decimal> GajiTunjangan_BPJS_Setahun(int month_conv, string year, string encrypted, decimal basicsalary, decimal totalallowance, decimal jkk, decimal jht, decimal jkm, decimal bpjsKesehatan)
    {
        decimal gajitunjangan_setahun = 0, bpjs_kesehatan_corp = 0, bpjs_ketenagakerjaan_corp = 0;
        if (month_conv > 1)
        {
            string month_else = "";
            for (int abc = 1; abc < month_conv; abc++)
            { month_else += "" + abc + "','"; }

            DataTable akumulasi = Db.Rs2("select (sum(Payroll_BasicSalary + Payroll_TotalAllowance)) as akumulasi_gaji_tunjangan, sum(Payroll_BPJS) as Payroll_BPJS, sum(payroll_JHT) as payroll_JHT, sum(payroll_JKK) as payroll_JKK, sum(payroll_JKM) as payroll_JKM from TBL_Payroll where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) in ('" + month_else + "')");

            if (!string.IsNullOrEmpty(akumulasi.Rows[0]["akumulasi_gaji_tunjangan"].ToString()))
            {
                gajitunjangan_setahun = Convert.ToDecimal(akumulasi.Rows[0]["akumulasi_gaji_tunjangan"]) + (basicsalary + totalallowance);
                bpjs_kesehatan_corp = Convert.ToDecimal(akumulasi.Rows[0]["Payroll_BPJS"]) + bpjsKesehatan;
                bpjs_ketenagakerjaan_corp = Convert.ToDecimal(akumulasi.Rows[0]["payroll_JHT"]) + (jht) + Convert.ToDecimal(akumulasi.Rows[0]["payroll_JKK"]) + (jkk) + Convert.ToDecimal(akumulasi.Rows[0]["payroll_JKM"]) + (jkm);
            }
            //else { gajitunjangan_setahun = (basicsalary + totalallowance) * (12 - (12 - month_conv)); }
            else { gajitunjangan_setahun = (basicsalary + totalallowance); }
        }
        else
        {
            bpjs_kesehatan_corp = bpjsKesehatan * (12 - (12 - month_conv));
            bpjs_ketenagakerjaan_corp = (jht + jkk + jkm) * (12 - (12 - month_conv));
            gajitunjangan_setahun = (basicsalary + totalallowance) * (12 - (12 - month_conv));
        }
        var tuple = new Tuple<decimal, decimal, decimal>(gajitunjangan_setahun, bpjs_kesehatan_corp, bpjs_ketenagakerjaan_corp);
        return tuple;
    }
    private string modal_dialog_allowance(string id_emp, string month)
    {
        string x = "";
        DataTable tun = Db.Rs2("select distinct m.component_name,he.employee_id,a.det_allowance_value,a.det_allowance_group,month(p.Payroll_PeriodTo) as bulan_ke from TBL_Payroll_Det_Allowance a left join TBL_payroll p on a.det_allowance_payroll_id=p.Payroll_ID left join TBL_Payroll_Privilege pp on pp.Admin_Role=a.det_allowance_group left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on EP.Employee_ID=he.Employee_ID left join TBL_MasterComponent m  on  m.component_id=a.det_allowance_component_id where he.employee_id='" + id_emp + "' and pp.privilege_choose='1' and m.component_kind='Tunjangan' and a.det_allowance_value is not null and a.det_allowance_value <>0 and month(p.Payroll_PeriodTo)=" + Cf.StrSql(month) + " and m.component_type='Regular Income' ");
        double tunjangan = 0;
        if (tun.Rows.Count > 0)
        {
            x += (" <div id=\"Regular" + id_emp + "\" class=\"modal fade\" role=\"dialog\">"
                     + "<div class=\"modal-dialog\">"
                     + "<!-- Modal content-->"
                     + "<div class=\"modal-content\">"
                       + "<div class=\"modal-header\">"

                         + "<h4 class=\"modal-title\">Allowance Details</h4>"
                       + "</div>"
                       + "<div class=\"modal-body\"><table>");
            string comName = "", comValue = "";
            for (int y = 0; y < tun.Rows.Count; y++)
            {
                comName = tun.Rows[y]["component_name"].ToString();
                comValue = tun.Rows[y]["det_allowance_value"].ToString();
                if (!string.IsNullOrEmpty(comValue))
                {
                    tunjangan += Convert.ToDouble(comValue);

                    x += ("<tr><td>" + comName + " </td><td>&nbsp; : &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(comValue)) + "</td><td></td></tr>");
                }
            }
            x += ("<tr><td colspan=\"3\" align=\"right\">--------------------------------------------------</td><td> &nbsp; &nbsp;+</td></tr>"
                + "<tr><td align=\"right\">Summary</td><td>&nbsp;  &nbsp;</td><td align=\"right\">"
+ App.Normal(Convert.ToDecimal(tunjangan)) + "</td></tr>"
                + "</table></div>"
                   + "<div class=\"modal-footer\">"
                   + "</div>"
                 + "</div>"
               + "</div>"
             + "</div>");
        }
        return x;
    }
    private static Tuple<string, decimal, decimal> modal_dialog_total_irregular(string id_emp, string month, string encrypted, string original, string strMonthPast, decimal IrregularIncome, decimal THR, decimal overtime, decimal Bonus, decimal incentive, decimal value_bpjsPribadi)
    {
        string z = "";

        DataTable tun2 = Db.Rs2("select distinct m.component_name,he.employee_id,a.det_allowance_value,a.det_allowance_group,month(p.Payroll_PeriodTo) as bulan_ke from TBL_Payroll_Det_Allowance a left join TBL_payroll p on a.det_allowance_payroll_id=p.Payroll_ID left join TBL_Payroll_Privilege pp on pp.Admin_Role=a.det_allowance_group left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on EP.Employee_ID=he.Employee_ID left join TBL_MasterComponent m  on  m.component_id=a.det_allowance_component_id where he.employee_id='" + id_emp + "' and pp.privilege_choose='1' and m.component_kind='Tunjangan' and a.det_allowance_value is not null and a.det_allowance_value <>0 and month(p.Payroll_PeriodTo)=" + Cf.StrSql(month) + " and m.component_type='Irregular Income' ");

        double counter = 0;
        if (Convert.ToDouble(Cf.StrSql(month)) > 1)
        { counter = Convert.ToDouble(Cf.StrSql(month)) - 1; }
        else { counter = Convert.ToDouble(Cf.StrSql(month)); }

        decimal tunjanganIrregular = 0, previous_IrregularIncome = 0;

        if (tun2.Rows.Count > 0)
        {
            DataTable cekIrregularIncome = Db.Rs2("select top 1 p.payroll_IrregularIncome   from TBL_Payroll p cross join  " + Param.Db + ".dbo.TBL_Employee he where  p.payroll_employeeid='" + encrypted + "' and he.employee_id='" + original + "' and he.Employee_Inactive='1' and month([Payroll_PeriodTo])=" + counter + " order by p.payroll_IrregularIncome desc");

            if (cekIrregularIncome.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(cekIrregularIncome.Rows[0]["payroll_IrregularIncome"].ToString()))
                {
                    if (Convert.ToDouble(Cf.StrSql(month)) > 1)
                    {
                        previous_IrregularIncome = Convert.ToDecimal(cekIrregularIncome.Rows[0]["payroll_IrregularIncome"].ToString());
                    }
                    else { previous_IrregularIncome = Convert.ToDecimal(cekIrregularIncome.Rows[0]["payroll_IrregularIncome"].ToString()); }
                }
            }

            z += (" <div id=\"Irregular" + id_emp + "\" class=\"modal fade\" role=\"dialog\">"
                   + "<div class=\"modal-dialog\">"
                   + "<!-- Modal content-->"
                   + "<div class=\"modal-content\">"
                     + "<div class=\"modal-header\">"

                       + "<h4 class=\"modal-title\">Irregular Details</h4>"
                     + "</div>"
                     + "<div class=\"modal-body\"><table>");
            string comNameIrregular = "", comValueIrregular = "";

            for (int y = 0; y < tun2.Rows.Count; y++)
            {
                comNameIrregular = tun2.Rows[y]["component_name"].ToString();
                comValueIrregular = tun2.Rows[y]["det_allowance_value"].ToString();
                if (!string.IsNullOrEmpty(comValueIrregular))
                {
                    tunjanganIrregular += Convert.ToDecimal(comValueIrregular);
                    z += ("<tr><td>" + comNameIrregular + " </td><td>&nbsp; : &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(comValueIrregular)) + "</td><td></td></tr>");
                }
            }
            //IrregularIncome = (THR + overtime + Bonus + incentive + value_bpjsPribadi) + db_IrregularIncome + tunjanganIrregular;
            z += ("<tr><td >BPJS Pribadi</td><td>&nbsp; : &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(value_bpjsPribadi)) + "</td>"

                + "<tr><td colspan=\"3\" align=\"right\">--------------------------------------------------</td><td> &nbsp; &nbsp;+</td></tr>"
                + "<tr><td align=\"right\" >Last Irregular Income - " + strMonthPast + "</td><td>&nbsp;  &nbsp;</td><td align=\"right\">"
                + App.Normal(Convert.ToDecimal(previous_IrregularIncome + value_bpjsPribadi))
                + "</td>");

            if (THR != 0)
            {
                z += ("<tr><td align=\"right\">THR</td><td>&nbsp;  &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(THR)) + "</td>");
            }
            if (overtime != 0)
            {
                z += ("<tr><td align=\"right\">Overtime</td><td>&nbsp;  &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(overtime)) + "</td>");
            }
            if (Bonus != 0)
            {
                z += ("<tr><td align=\"right\">Bonus</td><td>&nbsp;  &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(Bonus)) + "</td>");
            }
            if (incentive != 0)
            {
                z += ("<tr><td align=\"right\">Incentive</td><td>&nbsp;  &nbsp;</td><td align=\"right\">" + App.Normal(Convert.ToDecimal(incentive)) + "</td>");
            }
            z += ("<tr><td align=\"right\">Summary</td><td>&nbsp;  &nbsp;</td><td align=\"right\">"
+ App.Normal(Convert.ToDecimal(IrregularIncome)) + "</td></tr>"
                + "</table></div>"
                   + "<div class=\"modal-footer\">"
                   + "</div>"
                 + "</div>"
               + "</div>"
             + "</div>");
        }

        var tuple = new Tuple<string, decimal, decimal>(z, tunjanganIrregular, previous_IrregularIncome);
        return tuple;

    }

    public void preview(string num, string search)
    {
        System.Globalization.DateTimeFormatInfo mfi = new
        System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Cf.Int(month)).ToString();
        string strMonthPast = strMonthName;
        if (Cf.Int(month) == 1) strMonthPast = ""; else { strMonthPast = mfi.GetMonthName(Cf.Int(Convert.ToString(Convert.ToInt32(month) - 1))).ToString(); }
        StringBuilder v = new StringBuilder();
        StringBuilder x = new StringBuilder();
        x.Append(header_content());

        string sql_where = " and 1=1";
        if (search != "")
        {
            sql_where += " and Employee_Full_Name like '%" + search + "%'";
        }

        DataTable rs = Db.Rs("SELECT distinct he.Employee_ID,pp.Admin_Role, he.Employee_Full_Name, EP.Payroll_BPJS_Pribadi, EP.Payroll_salaryPPH, EP.Payroll_salaryBPJS, EP.Payroll_UnpaidLeave, he.Employee_NIK,  EP.Payroll_OthersAllowance, EP.Payroll_Jamsostek, he.Employee_Sum_LeaveBalance, he.Employee_Sum_SickLeave, EP.Payroll_UnpaidLeave,EP.PTKP_ID, EP.Payroll_NPWP, he.Employee_JoinDate, he.Division_ID, EP.Payroll_BPJS_Pribadi_value FROM TBL_Employee he left join TBL_Position t on he.Position_ID=t.Position_ID left join TBL_Employee_Payroll EP on EP.Employee_ID=he.Employee_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role r on  EP.Payroll_Group=r.role_id  left join " + Param.Db2 + ".dbo.TBL_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role  where EP.Payroll_Group is not null and pp.privilege_choose=1 and he.Employee_Inactive='1' order by  pp.admin_role,he.Employee_JoinDate,he.Division_ID asc");
        // and employee_id in ('17','18','16','15') 
        lable.Text = "";
        table.Visible = true;
        div_table.Visible = true;
        div_download.Visible = true;
        if (rs.Rows.Count > 0)
        {
            x.Append("<tbody>");
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string encryptionPassword = "BatavianetICCTFHRISpass@word1";
                string id_emp = rs.Rows[i]["Employee_ID"].ToString();
                string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                string original = Crypto.Decrypt(encrypted, encryptionPassword);
                decimal basicsalaryPPH = 0, basicsalaryBPJS = 0, tunjanganIrregular;

                if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryBPJS"].ToString()))
                { basicsalaryBPJS = Convert.ToDecimal(rs.Rows[i]["Payroll_salaryBPJS"]); }
                else { basicsalaryBPJS = 0; }
                if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryPPH"].ToString()))
                { basicsalaryPPH = Convert.ToDecimal(rs.Rows[i]["Payroll_salaryPPH"]); }
                else { basicsalaryPPH = 0; }
                DataTable select_satu = Db.Rs2("Select Flag_ID from TBL_Payroll_Flag where Flag_Month = '" + Cf.StrSql(month) + "' and Flag_Year = '" + year + "'");
                DataTable select_all = Db.Rs2("Select distinct da.det_allowance_group, p.* from TBL_Payroll p left join TBL_payroll_det_allowance da on da.det_allowance_employee_id=p.payroll_employeeid left join TBL_payroll_role r on r.role_id=da.det_allowance_group where p.Flag_ID = '" + select_satu.Rows[0]["Flag_ID"].ToString() + "' and p.Payroll_EmployeeID = '" + encrypted + "' and year([Payroll_PeriodTo])= '" + year + "' and month([Payroll_PeriodTo])= '" + Cf.StrSql(month) + "' and da.det_allowance_group is not null");
                for (int a = 0; a < select_all.Rows.Count; a++)
                {
                    decimal basicsalary = Convert.ToDecimal(select_all.Rows[a]["Payroll_BasicSalary"]);
                    decimal totalallowance = Convert.ToDecimal(select_all.Rows[a]["Payroll_totalAllowance"]);
                    decimal db_IrregularIncome = Convert.ToDecimal(select_all.Rows[a]["Payroll_IrregularIncome"]);
                    decimal tunjangan_lainnya = Convert.ToDecimal(select_all.Rows[a]["payroll_tunjangan_lainnya"]);
                    decimal Bonus = Convert.ToDecimal(select_all.Rows[a]["Payroll_Bonus"]);
                    decimal THR = Convert.ToDecimal(select_all.Rows[a]["Payroll_THR"]);
                    decimal incentive = Convert.ToDecimal(select_all.Rows[a]["payroll_incentive"]);
                    decimal overtime = Convert.ToDecimal(select_all.Rows[a]["Payroll_Overtime"]);
                    decimal bpjs = Convert.ToDecimal(select_all.Rows[a]["Payroll_bpjs"]);
                    decimal jkk = Convert.ToDecimal(select_all.Rows[a]["Payroll_jkk"]);
                    decimal jkm = Convert.ToDecimal(select_all.Rows[a]["Payroll_jkm"]);
                    decimal jht = Convert.ToDecimal(select_all.Rows[a]["Payroll_jht"]);
                    decimal deduction_jhtPeg = Convert.ToDecimal(select_all.Rows[a]["Payroll_jhtPeg"]);
                    decimal deduction_bpjs_kesehatan = Convert.ToDecimal(select_all.Rows[a]["Payroll_BPJSPeg"]);
                    decimal bpjsFix = Convert.ToDecimal(select_all.Rows[a]["Payroll_bpjsfix"]);
                    decimal value_bpjsPribadi = Convert.ToDecimal(select_all.Rows[a]["Payroll_BPJSPribadi"]);
                    decimal unpaid = Convert.ToDecimal(select_all.Rows[a]["Payroll_UnpaidLeave"]);
                    decimal bpjsPegawai = bpjsFix - bpjs;
                    string adminrole = select_all.Rows[a]["det_allowance_group"].ToString();
                    decimal biaya_jabatan = Convert.ToDecimal(select_all.Rows[a]["Payroll_biaya_jabatan"]);
                    decimal ptkp = Convert.ToDecimal(select_all.Rows[a]["Payroll_ptkp"]);
                    decimal pkp = Convert.ToDecimal(select_all.Rows[a]["Payroll_pkp"]);
                    decimal netto = Convert.ToDecimal(select_all.Rows[a]["Payroll_netto"]);
                    decimal tax = Convert.ToDecimal(select_all.Rows[a]["Payroll_pph21_disetahunkan"]);
                    decimal taxPeriodeIni = Convert.ToDecimal(select_all.Rows[a]["Payroll_pph21_periode_ini"]);

                    string bpjsPribadi = rs.Rows[i]["Payroll_BPJS_Pribadi"].ToString();
                    decimal BJ = 0;
                    string note;
                    if (bpjsPribadi == "yes")
                    {
                        note = "<b>BPJS Pribadi</b>";

                        bpjsPegawai = 0;
                        bpjsFix = 0;
                    }
                    else if (bpjsPribadi == "no")
                    {
                        bpjsPegawai = bpjsFix - bpjs;
                        note = "";
                    }
                    else { note = ""; }

                    decimal gajitunjangan = 0, gajitunjangan_setahun = 0, RegularIncome = 0, IrregularIncome = 0, pengurang = 0, bpjs_kesehatan_corp = 0, bpjs_ketenagakerjaan_corp = 0, bruto = 0, prev_irregularIncome = 0;

                    int month_conv = Convert.ToInt32(Cf.StrSql(month));


                    decimal taxPotong = (taxPeriodeIni * (Convert.ToInt32(month_conv) - 1));

                    /*Gajitunjangan Setahun dan (BPJS Kesehatan, BPJS Ketenagakerjaan, BPJS Pribadi - Allowance)   */
                    /*Start Here :)*/
                    var result = GajiTunjangan_BPJS_Setahun(month_conv, year, encrypted, basicsalary, totalallowance, jkk, jht, jkm, bpjs);
                    gajitunjangan_setahun = result.Item1;
                    bpjs_kesehatan_corp = result.Item2;
                    bpjs_ketenagakerjaan_corp = result.Item3;
                    /*End Here !*/

                    /* MODAL DIALOG */
                    /*Start Here :)*/
                    v.Append(modal_dialog_allowance(id_emp, month));
                    var resultIrreguler = modal_dialog_total_irregular(id_emp, month, encrypted, original, strMonthPast, IrregularIncome, THR, overtime, Bonus, incentive, value_bpjsPribadi);
                    v.Append(resultIrreguler.Item1);
                    tunjanganIrregular = resultIrreguler.Item2;
                    prev_irregularIncome = resultIrreguler.Item3;
                    /*End Here !*/

                    gajitunjangan = basicsalary + totalallowance;
                    RegularIncome = gajitunjangan_setahun + bpjs_kesehatan_corp + bpjs_ketenagakerjaan_corp;
                    bruto = RegularIncome + IrregularIncome + prev_irregularIncome;

                    pengurang = (biaya_jabatan + (unpaid + bpjsPegawai + deduction_jhtPeg));
                    // netto = bruto - pengurang;
                    //  pkp = netto - ptkp;
                    if (pkp < 0)
                    { pkp = 0; }

                    decimal summary = (basicsalary + totalallowance + (THR + overtime + Bonus + incentive) + prev_irregularIncome) - (unpaid + taxPeriodeIni + bpjsPegawai + deduction_jhtPeg);
                    x.Append(" <tr>"
                            + "<td valign=\"top\" width=\"1%\" style=\"text-align:center;\">" + (i + 1) + "</td>"
                            + "<td valign=\"top\" width=\"17%\" ><div style=\"width:150px;padding:2px;\">" + Cf.UpperFirst(adminrole) + "</div></td>"
                            + "<td  ><a href=\"../../data-karyawan/history/slip-gaji/detail/?id=" + id_emp + "&month=" + Cf.StrSql(month) + "&year=" + Cf.StrSql(year) + "\"><div style=\"width:80px;padding:2px;\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</a></div></td>");
                    x.Append("<td   valign=\"top\" style=\"text-align:right; min-width: 85px;\">" + App.Normal(basicsalary) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px;\"><a style=\"cursor:pointer\" class=\" \" data-toggle=\"modal\" data-target=\"#Regular" + id_emp + "\">" + App.Normal(totalallowance) + "</button></td>"

                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(bpjs) + "</b></span></td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(jkk) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(jht) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(jkm) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(RegularIncome) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(THR) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(overtime) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(Bonus) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(incentive) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px;\">" + App.Normal(value_bpjsPribadi) + "</td>"
                            );

                    if (tunjanganIrregular != 0)
                    {
                        x.Append("<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \"><a style=\"cursor:pointer\" class=\" \" data-toggle=\"modal\" data-target=\"#Irregular" + id_emp + "\">"
                            + App.Normal(IrregularIncome + value_bpjsPribadi)
                            + "</a></td>");
                    }
                    else
                    {
                        bruto += db_IrregularIncome;
                        x.Append("<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(db_IrregularIncome + value_bpjsPribadi) + "</td>");
                    }
                    x.Append("<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(bruto) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(biaya_jabatan) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(deduction_bpjs_kesehatan) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(deduction_jhtPeg) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(unpaid) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(pengurang) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(netto) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(ptkp) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(pkp) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(tax) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(taxPotong) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(taxPeriodeIni) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right; min-width: 85px; \">" + App.Normal(summary) + "</td>");
                    x.Append("</tr>");
                }

                for (int a = 0; a < select_all.Rows.Count; a++)
                { x.Append("<tr>"); }


            } x.Append("</tbody>");
            x.Append("</table>");
            DataTable total_Summary = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where YEAR(Payroll_PeriodTo) ='" + Cf.Int(year) + "' and month(Payroll_PeriodTo) = '" + Cf.Int(month) + "'");
            decimal total = 0;
            if (!string.IsNullOrEmpty(total_Summary.Rows[0]["TOTAL"].ToString()))
            {
                total = Convert.ToDecimal(total_Summary.Rows[0]["TOTAL"]);
            }
            summary_total.Text = "<H4><b><table>"
                                    + "<tr>"
                                        + "<td>Periode</td>"
                                        + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                        + "<td>" + strMonthName + " " + Cf.Int(year) + "</td>"
                                    + "</tr>"
                                    + "<tr>"
                                        + "<td>Total Summary</td>"
                                        + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                        + "<td>Rp. " + App.Normal(total) + "</td>"
                                    + "</tr>"
                                + "</table></b></H4>";
            table.Text = x.ToString();
            modal.Text = v.ToString();
        }
        else
        {
            lable.Text = "<h3 style=\"text-align:center;\"><i>Data Not Found</i></h3>";
            table.Visible = false;
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        preview("1", search_text.Text);
    }
    protected void BtnExportExcel(object sender, EventArgs e)
    {
        exportexcelnew("1", search_text.Text);
    }
    private void exportexcelnew(string num, string search)
    {
        DataTable total_Summary = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where YEAR(Payroll_PeriodTo) ='" + Cf.Int(year) + "' and month(Payroll_PeriodTo) = '" + Cf.Int(month) + "'");
        decimal total = Convert.ToDecimal(total_Summary.Rows[0]["TOTAL"]);
        System.Globalization.DateTimeFormatInfo mfi = new
        System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Cf.Int(month)).ToString();
        string strMonthPast = strMonthName;

        DataTable tun = Db.Rs2("select distinct m.component_name as 'component_name' from TBL_Payroll_Det_Allowance a left join TBL_payroll p on a.det_allowance_payroll_id=p.Payroll_ID left join TBL_Payroll_Privilege pp on pp.Admin_Role=a.det_allowance_group left join TBL_Payroll_Role r on pp.Admin_Role=r.Admin_Role  left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on EP.Employee_ID=he.Employee_ID left join TBL_MasterComponent m  on  m.component_id=a.det_allowance_component_id where pp.privilege_choose='1' and m.component_kind='Tunjangan' and a.det_allowance_value is not null and a.det_allowance_value <>0 and month(p.Payroll_PeriodTo)=" + Cf.Int(month) + " and year(p.Payroll_PeriodTo)=" + Cf.Int(year) + " and m.component_type='Regular Income'");

        StringBuilder v = new StringBuilder();
        StringBuilder w = new StringBuilder();
        if (tun.Rows.Count > 0)
        {
            v.Append("<th colspan=\"" + tun.Rows.Count + "\" style=\"text-align: center; vertical-align:middle; background:#037db9; color:white; font-weight:bold;\">Detail Allowance</th>");
            string comName = "";
            for (int y = 0; y < tun.Rows.Count; y++)
            {
                comName = tun.Rows[y]["component_name"].ToString().Replace("Tunjangan ", "");
                //decimal comValue = Convert.ToDecimal(tun.Rows[y]["det_allowance_value"]);
                w.Append("<th style=\"text-align: center; vertical-align:middle; background:#037db9; color:white; font-weight:bold;\">" + comName + "</th>");
            }
        }

        /* Header di excel */
        StringBuilder x = new StringBuilder();
        x.Append("<div style=\"width:100%;text-align:left;font-weight:bold;\">Payroll " + DateTime.Now + "</div>");
        x.Append("<table style=\"width:100%;text-align:left;font-weight:bold;\">"
                    + "<tr>"
                        + "<td>Periode</td>"
                        + "<td>: " + strMonthName + " " + Cf.Int(year) + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td>Total Summary</td>"
                        + "<td>: Rp. " + App.Normal(total) + "</td>"
                    + "</tr>"
                + "</table>");
        x.Append("<table class=\"table table-bordered responsive\" width=\"100%\" border=\"1px\">"
                    + "<thead>"
                        + " <tr> "
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">No</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Group</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Full Name</th>"
                          + "<th colspan=\"8\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Regular Income</th>"
            //+ "<th colspan=\"" + (8 + tun.Rows.Count) + "\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Regular Income</th>"
                          + "<th colspan=\"5\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Irregular Income</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Total Bruto</th>"
                          + "<th colspan=\"5\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Deduction</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Total Netto</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">PTKP</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">PKP</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">PPH Yearly</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">PPH Terpotong</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">PPH Monthly</th>"
                          + "<th rowspan=\"3\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">THP</th>"
                      + "</tr>"
                      + "<tr>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Basic Salary </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Allowance</th>"
            //+v.ToString()

                          + "<th colspan=\"5\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">BPJS  </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Total Regular Income</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">THR Prorated </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Overtime </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Bonus  </th> "
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Insentive</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Total Irregular Income</th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Biaya Jabatan  </th>"
                          + "<th colspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">BPJS </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Unpaid Leave </th>"
                          + "<th rowspan=\"2\" style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Total Deduction</th>"
                      + "</tr>"
                      + "<tr>"
            //+w.ToString()
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Pribadi</th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Kesehatan</th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">JKK </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">JHT </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">JKM </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">Kesehatan </th>"
                          + "<th style=\"text-align: center; vertical-align:middle; background:#033363; color:white; font-weight:bold;\">JHT </th>"


                      + "</tr>"
                    + "</thead>");

        string sql_where = " and 1=1";
        if (search != "")
        {
            sql_where += " and Employee_Full_Name like '%" + search + "%'";
        }


        DataTable rs = Db.Rs("SELECT distinct he.Employee_ID,pp.Admin_Role, he.Employee_Full_Name, EP.Payroll_BPJS_Pribadi, EP.Payroll_salaryPPH, EP.Payroll_salaryBPJS, EP.Payroll_UnpaidLeave, he.Employee_NIK,  EP.Payroll_OthersAllowance, EP.Payroll_Jamsostek, he.Employee_Sum_LeaveBalance, he.Employee_Sum_SickLeave, EP.Payroll_UnpaidLeave,EP.PTKP_ID, EP.Payroll_NPWP, he.Employee_JoinDate, he.Division_ID, EP.Payroll_BPJS_Pribadi_value FROM TBL_Employee he left join TBL_Position t on he.Position_ID=t.Position_ID left join TBL_Employee_Payroll EP on EP.Employee_ID=he.Employee_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role r on  EP.Payroll_Group=r.role_id  left join " + Param.Db2 + ".dbo.TBL_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role  where EP.Payroll_Group is not null and pp.privilege_choose=1 and he.Employee_Inactive='1' order by  pp.admin_role,he.Employee_JoinDate,he.Division_ID asc");

        // DataTable rs = Db.Rs("SELECT distinct ROW_NUMBER() OVER(ORDER BY Employee_ID ASC) AS Row, Employee_ID, he.NPWP, he.BPJS_pribadi,he.salaryPPH, he.salaryBPJS, he.PTKP_status,  he.Employee_NIK,he.Employee_Last_Name, he.Employee_Name, he.Employee_OthersAllowance,he.Employee_Jamsostek, he.Employee_LeaveBalance, he.Employee_SickLeave, he.Employee_UnpaidLeave, he.Employee_JoinDate, he.Employee_Division FROM HRIS_Employee he join HRIS_Title T on he.Title_ID=t.Title_ID left join " + Param.DB4 + ".dbo.HRIS_Payroll_Role r on  he.Payroll_Group=r.role_id  left join " + Param.DB4 + ".dbo.HRIS_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role where employee_name != 'superadmin' and he.npwp is not null " + sql_where + " order by pp.admin_role, he.Employee_JoinDate,he.Employee_Division asc");


        lable.Text = "";
        table.Visible = true;
        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string encryptionPassword = "BatavianetICCTFHRISpass@word1";
                string id_emp = rs.Rows[i]["Employee_ID"].ToString();
                string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                string original = Crypto.Decrypt(encrypted, encryptionPassword);
                decimal basicsalaryPPH = 0, basicsalaryBPJS = 0;

                if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryBPJS"].ToString()))
                {
                    basicsalaryBPJS = Convert.ToDecimal(rs.Rows[i]["Payroll_salaryBPJS"]);
                }
                else { basicsalaryBPJS = 0; }
                if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryPPH"].ToString()))
                {
                    basicsalaryPPH = Convert.ToDecimal(rs.Rows[i]["Payroll_salaryPPH"]);
                }
                else { basicsalaryPPH = 0; }
                DataTable select_satu = Db.Rs2("Select Flag_ID from TBL_Payroll_Flag where Flag_Month = '" + month + "' and Flag_Year = '" + year + "'");
                DataTable select_all = Db.Rs2("Select distinct da.det_allowance_group, p.* from TBL_Payroll p left join TBL_payroll_det_allowance da on da.det_allowance_employee_id=p.payroll_employeeid left join TBL_payroll_role r on r.role_id=da.det_allowance_group where p.Flag_ID = '" + select_satu.Rows[0]["Flag_ID"].ToString() + "' and p.Payroll_EmployeeID = '" + encrypted + "' and year([Payroll_PeriodTo])= '" + year + "' and month([Payroll_PeriodTo])= '" + month + "' and da.det_allowance_group is not null");

                x.Append("<tbody>");

                for (int a = 0; a < select_all.Rows.Count; a++)
                {
                    decimal basicsalary = Convert.ToDecimal(select_all.Rows[a]["Payroll_BasicSalary"]);
                    decimal totalallowance = Convert.ToDecimal(select_all.Rows[a]["Payroll_totalAllowance"]);
                    decimal db_IrregularIncome = Convert.ToDecimal(select_all.Rows[a]["Payroll_IrregularIncome"]);
                    decimal tunjangan_lainnya = Convert.ToDecimal(select_all.Rows[a]["payroll_tunjangan_lainnya"]);
                    decimal Bonus = Convert.ToDecimal(select_all.Rows[a]["Payroll_Bonus"]);
                    decimal THR = Convert.ToDecimal(select_all.Rows[a]["Payroll_THR"]);
                    decimal incentive = Convert.ToDecimal(select_all.Rows[a]["payroll_incentive"]);
                    decimal overtime = Convert.ToDecimal(select_all.Rows[a]["Payroll_Overtime"]);
                    decimal bpjs = Convert.ToDecimal(select_all.Rows[a]["Payroll_bpjs"]);
                    decimal jkk = Convert.ToDecimal(select_all.Rows[a]["Payroll_jkk"]);
                    decimal jkm = Convert.ToDecimal(select_all.Rows[a]["Payroll_jkm"]);
                    decimal jht = Convert.ToDecimal(select_all.Rows[a]["Payroll_jht"]);
                    decimal deduction_jhtPeg = Convert.ToDecimal(select_all.Rows[a]["Payroll_jhtPeg"]);
                    decimal deduction_bpjs_kesehatan = Convert.ToDecimal(select_all.Rows[a]["Payroll_BPJSPeg"]);
                    decimal bpjsFix = Convert.ToDecimal(select_all.Rows[a]["Payroll_bpjsfix"]);
                    decimal unpaid = Convert.ToDecimal(select_all.Rows[a]["Payroll_UnpaidLeave"]);
                    decimal value_bpjsPribadi = Convert.ToDecimal(select_all.Rows[a]["Payroll_BPJSPribadi"]);
                    decimal bpjsPegawai = bpjsFix - bpjs;
                    string adminrole = select_all.Rows[a]["det_allowance_group"].ToString();
                    decimal biaya_jabatan = Convert.ToDecimal(select_all.Rows[a]["Payroll_biaya_jabatan"]);
                    decimal ptkp = Convert.ToDecimal(select_all.Rows[a]["Payroll_ptkp"]);
                    decimal pkp = Convert.ToDecimal(select_all.Rows[a]["Payroll_pkp"]);
                    decimal netto = Convert.ToDecimal(select_all.Rows[a]["Payroll_netto"]);
                    decimal tax = Convert.ToDecimal(select_all.Rows[a]["Payroll_pph21_disetahunkan"]);
                    decimal taxPeriodeIni = Convert.ToDecimal(select_all.Rows[a]["Payroll_pph21_periode_ini"]);
                    decimal bruto = Convert.ToDecimal(select_all.Rows[a]["Payroll_bruto"]);
                    string bpjsPribadi = rs.Rows[i]["Payroll_BPJS_Pribadi"].ToString();
                    decimal BJ = 0;
                    string note;
                    if (bpjsPribadi == "yes")
                    {
                        note = "<b>BPJS Pribadi</b>";

                        bpjsPegawai = 0;
                        bpjsFix = 0;
                    }
                    else if (bpjsPribadi == "no")
                    {
                        bpjsPegawai = bpjsFix - bpjs;
                        note = "";
                    }
                    else { note = ""; }

                    decimal gajitunjangan = 0, gajitunjangan_setahun = 0, RegularIncome = 0, IrregularIncome = 0, pengurang = 0, bpjs_kesehatan_corp = 0, bpjs_ketenagakerjaan_corp = 0;
                    decimal tunjanganIrregular = 0;
                    int month_conv = Convert.ToInt32(Cf.StrSql(month));
                    bpjs_kesehatan_corp = bpjs * 12;
                    bpjs_ketenagakerjaan_corp = (jht + jkk + jkm) * 12;
                    gajitunjangan_setahun = (basicsalary + totalallowance) * 12;
                    decimal taxPotong = (taxPeriodeIni * (Convert.ToInt32(month_conv) - 1));
                    /*Gajitunjangan Setahun dan (BPJS Kesehatan, BPJS Ketenagakerjaan, BPJS Pribadi - Allowance)   */
                    /*Start Here :)*/
                    var result = GajiTunjangan_BPJS_Setahun(month_conv, year, encrypted, basicsalary, totalallowance, jkk, jht, jkm, bpjs);
                    gajitunjangan_setahun = result.Item1;
                    bpjs_kesehatan_corp = result.Item2;
                    bpjs_ketenagakerjaan_corp = result.Item3;
                    /*End Here !*/

                    /* MODAL DIALOG */
                    /*Start Here :)*/
                    v.Append(modal_dialog_allowance(id_emp, month));
                    var resultIrreguler = modal_dialog_total_irregular(id_emp, month, encrypted, original, strMonthPast, IrregularIncome, THR, overtime, Bonus, incentive, value_bpjsPribadi);

                    tunjanganIrregular = resultIrreguler.Item2;
                    /*End Here !*/


                    gajitunjangan = basicsalary + totalallowance;
                    RegularIncome = gajitunjangan_setahun + bpjs_kesehatan_corp + bpjs_ketenagakerjaan_corp;
                    IrregularIncome = (THR + overtime + Bonus + incentive) + tunjanganIrregular;
                    //bruto = RegularIncome + IrregularIncome;


                    pengurang = (biaya_jabatan + (unpaid + bpjsPegawai + deduction_jhtPeg));
                    // netto = bruto - pengurang;
                    //  pkp = netto - ptkp;
                    if (pkp < 0)
                    {
                        pkp = 0;
                    }

                    decimal summary = (basicsalary + totalallowance + (THR + overtime + Bonus + incentive)) - (unpaid + taxPeriodeIni + bpjsPegawai + deduction_jhtPeg);

                    x.Append(" <tr>"
                            + "<td valign=\"top\" width=\"1%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                            + "<td valign=\"top\" width=\"7%\" >" + Cf.UpperFirst(adminrole) + "</td>"
                            + "<td><div style=\"width:180px;\">" + rs.Rows[i]["Employee_Full_Name"].ToString() + "</div></td>");
                    x.Append("<td   valign=\"top\" style=\"text-align:right;\">" + App.Normal(basicsalary) + "</td>"
                            + "<td  valign=\"top\" style=\"text-align:right;\">" + App.Normal(totalallowance) + "</td>"
                        //+ allowance_value.ToString()
                            + "<td  valign=\"top\" style=\"text-align:right;\">" + App.Normal(value_bpjsPribadi) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;>" + App.Normal(bpjs) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right; \">" + App.Normal(bpjs) + "</b></span></td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(jkk) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(jht) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(jkm) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(RegularIncome) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(THR) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(overtime) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(Bonus) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(incentive) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(IrregularIncome) + "</td>"

                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(bruto) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(biaya_jabatan) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(deduction_bpjs_kesehatan) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(deduction_jhtPeg) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(unpaid) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(pengurang) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(netto) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(ptkp) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(pkp) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(tax) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(taxPotong) + "</td>"
                            + "<td  valign=\"top\" width=\"7%\" style=\"text-align:right;\">" + App.Normal(taxPeriodeIni) + "</td>"
                            + "<td  valign=\"top\" width=\"11%\" style=\"text-align:right;\">" + App.Normal(summary) + "</td>");

                    x.Append("</tr>");
                }

                //for (int a = 0; a < select_all.Rows.Count; a++)
                //{
                //    x.Append("<tr>");
                //}
                x.Append("</tbody>");

            }
        }
        x.Append("</table>");
        table.Text = x.ToString();

        PlaceHolder report = new PlaceHolder();
        report.Controls.Add(table);
        string filename = "Payroll PerBulan " + strMonthName + " " + Cf.Int(year) + "";
        Rpt.ToExcel(this, report, filename);
    }
    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CnnStr2"].ConnectionString;
        string query = "select Payroll_Allowance as '<font style=\"background-color:red\">Allowance</font>' from TBL_Payroll";

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);

                        //Set Name of DataTables.
                        ds.Tables[0].TableName = "Employee";

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                //Add DataTable as Worksheet.
                                wb.Worksheets.Add(dt);
                            }
                            //Export the Excel file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            //Response.ContentType = " application/vnd.ms-excel";
                            Response.AddHeader("content-disposition", "attachment;filename=Employee " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                }
            }
        }
    }
    /////// REPORT SUMMARY PERBULAN ///////////


    protected void date_perperiode_from_TextChanged(object sender, EventArgs e)
    {
        DateTime Date_from = Convert.ToDateTime(date_perperiode_from.Text);
        DateTime Date_to = Convert.ToDateTime(date_perperiode_to.Text);

        if (Date_from > Date_to)
        {
            date_perperiode_to.Text = date_perperiode_from.Text;
        }

        string from = date_perperiode_from.Text;
        string to = date_perperiode_to.Text;
        if ((from != ("")) && (to != ("")))
        {
            div_cari.Visible = true;
            div_download.Visible = false;
        }
        else
        {
            div_cari.Visible = false;
            div_download.Visible = false;
        }
    }
    protected void date_perperiode_to_TextChanged(object sender, EventArgs e)
    {
        DateTime Date_from = Convert.ToDateTime(date_perperiode_from.Text);
        DateTime Date_to = Convert.ToDateTime(date_perperiode_to.Text);

        if (Date_from > Date_to)
        {
            date_perperiode_from.Text = date_perperiode_to.Text;
        }

        string from = date_perperiode_from.Text;
        string to = date_perperiode_to.Text;
        if ((from != ("")) && (to != ("")))
        {
            div_cari.Visible = true;
            div_download.Visible = false;
        }
        else
        {
            div_cari.Visible = false;
            div_download.Visible = false;
        }
    }


    /////// REPORT SUMMARY PERPERIODE ///////////
    public void preview_periode()
    {
        DateTime Date_from = Convert.ToDateTime(date_from);
        DateTime Date_to = Convert.ToDateTime(date_to);
        

        string bulan_mulai = Date_from.ToString("MM").ToString();
        string bulan_mulai_nama = Date_from.ToString("MMMM").ToString();
        string tahun_mulai = Date_from.ToString("yyyy").ToString();

        string bulan_selesai = Date_to.ToString("MM").ToString();
        string bulan_selesai_nama = Date_to.ToString("MMMM").ToString();
        string tahun_selesai = Date_to.ToString("yyyy").ToString();


        StringBuilder per = new StringBuilder();
        string table_periode = "<table class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:70%\">"
                    + "<thead>"
                        + " <tr> "
                          + "<th style=\"text-align: center; vertical-align:middle;\">No</th>"
                          + "<th style=\"text-align: center;vertical-align:middle;\">Periode</th>"
                          + "<th style=\"text-align: center; vertical-align:middle;\">Take Home Pay</th>"
                      + "</tr>"
                    + "</thead><tbody>";

        DataTable rs_bulan_tahun_periode = Db.Rs2("SELECT distinct YEAR(Payroll_PeriodTo) as 'Year', month(Payroll_PeriodTo) as 'month' FROM TBL_Payroll where FORMAT(Payroll_PeriodTo,'yyyyMM') between '" + tahun_mulai + bulan_mulai + "' AND '"+ tahun_selesai + bulan_selesai +"' order by YEAR(Payroll_PeriodTo), month(Payroll_PeriodTo) asc");

        if (rs_bulan_tahun_periode.Rows.Count > 0)
        {
            for (int a = 0; a < rs_bulan_tahun_periode.Rows.Count; a++)
            {
                string bulan = rs_bulan_tahun_periode.Rows[a]["month"].ToString();
                string tahun = rs_bulan_tahun_periode.Rows[a]["Year"].ToString();

                DataTable rs_bulan_nama = Db.Rs2("select DateName( month , DateAdd( month , " + bulan + " , -1 ) ) as 'monthname' from TBL_Payroll");

                string bulan_nama = rs_bulan_nama.Rows[a]["monthname"].ToString();

                DataTable rs_summary_perbulan = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where YEAR(Payroll_PeriodTo) = '" + tahun + "' and month(Payroll_PeriodTo) = '" + bulan + "'");

                decimal Total = Convert.ToDecimal(rs_summary_perbulan.Rows[0]["TOTAL"]);
                table_periode += " <tr> "
                              + "<td style=\"text-align: center; vertical-align:middle;\">" + (a + 1) + "</td>"
                              + "<td style=\"text-align: center;vertical-align:middle;\"><a href=\"../../keuangan/payroll/detail/?month=" + bulan + "&year="+tahun+"\">" + bulan_nama + " " + tahun + "</a></td>"
                              + "<td style=\"text-align: center; vertical-align:middle;\">" + App.Normal(Total) + "</td>"
                          + "</tr>";
            }
            table_periode += " </tbody></table>";
            per.Append(table_periode);
            table.Text = per.ToString();
            lable.Text = "";
            table.Visible = true;
            div_table.Visible = true;
            div_download.Visible = true;

            DataTable rs_summary_total = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where FORMAT(Payroll_PeriodTo,'yyyyMM') between '" + tahun_mulai + bulan_mulai + "' AND '" + tahun_selesai + bulan_selesai + "'");
            
            decimal total = 0;
            if (!string.IsNullOrEmpty(rs_summary_total.Rows[0]["TOTAL"].ToString()))
            {
                total = Convert.ToDecimal(rs_summary_total.Rows[0]["TOTAL"]);
            }

            summary_total.Text = "<H4><b><table>"
                                    + "<tr>"
                                        + "<td>Periode</td>"
                                        + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                        + "<td>"+bulan_mulai_nama+" "+tahun_mulai+" - "+bulan_selesai_nama+" "+tahun_selesai+"</td>"
                                    + "</tr>"
                                    + "<tr>"
                                        + "<td>Total Summary</td>"
                                        + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                        + "<td>Rp. " + App.Normal(total) + "</td>"
                                    + "</tr>"
                                + "</table></b></H4>";
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            table.Visible = false;
            div_table.Visible = false;
            div_download.Visible = false;
        }
    }
    public void export_periode()
    {
        DateTime Date_from = Convert.ToDateTime(date_from);
        DateTime Date_to = Convert.ToDateTime(date_to);

        string bulan_mulai = Date_from.ToString("MM").ToString();
        string bulan_mulai_nama = Date_from.ToString("MMMM").ToString();
        string tahun_mulai = Date_from.ToString("yyyy").ToString();

        string bulan_selesai = Date_to.ToString("MM").ToString();
        string bulan_selesai_nama = Date_to.ToString("MMMM").ToString();
        string tahun_selesai = Date_to.ToString("yyyy").ToString();


        StringBuilder per = new StringBuilder();

        DataTable rs_summary_total = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where FORMAT(Payroll_PeriodTo,'yyyyMM') between '" + tahun_mulai + bulan_mulai + "' AND '" + tahun_selesai + bulan_selesai + "'");

        decimal total = 0;
        if (!string.IsNullOrEmpty(rs_summary_total.Rows[0]["TOTAL"].ToString()))
        {
            total = Convert.ToDecimal(rs_summary_total.Rows[0]["TOTAL"]);
        }

        string table_periode = "<H4><b><table>"
                                + "<tr>"
                                    + "<td>Periode</td>"
                                    + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                    + "<td>" + bulan_mulai_nama + " " + tahun_mulai + " - " + bulan_selesai_nama + " " + tahun_selesai + "</td>"
                                + "</tr>"
                                + "<tr>"
                                    + "<td>Total Summary</td>"
                                    + "<td style=\"padding-left:10px; padding-right:10px;\">:</td>"
                                    + "<td>Rp. " + App.Normal(total) + "</td>"
                                + "</tr>"
                            + "</table></b></H4>";

        table_periode += "<table border=\"1\" style=\"width:70%;\">"
                    + "<thead>"
                        + " <tr> "
                          + "<th style=\"text-align: center; vertical-align:middle;\">No</th>"
                          + "<th style=\"text-align: center;vertical-align:middle;\">Periode</th>"
                          + "<th style=\"text-align: center; vertical-align:middle;\">Take Home Pay</th>"
                      + "</tr>"
                    + "</thead><tbody>";

        DataTable rs_bulan_tahun_periode = Db.Rs2("SELECT distinct YEAR(Payroll_PeriodTo) as 'Year', month(Payroll_PeriodTo) as 'month' FROM TBL_Payroll where FORMAT(Payroll_PeriodTo,'yyyyMM') between '" + tahun_mulai + bulan_mulai + "' AND '" + tahun_selesai + bulan_selesai + "' order by YEAR(Payroll_PeriodTo), month(Payroll_PeriodTo) asc");

        if (rs_bulan_tahun_periode.Rows.Count > 0)
        {
            for (int a = 0; a < rs_bulan_tahun_periode.Rows.Count; a++)
            {
                string bulan = rs_bulan_tahun_periode.Rows[a]["month"].ToString();
                string tahun = rs_bulan_tahun_periode.Rows[a]["Year"].ToString();

                DataTable rs_bulan_nama = Db.Rs2("select DateName( month , DateAdd( month , " + bulan + " , -1 ) ) as 'monthname' from TBL_Payroll");

                string bulan_nama = rs_bulan_nama.Rows[a]["monthname"].ToString();

                DataTable rs_summary_perbulan = Db.Rs2("SELECT (SUM(Payroll_BasicSalary) + SUM(Payroll_TotalAllowance) + (SUM(Payroll_THR)+SUM(Payroll_Overtime)+SUM(Payroll_Incentive))) - (SUM(Payroll_UnpaidLeave)+SUM(Payroll_pph21_periode_ini)) AS 'TOTAL' FROM TBL_Payroll where YEAR(Payroll_PeriodTo) = '" + tahun + "' and month(Payroll_PeriodTo) = '" + bulan + "'");

                decimal Total = Convert.ToDecimal(rs_summary_perbulan.Rows[0]["TOTAL"]);

                table_periode += " <tr> "
                              + "<td style=\"text-align: center; vertical-align:middle;\">" + (a + 1) + "</th>"
                              + "<td style=\"text-align: center;vertical-align:middle;\">" + bulan_nama + " " + tahun + "</th>"
                              + "<td style=\"text-align: center; vertical-align:middle;\">" + App.Normal(Total) + "</th>"
                          + "</tr>";
            }
            table_periode += " </tbody></table>";
        }
        per.Append(table_periode);
        table.Text = per.ToString();
        PlaceHolder report = new PlaceHolder();
        report.Controls.Add(table);
        string filename = "Payroll Summary Periode " + bulan_mulai_nama + " "+tahun_mulai+" - "+bulan_selesai_nama+" "+tahun_selesai+"";
        Rpt.ToExcel(this, report, filename);
    }
    /////// REPORT SUMMARY PERPERIODE ///////////
}