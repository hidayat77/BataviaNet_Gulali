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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;

public partial class _admin_laporan_slip_gaji : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Slip Gaji", "view");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {

            combo_department();
            combo_employee(ddl_division.SelectedValue);
        }
    }
    protected void combo_department()
    {
        //DEPARTMENT
        DataTable rs_department = Db.Rs("select * from TBL_Department order by Department_Name asc");

        if (rs_department.Rows.Count > 0)
        {
            for (int i = 0; i < rs_department.Rows.Count; i++)
            {
                ddl_department.Items.Add(new System.Web.UI.WebControls.ListItem(rs_department.Rows[i]["Department_Name"].ToString(), rs_department.Rows[i]["Department_ID"].ToString()));
            }
        }
        updatePanelTable.Update();
    }

    protected void combo_division(string id_department)
    {
        //DIVISION
        ddl_division.Items.Clear();
        ddl_employee.Items.Clear();
        DataTable rs_division = Db.Rs("select * from TBL_Division where Department_ID = '" + Cf.Int(id_department) + "'order by Division_Name asc");

        if (rs_division.Rows.Count > 0)
        {
            ddl_division.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih Divisi  --", "0"));
            for (int i = 0; i < rs_division.Rows.Count; i++)
            {
                ddl_division.Items.Add(new System.Web.UI.WebControls.ListItem(rs_division.Rows[i]["Division_Name"].ToString(), rs_division.Rows[i]["Division_ID"].ToString()));
            }
        }
    }
    protected void combo_employee(string id_division)
    {
        //EMPLOYEE
        ddl_employee.Items.Clear();
        DataTable rsa = Db.Rs("select * from TBL_Employee where Employee_ID != 1 and Employee_ID != '" + App.Employee_ID + "' and Division_ID = '" + Cf.Int(id_division) + "'  order by Employee_Full_Name asc");

        if (rsa.Rows.Count > 0)
        {
            ddl_employee.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih Karyawan  --", "0"));
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_employee.Items.Add(new System.Web.UI.WebControls.ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }
    protected void selected_department(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_department.SelectedValue);
        if (a.Equals(0))
        {
            combo_division(ddl_department.SelectedValue);
            div_divison.Visible = false;
            div_employee.Visible = false;
            date_periode.Visible = false;
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
        else
        {
            combo_division(ddl_department.SelectedValue);
            div_divison.Visible = true;
            div_employee.Visible = false;
            date_periode.Visible = false;
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
    }
    protected void selected_division(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_division.SelectedValue);
        if (a.Equals(0))
        {
            combo_employee(ddl_division.SelectedValue);
            div_employee.Visible = false;
            date_periode.Visible = false;
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
        else
        {
            combo_employee(ddl_division.SelectedValue);
            div_employee.Visible = true;
            date_periode.Visible = false;
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
    }
    protected void selected_employee(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_employee.SelectedValue);
        if (a.Equals(0))
        {
            date_periode.Visible = false;
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
        else
        {
            date_periode.Visible = true; date_periode.Text = "";
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
    }
    protected void selected_date_periode(object sender, EventArgs e)
    {
        string a = date_periode.Text;
        if (a.Equals(""))
        {
            div_cari.Visible = false;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
        else
        {
            div_cari.Visible = true;
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
    }
    protected void click_go(object sender, EventArgs e)
    {
        string employee_id = ddl_employee.SelectedValue;
        
        string periode = "01-" + date_periode.Text;
        
        DateTime convert_date_join = Convert.ToDateTime(periode);
        //Dapetin Tahun
        string year = convert_date_join.ToString("yyyy");
        //Dapetin Bulan
        string month = convert_date_join.ToString("MM");

        //Encrypt ID Employee
        string encryptionPassword = Param.encryptionPassword;
        string id_emp = Cf.StrSql(employee_id.ToString());
        string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
        string original = Crypto.Decrypt(encrypted, encryptionPassword);

        DataTable cek_payslip = Db.Rs2("select DATENAME(month, Payroll_PeriodTo) AS payroll_period_month, year(Payroll_PeriodTo) as year from TBL_Payroll where Payroll_EmployeeID='" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo)= '" + Cf.StrSql(month) + "'");

        if (cek_payslip.Rows.Count > 0)
        {
            div_payslip.Visible = true;
            div_download.Visible = true;
            fill(employee_id, month, year);
            alert_payslip.Text = "";
        }
        else
        {
            alert_payslip.Text = "<h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3>";
            div_download.Visible = false;
            div_payslip.Visible = false;
        }
    }

    /////////////////////////////////////////////////

    protected void fill(string id, string month, string year)
    {
        DataTable employee = Db.Rs("Select Employee_Full_Name, Employee_Bank_AccNumber_Primary, Employee_JoinDate, Employee_DateOfBirth from TBL_Employee where Employee_ID = '" + Cf.StrSql(id) + "'");

        DataTable employee_payroll = Db.Rs("Select Payroll_SalaryPPH, Payroll_SalaryBPJS, Payroll_NPWP_Number, b.PTKP_Status from TBL_Employee_Payroll a join Gulali_Payroll_New.dbo.TBL_PTKP b on a.PTKP_ID = b.PTKP_ID where Employee_ID = '" + Cf.StrSql(id) + "'");

        string encryptionPassword = Param.encryptionPassword;
        string encrypted = Crypto.Encrypt(Cf.StrSql(id), encryptionPassword);

        double salary_pph = 0, salary_bpjs = 0;
        if (!string.IsNullOrEmpty(employee_payroll.Rows[0]["Payroll_SalaryPPH"].ToString()))
        { salary_pph = Convert.ToDouble(employee_payroll.Rows[0]["Payroll_SalaryPPH"]); }
        if (!string.IsNullOrEmpty(employee_payroll.Rows[0]["Payroll_SalaryBPJS"].ToString()))
        { salary_bpjs = Convert.ToDouble(employee_payroll.Rows[0]["Payroll_SalaryBPJS"]); }

        txt_name.Text = employee.Rows[0]["Employee_Full_Name"].ToString();
        txt_rekening.Text = employee.Rows[0]["Employee_Bank_AccNumber_Primary"].ToString();

        txt_npwp.Text = employee_payroll.Rows[0]["Payroll_NPWP_Number"].ToString();
        txt_status_ptkp.Text = employee_payroll.Rows[0]["PTKP_Status"].ToString();

        int month_conv = Convert.ToInt32(Cf.StrSql(month));

        DataTable payroll = Db.Rs2("select *, CONVERT(VARCHAR(11),Payroll_PeriodTo ,106) as Payroll_PeriodTo, DATENAME(month, Payroll_PeriodTo) AS payroll_period_month, year(Payroll_PeriodTo) as year from TBL_Payroll where Payroll_EmployeeID='" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo)= '" + Cf.StrSql(month) + "'");

        double homepay_thr_conv = 0, homepay_overtime_conv = 0, homepay_bonus_conv = 0, homepay_incentive_conv = 0, thr_conv = 0, overtime_conv = 0, curr_salary = 0, bonus_conv = 0, incentive_conv = 0, unpaidleave_conv = 0, gaji_dan_tunjangan = 0, bpjs_kesehatan_corp = 0, bpjs_ketenagakerjaan_corp = 0, allowance_val = 0, tunjangan_val = 0, total_tak_teratur = 0, total_teratur = 0, bpjs_peg = 0, jht_peg = 0, biaya_jabatan = 0, total_pengurang_penghasilan = 0, total_netto = 0, total_bruto = 0, ptkp = 0, pph_periode_ini = 0, sum_tunjangan = 0, sum_tunjangan_takteratur = 0, tunjangan_takteratur_val = 0, bpjs_pribadi_val = 0;

        if (payroll.Rows.Count > 0)
        {
            txt_tgl_tanggal_terima.Text = payroll.Rows[0]["Payroll_PeriodTo"].ToString();
            txt_month.Text = payroll.Rows[0]["payroll_period_month"].ToString() + " " + payroll.Rows[0]["year"].ToString();
            Employee_NumberID.Text = payroll.Rows[0]["Payroll_ID"].ToString();

            homepay_thr_conv = Convert.ToDouble(payroll.Rows[0]["Payroll_THR"]);
            homepay_overtime_conv = Convert.ToDouble(payroll.Rows[0]["Payroll_Overtime"]);
            homepay_bonus_conv = Convert.ToDouble(payroll.Rows[0]["Payroll_Bonus"]);
            homepay_incentive_conv = Convert.ToDouble(payroll.Rows[0]["Payroll_Incentive"]);
            curr_salary = Convert.ToDouble(payroll.Rows[0]["Payroll_BasicSalary"]);
            unpaidleave_conv = Convert.ToDouble(payroll.Rows[0]["Payroll_UnpaidLeave"]);
            bpjs_peg = Convert.ToDouble(payroll.Rows[0]["Payroll_BPJSPeg"]) * 12;
            jht_peg = Convert.ToDouble(payroll.Rows[0]["Payroll_JHTPeg"]) * 12;
            biaya_jabatan = Convert.ToDouble(payroll.Rows[0]["Payroll_biaya_jabatan"]);
            total_pengurang_penghasilan = bpjs_peg + jht_peg + unpaidleave_conv + biaya_jabatan;
            ptkp = Convert.ToDouble(payroll.Rows[0]["Payroll_ptkp"]);
            bpjs_kesehatan_corp = Convert.ToDouble(payroll.Rows[0]["Payroll_BPJS"]) * 12;
            bpjs_ketenagakerjaan_corp = (Convert.ToDouble(payroll.Rows[0]["payroll_JHT"]) + Convert.ToDouble(payroll.Rows[0]["Payroll_JKK"]) + Convert.ToDouble(payroll.Rows[0]["Payroll_JKM"])) * 12;
            gaji_dan_tunjangan = (Convert.ToDouble(payroll.Rows[0]["Payroll_BasicSalary"]) + Convert.ToDouble(payroll.Rows[0]["Payroll_TotalAllowance"])) * 12;
            pph_periode_ini = Convert.ToDouble(payroll.Rows[0]["Payroll_pph21_periode_ini"]);

            if (month_conv > 1)
            {
                string month_else = "";
                for (int i = 1; i < month_conv; i++)
                {
                    month_else += "" + i + "','";
                }

                DataTable akumulasi = Db.Rs2("select (sum(Payroll_BasicSalary + Payroll_TotalAllowance)) as akumulasi_gaji_tunjangan, sum(Payroll_BPJS) as Payroll_BPJS, sum(payroll_JHT) as payroll_JHT, sum(payroll_JKK) as payroll_JKK, sum(payroll_JKM) as payroll_JKM from TBL_Payroll where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) in ('" + month_else + "')");


                if (akumulasi.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(akumulasi.Rows[0]["akumulasi_gaji_tunjangan"].ToString()))
                    {
                        gaji_dan_tunjangan = Convert.ToDouble(akumulasi.Rows[0]["akumulasi_gaji_tunjangan"]) + (Convert.ToDouble(payroll.Rows[0]["Payroll_BasicSalary"]) + Convert.ToDouble(payroll.Rows[0]["Payroll_TotalAllowance"])) * (13 - month_conv);
                        bpjs_kesehatan_corp = Convert.ToDouble(akumulasi.Rows[0]["Payroll_BPJS"]) + (Convert.ToDouble(payroll.Rows[0]["Payroll_BPJS"]) * (13 - month_conv));
                        bpjs_ketenagakerjaan_corp = Convert.ToDouble(akumulasi.Rows[0]["Payroll_JHT"]) + (Convert.ToDouble(payroll.Rows[0]["payroll_JHT"]) * (13 - month_conv)) + Convert.ToDouble(akumulasi.Rows[0]["Payroll_JKK"]) + (Convert.ToDouble(payroll.Rows[0]["Payroll_JKK"]) * (13 - month_conv)) + Convert.ToDouble(akumulasi.Rows[0]["Payroll_JKM"]) + (Convert.ToDouble(payroll.Rows[0]["Payroll_JKM"]) * (13 - month_conv));
                    }
                    else
                    {
                        gaji_dan_tunjangan = (Convert.ToDouble(payroll.Rows[0]["Payroll_BasicSalary"]) + Convert.ToDouble(payroll.Rows[0]["Payroll_TotalAllowance"])) * (13 - month_conv);
                        bpjs_kesehatan_corp = (Convert.ToDouble(payroll.Rows[0]["Payroll_BPJS"]) * (13 - month_conv));
                        bpjs_ketenagakerjaan_corp = (Convert.ToDouble(payroll.Rows[0]["payroll_JHT"]) * (13 - month_conv)) + (Convert.ToDouble(payroll.Rows[0]["Payroll_JKK"]) * (13 - month_conv)) + (Convert.ToDouble(payroll.Rows[0]["Payroll_JKM"]) * (13 - month_conv));
                    }
                }


            }

            string month_satu_tahun = "";
            for (int i = 1; i <= month_conv; i++)
            {
                month_satu_tahun += "" + i + "','";
            }

            DataTable payroll_satu_tahun = Db.Rs2("select sum(Payroll_THR) as thr, sum(Payroll_Overtime) as overtime, sum(Payroll_Bonus) as bonus, sum(Payroll_Incentive) as incentive, sum(Payroll_BPJSPribadi) as BPJSPribadi, (select sum(det_allowance_value) from TBL_Payroll_Det_Allowance a join TBL_Payroll b on a.det_allowance_payroll_id = b.Payroll_ID where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) in ('" + month_satu_tahun + "') and det_allowance_component_id = '50') as tunjangan_lainnya from TBL_Payroll where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) in ('" + month_satu_tahun + "')");

            thr_conv = Convert.ToDouble(payroll_satu_tahun.Rows[0]["thr"]);
            overtime_conv = Convert.ToDouble(payroll_satu_tahun.Rows[0]["overtime"]);
            bonus_conv = Convert.ToDouble(payroll_satu_tahun.Rows[0]["bonus"]);
            incentive_conv = Convert.ToDouble(payroll_satu_tahun.Rows[0]["incentive"]);
            bpjs_pribadi_val = Convert.ToDouble(payroll_satu_tahun.Rows[0]["BPJSPribadi"]);
            if (!string.IsNullOrEmpty(payroll_satu_tahun.Rows[0]["tunjangan_lainnya"].ToString())) { allowance_val = Convert.ToDouble(payroll_satu_tahun.Rows[0]["tunjangan_lainnya"]); } else { allowance_val = 0; };

            //tunjangan tak teratur
            StringBuilder y = new StringBuilder();
            DataTable loop_takteratur = Db.Rs2("select distinct component_name from TBL_Payroll_Det_Allowance a join TBL_Payroll b on a.det_allowance_payroll_id = b.Payroll_ID join TBL_MasterComponent c on a.det_allowance_component_id = c.component_id where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and c.component_type = 'Irregular Income'");

            for (int i = 0; i < loop_takteratur.Rows.Count; i++)
            {
                DataTable tunjangan_takteratur = Db.Rs2("select component_id, component_name, det_allowance_value from TBL_Payroll_Det_Allowance a join TBL_Payroll b on a.det_allowance_payroll_id = b.Payroll_ID join TBL_MasterComponent c on a.det_allowance_component_id = c.component_id where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo)= '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) in ('" + month_satu_tahun + "') and c.component_name = '" + loop_takteratur.Rows[i]["component_name"] + "'");

                if (tunjangan_takteratur.Rows.Count > 0)
                {
                    for (int a = 0; a < tunjangan_takteratur.Rows.Count; a++)
                    {
                        tunjangan_takteratur_val = Convert.ToDouble(tunjangan_takteratur.Rows[a]["det_allowance_value"]);
                        sum_tunjangan_takteratur += tunjangan_takteratur_val;
                    }
                }

                y.Append("<tr>"
                            + "<td>" + loop_takteratur.Rows[i]["component_name"] + "</td>"
                            + "<td>:</td>"
                            + "<td>Rp " + String.Format("{0:#,0.00}", sum_tunjangan_takteratur) + "</td>"
                          + "</tr>");
            }
            tunjangan_tak_teratur.Text = y.ToString();

            // Dua Huruf ATM Dibelakang 
            string bca = employee.Rows[0]["Employee_Bank_AccNumber_Primary"].ToString().Replace(".", "");
            string bca_number;
            if (bca.Equals(""))
            {
                Js.Alert(this, "Account Number Is Empty");
                return;
            }
            else
            {
                int dua = 2;
                int length = bca.Length - dua;
                bca_number = bca.Substring(length, dua);
            }

            //Tanggal Gabung
            string date_join = employee.Rows[0]["Employee_JoinDate"].ToString();
            string join;
            if (date_join.Equals(""))
            {
                Js.Alert(this, "Join Date Is Empty");
                return;
            }
            else
            {
                DateTime convert_date_join = Convert.ToDateTime(date_join);
                join = convert_date_join.ToString("yy");
            }

            //Tanggal Lahir
            string birthdate = employee.Rows[0]["Employee_DateOfBirth"].ToString();
            string birth;
            if (birthdate.Equals(""))
            {
                Js.Alert(this, "Birth Date Is Empty");
                return;
            }
            else
            {
                DateTime date = Convert.ToDateTime(birthdate);
                birth = date.ToString("ddMMyy");
            }
            string password = bca_number + join + birth;
            password_slip.Text = password;

        }

        txt_thr.Text = "Rp " + String.Format("{0:#,0.00}", thr_conv);
        txt_overtime.Text = "Rp " + String.Format("{0:#,0.00}", overtime_conv);
        txt_homepay_gapok.Text = "Rp " + String.Format("{0:#,0.00}", curr_salary);
        txt_bonus.Text = "Rp " + String.Format("{0:#,0.00}", bonus_conv);
        txt_incentive.Text = "Rp " + String.Format("{0:#,0.00}", incentive_conv);
        txt_unpaid_leave.Text = "Rp " + String.Format("{0:#,0.00}", unpaidleave_conv);
        txt_gaji_dan_tunjangan.Text = "Rp " + String.Format("{0:#,0.00}", gaji_dan_tunjangan);
        txt_bpjs_kesehatan_corp.Text = "Rp " + String.Format("{0:#,0.00}", bpjs_kesehatan_corp);
        txt_bpjs_ketenagakerjaan_corp.Text = "Rp " + String.Format("{0:#,0.00}", bpjs_ketenagakerjaan_corp);
        txt_allowance.Text = "Rp " + String.Format("{0:#,0.00}", allowance_val);
        txt_kesehatan_empl.Text = "Rp " + String.Format("{0:#,0.00}", bpjs_peg);
        txt_ketenagakerjaan_empl.Text = "Rp " + String.Format("{0:#,0.00}", jht_peg);
        txt_biaya_jabatan.Text = "Rp " + String.Format("{0:#,0.00}", biaya_jabatan);
        txt_deduction.Text = "<b>Rp " + String.Format("{0:#,0.00}", total_pengurang_penghasilan) + "</b>";
        total_teratur = gaji_dan_tunjangan + bpjs_kesehatan_corp + bpjs_ketenagakerjaan_corp;
        txt_penghasilan_teratur.Text = "<b>Rp " + String.Format("{0:#,0.00}", (total_teratur)) + "</b>";
        txt_bpjs_pribadi.Text = "Rp " + String.Format("{0:#,0.00}", bpjs_pribadi_val);
        total_tak_teratur = thr_conv + overtime_conv + bonus_conv + incentive_conv + allowance_val + sum_tunjangan_takteratur + bpjs_pribadi_val;
        txt_total_tak_teratur.Text = "<b>Rp " + String.Format("{0:#,0.00}", (total_tak_teratur)) + "</b>";
        total_bruto = total_teratur + total_tak_teratur;
        total_netto = total_bruto - total_pengurang_penghasilan;
        txt_bruto.Text = "<b>Rp " + String.Format("{0:#,0.00}", (total_bruto)) + "</b>";
        txt_netto.Text = "<b>Rp " + String.Format("{0:#,0.00}", (total_netto)) + "</b>";
        txt_ptkp.Text = "Rp " + String.Format("{0:#,0.00}", ptkp);
        txt_kena_pajak.Text = "<b>Rp " + String.Format("{0:#,0.00}", (total_netto - ptkp)) + "</b>";
        txt_pph_periode_ini.Text = "<b>Rp " + String.Format("{0:#,0.00}", (pph_periode_ini)) + "</b>";
        txt_pph_sudah_dipotong.Text = "<b>Rp " + String.Format("{0:#,0.00}", (pph_periode_ini * (Convert.ToInt64(month) - 1))) + "</b>";
        txt_pph_pkp.Text = "Rp " + String.Format("{0:#,0.00}", (pph_periode_ini * 12));
        txt_pph_pkp1.Text = "<b>Rp " + String.Format("{0:#,0.00}", (pph_periode_ini * 12)) + "</b>";

        //tunjangan
        StringBuilder x = new StringBuilder();
        DataTable tunjangan_ = Db.Rs2("select component_id, component_name, det_allowance_value from TBL_Payroll_Det_Allowance a join TBL_Payroll b on a.det_allowance_payroll_id = b.Payroll_ID join TBL_MasterComponent c on a.det_allowance_component_id = c.component_id where Payroll_EmployeeID = '" + encrypted + "' and year(Payroll_PeriodTo) = '" + Cf.StrSql(year) + "' and month(Payroll_PeriodTo) = '" + Cf.StrSql(month) + "' and component_id not in ('18', '19', '20', '29', '30', '31') and c.component_type != 'Irregular Income'");

        if (tunjangan_.Rows.Count > 0)
        {
            for (int i = 0; i < tunjangan_.Rows.Count; i++)
            {
                tunjangan_val = Convert.ToDouble(tunjangan_.Rows[i]["det_allowance_value"]);
                x.Append("<tr>"
                            + "<td>" + tunjangan_.Rows[i]["component_name"] + "</td>"
                            + "<td>:</td>"
                            + "<td>Rp " + String.Format("{0:#,0.00}", tunjangan_val) + "</td>"
                          + "</tr>");
                sum_tunjangan += tunjangan_val;
            }
        }
        list_tunjangan.Text = x.ToString();

        txt_homepay_thr.Text = "Rp " + String.Format("{0:#,0.00}", homepay_thr_conv);
        txt_homepay_overtime.Text = "Rp " + String.Format("{0:#,0.00}", homepay_overtime_conv);
        txt_homepay_bonus.Text = "Rp " + String.Format("{0:#,0.00}", homepay_bonus_conv);
        txt_homepay_incentive.Text = "Rp " + String.Format("{0:#,0.00}", homepay_incentive_conv);
        txt_homepay_bpjs_total.Text = "Rp " + String.Format("{0:#,0.00}", bpjs_peg + jht_peg);
        txt_homepay_unpaidleave.Text = "Rp " + String.Format("{0:#,0.00}", unpaidleave_conv);
        txt_homepay_pph_periode_ini.Text = "Rp -" + String.Format("{0:#,0.00}", (pph_periode_ini)) + "";
        double homepay = (curr_salary + sum_tunjangan + homepay_thr_conv + homepay_overtime_conv + homepay_bonus_conv + homepay_incentive_conv) - (unpaidleave_conv + pph_periode_ini);
        take_homepay.Text = "Rp " + String.Format("{0:#,0.00}", homepay);
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                UpdatePanel1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 30, 30, 10, 10);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter.GetInstance(pdfDoc, memoryStream);
                    StyleSheet style = new iTextSharp.text.html.simpleparser.StyleSheet();
                    style.LoadStyle("isitb", "size", "9pt");
                    style.LoadStyle("headertb", "size", "9pt");
                    style.LoadStyle("titletb", "size", "9pt");
                    htmlparser.SetStyleSheet(style);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    using (MemoryStream input = new MemoryStream(bytes))
                    {
                        using (MemoryStream output = new MemoryStream())
                        {
                            DataTable employee = Db.Rs("Select Employee_Full_Name, Employee_Bank_AccNumber_Primary, Employee_JoinDate, Employee_DateOfBirth from TBL_Employee where Employee_ID = '" + Cf.StrSql(ddl_employee.SelectedValue) + "' and Employee_ID != 1");

                            // Dua Huruf ATM Dibelakang 
                            string bca = employee.Rows[0]["Employee_Bank_AccNumber_Primary"].ToString().Replace(".", "");
                            string bca_number;
                            if (bca.Equals(""))
                            {
                                Js.Alert(this, "Account Number Is Empty");
                                return;
                            }
                            else
                            {
                                int dua = 2;
                                int length = bca.Length - dua;
                                bca_number = bca.Substring(length, dua);
                            }

                            //Tanggal Gabung
                            string date_join = employee.Rows[0]["Employee_JoinDate"].ToString();
                            string join;
                            if (date_join.Equals(""))
                            {
                                Js.Alert(this, "Join Date Is Empty");
                                return;
                            }
                            else
                            {
                                DateTime convert_date_join = Convert.ToDateTime(date_join);
                                join = convert_date_join.ToString("yy");
                            }

                            //Tanggal Lahir
                            string birthdate = employee.Rows[0]["Employee_DateOfBirth"].ToString();
                            string birth;
                            if (birthdate.Equals(""))
                            {
                                Js.Alert(this, "Birth Date Is Empty");
                                return;
                            }
                            else
                            {
                                DateTime date = Convert.ToDateTime(birthdate);
                                birth = date.ToString("ddMMyy");
                            }
                            string password = bca_number + join + birth;
                            PdfReader reader = new PdfReader(input);
                            PdfEncryptor.Encrypt(reader, output, true, password, password, PdfWriter.ALLOW_SCREENREADERS);
                            bytes = output.ToArray();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=Payslip " + employee.Rows[0]["Employee_Full_Name"].ToString() + " " + txt_month.Text + ".pdf");
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(bytes);
                            Response.End();
                        }
                    }
                }
            }
        }
    }
}