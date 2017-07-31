using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class _user_data_pribadi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Data Pribadi", "view");

        tab_data_pribadi.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/data-pribadi/\">Data Pribadi</a>";
        tab_kontrak.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/kontrak/\">Kontrak</a>";
        tab_surat_peringatan.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/surat-peringatan/\">Surat Peringatan</a>";
        tab_pinjaman.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/pinjaman/\">Pinjaman</a>";
        tab_exit_clearance.Text = "<a href=\"" + Param.Path_User + "personal-data/data-diri/exit-clearance/\">Exit Clearance</a>";

        if (!IsPostBack)
        {
            DataTable exist = Db.Rs("select * from TBL_Employee where Employee_ID = '" + App.Employee_ID + "' and Employee_ID != 1");
            if (exist.Rows.Count > 0)
            {
                dropdownPrev(); fill();
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }

    protected void dropdownPrev()
    {
        DataTable rsa = Db.Rs("select Employee_Full_Name, Employee_Last_Name, Employee_ID from TBL_Employee where Employee_ID != 1 order by Employee_Full_Name asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_directSpv.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString() + " " + rsa.Rows[i]["Employee_Last_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }

        DataTable rsv = Db.Rs("Select Department_ID, Department_Name from TBL_Department order by Department_Name asc");
        if (rsv.Rows.Count > 0)
        {
            for (int i = 0; i < rsv.Rows.Count; i++)
            {
                ddl_department.Items.Add(new ListItem(rsv.Rows[i]["Department_Name"].ToString(), rsv.Rows[i]["Department_ID"].ToString()));
            }
        }

        DataTable rs_payroll = Db.Rs2("Select Role_ID, Admin_Role from TBL_Payroll_Role where Role_ID != 1 order by Admin_Role asc");
        if (rs_payroll.Rows.Count > 0)
        {
            for (int i = 0; i < rs_payroll.Rows.Count; i++)
            {
                ddl_payroll_group.Items.Add(new ListItem(rs_payroll.Rows[i]["Admin_Role"].ToString(), rs_payroll.Rows[i]["Role_ID"].ToString()));
            }
        }

        DataTable rsx = Db.Rs("Select Division_ID, Division_Name from TBL_Division order by Division_Name asc");
        if (rsx.Rows.Count > 0)
        {
            for (int i = 0; i < rsx.Rows.Count; i++)
            {
                ddl_division.Items.Add(new ListItem(rsx.Rows[i]["Division_Name"].ToString(), rsx.Rows[i]["Division_ID"].ToString()));
            }
        }

        DataTable rs = Db.Rs("select Position_ID, Position_Name from TBL_Position order by Position_Name asc");
        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                ddl_position.Items.Add(new ListItem(rs.Rows[i]["Position_Name"].ToString(), rs.Rows[i]["Position_ID"].ToString()));
            }
        }

        DataTable rsagama = Db.Rs("Select Religion_ID, Religion_Name_ID from TBL_Religion");
        for (int i = 0; i < rsagama.Rows.Count; i++)
        {
            agama.Items.Add(new ListItem(rsagama.Rows[i]["Religion_Name_ID"].ToString(), rsagama.Rows[i]["Religion_ID"].ToString()));
        }

        DataTable selectptkp = Db.Rs2("Select PTKP_ID, PTKP_Status, PTKP_Description From TBL_PTKP");
        for (int i = 0; i < selectptkp.Rows.Count; i++)
        {
            ddl_status_pajak.Items.Add(new ListItem(selectptkp.Rows[i]["PTKP_Status"].ToString() + " (" + selectptkp.Rows[i]["PTKP_Description"].ToString() + ")", selectptkp.Rows[i]["PTKP_ID"].ToString()));
        }
    }

    protected void fill()
    {
        DataTable rs = Db.Rs("Select CONVERT(VARCHAR(11),Employee_DateOfBirth ,105) as Employee_DateOfBirth, CONVERT(VARCHAR(11),Employee_StartofEmployment ,105) as Employee_StartofEmployment, CONVERT(VARCHAR(11),Employee_JoinDate ,105) as Employee_JoinDate, CONVERT(VARCHAR(11),Employee_EndDate ,105) as Employee_EndDate, * from TBL_Employee a left join TBL_Employee_Payroll b on a.Employee_ID = b.Employee_ID where a.Employee_ID = '" + App.Employee_ID + "'");

        if (rs.Rows.Count > 0)
        {

            nik.Text = rs.Rows[0]["Employee_NIK"].ToString();
            nama_lengkap.Text = rs.Rows[0]["Employee_Full_Name"].ToString();
            nama_depan.Text = rs.Rows[0]["Employee_First_Name"].ToString();
            nama_tengah.Text = rs.Rows[0]["Employee_Middle_Name"].ToString();
            nama_belakang.Text = rs.Rows[0]["Employee_Last_Name"].ToString();
            nama_alias.Text = rs.Rows[0]["Employee_Alias_Name"].ToString();
            tempat_lahir.Text = rs.Rows[0]["Employee_PlaceofBirth"].ToString();
            tanggal_lahir.Text = rs.Rows[0]["Employee_DateOfBirth"].ToString();

            DateTime today = DateTime.Today;
            DateTime bday;
            if (!string.IsNullOrEmpty(rs.Rows[0]["Employee_DateOfBirth"].ToString()))
            {
                bday = Convert.ToDateTime(rs.Rows[0]["Employee_DateOfBirth"].ToString());
            }
            else { bday = DateTime.Today; }

            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age)) age--;
            old.Text = age.ToString() + " Years old";

            agama.SelectedValue = rs.Rows[0]["Religion_ID"].ToString();
            no_ktp.Text = rs.Rows[0]["Employee_IDCard_Number"].ToString();
            alamat_ktp.Text = rs.Rows[0]["Employee_IDCard_Address"].ToString();
            alamat_domisili.Text = rs.Rows[0]["Employee_Domicile_Address"].ToString();
            marital_status.SelectedValue = rs.Rows[0]["Employee_Marital_Status"].ToString();
            nama_pasangan.Text = rs.Rows[0]["Employee_Spouse_Name"].ToString();
            no_hp1.Text = rs.Rows[0]["Employee_Phone_Number_Primary"].ToString();
            no_hp2.Text = rs.Rows[0]["Employee_Phone_Number"].ToString();
            jenis_kelamin.Text = rs.Rows[0]["Employee_Gender"].ToString();
            email_kantor.Text = rs.Rows[0]["Employee_Company_Email"].ToString();
            email_pribadi.Text = rs.Rows[0]["Employee_Personal_Email"].ToString();
            ddl_department.SelectedValue = rs.Rows[0]["Department_ID"].ToString();
            ddl_division.SelectedValue = rs.Rows[0]["Division_ID"].ToString();
            ddl_position.SelectedValue = rs.Rows[0]["Position_ID"].ToString();
            ddl_directSpv.Text = rs.Rows[0]["Employee_DirectSpv"].ToString();
            ddl_blood_type.SelectedValue = rs.Rows[0]["Employee_Blood_Type"].ToString();
            npwp_number.Text = rs.Rows[0]["Payroll_NPWP_Number"].ToString();
            string npwp_status = rs.Rows[0]["Payroll_NPWP"].ToString();
            if (npwp_status.Equals("1")) { radio_npwp.Checked = true; }
            else if (npwp_status.Equals("2")) { radio_non_npwp.Checked = true; }
            else { radio_npwp.Checked = true; div_npwp_number.Visible = false; }
            radio_npwp.Enabled = false; radio_non_npwp.Enabled = false;
            ddl_payroll_group.SelectedValue = rs.Rows[0]["Payroll_Group"].ToString();
            ddl_employee_status.Text = rs.Rows[0]["Employee_Inactive"].ToString();
            remarks.Text = rs.Rows[0]["Employee_Inactive_Remarks"].ToString();

            contract_start.Text = rs.Rows[0]["Employee_JoinDate"].ToString();
            contract_end.Text = rs.Rows[0]["Employee_EndDate"].ToString();
            in_the_name1.Text = rs.Rows[0]["Employee_Bank_AccName_Primary"].ToString();
            bank_account_1.Text = rs.Rows[0]["Employee_Bank_AccNumber_Primary"].ToString();
            in_the_name2.Text = rs.Rows[0]["Employee_Bank_AccName"].ToString();
            bank_account_2.Text = rs.Rows[0]["Employee_Bank_AccNumber"].ToString();
            last_education.SelectedValue = rs.Rows[0]["Employee_Education"].ToString();
            jurusan.Text = rs.Rows[0]["Employee_Education_Major"].ToString();
            start_of_employment.Text = rs.Rows[0]["Employee_StartofEmployment"].ToString();

            DataTable education = Db.Rs("Select * from TBL_Employee_Education where Employee_ID = '" + App.Employee_ID + "'");
            if (education.Rows.Count > 0)
            {
                edu_1_name.Text = education.Rows[0]["Education_1_Name"].ToString();
                edu_1_year.Text = education.Rows[0]["Education_1_Years"].ToString();
                edu_1_location.Text = education.Rows[0]["Education_1_Location"].ToString();
                edu_2_name.Text = education.Rows[0]["Education_2_Name"].ToString();
                edu_2_year.Text = education.Rows[0]["Education_2_Years"].ToString();
                edu_2_location.Text = education.Rows[0]["Education_2_Location"].ToString();
                edu_3_name.Text = education.Rows[0]["Education_3_Name"].ToString();
                edu_3_year.Text = education.Rows[0]["Education_3_Years"].ToString();
                edu_3_location.Text = education.Rows[0]["Education_3_Location"].ToString();
                edu_4_name.Text = education.Rows[0]["Education_4_Name"].ToString();
                edu_4_year.Text = education.Rows[0]["Education_4_Years"].ToString();
                edu_4_location.Text = education.Rows[0]["Education_4_Location"].ToString();
                edu_5.Text = education.Rows[0]["Education_5"].ToString();
                edu_5_name.Text = education.Rows[0]["Education_5_Name"].ToString();
                edu_5_year.Text = education.Rows[0]["Education_5_Years"].ToString();
                edu_5_location.Text = education.Rows[0]["Education_5_Location"].ToString();
                edu_6.Text = education.Rows[0]["Education_6"].ToString();
                edu_6_name.Text = education.Rows[0]["Education_6_Name"].ToString();
                edu_6_year.Text = education.Rows[0]["Education_6_Years"].ToString();
                edu_6_location.Text = education.Rows[0]["Education_6_Location"].ToString();
            }
            else { div_riwayat_pendidikan_formal.Visible = false; h4_riwayat_pendidikan_formal.Visible = false; }

            DataTable working_experience = Db.Rs("Select * from TBL_Employee_Working_Experience where Employee_ID = '" + App.Employee_ID + "'");
            if (working_experience.Rows.Count > 0)
            {
                Company_1.Text = working_experience.Rows[0]["Experience_1_Company_Name"].ToString();
                Position_1.Text = working_experience.Rows[0]["Experience_1_Position"].ToString();
                Start_1.Text = working_experience.Rows[0]["Experience_1_Start_Year"].ToString();
                End_1.Text = working_experience.Rows[0]["Experience_1_End_Year"].ToString();

                Company_2.Text = working_experience.Rows[0]["Experience_2_Company_Name"].ToString();
                Position_2.Text = working_experience.Rows[0]["Experience_2_Position"].ToString();
                Start_2.Text = working_experience.Rows[0]["Experience_2_Start_Year"].ToString();
                End_2.Text = working_experience.Rows[0]["Experience_2_End_Year"].ToString();

                Company_3.Text = working_experience.Rows[0]["Experience_3_Company_Name"].ToString();
                Position_3.Text = working_experience.Rows[0]["Experience_3_Position"].ToString();
                Start_3.Text = working_experience.Rows[0]["Experience_3_Start_Year"].ToString();
                End_3.Text = working_experience.Rows[0]["Experience_3_End_Year"].ToString();

                Company_4.Text = working_experience.Rows[0]["Experience_4_Company_Name"].ToString();
                Position_4.Text = working_experience.Rows[0]["Experience_4_Position"].ToString();
                Start_4.Text = working_experience.Rows[0]["Experience_4_Start_Year"].ToString();
                End_4.Text = working_experience.Rows[0]["Experience_4_End_Year"].ToString();

                Company_5.Text = working_experience.Rows[0]["Experience_5_Company_Name"].ToString();
                Position_5.Text = working_experience.Rows[0]["Experience_5_Position"].ToString();
                Start_5.Text = working_experience.Rows[0]["Experience_5_Start_Year"].ToString();
                End_5.Text = working_experience.Rows[0]["Experience_5_End_Year"].ToString();
            }
            else { div_riwayat_pengalaman_kerja.Visible = false; h4_riwayat_pengalaman_kerja.Visible = false; }

            DataTable emergency = Db.Rs("select * from TBL_Employee_Emergency_Contact where Employee_ID = '" + App.Employee_ID + "'");
            string emergency_name, emergency_address, emergency_phone, emergency_relation;

            StringBuilder looping_kontak = new StringBuilder();
            if (emergency.Rows.Count > 0)
            {
                for (int a = 0; a < emergency.Rows.Count; a++)
                {

                    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Name"].ToString())) { emergency_name = "-"; } else { emergency_name = emergency.Rows[a]["Emergency_Name"].ToString(); };
                    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Address"].ToString())) { emergency_address = "-"; } else { emergency_address = emergency.Rows[a]["Emergency_Address"].ToString(); };
                    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Phone"].ToString())) { emergency_phone = "-"; } else { emergency_phone = emergency.Rows[a]["Emergency_Phone"].ToString(); };
                    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Relation"].ToString())) { emergency_relation = "-"; } else { emergency_relation = emergency.Rows[a]["Emergency_Relation"].ToString(); };

                    looping_kontak.Append("<tr>"
                                        + "<td style=\"text-align: center;\">" + emergency_name + "</td>"
                                        + "<td style=\"text-align: center;\">" + emergency_address + "</td>"
                                        + "<td style=\"text-align: center;\">" + emergency_phone + "</td>"
                                        + "<td style=\"text-align: center;\">" + emergency_relation + "</td>"
                                    + "</tr>");
                }
                loop_kontak.Text = looping_kontak.ToString();
            }
            else { div_kontak_darurat.Visible = false; h4_kontak_darurat.Visible = false; }

            DataTable depend = Db.Rs("select Dependent_Name, Dependent_Gender, CONVERT(VARCHAR(11), Dependent_DateofBirth ,105) as Dependent_DateofBirth, Dependent_PlaceofBirth, Dependent_Relationship from TBL_Employee_Dependent where Employee_ID = '" + App.Employee_ID + "'");

            string dep_name = "", dep_gender = "", dep_placeofbirth = "", dep_dateofbirth = "", dep_relationship = "";
            StringBuilder looping_depend = new StringBuilder();
            if (depend.Rows.Count > 0)
            {
                for (int a = 0; a < depend.Rows.Count; a++)
                {
                    dep_name = depend.Rows[a]["Dependent_Name"].ToString();
                    if (depend.Rows[a]["Dependent_Gender"].ToString() == "L") { dep_gender = "Male"; } else if (depend.Rows[a]["Dependent_Gender"].ToString() == "P") { dep_gender = "Female"; };
                    dep_placeofbirth = depend.Rows[a]["Dependent_PlaceofBirth"].ToString();
                    dep_dateofbirth = depend.Rows[a]["Dependent_DateofBirth"].ToString();
                    dep_relationship = depend.Rows[a]["Dependent_Relationship"].ToString();

                    looping_depend.Append("<tr>"
                                        + "<td style=\"text-align: center;\">" + dep_name + "</td>"
                                        + "<td style=\"text-align: center;\">" + dep_gender + "</td>"
                                        + "<td style=\"text-align: center;\">" + dep_placeofbirth + "</td>"
                                        + "<td style=\"text-align: center;\">" + dep_dateofbirth + "</td>"
                                        + "<td style=\"text-align: center;\">" + dep_relationship + "</td>"
                                    + "</tr>");
                }
                loop_depend.Text = looping_depend.ToString();
            }
            else { div_tanggungan.Visible = false; h4_tanggungan.Visible = false; }

            decimal bpjs_personal_val = 0;
            if (!string.IsNullOrEmpty(rs.Rows[0]["Payroll_BPJS_Pribadi_Value"].ToString())) { bpjs_personal_val = Convert.ToDecimal(rs.Rows[0]["Payroll_BPJS_Pribadi_Value"].ToString()); }
            personal_bpjs_value.Text = (bpjs_personal_val).ToString("#,##0");

            salary_pph.Text = rs.Rows[0]["Payroll_SalaryPPH"].ToString();
            salary_bpjs.Text = rs.Rows[0]["Payroll_SalaryBPJS"].ToString();

            ddl_status_pajak.SelectedValue = rs.Rows[0]["PTKP_ID"].ToString();
            ddl_personal_bpjs.SelectedValue = rs.Rows[0]["Payroll_BPJS_Pribadi"].ToString();
            ddl_bpjs_ditanggung.SelectedValue = rs.Rows[0]["Payroll_BPJS_Ditanggung"].ToString();
            if (!string.IsNullOrEmpty(rs.Rows[0]["Employee_SIM_A"].ToString())) { checkbox_sim_a.Checked = Convert.ToBoolean(rs.Rows[0]["Employee_SIM_A"].ToString()); }
            if (!string.IsNullOrEmpty(rs.Rows[0]["Employee_SIM_C"].ToString())) { checkbox_sim_c.Checked = Convert.ToBoolean(rs.Rows[0]["Employee_SIM_C"].ToString()); }
            string encryptionPassword = Param.encryptionPassword;
            string id_emp = App.Employee_ID, foto_link = "";
            string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
            string original = Crypto.Decrypt(encrypted, encryptionPassword);

            DataTable salary = Db.Rs2("select * from TBL_HistorySalary where employee_id='" + encrypted + "' order by HistorySalary_DateChanged desc");
            if (salary.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(salary.Rows[0]["HistorySalary_value"].ToString())) { current_salary.Text = salary.Rows[0]["HistorySalary_value"].ToString(); }
            }
            else { current_salary.Text = "0,00"; }

            if (string.IsNullOrEmpty(rs.Rows[0]["Employee_CV"].ToString())) { cv.Text = "<font style=\"color:red;\">There is No Uploaded File</font>"; }
            else { cv.Text = "<a href=\"/file-upload/cv/" + rs.Rows[0]["Employee_CV"].ToString() + "\">" + rs.Rows[0]["Employee_CV"].ToString() + "</a>"; }

            if (!string.IsNullOrEmpty(rs.Rows[0]["Employee_Photo"].ToString())) { foto_link = "/assets/images/employee-photo/" + rs.Rows[0]["Employee_Photo"].ToString(); }
            else { foto_link = "/assets/images/no-user-image.gif"; }

            link_foto.Text = "<a href=\"" + foto_link + "\" class=\"image-popup\" title=\"Screenshot-1\"><img src=\"" + foto_link + "\" class=\"thumb-img\" alt=\"work-thumbnail\" style=\"max-width:150px;\"></a>";

            if (!string.IsNullOrEmpty(rs.Rows[0]["Employee_JoinDate"].ToString()))
            {
                DateTime dt = Convert.ToDateTime(rs.Rows[0]["Employee_JoinDate"].ToString());
                int diffDays = (DateTime.Now - dt).Days;
                lw_Year.Text = "0"; lw_Month.Text = "0"; lw_Week.Text = "0";
                if (diffDays > 365)
                {
                    int count_year = diffDays / 365;
                    lw_Year.Text = count_year.ToString();
                    diffDays -= count_year * 365;
                }
                if (diffDays > 30)
                {
                    int count_month = diffDays / 30;
                    lw_Month.Text = count_month.ToString();
                    diffDays -= count_month * 30;
                }
                if (diffDays > 7)
                {
                    int count_week = diffDays / 7;
                    lw_Week.Text = count_week.ToString();
                    diffDays -= count_week * 7;
                }
                lw_Day.Text = diffDays.ToString();
            }
        }
    }
}