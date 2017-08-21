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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class _data_karyawan_detail : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string recent { get { return App.GetStr(this, "recent"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Data Karyawan", "Update");

        if (!IsPostBack)
        {
            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            link_back.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
            //try
            //{

            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                if (recent != "")
                {
                    DataTable ch = Db.Rs("select * from TBL_Employee where Employee_ID = " + recent + "");
                    if (ch.Rows.Count > 0)
                    {
                        link_added.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                                        + "<h4 class=\"page-title\">Data Success Updated!</h4>"
                                        + "<h5><strong>It's the link of detail data currently added : </strong></h5><a href=\"" + Param.Path_Admin + "data-karyawan/detail/?id=" + recent + "\">link employee</a></div>";
                    }
                }

                if (Fv.cekInt(Cf.StrSql(id_get)))
                {
                    DataTable exist = Db.Rs("select * from TBL_Employee where Employee_ID = '" + id_get + "' and Employee_ID != 1");
                    if (exist.Rows.Count > 0)
                    {
                        ddl_DepartmentPrev(); ddl_TitlePrev(); ddl_ReligionPrev(); ddl_status_pajakPrev(); ddl_payroll_groupPrev();
                        fill();
                        update.Update();
                    }
                    else { Response.Redirect("/gulali/page/404.aspx"); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
            //}
            //catch (Exception ) { }
        }
    }


    //Dropdown
    protected void ddl_TitlePrev()
    {
        DataTable rsa = Db.Rs("select Position_ID, Position_Name from TBL_Position order by Position_Name asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_position.Items.Add(new ListItem(rsa.Rows[i]["Position_Name"].ToString(), rsa.Rows[i]["Position_ID"].ToString()));
            }
        }
    }

    protected void ddl_DepartmentPrev()
    {
        DataTable rsa = Db.Rs("Select Department_ID, Department_Name from TBL_Department order by Department_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_department.Items.Add(new ListItem(rsa.Rows[i]["Department_Name"].ToString(), rsa.Rows[i]["Department_ID"].ToString()));
        }
    }

    protected void ddl_DivisionPrev(string department)
    {
        DataTable rsa = Db.Rs("Select Division_ID, Division_Name from TBL_division Where Department_ID = " + Cf.Int(department) + " order by Division_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_division.Items.Add(new ListItem(rsa.Rows[i]["Division_Name"].ToString(), rsa.Rows[i]["Division_ID"].ToString()));
        }
    }

    protected void ddl_directSpvPrev(string division)
    {
        DataTable rsa = Db.Rs("select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_ID != 1 and Division_ID = " + Cf.Int(division) + " order by Employee_First_Name asc");
        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_directSpv.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }

    protected void ddl_ReligionPrev()
    {
        DataTable rsa = Db.Rs("Select Religion_ID, Religion_Name_ID from TBL_Religion");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            agama.Items.Add(new ListItem(rsa.Rows[i]["Religion_Name_ID"].ToString(), rsa.Rows[i]["Religion_ID"].ToString()));
        }
    }

    protected void ddl_status_pajakPrev()
    {
        DataTable rsa = Db.Rs2("Select PTKP_ID, PTKP_Status, PTKP_Description from TBL_PTKP order by PTKP_ID asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_status_pajak.Items.Add(new ListItem(rsa.Rows[i]["PTKP_Status"].ToString() + " (" + rsa.Rows[i]["PTKP_Description"].ToString() + ")", rsa.Rows[i]["PTKP_ID"].ToString()));
        }
    }

    protected void ddl_payroll_groupPrev()
    {
        DataTable rs_payroll = Db.Rs2("Select Role_ID, Admin_Role from TBL_Payroll_Role where Role_ID != 1 order by Admin_Role asc");
        if (rs_payroll.Rows.Count > 0)
        {
            for (int i = 0; i < rs_payroll.Rows.Count; i++)
            {
                ddl_payroll_group.Items.Add(new ListItem(rs_payroll.Rows[i]["Admin_Role"].ToString(), rs_payroll.Rows[i]["Role_ID"].ToString()));
            }
        }
    }

    protected void fill()
    {
        DataTable rs = Db.Rs("Select CONVERT(VARCHAR(11),Employee_DateOfBirth ,105) as Employee_DateOfBirth, CONVERT(VARCHAR(11),Employee_StartofEmployment ,105) as Employee_StartofEmployment, CONVERT(VARCHAR(11),Employee_JoinDate ,105) as Employee_JoinDate, CONVERT(VARCHAR(11),Employee_EndDate ,105) as Employee_EndDate, * from TBL_Employee a join TBL_Employee_Payroll b on a.Employee_ID = b.Employee_ID where a.Employee_ID = '" + id_get + "'");

        DataTable education = Db.Rs("Select * from TBL_Employee_Education where Employee_ID = '" + id_get + "'");
        if (education.Rows.Count > 0)
        {
            edu_name1.Text = education.Rows[0]["Education_1_Name"].ToString();
            edu_year1.Text = education.Rows[0]["Education_1_Years"].ToString();
            edu_location1.Text = education.Rows[0]["Education_1_Location"].ToString();
            edu_name2.Text = education.Rows[0]["Education_2_Name"].ToString();
            edu_year2.Text = education.Rows[0]["Education_2_Years"].ToString();
            edu_location2.Text = education.Rows[0]["Education_2_Location"].ToString();
            edu_name3.Text = education.Rows[0]["Education_3_Name"].ToString();
            edu_year3.Text = education.Rows[0]["Education_3_Years"].ToString();
            edu_location3.Text = education.Rows[0]["Education_3_Location"].ToString();
            edu_name4.Text = education.Rows[0]["Education_4_Name"].ToString();
            edu_year4.Text = education.Rows[0]["Education_4_Years"].ToString();
            edu_location4.Text = education.Rows[0]["Education_4_Location"].ToString();
            edu_5.Text = education.Rows[0]["Education_5"].ToString();
            edu_name5.Text = education.Rows[0]["Education_5_Name"].ToString();
            edu_year5.Text = education.Rows[0]["Education_5_Years"].ToString();
            edu_location5.Text = education.Rows[0]["Education_5_Location"].ToString();
            edu_6.Text = education.Rows[0]["Education_6"].ToString();
            edu_name6.Text = education.Rows[0]["Education_6_Name"].ToString();
            edu_year6.Text = education.Rows[0]["Education_6_Years"].ToString();
            edu_location6.Text = education.Rows[0]["Education_6_Location"].ToString();
        }

        DataTable working_experience = Db.Rs("Select * from TBL_Employee_Working_Experience where Employee_ID = '" + id_get + "'");
        if (working_experience.Rows.Count > 0)
        {
            work_exp_name1.Text = working_experience.Rows[0]["Experience_1_Company_Name"].ToString();
            work_exp_position1.Text = working_experience.Rows[0]["Experience_1_Position"].ToString();
            work_exp_start1.Text = working_experience.Rows[0]["Experience_1_Start_Year"].ToString();
            work_exp_end1.Text = working_experience.Rows[0]["Experience_1_End_Year"].ToString();

            work_exp_name2.Text = working_experience.Rows[0]["Experience_2_Company_Name"].ToString();
            work_exp_position2.Text = working_experience.Rows[0]["Experience_2_Position"].ToString();
            work_exp_start2.Text = working_experience.Rows[0]["Experience_2_Start_Year"].ToString();
            work_exp_end2.Text = working_experience.Rows[0]["Experience_2_End_Year"].ToString();

            work_exp_name3.Text = working_experience.Rows[0]["Experience_3_Company_Name"].ToString();
            work_exp_position3.Text = working_experience.Rows[0]["Experience_3_Position"].ToString();
            work_exp_start3.Text = working_experience.Rows[0]["Experience_3_Start_Year"].ToString();
            work_exp_end3.Text = working_experience.Rows[0]["Experience_3_End_Year"].ToString();

            work_exp_name4.Text = working_experience.Rows[0]["Experience_4_Company_Name"].ToString();
            work_exp_position4.Text = working_experience.Rows[0]["Experience_4_Position"].ToString();
            work_exp_start4.Text = working_experience.Rows[0]["Experience_4_Start_Year"].ToString();
            work_exp_end4.Text = working_experience.Rows[0]["Experience_4_End_Year"].ToString();

            work_exp_name5.Text = working_experience.Rows[0]["Experience_5_Company_Name"].ToString();
            work_exp_position5.Text = working_experience.Rows[0]["Experience_5_Position"].ToString();
            work_exp_start5.Text = working_experience.Rows[0]["Experience_5_Start_Year"].ToString();
            work_exp_end5.Text = working_experience.Rows[0]["Experience_5_End_Year"].ToString();
        }

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

        ddl_DivisionPrev(rs.Rows[0]["Department_ID"].ToString());
        ddl_directSpvPrev(rs.Rows[0]["Division_ID"].ToString());

        ddl_division.SelectedValue = rs.Rows[0]["Division_ID"].ToString();
        ddl_directSpv.Text = rs.Rows[0]["Employee_DirectSpv"].ToString();

        ddl_position.SelectedValue = rs.Rows[0]["Position_ID"].ToString();
        ddl_blood_type.SelectedValue = rs.Rows[0]["Employee_Blood_Type"].ToString();
        npwp_number.Text = rs.Rows[0]["Payroll_NPWP_Number"].ToString();
        string npwp_status = rs.Rows[0]["Payroll_NPWP"].ToString();
        if (npwp_status.Equals("1")) { radio_npwp.Checked = true; }
        else if (npwp_status.Equals("2")) { radio_non_npwp.Checked = true; }
        else { radio_npwp.Checked = true; div_npwp_number.Visible = false; }
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

        DataTable emergency = Db.Rs("select * from TBL_Employee_Emergency_Contact where Employee_ID = '" + id_get + "'");

        //string emergency_name, emergency_address, emergency_phone, emergency_relation;

        StringBuilder looping_kontak = new StringBuilder();
        if (emergency.Rows.Count > 0)
        {
            //for (int a = 0; a < emergency.Rows.Count; a++)
            //{

            //    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Name"].ToString())) { emergency_name = "-"; } else { emergency_name = emergency.Rows[a]["Emergency_Name"].ToString(); };
            //    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Address"].ToString())) { emergency_address = "-"; } else { emergency_address = emergency.Rows[a]["Emergency_Address"].ToString(); };
            //    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Phone"].ToString())) { emergency_phone = "-"; } else { emergency_phone = emergency.Rows[a]["Emergency_Phone"].ToString(); };
            //    if (string.IsNullOrEmpty(emergency.Rows[a]["Emergency_Relation"].ToString())) { emergency_relation = "-"; } else { emergency_relation = emergency.Rows[a]["Emergency_Relation"].ToString(); };

            //    looping_kontak.Append("<tr>"
            //                        + "<td style=\"text-align: center;\">" + emergency_name + "</td>"
            //                        + "<td style=\"text-align: center;\">" + emergency_address + "</td>"
            //                        + "<td style=\"text-align: center;\">" + emergency_phone + "</td>"
            //                        + "<td style=\"text-align: center;\">" + emergency_relation + "</td>"
            //                    + "</tr>");
            //}
            //table_kontak.Text = looping_kontak.ToString();

            Emergency_name1.Text = emergency.Rows[0]["Emergency_Name"].ToString();
            Emergency_address1.Text = emergency.Rows[0]["Emergency_Address"].ToString();
            Emergency_phone1.Text = emergency.Rows[0]["Emergency_Phone"].ToString();
            Emergency_relation1.Text = emergency.Rows[0]["Emergency_Relation"].ToString();
            if (emergency.Rows.Count > 1)
            {
                Emergency_name2.Text = emergency.Rows[1]["Emergency_Name"].ToString();
                Emergency_address2.Text = emergency.Rows[1]["Emergency_Address"].ToString();
                Emergency_phone2.Text = emergency.Rows[1]["Emergency_Phone"].ToString();
                Emergency_relation2.Text = emergency.Rows[1]["Emergency_Relation"].ToString();
            }
        }

        DataTable depend = Db.Rs("select Dependent_Name, Dependent_Gender, CONVERT(VARCHAR(11), Dependent_DateofBirth ,105) as Dependent_DateofBirth, Dependent_PlaceofBirth, Dependent_Relationship from TBL_Employee_Dependent where Employee_ID = '" + id_get + "'");

        string dep_name = "", dep_gender = "", dep_placeofbirth = "", dep_dateofbirth = "", dep_relationship = "";
        StringBuilder looping_depend = new StringBuilder();
        if (depend.Rows.Count > 0)
        {
            //for (int a = 0; a < depend.Rows.Count; a++)
            //{
            //    dep_name = depend.Rows[a]["Dependent_Name"].ToString();
            //    if (depend.Rows[a]["Dependent_Gender"].ToString() == "L") { dep_gender = "Male"; } else if (depend.Rows[a]["Dependent_Gender"].ToString() == "P") { dep_gender = "Female"; };
            //    dep_placeofbirth = depend.Rows[a]["Dependent_PlaceofBirth"].ToString();
            //    dep_dateofbirth = depend.Rows[a]["Dependent_DateofBirth"].ToString();
            //    dep_relationship = depend.Rows[a]["Dependent_Relationship"].ToString();

            //    looping_depend.Append("<tr>"
            //                        + "<td style=\"text-align: center;\">" + dep_name + "</td>"
            //                        + "<td style=\"text-align: center;\">" + dep_gender + "</td>"
            //                        + "<td style=\"text-align: center;\">" + dep_placeofbirth + "</td>"
            //                        + "<td style=\"text-align: center;\">" + dep_dateofbirth + "</td>"
            //                        + "<td style=\"text-align: center;\">" + dep_relationship + "</td>"
            //                    + "</tr>");
            //}
            //loop_depend.Text = looping_depend.ToString();
            dep_name1.Text = depend.Rows[0]["Dependent_Name"].ToString();
            dep_gender1.SelectedValue = depend.Rows[0]["Dependent_Gender"].ToString();
            dep_placeofbirth1.Text = depend.Rows[0]["Dependent_PlaceofBirth"].ToString();
            dep_dateofbirth1.Text = depend.Rows[0]["Dependent_DateofBirth"].ToString();
            dep_relationship1.Text = depend.Rows[0]["Dependent_Relationship"].ToString();
            if (depend.Rows.Count > 1)
            {
                dep_name2.Text = depend.Rows[1]["Dependent_Name"].ToString();
                dep_gender2.SelectedValue = depend.Rows[1]["Dependent_Gender"].ToString();
                dep_placeofbirth2.Text = depend.Rows[1]["Dependent_PlaceofBirth"].ToString();
                dep_dateofbirth2.Text = depend.Rows[1]["Dependent_DateofBirth"].ToString();
                dep_relationship2.Text = depend.Rows[1]["Dependent_Relationship"].ToString();

                if (depend.Rows.Count > 2)
                {
                    dep_name3.Text = depend.Rows[2]["Dependent_Name"].ToString();
                    dep_gender3.SelectedValue = depend.Rows[2]["Dependent_Gender"].ToString();
                    dep_placeofbirth3.Text = depend.Rows[2]["Dependent_PlaceofBirth"].ToString();
                    dep_dateofbirth3.Text = depend.Rows[2]["Dependent_DateofBirth"].ToString();
                    dep_relationship3.Text = depend.Rows[2]["Dependent_Relationship"].ToString();

                    if (depend.Rows.Count > 3)
                    {
                        dep_name4.Text = depend.Rows[3]["Dependent_Name"].ToString();
                        dep_gender4.SelectedValue = depend.Rows[3]["Dependent_Gender"].ToString();
                        dep_placeofbirth4.Text = depend.Rows[3]["Dependent_PlaceofBirth"].ToString();
                        dep_dateofbirth4.Text = depend.Rows[3]["Dependent_DateofBirth"].ToString();
                        dep_relationship4.Text = depend.Rows[3]["Dependent_Relationship"].ToString();

                        if (depend.Rows.Count > 4)
                        {
                            dep_name5.Text = depend.Rows[4]["Dependent_Name"].ToString();
                            dep_gender5.SelectedValue = depend.Rows[4]["Dependent_Gender"].ToString();
                            dep_placeofbirth5.Text = depend.Rows[4]["Dependent_PlaceofBirth"].ToString();
                            dep_dateofbirth5.Text = depend.Rows[4]["Dependent_DateofBirth"].ToString();
                            dep_relationship5.Text = depend.Rows[4]["Dependent_Relationship"].ToString();
                        }
                    }
                }
            }
        }

        decimal bpjs_personal_val = 0;
        if (!string.IsNullOrEmpty(rs.Rows[0]["Payroll_BPJS_Pribadi_Value"].ToString())) { bpjs_personal_val = Convert.ToDecimal(rs.Rows[0]["Payroll_BPJS_Pribadi_Value"].ToString()); }
        personal_bpjs_value.Text = (bpjs_personal_val).ToString("#,##0");

        salary_pph.Text = rs.Rows[0]["Payroll_SalaryPPH"].ToString();
        salary_bpjs.Text = rs.Rows[0]["Payroll_SalaryBPJS"].ToString();

        ddl_status_pajak.SelectedValue = rs.Rows[0]["PTKP_ID"].ToString();
        ddl_personal_bpjs.SelectedValue = rs.Rows[0]["Payroll_BPJS_Pribadi"].ToString();
        ddl_bpjs_ditanggung.SelectedValue = rs.Rows[0]["Payroll_BPJS_Ditanggung"].ToString();
        checkbox_sim_a.Checked = Convert.ToBoolean(rs.Rows[0]["Employee_SIM_A"].ToString());
        checkbox_sim_c.Checked = Convert.ToBoolean(rs.Rows[0]["Employee_SIM_C"].ToString());
        string encryptionPassword = Param.encryptionPassword;
        string id_emp = id_get, foto_link = "";
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
    protected void BtnSubmit(object sender, EventArgs e)
    {
        DataTable cek_existEmail = Db.Rs("Select Employee_Company_Email, Employee_ID from TBL_Employee where Employee_Company_Email = '" + Cf.StrSql(email_kantor.Text) + "'");
        //if (cek_existEmail.Rows.Count < 1)
        //{
        string sim_a = "", sim_c = "";
        if (checkbox_sim_a.Checked) { sim_a = "1"; }
        else { sim_a = "0"; }

        if (checkbox_sim_c.Checked) { sim_c = "1"; }
        else { sim_c = "0"; }

        //convert date
        DateTime Birth = Convert.ToDateTime(tanggal_lahir.Text);
        DateTime Join = Convert.ToDateTime(contract_start.Text);
        DateTime Resign = Convert.ToDateTime(contract_end.Text);
        DateTime start = Convert.ToDateTime(start_of_employment.Text);

        string Birth_Conv = Birth.ToString("yyyy-MM-dd").ToString();
        string Join_Conv = Join.ToString("yyyy-MM-dd").ToString();
        string Resign_Conv = Resign.ToString("yyyy-MM-dd").ToString();
        string start_conv = start.ToString("yyyy-MM-dd").ToString();

        string dep_date1_conv = ""; string dep_date2_conv = ""; string dep_date3_conv = ""; string dep_date4_conv = ""; string dep_date5_conv = "";
        if (dep_dateofbirth1.Text != "") { DateTime dep_date1 = Convert.ToDateTime(dep_dateofbirth1.Text); dep_date1_conv = dep_date1.ToString("yyyy-MM-dd").ToString(); }
        if (dep_dateofbirth2.Text != "") { DateTime dep_date2 = Convert.ToDateTime(dep_dateofbirth2.Text); dep_date2_conv = dep_date2.ToString("yyyy-MM-dd").ToString(); }
        if (dep_dateofbirth3.Text != "") { DateTime dep_date3 = Convert.ToDateTime(dep_dateofbirth3.Text); dep_date3_conv = dep_date3.ToString("yyyy-MM-dd").ToString(); }
        if (dep_dateofbirth4.Text != "") { DateTime dep_date4 = Convert.ToDateTime(dep_dateofbirth4.Text); dep_date4_conv = dep_date4.ToString("yyyy-MM-dd").ToString(); }
        if (dep_dateofbirth5.Text != "") { DateTime dep_date5 = Convert.ToDateTime(dep_dateofbirth5.Text); dep_date5_conv = dep_date5.ToString("yyyy-MM-dd").ToString(); }

        if (Join < Resign)
        {
            int leavebalance = 0; int sickleave = 0;

            if (DateTime.Now.Year >= Join.Year)
            {
                if (DateTime.Now.Month >= Join.Month) { leavebalance = 24; sickleave = 12; }
                int a = DateTime.Now.Month - Join.Month;
                leavebalance = a * 2; sickleave = a;
            }
            //upload photo
            int h, w; h = 500; w = 500;

            string cv_path = "", cv_update = "";
            if (FileUpload_cv.HasFile)
            {
                string Folder = "cv/";
                cv_path = uploadFile(Folder, FileUpload_cv);
                cv_update = "Employee_CV = '" + cv_path + "', ";
            }

            string sts_npwp = "";
            if (radio_npwp.Checked.Equals(true)) { sts_npwp = "1"; } else if (radio_non_npwp.Checked.Equals(true)) { sts_npwp = "2"; }

            string Full_Name = Cf.StrSql(nama_depan.Text) + " " + Cf.StrSql(nama_tengah.Text) + " " + Cf.StrSql(nama_belakang.Text);

            string update = "update TBL_Employee set Employee_NIK = '" + Cf.StrSql(nik.Text) + "', Employee_Full_Name = '" + Cf.StrSql(Full_Name) + "', Employee_First_name = '" + Cf.StrSql(nama_depan.Text) + "', Employee_Middle_Name = '" + Cf.StrSql(nama_tengah.Text) + "', Employee_Last_Name = '" + Cf.StrSql(nama_belakang.Text) + "', Employee_Alias_Name = '" + Cf.StrSql(nama_alias.Text) + "', Employee_Gender = '" + Cf.StrSql(jenis_kelamin.SelectedValue) + "', Employee_Blood_Type = '" + Cf.StrSql(ddl_blood_type.SelectedValue) + "', Employee_PlaceOfBirth = '" + Cf.StrSql(tempat_lahir.Text) + "', Employee_DateOfBirth = '" + Cf.StrSql(Birth_Conv) + "', Employee_Phone_Number_Primary = '" + Cf.StrSql(no_hp1.Text) + "', Employee_Phone_Number = '" + Cf.StrSql(no_hp2.Text) + "', Employee_IDCard_Number = '" + Cf.StrSql(no_ktp.Text) + "', Employee_IDCard_Address = '" + Cf.StrSql(alamat_ktp.Text) + "', Employee_Domicile_Address = '" + Cf.StrSql(alamat_domisili.Text) + "', Employee_Marital_Status = '" + Cf.StrSql(marital_status.SelectedValue) + "', Employee_Spouse_Name = '" + Cf.StrSql(nama_pasangan.Text) + "', Employee_SIM_A = '" + sim_a + "', Employee_SIM_C = '" + sim_c + "', Employee_Company_Email = '" + Cf.StrSql(email_kantor.Text) + "', Employee_Personal_Email = '" + Cf.StrSql(email_pribadi.Text) + "', Employee_Education = '" + Cf.StrSql(last_education.SelectedValue) + "', Employee_Education_Major = '" + Cf.StrSql(jurusan.Text) + "', Employee_DirectSpv = '" + Cf.StrSql(ddl_directSpv.SelectedValue) + "', Employee_Bank_AccName_Primary = '" + Cf.StrSql(in_the_name1.Text) + "', Employee_Bank_AccNumber_Primary = '" + Cf.StrSql(bank_account_1.Text) + "', Employee_Bank_AccName = '" + Cf.StrSql(in_the_name2.Text) + "', Employee_Bank_AccNumber = '" + Cf.StrSql(bank_account_2.Text) + "', " + cv_update + " Employee_StartofEmployment = '" + start_conv + "', Employee_JoinDate = '" + Join_Conv + "', Employee_EndDate = '" + Resign_Conv + "', Employee_Sum_LeaveBalance = '0', Employee_Sum_SickLeave = '" + sickleave + "', Religion_ID = '" + Cf.StrSql(agama.SelectedValue) + "', Department_ID = '" + ddl_department.SelectedValue + "', Division_ID = '" + ddl_division.SelectedValue + "', Position_ID = '" + ddl_position.SelectedValue + "', Employee_DateLastModified = getdate(), Employee_Inactive = '" + ddl_employee_status.SelectedValue + "', Employee_Inactive_Date = getdate(), Employee_Inactive_Remarks = '" + Cf.StrSql(remarks.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "'";
            Db.Execute(update);

            //Insert/update emergency contact
            DataTable employee_emergency_data = Db.Rs("Select Emergency_ID, Employee_ID from TBL_Employee_Emergency_Contact where Employee_ID = '" + Cf.Int(id_get) + "'");

            string insert_emergency_contact_1 = "insert into TBL_Employee_Emergency_Contact (Emergency_Name, Emergency_Relation, Emergency_Address, Emergency_Phone, Employee_ID)"
                                                   + "values('" + Cf.StrSql(Emergency_name1.Text) + "','" + Cf.StrSql(Emergency_relation1.Text) + "','" + Cf.StrSql(Emergency_address1.Text) + "','" + Cf.StrSql(Emergency_phone1.Text) + "','" + Cf.Int(id_get) + "')";

            string insert_emergency_contact_2 = "insert into TBL_Employee_Emergency_Contact (Emergency_Name, Emergency_Relation, Emergency_Address, Emergency_Phone, Employee_ID)"
                                                   + "values('" + Cf.StrSql(Emergency_name2.Text) + "','" + Cf.StrSql(Emergency_relation2.Text) + "','" + Cf.StrSql(Emergency_address2.Text) + "','" + Cf.StrSql(Emergency_phone2.Text) + "','" + Cf.Int(id_get) + "')";


            if (employee_emergency_data.Rows.Count > 0)
            {
                string update_emergency_contact_1 = "update TBL_Employee_Emergency_Contact set Emergency_Name = '" + Cf.StrSql(Emergency_name1.Text) + "', Emergency_Relation = '" + Cf.StrSql(Emergency_relation1.Text) + "', Emergency_Address = '" + Cf.StrSql(Emergency_address1.Text) + "', Emergency_Phone = '" + Cf.StrSql(Emergency_phone1.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Emergency_ID = '" + employee_emergency_data.Rows[0]["Emergency_ID"].ToString() + "'";

                Db.Execute(update_emergency_contact_1);

                if (employee_emergency_data.Rows.Count > 1)
                {
                    string update_emergency_contact_2 = "update TBL_Employee_Emergency_Contact set Emergency_Name = '" + Cf.StrSql(Emergency_name2.Text) + "', Emergency_Relation = '" + Cf.StrSql(Emergency_relation2.Text) + "', Emergency_Address = '" + Cf.StrSql(Emergency_address2.Text) + "', Emergency_Phone = '" + Cf.StrSql(Emergency_phone2.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Emergency_ID = '" + employee_emergency_data.Rows[1]["Emergency_ID"].ToString() + "'";
                    Db.Execute(update_emergency_contact_2);
                }
                else
                {
                    if (Emergency_name2.Text != "" && Emergency_address2.Text != "" && Emergency_phone2.Text != "" && Emergency_relation2.Text != "")
                    {
                        Db.Execute(insert_emergency_contact_2);
                    }
                }
            }
            else
            {
                if (Emergency_name1.Text != "" && Emergency_address1.Text != "" && Emergency_phone1.Text != "" && Emergency_relation1.Text != "")
                {
                    Db.Execute(insert_emergency_contact_1);
                    if (Emergency_name2.Text != "" && Emergency_address2.Text != "" && Emergency_phone2.Text != "" && Emergency_relation2.Text != "")
                    {
                        Db.Execute(insert_emergency_contact_2);
                    }
                }
            }

            //Insert/update formal education
            DataTable employee_formal_education_data = Db.Rs("Select Employee_ID from TBL_Employee_Education where Employee_ID = '" + Cf.Int(id_get) + "'");
            if (employee_formal_education_data.Rows.Count > 0)
            {
                string update_formal_education = "update TBL_Employee_Education set Education_1_Name = '" + Cf.StrSql(edu_name1.Text) + "', Education_1_Years = '" + Cf.StrSql(edu_year1.Text) + "',  Education_1_Location = '" + Cf.StrSql(edu_location1.Text) + "', Education_2_Name = '" + Cf.StrSql(edu_name2.Text) + "',  Education_2_Years = '" + Cf.StrSql(edu_year2.Text) + "',  Education_2_Location = '" + Cf.StrSql(edu_location2.Text) + "', Education_3_Name = '" + Cf.StrSql(edu_name3.Text) + "', Education_3_Years = '" + Cf.StrSql(edu_year3.Text) + "',  Education_3_Location = '" + Cf.StrSql(edu_location3.Text) + "', Education_4_Name = '" + Cf.StrSql(edu_name4.Text) + "', Education_4_Years = '" + Cf.StrSql(edu_year4.Text) + "', Education_4_Location = '" + Cf.StrSql(edu_location4.Text) + "', Education_5 = '" + Cf.StrSql(edu_5.Text) + "', Education_5_Name = '" + Cf.StrSql(edu_name5.Text) + "', Education_5_Years = '" + Cf.StrSql(edu_year5.Text) + "', Education_5_Location = '" + Cf.StrSql(edu_location5.Text) + "', Education_6 = '" + Cf.StrSql(edu_6.Text) + "', Education_6_Name = '" + Cf.StrSql(edu_name6.Text) + "', Education_6_Years = '" + Cf.StrSql(edu_year6.Text) + "', Education_6_Location = '" + Cf.StrSql(edu_location6.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "'";
                Db.Execute(update_formal_education);
            }
            else
            {
                if ((edu_name1.Text != "" && edu_year1.Text != "" && edu_location1.Text != "") || (edu_name2.Text != "" && edu_year2.Text != "" && edu_location2.Text != "") || (edu_name3.Text != "" && edu_year3.Text != "" && edu_location3.Text != ""))
                {
                    string insert_formal_education = "insert into TBL_Employee_Education (Education_1_Name, Education_1_Years, Education_1_Location, "
                                                        + "Education_2_Name, Education_2_Years, Education_2_Location, "
                                                        + "Education_3_Name, Education_3_Years, Education_3_Location, "
                                                        + "Education_4_Name, Education_4_Years, Education_4_Location, "
                                                        + "Education_5, Education_5_Name, Education_5_Years, Education_5_Location, "
                                                        + "Education_6, Education_6_Name, Education_6_Years, Education_6_Location, Employee_ID) "
                                                        + "values('" + Cf.StrSql(edu_name1.Text) + "','" + Cf.StrSql(edu_year1.Text) + "','" + Cf.StrSql(edu_location1.Text) + "', "
                                                        + "'" + Cf.StrSql(edu_name2.Text) + "','" + Cf.StrSql(edu_year2.Text) + "','" + Cf.StrSql(edu_location2.Text) + "', "
                                                        + "'" + Cf.StrSql(edu_name3.Text) + "','" + Cf.StrSql(edu_year3.Text) + "','" + Cf.StrSql(edu_location3.Text) + "', "
                                                        + "'" + Cf.StrSql(edu_name4.Text) + "','" + Cf.StrSql(edu_year4.Text) + "','" + Cf.StrSql(edu_location4.Text) + "', "
                                                        + "'" + Cf.StrSql(edu_5.Text) + "','" + Cf.StrSql(edu_name5.Text) + "','" + Cf.StrSql(edu_year5.Text) + "','" + Cf.StrSql(edu_location5.Text) + "', "
                                                        + "'" + Cf.StrSql(edu_6.Text) + "','" + Cf.StrSql(edu_name6.Text) + "','" + Cf.StrSql(edu_year6.Text) + "','" + Cf.StrSql(edu_location6.Text) + "','" + Cf.Int(id_get) + "')";
                    Db.Execute(insert_formal_education);
                }
            }

            //insert/Update working experience
            DataTable employee_working_experience_data = Db.Rs("Select Employee_ID from TBL_Employee_Working_Experience where Employee_ID = '" + Cf.Int(id_get) + "'");
            if (employee_working_experience_data.Rows.Count > 0)
            {
                string update_working_experience = "update TBL_Employee_Working_Experience set Experience_1_Company_Name = '" + Cf.StrSql(work_exp_name1.Text) + "', Experience_1_Position = '" + Cf.StrSql(work_exp_position1.Text) + "', Experience_1_Start_Year = '" + Cf.StrSql(work_exp_start1.Text) + "', Experience_1_End_Year = '" + Cf.StrSql(work_exp_end1.Text) + "', "
                + "Experience_2_Company_Name = '" + Cf.StrSql(work_exp_name2.Text) + "', Experience_2_Position = '" + Cf.StrSql(work_exp_position2.Text) + "', Experience_2_Start_Year = '" + Cf.StrSql(work_exp_start2.Text) + "', Experience_2_End_Year = '" + Cf.StrSql(work_exp_end2.Text) + "', "
                + "Experience_3_Company_Name = '" + Cf.StrSql(work_exp_name3.Text) + "', Experience_3_Position = '" + Cf.StrSql(work_exp_position3.Text) + "', Experience_3_Start_Year = '" + Cf.StrSql(work_exp_start3.Text) + "', Experience_3_End_Year = '" + Cf.StrSql(work_exp_end3.Text) + "', "
                + "Experience_4_Company_Name = '" + Cf.StrSql(work_exp_name4.Text) + "', Experience_4_Position = '" + Cf.StrSql(work_exp_position4.Text) + "', Experience_4_Start_Year = '" + Cf.StrSql(work_exp_start4.Text) + "', Experience_4_End_Year = '" + Cf.StrSql(work_exp_end4.Text) + "', "
                + "Experience_5_Company_Name = '" + Cf.StrSql(work_exp_name5.Text) + "', Experience_5_Position = '" + Cf.StrSql(work_exp_position5.Text) + "', Experience_5_Start_Year = '" + Cf.StrSql(work_exp_start5.Text) + "', Experience_5_End_Year = '" + Cf.StrSql(work_exp_end5.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "'";
                Db.Execute(update_working_experience);
            }
            else
            {
                if ((work_exp_name1.Text != "" && work_exp_position1.Text != "" && work_exp_start1.Text != "" && work_exp_end1.Text != "") || (work_exp_name2.Text != "" && work_exp_position2.Text != "" && work_exp_start2.Text != "" && work_exp_end2.Text != "") || (work_exp_name3.Text != "" && work_exp_position3.Text != "" && work_exp_start3.Text != "" && work_exp_end3.Text != "") || (work_exp_name4.Text != "" && work_exp_position4.Text != "" && work_exp_start4.Text != "" && work_exp_end4.Text != "") ||
              (work_exp_name5.Text != "" && work_exp_position5.Text != "" && work_exp_start5.Text != "" && work_exp_end5.Text != ""))
                {
                    string insert_working_experience = "insert into TBL_Employee_Working_Experience (Experience_1_Company_Name, Experience_1_Position, Experience_1_Start_Year, Experience_1_End_Year, "
                                                       + "Experience_2_Company_Name, Experience_2_Position, Experience_2_Start_Year, Experience_2_End_Year, "
                                                       + "Experience_3_Company_Name, Experience_3_Position, Experience_3_Start_Year, Experience_3_End_Year, "
                                                       + "Experience_4_Company_Name, Experience_4_Position, Experience_4_Start_Year, Experience_4_End_Year, "
                                                       + "Experience_5_Company_Name, Experience_5_Position, Experience_5_Start_Year, Experience_5_End_Year, Employee_ID ) "
                                                       + "values('" + Cf.StrSql(work_exp_name1.Text) + "','" + Cf.StrSql(work_exp_position1.Text) + "','" + Cf.StrSql(work_exp_start1.Text) + "','" + Cf.StrSql(work_exp_end1.Text) + "', "
                                                       + "'" + Cf.StrSql(work_exp_name2.Text) + "','" + Cf.StrSql(work_exp_position2.Text) + "','" + Cf.StrSql(work_exp_start2.Text) + "','" + Cf.StrSql(work_exp_end2.Text) + "', "
                                                       + "'" + Cf.StrSql(work_exp_name3.Text) + "','" + Cf.StrSql(work_exp_position3.Text) + "','" + Cf.StrSql(work_exp_start3.Text) + "','" + Cf.StrSql(work_exp_end3.Text) + "', "
                                                       + "'" + Cf.StrSql(work_exp_name4.Text) + "','" + Cf.StrSql(work_exp_position4.Text) + "','" + Cf.StrSql(work_exp_start4.Text) + "','" + Cf.StrSql(work_exp_end4.Text) + "', "
                                                       + "'" + Cf.StrSql(work_exp_name5.Text) + "','" + Cf.StrSql(work_exp_position5.Text) + "','" + Cf.StrSql(work_exp_start5.Text) + "','" + Cf.StrSql(work_exp_end5.Text) + "','" + Cf.Int(id_get) + "')";
                    Db.Execute(insert_working_experience);
                }
            }

            //insert/update dependent
            DataTable employee_dependent_data = Db.Rs("Select Dependent_ID, Employee_ID from TBL_Employee_Dependent where Employee_ID = '" + Cf.Int(id_get) + "'");

            string insert_dependent_1 = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                        + "values('" + Cf.StrSql(dep_name1.Text) + "','" + dep_gender1.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth1.Text) + "', '" + dep_date1_conv + "', '" + Cf.StrSql(dep_relationship1.Text) + "', '" + Cf.Int(id_get) + "')";

            string insert_dependent_2 = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                        + "values('" + Cf.StrSql(dep_name2.Text) + "','" + dep_gender2.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth2.Text) + "', '" + dep_date2_conv + "', '" + Cf.StrSql(dep_relationship2.Text) + "', '" + Cf.Int(id_get) + "')";

            string insert_dependent_3 = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                       + "values('" + Cf.StrSql(dep_name3.Text) + "','" + dep_gender3.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth3.Text) + "', '" + dep_date3_conv + "', '" + Cf.StrSql(dep_relationship3.Text) + "', '" + Cf.Int(id_get) + "')";

            string insert_dependent_4 = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                            + "values('" + Cf.StrSql(dep_name4.Text) + "','" + dep_gender4.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth4.Text) + "', '" + dep_date4_conv + "', '" + Cf.StrSql(dep_relationship4.Text) + "', '" + Cf.Int(id_get) + "')";

            string insert_dependent_5 = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                                + "values('" + Cf.StrSql(dep_name5.Text) + "','" + dep_gender5.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth5.Text) + "', '" + dep_date5_conv + "', '" + Cf.StrSql(dep_relationship5.Text) + "', '" + Cf.Int(id_get) + "')";

            if (employee_dependent_data.Rows.Count > 0)
            {
                string update_emergency_contact_1 = "update TBL_Employee_Dependent set Dependent_Name = '" + Cf.StrSql(dep_name1.Text) + "', Dependent_Gender = '" + Cf.StrSql(dep_gender1.SelectedValue) + "', Dependent_PlaceofBirth = '" + Cf.StrSql(dep_placeofbirth1.Text) + "', Dependent_DateofBirth = '" + Cf.StrSql(dep_date1_conv) + "', Dependent_Relationship = '" + Cf.StrSql(dep_relationship1.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Dependent_ID = '" + employee_dependent_data.Rows[0]["Dependent_ID"].ToString() + "'";
                Db.Execute(update_emergency_contact_1);
                if (employee_dependent_data.Rows.Count > 1)
                {
                    string update_emergency_contact_2 = "update TBL_Employee_Dependent set Dependent_Name = '" + Cf.StrSql(dep_name2.Text) + "', Dependent_Gender = '" + Cf.StrSql(dep_gender2.SelectedValue) + "', Dependent_PlaceofBirth = '" + Cf.StrSql(dep_placeofbirth2.Text) + "', Dependent_DateofBirth = '" + Cf.StrSql(dep_date2_conv) + "', Dependent_Relationship = '" + Cf.StrSql(dep_relationship2.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Dependent_ID = '" + employee_dependent_data.Rows[1]["Dependent_ID"].ToString() + "'";
                    Db.Execute(update_emergency_contact_2);

                    if (employee_dependent_data.Rows.Count > 2)
                    {
                        string update_emergency_contact_3 = "update TBL_Employee_Dependent set Dependent_Name = '" + Cf.StrSql(dep_name3.Text) + "', Dependent_Gender = '" + Cf.StrSql(dep_gender3.SelectedValue) + "', Dependent_PlaceofBirth = '" + Cf.StrSql(dep_placeofbirth3.Text) + "', Dependent_DateofBirth = '" + Cf.StrSql(dep_date3_conv) + "', Dependent_Relationship = '" + Cf.StrSql(dep_relationship3.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Dependent_ID = '" + employee_dependent_data.Rows[2]["Dependent_ID"].ToString() + "'";
                        Db.Execute(update_emergency_contact_3);

                        if (employee_dependent_data.Rows.Count > 3)
                        {
                            string update_emergency_contact_4 = "update TBL_Employee_Dependent set Dependent_Name = '" + Cf.StrSql(dep_name4.Text) + "', Dependent_Gender = '" + Cf.StrSql(dep_gender4.SelectedValue) + "', Dependent_PlaceofBirth = '" + Cf.StrSql(dep_placeofbirth4.Text) + "', Dependent_DateofBirth = '" + Cf.StrSql(dep_date4_conv) + "', Dependent_Relationship = '" + Cf.StrSql(dep_relationship4.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Dependent_ID = '" + employee_dependent_data.Rows[3]["Dependent_ID"].ToString() + "'";
                            Db.Execute(update_emergency_contact_4);

                            if (employee_dependent_data.Rows.Count > 4)
                            {
                                string update_emergency_contact_5 = "update TBL_Employee_Dependent set Dependent_Name = '" + Cf.StrSql(dep_name5.Text) + "', Dependent_Gender = '" + Cf.StrSql(dep_gender5.SelectedValue) + "', Dependent_PlaceofBirth = '" + Cf.StrSql(dep_placeofbirth5.Text) + "', Dependent_DateofBirth = '" + Cf.StrSql(dep_date5_conv) + "', Dependent_Relationship = '" + Cf.StrSql(dep_relationship5.Text) + "' where Employee_ID = '" + Cf.Int(id_get) + "' and Dependent_ID = '" + employee_dependent_data.Rows[4]["Dependent_ID"].ToString() + "'";
                                Db.Execute(update_emergency_contact_5);
                            }
                            else
                            {
                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "") { Db.Execute(insert_dependent_5); }
                            }
                        }
                        else
                        {
                            if (dep_name4.Text != "" && dep_placeofbirth4.Text != "" && dep_relationship4.Text != "" && dep_dateofbirth4.Text != "")
                            {
                                Db.Execute(insert_dependent_4);
                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "") { Db.Execute(insert_dependent_5); }
                            }
                        }
                    }
                    else
                    {
                        if (dep_name3.Text != "" && dep_placeofbirth3.Text != "" && dep_relationship3.Text != "" && dep_dateofbirth3.Text != "")
                        {
                            Db.Execute(insert_dependent_3);
                            if (dep_name4.Text != "" && dep_placeofbirth4.Text != "" && dep_relationship4.Text != "" && dep_dateofbirth4.Text != "")
                            {
                                Db.Execute(insert_dependent_4);
                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "") { Db.Execute(insert_dependent_5); }
                            }
                        }
                    }
                }
                else
                {
                    if (dep_name2.Text != "" && dep_placeofbirth2.Text != "" && dep_relationship2.Text != "" && dep_dateofbirth2.Text != "")
                    {
                        Db.Execute(insert_dependent_2);
                        if (dep_name3.Text != "" && dep_placeofbirth3.Text != "" && dep_relationship3.Text != "" && dep_dateofbirth3.Text != "")
                        {
                            Db.Execute(insert_dependent_3);
                            if (dep_name4.Text != "" && dep_placeofbirth4.Text != "" && dep_relationship4.Text != "" && dep_dateofbirth4.Text != "")
                            {
                                Db.Execute(insert_dependent_4);
                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "") { Db.Execute(insert_dependent_5); }
                            }
                        }
                    }
                }
            }
            else
            {
                if (dep_name1.Text != "" && dep_placeofbirth1.Text != "" && dep_relationship1.Text != "" && dep_dateofbirth1.Text != "")
                {
                    Db.Execute(insert_dependent_1);
                    if (dep_name2.Text != "" && dep_placeofbirth2.Text != "" && dep_relationship2.Text != "" && dep_dateofbirth2.Text != "")
                    {
                        Db.Execute(insert_dependent_2);
                        if (dep_name3.Text != "" && dep_placeofbirth3.Text != "" && dep_relationship3.Text != "" && dep_dateofbirth3.Text != "")
                        {
                            Db.Execute(insert_dependent_3);
                            if (dep_name4.Text != "" && dep_placeofbirth4.Text != "" && dep_relationship4.Text != "" && dep_dateofbirth4.Text != "")
                            {
                                Db.Execute(insert_dependent_4);
                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "")
                                { Db.Execute(insert_dependent_5); }
                            }
                        }
                    }
                }
            }


            //Update info payment / payroll
            string update_info_payment = "update TBL_Employee_Payroll set Payroll_NPWP = '" + Cf.Int(sts_npwp) + "', Payroll_NPWP_Number = '" + Cf.StrSql(npwp_number.Text) + "',Payroll_OthersAllowance = '0',Payroll_Jamsostek = '0', Payroll_BPJS_Pribadi = '" + Cf.StrSql(ddl_personal_bpjs.SelectedValue) + "',Payroll_BPJS_Pribadi_Value = '" + Cf.StrSql(personal_bpjs_value.Text) + "',Payroll_BPJS_Ditanggung = '" + Cf.StrSql(ddl_bpjs_ditanggung.Text) + "',Payroll_SalaryPPH = '" + Cf.StrSql(salary_pph.Text.Replace(".","")) + "',Payroll_SalaryBPJS = '" + Cf.StrSql(salary_bpjs.Text.Replace(".","")) + "',Payroll_Group = '" + Cf.StrSql(ddl_payroll_group.SelectedValue) + "' where Employee_ID = '" + Cf.Int(id_get) + "'";
            Db.Execute(update_info_payment);

            string encryptionPassword = Param.encryptionPassword;
            string encrypted = Crypto.Encrypt(id_get, encryptionPassword);

            //Update info payment / payroll history
            string update_payment_history = "update TBL_HistorySalary set HistorySalary_Value = '" + Cf.StrSql(current_salary.Text.Replace(".", "")) + "', HistorySalary_DateChanged = getdate() where Employee_ID = '" + encrypted + "'";
            Db.Execute2(update_payment_history);

            if (Employee_Photo.PostedFile != null && Employee_Photo.PostedFile.FileName != "")
            {
                string Folder = "images/employee-photo/";
                //images//
                string banner_path = "";
                if (Employee_Photo.HasFile)
                {
                    banner_path = uploadIncResize(h, w, Folder, Employee_Photo, true);
                }
                string get_foto = App.UserID + "_(Profile)_" + Cf.StrSql(banner_path);
                string updates = "update TBL_Employee set Employee_Photo = '" + get_foto + "' where Employee_ID = '" + Cf.Int(id_get) + "'";
                Db.Execute(updates);
            }
            Response.Redirect(Param.Path_Admin + "data-karyawan/");
        }
        else { Js.Alert(this, "Cek Periode Kontrak"); }
        //}
        //else { Js.Alert(this, "Email Kantor Sudah Ada!"); }
    }
    protected string uploadIncResize(int h, int w, string Folder, FileUpload gam, bool resize)
    {
        string upload_file = "";
        try
        {
            if (gam.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || gam.PostedFile.ContentType == "application/msword" || gam.PostedFile.ContentType == "image/jpeg" || gam.PostedFile.ContentType == "image/png" || gam.PostedFile.ContentType == "image/gif" || gam.PostedFile.ContentType == "application/pdf")
            {
                string filename = Path.GetFileName(gam.FileName), dir = Folder;
                bool status = true;

                while (status)
                {
                    status = FileExists(filename, Server.MapPath("~/assets/" + Folder));
                    if (status) filename = getRandomFileName() + System.IO.Path.GetExtension(gam.PostedFile.FileName);
                }

                //teaser_image.SaveAs(Server.MapPath("~/images/") + Folder + filename);
                upload_file = filename;

                string type = "";
                if (Path.GetExtension(gam.FileName).ToLower() != ".jpg" || Path.GetExtension(gam.FileName).ToLower() != ".png" || Path.GetExtension(gam.FileName).ToLower() != ".jpeg")
                { type = "_(Profile)_"; }
                else { type = "_(CV)_"; }

                string large = Server.MapPath("~/assets/") + dir + App.UserID + type + filename;
                string temp = Server.MapPath("~/assets/") + dir + filename;

                gam.PostedFile.SaveAs(temp);
                if (resize)
                {
                    Imgh.Crop(temp, large, h, w);
                    File.Delete(temp);
                }
                //File.Delete(temp);
            }
            else { Js.Alert(this, "Upload status: Only JPEG , PNG or GIF files are allowed"); }
        }
        catch (Exception ex) { Js.Alert(this, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message); }
        return upload_file;
    }
    protected string getRandomFileName()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        int newFile = 0;
        newFile = rand.Next(1, 9999999);
        return newFile.ToString();
    }
    protected bool FileExists(string filename, string path)
    {
        FileInfo imageFile = new FileInfo(path + filename);
        bool fileStatus = imageFile.Exists;
        return fileStatus;
    }
    protected string uploadFile(string Folder, FileUpload img)
    {
        string upload_file = "";
        try
        {
            string filename = Path.GetFileName(img.FileName);
            string dir = Param.PathFile + Folder;
            bool status = true;

            while (status)
            {
                status = FileExists(filename, Server.MapPath("~/file-upload/" + Folder));
                if (status)
                    filename = getRandomFileName() + System.IO.Path.GetExtension(img.PostedFile.FileName);
            }

            upload_file = filename;
            string large = dir + filename;

            img.PostedFile.SaveAs(large);
        }
        catch (Exception ex)
        {
            Js.Alert(this, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
        }
        return upload_file;
    }
    //////////Selected Index Change
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_division.Items.Clear();
        ddl_directSpv.Items.Clear();
        ddl_directSpv.Items.Add(new ListItem("-- Pilih Direct Supervisor --", ""));
        ddl_division.Items.Add(new ListItem("-- Pilih Divisi --", ""));
        ddl_DivisionPrev(ddl_department.SelectedValue);
    }
    protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_directSpv.Items.Clear();
        ddl_directSpv.Items.Add(new ListItem("-- Pilih Direct Supervisor --", ""));
        ddl_directSpvPrev(ddl_division.SelectedValue);
    }
    protected void radio_1(object sender, EventArgs e)
    {
        div_npwp_number.Visible = true;
        npwp_number.Text = "";
        update.Update();
    }
    protected void radio_2(object sender, EventArgs e)
    {
        div_npwp_number.Visible = false;
        npwp_number.Text = "00.000.000.0-000.000";
        update.Update();
    }
    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect(Param.Path_Admin + "data-karyawan/"); }
}