using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using AjaxControlToolkit;
using System.Globalization;

public partial class _admin_keuangan_payroll : System.Web.UI.Page
{
    protected string from_date { get { return App.GetStr(this, "from_date"); } }
    protected string to_date { get { return App.GetStr(this, "to_date"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Keuangan >> Payroll", "Add");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "keuangan/payroll/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        btn_generate.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");

        if (!IsPostBack)
        {
            tanggal_mulai.Text = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            tanggal_selesai.Text = DateTime.Now.ToString("dd-MM-yyyy");
            jam_mulai.Text = Convert.ToString(0);
            jam_selesai.Text = Convert.ToString(DateTime.Now.TimeOfDay);
            ddl_bulan.SelectedValue = Convert.ToString(DateTime.Now.Month);

            DataTable rs = Db.Rs2("Select max(Flag_DateCreate) as Flag_DateCreate from TBL_Payroll_Flag");
            if (string.IsNullOrEmpty(rs.Rows[0]["Flag_DateCreate"].ToString()))
            {
                var now = DateTime.Now.AddMonths(-1);
                var startOfMonth = new DateTime(now.Year, now.Month, 25);

                tanggal_mulai.Text = Convert.ToString(startOfMonth.Day) + "-" + Convert.ToString(startOfMonth.Month) + "-" + Convert.ToString(startOfMonth.Year);
                //var now = DateTime.Now.AddMonths(-1);
                //var startOfMonth = new DateTime(now.Year, now.Month, 25);
                //from.Text =  Convert.ToString(startOfMonth) ;


                //ddlh.SelectedValue = Convert.ToString(DateTime.Now.Hour);
                //ddlm.SelectedValue = Convert.ToString(DateTime.Now.Minute);
                jam_mulai.Text = Convert.ToString(0);
            }
            else
            {
                DateTime nana = Convert.ToDateTime(rs.Rows[0]["Flag_DateCreate"].ToString());
                tanggal_mulai.Text = nana.ToString("dd-MM-yyyy").ToString();
                //ddlh.SelectedValue = nana.ToString("hh").ToString();
                //ddlm.SelectedValue = nana.ToString("mm").ToString();
                jam_mulai.Text = nana.ToString("hh:mm").ToString();

                // from.Enabled = false;
                // ddlh.Enabled = false;
                // ddlm.Enabled = false;

                //to.Enabled = false;
                // ddlh1.Enabled = false;
                // ddlm1.Enabled = false;
                // ddl_month.Enabled = false;

            }

            var nows = DateTime.Now;
            var endOfMonth = new DateTime(nows.Year, nows.Month, nows.Day);
            tanggal_selesai.Text = Convert.ToString(endOfMonth.Day) + "-" + Convert.ToString(endOfMonth.Month) + "-" + Convert.ToString(endOfMonth.Year);

            // to.Text = Convert.ToString(DateTime.Now.AddDays(-1)) ;
            //ddlh1.SelectedValue = Convert.ToString(DateTime.Now.Hour);
            //ddlm1.SelectedValue = Convert.ToString(DateTime.Now.Minute);
            jam_mulai.Text = Convert.ToString(0);
            ddl_bulan.SelectedValue = Convert.ToString(DateTime.Now.Month);
            updatePanel1.Update();
        }
        fill();
    }

    protected double HistorySalary(string encrypted)
    {
        double x = 0;

        DataTable baseSalary = Db.Rs2("Select top 1 HistorySalary_value, HistorySalary_DateChanged from TBL_HistorySalary where Employee_ID='" + encrypted + "' order by HistorySalary_DateChanged desc");
        DataTable baseSalary2 = Db.Rs2("Select * from TBL_HistorySalary where Employee_ID='" + encrypted + "' order by HistorySalary_DateChanged desc");
        if (baseSalary.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(baseSalary.Rows[0]["HistorySalary_Value"].ToString()))
            { x = Convert.ToDouble(baseSalary.Rows[0]["HistorySalary_Value"]); }
            else { x = 0; }
        }
        else
        {
            if (baseSalary2.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(baseSalary2.Rows[0]["HistorySalary_Value"].ToString()))
                {
                    x = Convert.ToDouble(baseSalary2.Rows[0]["HistorySalary_Value"]);
                }
                else { x = 0; }
            }
        }
        return x;
    }
    protected double GajiTunjanganFunction(int month_conv, string To_Conv_Year, string encrypted, double basicSalary, double tunjangan, double value_BPJSPribadi)
    {
        double gajitunjangan = 0;
        if (month_conv > 1)
        {
            string month_else = "";
            for (int f = 1; f < month_conv; f++)
            {
                month_else += "" + f + "','";
            }

            DataTable akumulasi = Db.Rs2("select (sum(Payroll_BasicSalary + Payroll_TotalAllowance)) as akumulasi from TBL_Payroll where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(To_Conv_Year) + "' and month(Payroll_PeriodTo) in ('" + month_else + "')");
            if (akumulasi.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(akumulasi.Rows[0]["akumulasi"].ToString()))
                {
                    gajitunjangan = Convert.ToDouble(akumulasi.Rows[0]["akumulasi"]) + (basicSalary + tunjangan + value_BPJSPribadi);
                }
                else { gajitunjangan = (basicSalary + tunjangan + value_BPJSPribadi) * (12 - ((12 - month_conv) + (month_conv - 1))); }
            }
           
        }
        else
        {
            gajitunjangan = (basicSalary + tunjangan + value_BPJSPribadi) * (12 - (12 - month_conv));
        }
        return gajitunjangan;
    }
    private static Tuple<double, double, double, double, double, double, double>
        PARAMETER_PAYROLL(string Status_bpjsPribadi, string IDEmployee, double basicsalaryBPJS, double basicsalaryBPJSTK)
    {
        string bpjsPribadi = Status_bpjsPribadi;
        double jkm = 0, jht = 0, jhtPeg = 0, jkk = 0, bpjsKesehatan = 0, bpjsPegawai = 0, bpjsFix = 0, val_biaya_jabatan = 0;

        double bpjsTK = 0, bpjsTKPegawai = 0;
        DataTable bpjsQue = Db.Rs2("select * from TBL_parameter ");
        if (bpjsQue.Rows.Count > 0)
        {
            val_biaya_jabatan = Convert.ToDouble(bpjsQue.Rows[0]["param_value"].ToString());
            double val_bpjsKesehatan_pegawai = Convert.ToDouble(bpjsQue.Rows[4]["param_value"].ToString());
            double val_bpjsTK_pegawai = Convert.ToDouble(bpjsQue.Rows[6]["param_value"].ToString());
            double val_bpjsKesehatan_corp = Convert.ToDouble(bpjsQue.Rows[5]["param_value"].ToString());
            double val_bpjsTK_corp = Convert.ToDouble(bpjsQue.Rows[7]["param_value"].ToString());

            double val_jkk = Convert.ToDouble(bpjsQue.Rows[10]["param_value"].ToString());
            double val_jht = Convert.ToDouble(bpjsQue.Rows[11]["param_value"].ToString());
            double val_jkm = Convert.ToDouble(bpjsQue.Rows[12]["param_value"].ToString());

            double val_jhtPeg = Convert.ToDouble(bpjsQue.Rows[13]["param_value"].ToString());

            DataTable cekbpjs = Db.Rs2("select pp.component_id, m.component_name,he.Employee_ID,pp.privilege_value from TBL_Payroll_Privilege pp left join TBL_Payroll_Role r on pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on he.Employee_ID=EP.Employee_ID left join TBL_MasterComponent m on m.component_id=pp.Component_ID where he.Employee_ID='" + IDEmployee + "' and pp.privilege_choose='1'");

            if (cekbpjs.Rows.Count > 0)
            {
                for (int loop_cekbpjs = 0; loop_cekbpjs < cekbpjs.Rows.Count; loop_cekbpjs++)
                {
                    string component = cekbpjs.Rows[loop_cekbpjs]["component_id"].ToString();
                    if (component == "18")
                    {
                        jkk = (basicsalaryBPJSTK * val_jkk);
                        jkm = (basicsalaryBPJSTK * val_jkm);
                        jht = (basicsalaryBPJSTK * val_jht);

                        jhtPeg = (basicsalaryBPJSTK * val_jhtPeg);

                        bpjsTK = (jkk + jkm + jht) * 12;
                        bpjsTKPegawai = jhtPeg;
                    }
                    if (bpjsPribadi == "yes")
                    {
                        bpjsPegawai = 0;
                        bpjsFix = 0;
                    }
                    else if (bpjsPribadi == "no")
                    {
                        if (component == "20")
                        {
                            bpjsKesehatan = (basicsalaryBPJS * val_bpjsKesehatan_corp);
                            bpjsPegawai = (basicsalaryBPJS * val_bpjsKesehatan_pegawai);
                            bpjsFix = bpjsKesehatan + bpjsPegawai;
                        }
                    }

                }

            }
            else
            {
                bpjsKesehatan = 0; bpjsPegawai = 0;
                bpjsTK = 0; bpjsTKPegawai = 0;
            }
        }
        var tuple = new Tuple<double, double, double, double, double, double, double>(jkk, jkm, jht, bpjsKesehatan, bpjsPegawai, bpjsFix, val_biaya_jabatan);
        return tuple;

    }
    protected double Allowance(string IDEmployee)
    {
        DataTable tun = Db.Rs2("select m.component_name,he.Employee_ID,pp.privilege_value from TBL_Payroll_Privilege pp left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on he.Employee_ID=EP.Employee_ID left join TBL_MasterComponent m  on  m.component_id=pp.Component_ID where he.Employee_ID='" + IDEmployee + "' and pp.privilege_choose='1' and m.component_kind='Tunjangan' and m.component_type='Regular Income' "); 

        double tunjangan = 0;
        if (tun.Rows.Count > 0)
        {
            string comName = "";
            for (int i = 0; i < tun.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(tun.Rows[i]["privilege_value"].ToString()))
                {
                    comName = tun.Rows[i]["component_name"].ToString();
                    tunjangan += Convert.ToDouble(tun.Rows[i]["privilege_value"].ToString());
                }
            }
        }
        return tunjangan;
    }
    protected double AllowanceIrreguler(string IDEmployee)
    {
        //select m.component_name,he.Employee_ID,pp.privilege_value from TBL_Payroll_Privilege pp left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join Gulali_HRIS_Demo.dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join Gulali_HRIS_Demo.dbo.TBL_Employee he on he.Employee_ID=EP.Employee_ID left join TBL_MasterComponent m  on  m.component_id=pp.Component_ID where he.Employee_ID='8' and pp.privilege_choose='1' and m.component_kind='Tunjangan' and m.component_type='Irregular Income'

        DataTable tun2 = Db.Rs2("select m.component_name,he.Employee_ID,pp.privilege_value from TBL_Payroll_Privilege pp left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on he.Employee_ID=EP.Employee_ID left join TBL_MasterComponent m  on  m.component_id=pp.Component_ID where he.Employee_ID='" + IDEmployee + "' and pp.privilege_choose='1' and m.component_kind='Tunjangan' and m.component_type='Irregular Income' ");
        double tunjanganIrregular = 0;
        if (tun2.Rows.Count > 0)
        {

            string comValueIrregular = "";
            for (int y = 0; y < tun2.Rows.Count; y++)
            {
                comValueIrregular = tun2.Rows[y]["privilege_value"].ToString();
                if (!string.IsNullOrEmpty(comValueIrregular))
                {
                    tunjanganIrregular += Convert.ToDouble(comValueIrregular);
                }
            }

        }
        return tunjanganIrregular;
    }
       private static Tuple<double, double>
     Cek_PreviousIrreguler(string encrypted, string original)
    {

        DataTable cekIrregularIncome = Db.Rs2("select top 1 p.payroll_IrregularIncome, p.payroll_unpaidleave   from TBL_Payroll p cross join  " + Param.Db + ".dbo.TBL_Employee he where  p.payroll_employeeid='" + encrypted + "' and he.Employee_ID='" + original + "' and he.Employee_Inactive='1' order by p.payroll_IrregularIncome desc");


        double previous_IrregularIncome = 0, previous_UnpaidLeave=0;
        if (cekIrregularIncome.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(cekIrregularIncome.Rows[0]["payroll_IrregularIncome"].ToString()))
            {
                previous_IrregularIncome = Convert.ToDouble(cekIrregularIncome.Rows[0]["payroll_IrregularIncome"].ToString());
            }
            if (!string.IsNullOrEmpty(cekIrregularIncome.Rows[0]["payroll_unpaidleave"].ToString()))
            {
                previous_UnpaidLeave = Convert.ToDouble(cekIrregularIncome.Rows[0]["payroll_unpaidleave"].ToString());
            }
        }

        var tuple = new Tuple<double, double>(previous_IrregularIncome, previous_UnpaidLeave);
        return tuple;
    }
    private static Tuple<double, double, string, string>
         THRFunction(string joinDate, string religion, double basicsalary, double tunjangan, int month_conv, string To_Conv_Year)
    {
        string year_employment = "0", month_employment = "0", week_employment = "0", day_employment = "0";
        if (!string.IsNullOrEmpty(joinDate))
        {
            DateTime dt = Convert.ToDateTime(joinDate);
            int diffDays = (DateTime.Now - dt).Days;

            if (diffDays > 365)
            {
                int count_year = diffDays / 365;
                year_employment = count_year.ToString();
                diffDays -= count_year * 365;
            }
            if (diffDays > 30)
            {
                int count_month = diffDays / 30;
                month_employment = count_month.ToString();
                diffDays -= count_month * 30;
            }
            if (diffDays > 7)
            {
                int count_week = diffDays / 7;
                week_employment = count_week.ToString();
                diffDays -= count_week * 7;
            }
            day_employment = diffDays.ToString();
        }
        double THRprorate = 0, THR = 0; ;

        DataTable thrQue = Db.Rs("select distinct a.Holiday_List_ID, b.thr_id, a.Holiday_List_Name, c.Holiday_Date from TBL_Holiday_List a join " + Param.Db2 + ".dbo.TBL_THR b on a.Holiday_List_ID = b.holiday_list_id join TBL_Holiday c on c.holiday_list_id = a.Holiday_List_ID where YEAR(Holiday_Date) =" + To_Conv_Year + " and MONTH(Holiday_Date) = " + month_conv + "");

        if (thrQue.Rows.Count > 0)
        {
            int natal = 0, fitri = 0;
            for (int y = 0; y < thrQue.Rows.Count; y++)
            {
                if (thrQue.Rows[y]["Holiday_List_ID"].ToString() == "1")
                {
                    fitri += 1;
                }
                else if (thrQue.Rows[y]["Holiday_List_ID"].ToString() == "2")
                {
                    natal += 1;
                }
            }

            if (religion == "1" && fitri == 1)
            {
                if (!string.IsNullOrEmpty(thrQue.Rows[0]["thr_id"].ToString()))
                {
                    if (!string.IsNullOrEmpty(year_employment))
                    {
                        if (Convert.ToDouble(year_employment) < 1)
                        {
                            THRprorate = ((basicsalary + tunjangan) * Convert.ToDouble(month_employment)) / 12;
                            THR = 0;
                        }
                        else
                        {
                            THR = (basicsalary + tunjangan);
                            THRprorate = 0;
                        }
                    }

                }
            }
            else if (religion == "2" && natal == 1)
            {
                if (!string.IsNullOrEmpty(thrQue.Rows[0]["thr_id"].ToString()))
                {
                    if (!string.IsNullOrEmpty(year_employment))
                    {
                        if (Convert.ToDouble(year_employment) < 1)
                        {
                            THRprorate = ((basicsalary + tunjangan) * Convert.ToDouble(month_employment)) / 12;
                            THR = 0;
                        }
                        else
                        {
                            THR = (basicsalary + tunjangan);
                            THRprorate = 0;
                        }
                    }

                }
            }
        }
        else { THRprorate = 0; THR = 0; }

        var tuple = new Tuple<double, double, string, string>(THR, THRprorate, year_employment, month_employment);
        return tuple;
    }

    private static Tuple<double, double, string, string> THRFunctionAngsur(string joinDate, string religion, double basicsalary, double tunjangan, int month_conv, string To_Conv_Year)
    {
        string year_employment = "0", month_employment = "0", week_employment = "0", day_employment = "0";
        if (!string.IsNullOrEmpty(joinDate))
        {
            DateTime dt = Convert.ToDateTime(joinDate);
            int diffDays = (DateTime.Now - dt).Days;

            if (diffDays > 365)
            {
                int count_year = diffDays / 365;
                year_employment = count_year.ToString();
                diffDays -= count_year * 365;
            }
            if (diffDays > 30)
            {
                int count_month = diffDays / 30;
                month_employment = count_month.ToString();
                diffDays -= count_month * 30;
            }
            if (diffDays > 7)
            {
                int count_week = diffDays / 7;
                week_employment = count_week.ToString();
                diffDays -= count_week * 7;
            }
            day_employment = diffDays.ToString();
        }
        double THRprorate = 0, THR = 0; ;

        DataTable thrQue = Db.Rs("select distinct a.Holiday_List_ID, b.thr_id, a.Holiday_List_Name, c.Holiday_Date from TBL_Holiday_List a join " + Param.Db2 + ".dbo.TBL_THR b on a.Holiday_List_ID = b.holiday_list_id join TBL_Holiday c on c.holiday_list_id = a.Holiday_List_ID where YEAR(Holiday_Date) =" + To_Conv_Year + " and MONTH(Holiday_Date) = " + month_conv + "");

        if (thrQue.Rows.Count > 0)
        {
            THRprorate = 0; THR = 0;
        }
        else
        {


            if ((religion == "1") || (religion != "2"))
            {
                if (!string.IsNullOrEmpty(year_employment))
                {
                    if (Convert.ToDouble(year_employment) < 1)
                    {
                        THRprorate = ((basicsalary + tunjangan) * Convert.ToDouble(month_employment)) / 12;
                        THR = 0;
                    }
                    else
                    {
                        THR = (basicsalary + tunjangan);
                        THRprorate = 0;
                    }
                }


            }
            else { THRprorate = 0; THR = 0; }
        }

        var tuple = new Tuple<double, double, string, string>(THR, THRprorate, year_employment, month_employment);
        return tuple;
    }
    protected double PKP_TaxProgressive(string NPWP, double pkp)
    {
        DataTable pkpQue = Db.Rs2("select * from TBL_progressive_tax ");
        double taxSetahun = 0;
        double pkp_level1_min = Convert.ToDouble(pkpQue.Rows[0]["progressive_tax_min"].ToString());
        double pkp_level1_max = Convert.ToDouble(pkpQue.Rows[0]["progressive_tax_max"].ToString());
        double pkp_level1_prosentase = Convert.ToDouble(pkpQue.Rows[0]["progressive_tax_prosentase"].ToString());
        double pkp_level2_min = Convert.ToDouble(pkpQue.Rows[1]["progressive_tax_min"].ToString());
        double pkp_level2_max = Convert.ToDouble(pkpQue.Rows[1]["progressive_tax_max"].ToString());
        double pkp_level2_prosentase = Convert.ToDouble(pkpQue.Rows[1]["progressive_tax_prosentase"].ToString());
        double pkp_level3_min = Convert.ToDouble(pkpQue.Rows[2]["progressive_tax_min"].ToString());
        double pkp_level3_max = Convert.ToDouble(pkpQue.Rows[2]["progressive_tax_max"].ToString());
        double pkp_level3_prosentase = Convert.ToDouble(pkpQue.Rows[2]["progressive_tax_prosentase"].ToString());
        double pkp_level4_min = Convert.ToDouble(pkpQue.Rows[3]["progressive_tax_min"].ToString());
        double pkp_level4_prosentase = Convert.ToDouble(pkpQue.Rows[3]["progressive_tax_prosentase"].ToString());

        if (NPWP == "1")
        {
            if (pkp <= pkp_level1_max)
            {
                taxSetahun = (pkp * pkp_level1_prosentase);
            }
            else if ((pkp > pkp_level2_min) && (pkp <= pkp_level2_max))
            {
                taxSetahun = ((pkp - pkp_level2_min) * pkp_level2_prosentase) + 2500000;
            }
            else if ((pkp > pkp_level3_min) && (pkp <= pkp_level3_max))
            {
                taxSetahun = ((pkp - pkp_level3_min) * pkp_level3_prosentase) + 32500000;
            }
            else if (pkp > pkp_level4_min)
            {
                taxSetahun = ((pkp - pkp_level4_min) * pkp_level4_prosentase) + 95000000;
            }
        }
        else
        {

            if (pkp <= pkp_level1_max)
            {
                taxSetahun = (pkp * pkp_level1_prosentase) * 120 / 100;
            }
            else if ((pkp > pkp_level2_min) && (pkp <= pkp_level2_max))
            {
                taxSetahun = (((pkp - pkp_level2_min) * pkp_level2_prosentase) + 2500000) * 120 / 100;
            }
            else if ((pkp > pkp_level3_min) && (pkp <= pkp_level3_max))
            {
                taxSetahun = (((pkp - pkp_level3_min) * pkp_level3_prosentase) + 32500000) * 120 / 100;
            }
            else if (pkp > pkp_level4_min)
            {
                taxSetahun = (((pkp - pkp_level4_min) * pkp_level4_prosentase) + 95000000) * 120 / 100;
            }
        }

        return taxSetahun;
    }

    protected void fill()
    {
        if (!string.IsNullOrEmpty(month))
        {
            div_table.Visible = true;
            table.Visible = true;
            btn_generate.Visible = false;
            btn_save.Visible = true;
            tanggal_mulai.Enabled = false;
            tanggal_selesai.Enabled = false;
            //ddlh.Enabled = false;
            //ddlh1.Enabled = false;
            //ddlm.Enabled = false;
            //ddlm1.Enabled = false;
            jam_mulai.Enabled = false;
            jam_selesai.Enabled = false;
            ddl_bulan.Enabled = false;


            ddl_bulan.SelectedValue = month;

            DateTime From = Convert.ToDateTime(from_date);
            DateTime To = Convert.ToDateTime(to_date);
            DateTime From2 = Convert.ToDateTime(From);
            DateTime To2 = Convert.ToDateTime(To).AddDays(1);

            string From_Conv = From.ToString("yyyy-MM-dd hh:mm").ToString();
            string To_Conv = To.ToString("yyyy-MM-dd hh:mm").ToString();

            string From_Conv2 = From.ToString("dd-MM-yyyy").ToString();
            string To_Conv2 = To.ToString("dd-MM-yyyy").ToString();
            tanggal_mulai.Text = From_Conv2;
            tanggal_selesai.Text = To_Conv2;

            string To_Conv_Year = To.ToString("yyyy").ToString();
            string To_Conv_Month = To.ToString("MM").ToString();
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

            //query untuk select dari TBL_Attendance_Holiday, dari periode from s/d periode to ada holiday apa nggak(kecuali sabtu minggu)
            DataTable total_holiday = Db.Rs("select DATENAME(WEEKDAY, Holiday_Date) from TBL_Attendance_Holiday where Holiday_Date between '" + From_Conv + "' and '" + To_Conv + "' and DATENAME(WEEKDAY, Holiday_Date) != 'Sunday' and DATENAME(WEEKDAY, Holiday_Date) != 'Saturday'");

            //total hari kerja yang sudah dipotong holiday
            int total_kerja_min_holiday = total_hari_kerja - total_holiday.Rows.Count;
            */
            //   DataTable employee_id = Db.Rs("SELECT distinct Employee_ID, he.Employee_BasicSalary, he.Employee_UnpaidLeave, he.Employee_NIK, he.Employee_Name, he.Employee_OthersAllowance, he.Employee_Jamsostek, he.Employee_LeaveBalance, he.Employee_SickLeave, he.Employee_UnpaidLeave FROM TBL_Absen ha join TBL_Employee he on Absen_Employee=Employee_Name where Absen_Date between '" + From_Conv + "' and '" + To_Conv + "'  order by Employee_ID asc");
            StringBuilder x = new StringBuilder();
            x.Append(" <table class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\"> <thead> <tr>"
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
            DataTable employee_id = Db.Rs("SELECT distinct Religion_ID, he.Employee_ID,pp.Admin_Role, he.Employee_Full_Name, EP.Payroll_BPJS_Pribadi, EP.Payroll_salaryPPH, EP.Payroll_salaryBPJS, EP.Payroll_salaryBPJSTK, EP.Payroll_UnpaidLeave, he.Employee_NIK,  EP.Payroll_OthersAllowance, EP.Payroll_Jamsostek,EP.PTKP_ID, he.Employee_Sum_LeaveBalance, he.Employee_Sum_SickLeave, EP.Payroll_UnpaidLeave, he.Employee_JoinDate, he.Division_ID, EP.Payroll_BPJS_Pribadi_Value   FROM  TBL_Employee he left join TBL_Position t on he.Position_ID=t.Position_ID left join TBL_Employee_Payroll EP on he.Employee_ID=EP.Employee_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role r on  EP.Payroll_Group=r.role_id  left join " + Param.Db2 + ".dbo.TBL_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role  where EP.Payroll_Group is not null and pp.privilege_choose=1 and he.Employee_Inactive='1' order by  pp.admin_role,he.Employee_JoinDate,he.Division_ID asc");
            //and he.Employee_ID in ('2','3','4','5','6','7')
            //and he.employee_id in ('1308','1304','1357','1352') 
            for (int a = 0; a < employee_id.Rows.Count; a++)
            {
                string id = employee_id.Rows[a]["Employee_ID"].ToString();
                decimal unpaidleave;
                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_UnpaidLeave"].ToString()))
                {
                    unpaidleave = Convert.ToDecimal(employee_id.Rows[a]["Payroll_UnpaidLeave"]);
                }
                else
                {
                    unpaidleave = 0;
                }
                string encryptionPassword = "BatavianetICCTFHRISpass@word1";
                string id_emp = id;
                string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                string original = Crypto.Decrypt(encrypted, encryptionPassword);

                double basicsalary = 0; double allowance = 0;
                double basicsalaryPPH = 0, basicsalaryBPJS = 0, basicsalaryBPJSTK = 0, value_BPJSPribadi = 0;

                /*History Salary*/
                basicsalary = HistorySalary(encrypted);

                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_salaryBPJS"].ToString()))
                {
                    basicsalaryBPJS = Convert.ToDouble(employee_id.Rows[a]["Payroll_salaryBPJS"]);
                }
                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_salaryBPJSTK"].ToString()))
                {
                    basicsalaryBPJSTK = Convert.ToDouble(employee_id.Rows[a]["Payroll_salaryBPJSTK"]);
                }
                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_BPJS_Pribadi_Value"].ToString()))
                {
                    value_BPJSPribadi = Convert.ToDouble(employee_id.Rows[a]["Payroll_BPJS_Pribadi_Value"]);
                }
                else { value_BPJSPribadi = 0; }
                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_salaryPPH"].ToString()))
                {
                    basicsalaryPPH = Convert.ToDouble(employee_id.Rows[a]["Payroll_salaryPPH"]);
                }
                else { basicsalaryPPH = 0; }


                if (!string.IsNullOrEmpty(employee_id.Rows[a]["Payroll_OthersAllowance"].ToString()))
                {
                    allowance = Convert.ToDouble(employee_id.Rows[a]["Payroll_OthersAllowance"]);
                }
                else { allowance = 0; }
                string ptkpstatus = employee_id.Rows[a]["PTKP_ID"].ToString();

                double jkm = 0, jht = 0, jhtPeg = 0, jkk = 0, bpjsKesehatan = 0, bpjsPegawai = 0, bpjsFix = 0;
                string bpjsPribadi = employee_id.Rows[a]["Payroll_BPJS_Pribadi"].ToString();

                var result = PARAMETER_PAYROLL(bpjsPribadi, id, basicsalaryBPJS, basicsalaryBPJSTK);

                jkk = result.Item1;
                jkm = result.Item2;
                jht = result.Item3;
                bpjsKesehatan = result.Item4;
                bpjsPegawai = result.Item5;
                bpjsFix = result.Item6;


                double tunjangan = 0;
                tunjangan = Allowance(id);

                string joinDate = employee_id.Rows[a]["Employee_JoinDate"].ToString();
                string religion = employee_id.Rows[a]["Religion_ID"].ToString();
                string year_employment = "", month_employment = "";
                double THR = 0, THRprorate = 0;




                int month_conv = Convert.ToInt32(Cf.StrSql(To_Conv_Month));
                var resultTHR = THRFunction(joinDate, religion, basicsalary, tunjangan, month_conv, To_Conv_Year);
                THRprorate = resultTHR.Item2;
                year_employment = resultTHR.Item3;
                month_employment = resultTHR.Item4;

                if (!string.IsNullOrEmpty(year_employment))
                {
                    if (Convert.ToDouble(year_employment) < 1)
                    {
                        THR = THRprorate;
                    }
                    else { THR = resultTHR.Item1; }
                }


                double count = 0;
                Literal lit_a = new Literal();
                lit_a.ID = "Print_a_" + a + "";
                lit_a.Text = "<tbody>"
                            + "<tr>"
                            + "<td  valign=\"top\"  style=\"text-align:center;vertical-align:middle;\">" + (a + 1).ToString() + "</td>"
                            + "<td valign=\"top\" style=\"padding-left:2px;vertical-align:middle;max-width:130px; min-width:130px;word-wrap:break-word;\" >" + Cf.UpperFirst(employee_id.Rows[a]["admin_role"].ToString()) + "</td>"
                            + "<td  valign=\"top\"  style=\"padding-left:2px;vertical-align:middle;max-width:140px; min-width:140px;word-wrap:break-word;\">" + employee_id.Rows[a]["Employee_Full_Name"].ToString() +"</td>"
                            + "<td valign=\"top\" style=\"text-align:right; vertical-align:middle; \">" + basicsalary.ToString("#,##0.00") + "</td>"
                            + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + tunjangan.ToString("#,##0.00") + "</td>"
                            + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + bpjsKesehatan.ToString("#,##0.00") + "</td>"
                            + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jkk.ToString("#,##0.00") + "</td>"
                            + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jht.ToString("#,##0.00") + "</td>"
                            + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;width:67px;\">" + jkm.ToString("#,##0.00") + "</td>"
                + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
                PH.Controls.Add(lit_a);

                TextBox txt_THR = new TextBox();
                txt_THR.ID = "txt_THR_" + (a + 1).ToString();
                txt_THR.Text = THR.ToString("#,##0.00");
                txt_THR.Width = 60;
                txt_THR.Height = 30;
                txt_THR.CssClass = "form-control";
                PH.Controls.Add(txt_THR);

                Literal lit_THR = new Literal();
                lit_THR.ID = "Print_THR_" + a + "";
                lit_THR.Text = "</td>"

                + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
                PH.Controls.Add(lit_THR);

                TextBox txt_overtime = new TextBox();
                txt_overtime.ID = "txt_overtime_" + (a + 1).ToString();
                txt_overtime.Text = count.ToString("#,##0.00");
                txt_overtime.Width = 60;
                txt_overtime.Height = 30;
                txt_overtime.CssClass = "form-control";
                PH.Controls.Add(txt_overtime);

                Literal lit_overtime = new Literal();
                lit_overtime.ID = "Print_overtime_" + a + "";
                lit_overtime.Text = "</td>"

                + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
                PH.Controls.Add(lit_overtime);

                TextBox txt_bonus = new TextBox();
                txt_bonus.ID = "Txt_bonus_" + (a + 1).ToString();
                txt_bonus.Text = count.ToString("#,##0.00");
                txt_bonus.Width = 60;
                txt_bonus.Height = 30;
                txt_bonus.CssClass = "form-control";
                PH.Controls.Add(txt_bonus);

                Literal lit_bonus = new Literal();
                lit_bonus.ID = "Print_bonus_" + a + "";
                lit_bonus.Text = "</td>"

                + "<td valign=\"top\" style=\"text-align:center;vertical-align:middle;\">";
                PH.Controls.Add(lit_bonus);

                TextBox txt_insentive = new TextBox();
                txt_insentive.ID = "txt_insentive_" + (a + 1).ToString();
                txt_insentive.Text = count.ToString("#,##0.00");
                txt_insentive.Width = 60;
                txt_insentive.Height = 30;
                txt_insentive.CssClass = "form-control";
                PH.Controls.Add(txt_insentive);

                Literal lit_insentive = new Literal();
                lit_insentive.ID = "Print_insentive_" + a + "";
                lit_insentive.Text = "</td>"
                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">" + value_BPJSPribadi.ToString("#,##0.00") + "</td>"
                + "<td valign=\"top\" style=\"text-align:right;width:78px; vertical-align:middle;\">" + bpjsPegawai.ToString("#,##0.00") + "</td>"
                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle; \">" + jhtPeg.ToString("#,##0.00") + "</td>"

                + "<td valign=\"top\" style=\"text-align:right;vertical-align:middle;\">";
                PH.Controls.Add(lit_insentive);


                TextBox txt_unpaidleave = new TextBox();
                txt_unpaidleave.ID = "Txt_unpaidleave_" + (a + 1).ToString();
                txt_unpaidleave.Text = count.ToString("#,##0.00");
                txt_unpaidleave.Width = 60;
                txt_unpaidleave.Height = 30;
                txt_unpaidleave.CssClass = "form-control";
                PH.Controls.Add(txt_unpaidleave);

                Literal lit_unpaidleave = new Literal();
                lit_unpaidleave.ID = "Print_unpaidleave_" + a + "";
                lit_unpaidleave.Text = "</td>"

                + "</tr>"
                + "</tbody>";
                PH.Controls.Add(lit_unpaidleave);
            }
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        //div_table.Attributes.Add("Visible","True");
        table.Visible = true;
        btn_generate.Visible = false;
        //td_final.Visible = true;
        //submit_final2.Visible = false;

        DateTime From = Convert.ToDateTime(tanggal_mulai.Text + " " + jam_mulai.Text);
        DateTime To = Convert.ToDateTime(tanggal_selesai.Text + " " + jam_selesai.Text);
        DateTime From2 = Convert.ToDateTime(tanggal_mulai.Text);
        DateTime To2 = Convert.ToDateTime(tanggal_selesai.Text).AddDays(1);

        string From_Conv = From.ToString("yyyy-MM-dd hh:mm").ToString();
        string To_Conv = To.ToString("yyyy-MM-dd hh:mm").ToString();

        string To_Conv_Year = To.ToString("yyyy").ToString();
        string To_Conv_Month = To.ToString("MM").ToString();

        DataTable rs = Db.Rs2("Select max(Flag_DateCreate) as Flag_DateCreate from TBL_Payroll_Flag");

        if (!string.IsNullOrEmpty(rs.Rows[0]["Flag_DateCreate"].ToString()))
        {

            DateTime nana = Convert.ToDateTime(rs.Rows[0]["Flag_DateCreate"].ToString());
            // BLOCK PERIODE HERE
            //if (nana.Month == DateTime.Now.Month)
            //{
            //    Response.Redirect("/payroll/");
            //}
            //else
            //{
            int month = DateTime.Now.Month;
            int select = Convert.ToInt32(ddl_bulan.SelectedValue);
            // BLOCK PERIODE HERE
            //if (month != select)
            //{
            //    Response.Redirect("/payroll/");
            //}
            //else
            //{
            Response.Redirect("/gulali/administrator/keuangan/payroll/generate/?from_date=" + From_Conv + "&to_date=" + To_Conv + "&month=" + ddl_bulan.SelectedValue + "");
            //}
            //}
        }

        else
        {
            int month = DateTime.Now.Month;
            int select = Convert.ToInt32(ddl_bulan.SelectedValue);
            // BLOCK PERIODE HERE
            //if (month != select)
            //{
            //    Response.Redirect("/payroll/");
            //}
            //else
            //{
            Response.Redirect("/gulali/administrator/keuangan/payroll/generate/?from_date=" + From_Conv + "&to_date=" + To_Conv + "&month=" + ddl_bulan.SelectedValue + "");
            //}
        }
    }

    protected void BtnSubmit_Final(object sender, EventArgs e)
    {
        DateTime From = Convert.ToDateTime(from_date);
        DateTime To = Convert.ToDateTime(to_date);
        DateTime From2 = Convert.ToDateTime(From);
        DateTime To2 = Convert.ToDateTime(To).AddDays(1);

        string From_Conv = From.ToString("yyyy-MM-dd hh:mm").ToString();
        string To_Conv = To.ToString("yyyy-MM-dd hh:mm").ToString();

        string From_Conv2 = From.ToString("dd-MM-yyyy").ToString();
        string To_Conv2 = To.ToString("dd-MM-yyyy").ToString();
        tanggal_mulai.Text = From_Conv2;
        tanggal_selesai.Text = To_Conv2;
        string To_Conv_Year = To.ToString("yyyy").ToString();
        string To_Conv_Month = To.ToString("MM").ToString();

        double v_thr = 0, v_overtime = 0, v_bonus = 0, v_insentive = 0, v_unpaidleave = 0;
        double total = 0, totala = 0;
        double value_BPJSPribadi = 0;
        DataTable cek = Db.Rs2("Select * from TBL_Payroll_Flag where Flag_Month = '" + To_Conv_Month + "' and Flag_Year = '" + To_Conv_Year + "' and Flag_ForMonth = '" + ddl_bulan.SelectedValue + "'");
        if (cek.Rows.Count == 0)
        {
            StringBuilder x = new StringBuilder();
            DataTable rs = Db.Rs("SELECT distinct he.Employee_ID,pp.Admin_Role, he.Employee_Full_Name, EP.Payroll_BPJS_Pribadi, EP.Payroll_salaryPPH, EP.Payroll_salaryBPJS,   EP.Payroll_UnpaidLeave, he.Employee_NIK,  EP.Payroll_OthersAllowance, EP.Payroll_Jamsostek, he.Employee_Sum_LeaveBalance, he.Employee_Sum_SickLeave, EP.Payroll_UnpaidLeave,EP.PTKP_ID, EP.Payroll_NPWP, he.Employee_JoinDate,he.Religion_ID,  he.Division_ID, EP.Payroll_BPJS_Pribadi_Value FROM TBL_Employee he left join TBL_Position t on he.Position_ID=t.Position_ID left join TBL_Employee_Payroll EP on he.Employee_ID=EP.Employee_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role r on  EP.Payroll_Group=r.role_id  left join " + Param.Db2 + ".dbo.TBL_Payroll_Privilege pp on r.Admin_Role=pp.Admin_Role  where EP.Payroll_Group is not null and pp.privilege_choose=1 and he.Employee_Inactive='1' order by  pp.admin_role,he.Employee_JoinDate,he.Division_ID asc");
            //and he.Employee_ID in ('2','3','4','5','6','7')
            //and he.employee_id in ('1308','1304','1357','1352')
            if (valid(rs.Rows.Count))
            {
                /*INSERT PAYROLL FLAG*/
                /*Start Here :)*/
                string insert_flag = "insert into TBL_Payroll_Flag(Flag_Month, Flag_Year, Flag_ForMonth, Flag_TotalGaji, Flag_DateCreate)"
                   + "values('" + Cf.StrSql(To_Conv_Month) + "', '" + Cf.StrSql(To_Conv_Year) + "',  '" + Cf.StrSql(ddl_bulan.SelectedValue) + "', 0, getdate())";
                Db.Execute2(insert_flag);
                /*End Here :)*/


                DataTable rsa = Db.Rs2("select MAX(Flag_ID) maksi from TBL_Payroll_Flag");
                string max;
                if (!string.IsNullOrEmpty(rsa.Rows[0]["maksi"].ToString()))
                {
                    max = rsa.Rows[0]["maksi"].ToString();
                }
                else
                {
                    max = "1";
                }

                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string id = rs.Rows[i]["Employee_ID"].ToString();
                    double basicSalaryBPJS = 0, basicSalaryPPH = 0, basicSalaryBPJSTK = 0;
                    decimal unpaidleavedb = 0;

                    if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryBPJS"].ToString()))
                    { basicSalaryBPJS = Convert.ToDouble(rs.Rows[i]["Payroll_salaryBPJS"]); }
                    else { basicSalaryBPJS = 0; }
                    if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_salaryPPH"].ToString()))
                    { basicSalaryPPH = Convert.ToDouble(rs.Rows[i]["Payroll_salaryPPH"]); }
                    else { basicSalaryPPH = 0; }

                    if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_BPJS_Pribadi_Value"].ToString()))
                    { value_BPJSPribadi = Convert.ToDouble(rs.Rows[i]["Payroll_BPJS_Pribadi_Value"]); }
                    else { value_BPJSPribadi = 0; }

                    if (!string.IsNullOrEmpty(rs.Rows[i]["Payroll_UnpaidLeave"].ToString()))
                    { unpaidleavedb = Convert.ToDecimal(rs.Rows[i]["Payroll_UnpaidLeave"]); }

                    string encryptionPassword = "BatavianetICCTFHRISpass@word1";
                    string id_emp = id;
                    string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                    string original = Crypto.Decrypt(encrypted, encryptionPassword);
                    DataTable baseSalary = Db.Rs2("Select * from TBL_HistorySalary where Employee_ID='" + encrypted + "' and month(HistorySalary_DateChanged)=" + ddl_bulan.SelectedValue + " order by HistorySalary_DateChanged desc");
                    DataTable baseSalary2 = Db.Rs2("Select * from TBL_HistorySalary where Employee_ID='" + encrypted + "' order by HistorySalary_DateChanged desc");


                    double basicSalary = 0;
                    /*History Salary*/
                    basicSalary = HistorySalary(encrypted);

                    decimal OtherAllowance = Convert.ToDecimal("0.0");
                    // Convert.ToDecimal(rs.Rows[i]["Employee_OthersAllowance"]);
                    decimal assurance = Convert.ToDecimal("0.0");
                    decimal incentive = Convert.ToDecimal("0.0");
                    double bpjsPegawai = 0, bpjsFix = 0, bpjsKesehatan = 0;

                    string bpjsPribadi = rs.Rows[i]["Payroll_BPJS_Pribadi"].ToString();

                    TextBox txt_THR = (TextBox)PH.FindControl("txt_THR_" + (i + 1));
                    TextBox txt_overtime = (TextBox)PH.FindControl("txt_overtime_" + (i + 1));
                    TextBox txt_bonus = (TextBox)PH.FindControl("txt_bonus_" + (i + 1));
                    TextBox txt_insentive = (TextBox)PH.FindControl("txt_insentive_" + (i + 1));
                    TextBox txt_unpaidleave = (TextBox)PH.FindControl("txt_unpaidleave_" + (i + 1));

                    //txt2=THR; txt3=overtime; txt4=bonus; txt5=insentive; txt6=unpaidleave;
                    v_thr = Cf.Double(txt_THR.Text);
                    string txt2 = v_thr.ToString().Replace(",", ".");
                    v_overtime = Cf.Double(txt_overtime.Text);
                    string txt3 = v_overtime.ToString().Replace(",", ".");
                    v_bonus = Cf.Double(txt_bonus.Text);
                    string txt4 = v_bonus.ToString().Replace(",", ".");
                    v_insentive = Cf.Double(txt_insentive.Text);
                    string txt5 = v_insentive.ToString().Replace(",", ".");
                    v_unpaidleave = Cf.Double(txt_unpaidleave.Text);
                    string txt6 = v_unpaidleave.ToString().Replace(",", ".");

                    double tax = 0;
                    double ptkp = 0, jkk = 0, jhtPeg = 0, jht = 0, jkm = 0, bruto = 0, netto = 0;
                    double BJ = 0, setahunkan = 0, taxSetahun = 0, pkp = 0, val_biaya_jabatan = 0;
                    string ptkpstatus = rs.Rows[i]["PTKP_ID"].ToString();

                    double tunjanganIrregular = 0;
                    double tunjangan = 0;

                    DataTable select_ptkp = Db.Rs2("Select * from TBL_PTKP where PTKP_ID='" + ptkpstatus + "'");
                    ptkp = Convert.ToDouble(select_ptkp.Rows[0]["ptkp_value"]);

                    string NPWP = rs.Rows[i]["Payroll_NPWP"].ToString();

                    tunjangan = Allowance(id);
                    tunjanganIrregular = AllowanceIrreguler(id);

                    var result = PARAMETER_PAYROLL(bpjsPribadi, id, basicSalaryBPJS, basicSalaryBPJSTK);

                    jkk = result.Item1;
                    jkm = result.Item2;
                    jht = result.Item3;
                    bpjsKesehatan = result.Item4;
                    bpjsPegawai = result.Item5;
                    bpjsFix = result.Item6;
                    val_biaya_jabatan = result.Item7;
                    // TEXTBOXT IRREGULER
                    //txt2=THR; txt3=overtime; txt4=bonus; txt5=insentive; txt6=unpaidleave;
                    double thr = 0, overtime = 0, bonus = 0, insentive = 0, unpaidleave = 0;
                    double RegularIncome = 0, IrregularIncome = 0, gajitunjangan = 0, previous_IrregularIncome = 0, previous_UnpaidLeave = 0; ;
                    thr = v_thr;
                    overtime = Convert.ToDouble(txt3.ToString());
                    bonus = v_bonus;
                    //insentive = v_insentive;
                    insentive = Convert.ToDouble(txt5.ToString());
                    unpaidleave = Convert.ToDouble(txt6.ToString());

                    int month_conv = Convert.ToInt32(Cf.StrSql(To_Conv_Month));

                    /* Gaji Tunjangan Setahun*/
                    /* Start Here :)*/
                    gajitunjangan = GajiTunjanganFunction(month_conv, To_Conv_Year, encrypted, basicSalary, tunjangan, value_BPJSPribadi);
                    /*End Here */

                    /* CHECK PREVIOUS IRREGULER INCOME */
                    /*Start Here :)*/
                    var resultPrevious = Cek_PreviousIrreguler(encrypted, original);
                    previous_IrregularIncome = resultPrevious.Item1;
                    previous_UnpaidLeave = resultPrevious.Item2;
                    /*End Here */

                    /* PPH Bulan Berjalan */
                    /* ---------------------- */


                    RegularIncome = gajitunjangan + (bpjsKesehatan * (12 - (12 - month_conv))) + ((jkk + jkm + jht) * (12 - (12 - month_conv)));
                    if (month_conv > 1)
                    { IrregularIncome = (overtime  + bonus + insentive + tunjanganIrregular) + thr + previous_IrregularIncome; }
                    else
                    { IrregularIncome = (overtime  + bonus + insentive + tunjanganIrregular); }


                    // bulan kenaikan gaji
                    int month_naik = 0;
                    DataTable MonthNaik = Db.Rs2("Select top 1 HistorySalary_value, HistorySalary_DateChanged from TBL_HistorySalary where Employee_ID='" + encrypted + "'  order by HistorySalary_DateChanged desc");

                    if (MonthNaik.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(MonthNaik.Rows[0]["HistorySalary_DateChanged"].ToString()))
                        {
                            DateTime naik = Convert.ToDateTime(MonthNaik.Rows[0]["HistorySalary_DateChanged"].ToString());
                            month_naik = Convert.ToInt32(naik.ToString("MM").ToString());
                        }

                    }

                    // Cek Kenaikan Gaji
                    DataTable Kenaikan_Gaji = Db.Rs2("select ([Payroll_BasicSalary]) as sum from TBL_Payroll p cross join  " + Param.Db + ".dbo.TBL_Employee he where  p.payroll_employeeid='" + encrypted + "' and he.Employee_ID='" + original + "' and he.Employee_Inactive='1' and month([Payroll_PeriodTo])< " + month_naik + "   group by  ([Payroll_BasicSalary]) order by  [Payroll_BasicSalary]desc");


                    double Up_salary = 0, let = 0;

                    if (Kenaikan_Gaji.Rows.Count > 0)
                    {
                        let = Convert.ToDouble(Kenaikan_Gaji.Rows[0]["sum"].ToString());

                        for (int g = 0; g < Kenaikan_Gaji.Rows.Count; g++)
                        {
                            if (!string.IsNullOrEmpty(Kenaikan_Gaji.Rows[g]["sum"].ToString()))
                            {
                                Up_salary = Convert.ToDouble(Kenaikan_Gaji.Rows[g]["sum"].ToString());
                            }
                            if (Up_salary < let) { let = Up_salary - let; }
                            else
                            {
                                //let = let- Up_salary; 
                            }

                        }
                    }

                    // Bruto

                    double RegularIncomeDefault = (basicSalary + tunjangan + value_BPJSPribadi);
                    double brutoDefault = RegularIncomeDefault + bpjsKesehatan + (jkk + jkm + jht) + (overtime + bonus + insentive + tunjanganIrregular);
                    bruto = RegularIncome + IrregularIncome;

                    // Biaya Jabatan = BJ
                    double BJProgressive = 0, BJProgressivedefault = 500000 * 11, BJDefault = 0;
                    BJ = (5 * (bruto - previous_UnpaidLeave)) / 100;
                    BJDefault = (5 * (brutoDefault - previous_UnpaidLeave)) / 100;
                    BJProgressive = (500000 * (12 - month_conv));

                    if (BJ >= (val_biaya_jabatan - BJProgressive))
                    { BJ = val_biaya_jabatan - BJProgressive; }

                    if (BJDefault >= (val_biaya_jabatan - BJProgressivedefault))
                    { BJDefault = val_biaya_jabatan - BJProgressivedefault; }

                    //Angsur THR
                    string joinDate = rs.Rows[i]["Employee_JoinDate"].ToString();
                    string religion = rs.Rows[i]["Religion_ID"].ToString();
                    string year_employment = "", month_employment = "";
                    double THR = 0, THRprorate = 0;

                    //THR COUNT to PPH
                    var resultTHR = THRFunctionAngsur(joinDate, religion, basicSalary, tunjangan, month_conv, To_Conv_Year);
                    THRprorate = resultTHR.Item2;
                    year_employment = resultTHR.Item3;
                    month_employment = resultTHR.Item4;

                    // Cek Previous PPH
                    DataTable prev_pph = Db.Rs2("select  sum( p.Payroll_pph21_periode_ini  ) as prev_pph from TBL_Payroll p cross join  " + Param.Db + ".dbo.TBL_Employee he where  p.payroll_employeeid='" + encrypted + "' and he.Employee_ID='" + original + "' and he.Employee_Inactive='1' and month([Payroll_PeriodTo])< " + month_conv + "");


                    double previous_pph = 0;
                    if (prev_pph.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(prev_pph.Rows[0]["prev_pph"].ToString()))
                        {
                            previous_pph = Convert.ToDouble(prev_pph.Rows[0]["prev_pph"].ToString());
                        }
                    }
                    if (!string.IsNullOrEmpty(year_employment))
                    {

                        if (Convert.ToDouble(year_employment) < 1)
                        {
                            THR = THRprorate;
                        }
                        else { THR = RegularIncomeDefault; }

                    }
                    double nettoDefault = 0;
                    nettoDefault = brutoDefault - (BJDefault + (unpaidleave + bpjsPegawai + jhtPeg));


                    // Netto Disetahunkan
                    if (month_conv > month_naik + 1)
                    {
                        netto = bruto - (BJ + (unpaidleave + (bpjsPegawai * (12 - (12 - month_conv))) + (jhtPeg * (12 - (12 - month_conv)))));

                    }
                    else
                    {

                            /* Cek Payroll Kosong */
                            /*Start Here :)*/
                            string month_else = "";
                            for (int f = 1; f < month_conv; f++)
                            {
                                month_else += "" + f + "','";
                            }

                            DataTable cekPayrollKosong = Db.Rs2("select (sum(Payroll_BasicSalary + Payroll_TotalAllowance+Payroll_Overtime)) as PayrollKosong from TBL_Payroll where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(To_Conv_Year) + "' and month(Payroll_PeriodTo) in ('" + month_else + "')");

                            /*End Here */

                            if (cekPayrollKosong.Rows.Count > 0) {
                                netto = bruto - (BJ + (previous_UnpaidLeave + (bpjsPegawai * (12 - (12 - month_conv))) + (jhtPeg * (12 - (12 - month_conv)))));
                            }
                            else {
                                netto = nettoDefault;
                            }
                        

                        
                    }
        

                    // Jika Basic Salary naik
                    if (Up_salary > 0)
                    {
                        if ((basicSalary * month_conv) != (Up_salary * month_conv))
                        {
                            setahunkan = ((nettoDefault * 12) + (overtime + bonus + insentive + tunjanganIrregular) + previous_IrregularIncome) - ((basicSalary - let) * (12 - (month_naik - 1)));
                        }
                        else
                        {
                            setahunkan = (nettoDefault * 12) + THR + (overtime + bonus + insentive + tunjanganIrregular) + previous_IrregularIncome;
                        }
                    }

                    else
                    {
                        if (month_conv > 1)
                        {
                            setahunkan = (netto * 12)  + THR + (overtime + bonus + insentive + tunjanganIrregular) + previous_IrregularIncome;
                        }
                        else
                        {
                            setahunkan = (netto * 12)  + THR + (overtime + bonus + insentive + tunjanganIrregular) + previous_IrregularIncome;
                        }
                    }



                    if (setahunkan <= ptkp) { taxSetahun = 0; }
                    else
                    {
                        pkp = setahunkan - ptkp;
                        taxSetahun = PKP_TaxProgressive(NPWP, pkp);
                        if (month_conv == 1) { tax = taxSetahun / 12; } else { tax = (taxSetahun - previous_pph) / (12 - (month_conv - 1)); }
                    }

                    /*End Here */

                    /*INSERT PAYROLL - TBL_Payroll*/
                    /*Start Here :)*/

                    string insert = "insert into TBL_Payroll(Payroll_EmployeeID, Payroll_PeriodFrom, Payroll_PeriodTo,Payroll_pph21_periode_ini, Payroll_BasicSalary, Payroll_BasicSalaryPPH,Payroll_BasicSalaryBPJS,Payroll_TotalAllowance,Payroll_Overtime, Payroll_bpjs, Payroll_BPJSfix,Payroll_UnpaidLeave, Payroll_UserCreate, Payroll_DateCreate, Flag_ID, Payroll_bonus, Payroll_THR, Payroll_Incentive,Payroll_pkp,Payroll_ptkp,Payroll_netto,Payroll_bruto,Payroll_setahunkan, Payroll_npwp, Payroll_pph21_disetahunkan, payroll_irregularincome,payroll_allowance,payroll_tunjangan_lainnya,payroll_biaya_jabatan,payroll_jkm, payroll_jkk,payroll_jht,payroll_jhtPeg, Payroll_BPJSPeg, Payroll_BPJSPribadi)"
                              + " values('" + encrypted + "', '" + From_Conv + "', '" + To_Conv + "','" + tax.ToString().Replace(",", ".") + "', '" + basicSalary.ToString().Replace(",", ".") + "', '" + basicSalaryPPH.ToString().Replace(",", ".") + "','" + basicSalaryBPJS.ToString().Replace(",", ".") + "', '" + tunjangan.ToString().Replace(",", ".") + "',  '" + txt3.ToString().Replace(",", ".") + "', '" + bpjsKesehatan.ToString().Replace(",", ".") + "','" + bpjsFix.ToString().Replace(",", ".") + "', '" + txt6 + "', '" + App.Employee_ID + "', getdate(), '" + max + "','" + txt4.ToString().Replace(",", ".") + "','" + txt2.ToString().Replace(",", ".") + "','" + txt5.ToString().Replace(",", ".") + "','" + pkp.ToString().Replace(",", ".") + "' ,'" + ptkp.ToString().Replace(",", ".") + "' ,'" + netto.ToString().Replace(",", ".") + "','" + bruto.ToString().Replace(",", ".") + "','" + setahunkan.ToString().Replace(",", ".") + "','" + NPWP.ToString() + "','" + taxSetahun.ToString().Replace(",", ".") + "','" + IrregularIncome.ToString().Replace(",", ".") + "',0,0,'" + BJ.ToString().Replace(",", ".") + "','" + jkm.ToString().Replace(",", ".") + "','" + jkk.ToString().Replace(",", ".") + "','" + jht.ToString().Replace(",", ".") + "','" + jhtPeg.ToString().Replace(",", ".") + "' , '" + bpjsPegawai.ToString().Replace(",", ".") + "', '" + value_BPJSPribadi.ToString().Replace(",", ".") + "')";
                    Db.Execute2(insert);
                    /*End Here :)*/

                    int maxID = 0;
                    DataTable IDPAY = Db.Rs2("select max(payroll_id)as y from TBL_Payroll");
                    if (IDPAY.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(IDPAY.Rows[0]["y"].ToString())))
                        {
                            maxID = Convert.ToInt32(IDPAY.Rows[0]["y"].ToString());
                        }
                        else
                        {
                            maxID = 1;
                        }
                    }

                    /*INSERT PAYROLL DETAIL ALLOWANCE - TBL_Payroll_Det_Allowance*/
                    /*Start Here :)*/
                    DataTable tunx = Db.Rs2("select r.admin_role,m.component_id,pp.privilege_value,he.Employee_ID from TBL_Payroll_Privilege pp left join TBL_Payroll_Role r on  pp.Admin_Role=r.Admin_Role left join " + Param.Db + ".dbo.TBL_Employee_Payroll EP on EP.Payroll_Group=r.role_id left join " + Param.Db + ".dbo.TBL_Employee he on he.Employee_ID=EP.Employee_ID left join TBL_MasterComponent m on m.component_id=pp.Component_ID where he.Employee_ID='" + id + "' and pp.privilege_choose='1' and pp.privilege_value is not null ");

                    string insert_tunjangan = "";
                    if (tunx.Rows.Count > 0)
                    {
                        string comID = "", comValue = "", employee_id = "", idrole = "";
                        for (int y = 0; y < tunx.Rows.Count; y++)
                        {
                            idrole = tunx.Rows[y]["admin_role"].ToString();
                            comID = tunx.Rows[y]["component_id"].ToString();
                            if (!string.IsNullOrEmpty(tunx.Rows[y]["privilege_value"].ToString()))
                            {
                                comValue = tunx.Rows[y]["privilege_value"].ToString();
                            }
                            else { comValue = "0"; }
                            employee_id = tunx.Rows[y]["employee_id"].ToString();
                            if (!string.IsNullOrEmpty(comValue))
                            {
                                insert_tunjangan += " INSERT INTO TBL_payroll_det_allowance "
                                     + "(det_allowance_payroll_id,det_allowance_component_id,det_allowance_value, det_allowance_employee_id,det_allowance_group, det_allowance_flag_id) "
                                     + "values "
                                     + "('" + maxID + "','" + comID + "','" + comValue + "','" + encrypted + "','" + Cf.UpperFirst(idrole) + "', '" + Convert.ToDouble(max) + "')";
                            }
                        }
                    }

                    Db.Execute2(insert_tunjangan);
                    /*End Here :)*/

                    total += v_thr;
                    totala += v_overtime;

                }
                string total2 = total.ToString().Replace(",", ".");
                string total3 = totala.ToString().Replace(",", ".");


                DataTable hitungtotal = Db.Rs2("select ((SUM(Payroll_BasicSalary) + SUM(Payroll_BPJS) + SUM(Payroll_Overtime) ) - " + total2 + " - " + total3 + " ) as total_gaji from TBL_Payroll where Flag_ID='" + max + "'");
                decimal gaji;
                if (!string.IsNullOrEmpty(hitungtotal.Rows[0]["total_gaji"].ToString()))
                {
                    gaji = Convert.ToDecimal(hitungtotal.Rows[0]["total_gaji"]);
                }
                else
                {
                    gaji = 0;
                }
                string gaji2 = gaji.ToString();

                string update = "Update TBL_Payroll_Flag set Flag_TotalGaji='" + gaji2 + "'";
                Db.Execute2(update);
                string update2 = "Update TBL_Employee_Payroll set Payroll_UnpaidLeave=0";
                Db.Execute(update2);
                Response.Redirect("../");
            }
            else
            {
                Js.Alert(this, "salah input format");
            }
        }

    }
    protected void BtnSubmit_Final2(object sender, EventArgs e)
    { Response.Redirect("../"); }

    protected void BtnCancel(object sender, EventArgs e)
    { Response.Redirect("../"); }

    private bool valid(int jumlah)
    {
        bool x = true;
        if (jumlah < 1) { x = false; }
        else
        {
            for (int i = 0; i < jumlah; i++)
            {
                TextBox txt_thr = (TextBox)PH.FindControl("txt_thr_" + (i + 1));
                TextBox txt_overtime = (TextBox)PH.FindControl("txt_overtime_" + (i + 1));
                x = Fv.isDecimal(txt_thr) ? x : false;
                x = Fv.isDecimal(txt_overtime) ? x : false;
            }
        }
        return x;
    }

    protected void Back_Click(object sender, EventArgs e)
    { Response.Redirect("../"); }
}
