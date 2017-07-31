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

public partial class _data_karyawan_add : System.Web.UI.Page
{
    protected string recent { get { return App.GetStr(this, "recent"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Data Karyawan", "insert");

        if (!IsPostBack)
        {
            ddl_DepartmentPrev(); ddl_TitlePrev(); ddl_ReligionPrev(); ddl_status_pajakPrev(); ddl_payroll_groupPrev();
            update.Update();

            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            if (recent != "")
            {
                DataTable ch = Db.Rs("select * from TBL_Employee where Employee_ID = " + recent + "");
                if (ch.Rows.Count > 0)
                {
                    link_added.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                                    + "<h4 class=\"page-title\">Data Berhasil Ditambahkan!</h4>"
                                    + "<a href=\"" + Param.Path_Admin + "data-karyawan/detail/?id=" + recent + "\">Klik disini</a></div>";
                }
            }
        }
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

    protected void BtnSubmit(object sender, EventArgs e)
    {
        DataTable cek_existEmail = Db.Rs("Select Employee_Company_Email, Employee_ID from TBL_Employee where Employee_Company_Email = '" + Cf.StrSql(email_kantor.Text) + "'");
        if (cek_existEmail.Rows.Count < 1)
        {
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

                string cv_path = "";
                if (FileUpload_cv.HasFile)
                {
                    string Folder = "cv/";
                    cv_path = uploadFile(Folder, FileUpload_cv);
                }

                string sts_npwp = "";
                if (radio_npwp.Checked.Equals(true)) { sts_npwp = "1"; } else if (radio_non_npwp.Checked.Equals(true)) { sts_npwp = "2"; }

                string Full_Name = Cf.StrSql(nama_depan.Text) + " " + Cf.StrSql(nama_tengah.Text) + " " + Cf.StrSql(nama_belakang.Text);

                string insert = "Insert into TBL_Employee (Employee_NIK, Employee_Full_Name, Employee_First_name, Employee_Middle_Name, Employee_Last_Name, Employee_Alias_Name, "
                                + "Employee_Gender, Employee_Blood_Type, Employee_PlaceOfBirth, Employee_DateOfBirth, Employee_Phone_Number_Primary, Employee_Phone_Number, Employee_IDCard_Number, "
                                + "Employee_IDCard_Address, Employee_Domicile_Address, Employee_Marital_Status, Employee_Spouse_Name, Employee_SIM_A, Employee_SIM_C, Employee_Company_Email, "
                                + "Employee_Personal_Email, Employee_Education, Employee_Education_Major, Employee_DirectSpv, Employee_Bank_AccName_Primary, "
                                + "Employee_Bank_AccNumber_Primary, Employee_Bank_AccName, Employee_Bank_AccNumber, Employee_CV, Employee_StartofEmployment, Employee_JoinDate, "
                                + "Employee_EndDate, Employee_Sum_LeaveBalance, Employee_Sum_SickLeave, Religion_ID, Department_ID, Division_ID, Position_ID, Employee_DateCreate, "
                                + "Employee_DateLastModified, Employee_Inactive, Employee_Inactive_Date, Employee_Inactive_Remarks)"
                                + "values('" + Cf.StrSql(nik.Text) + "','" /*Cf.StrSql(nama_lengkap.Text)*/ + Cf.StrSql(Full_Name) + "','" + Cf.StrSql(nama_depan.Text) + "','" + Cf.StrSql(nama_tengah.Text) + "','" + Cf.StrSql(nama_belakang.Text) + "','" + Cf.StrSql(nama_alias.Text) + "', "
                                + "'" + jenis_kelamin.SelectedItem.Value + "', '" + ddl_blood_type.SelectedValue + "', '" + Cf.StrSql(tempat_lahir.Text) + "','" + Birth_Conv + "','" + Cf.StrSql(no_hp1.Text) + "','" + Cf.StrSql(no_hp2.Text) + "','" + Cf.StrSql(no_ktp.Text) + "', "
                                + "'" + Cf.StrSql(alamat_ktp.Text) + "','" + Cf.StrSql(alamat_domisili.Text) + "','" + marital_status.SelectedValue + "','" + Cf.StrSql(nama_pasangan.Text) + "','" + sim_a + "','" + sim_c + "','" + Cf.StrSql(email_kantor.Text) + "', "
                                + "'" + Cf.StrSql(email_pribadi.Text) + "','" + last_education.SelectedValue + "','" + Cf.StrSql(jurusan.Text) + "','" + ddl_directSpv.SelectedItem.Value.ToString() + "','" + Cf.StrSql(in_the_name1.Text) + "', "
                                + "'" + Cf.StrSql(bank_account_1.Text) + "','" + Cf.StrSql(in_the_name2.Text) + "','" + Cf.StrSql(bank_account_2.Text) + "','" + cv_path + "','" + start_conv + "','" + Join_Conv + "', "
                                + "'" + Resign_Conv + "','0','" + sickleave + "','" + agama.SelectedValue + "','" + ddl_department.SelectedItem.Value.ToString() + "','" + ddl_division.SelectedItem.Value.ToString() + "','" + ddl_position.SelectedItem.Value.ToString() + "',getdate(), "
                                + "getdate(),'" + ddl_employee_status.SelectedItem.Value.ToString() + "', getdate(),'" + Cf.StrSql(remarks.Text) + "')";

                Db.Execute(insert);

                DataTable idMax = Db.Rs("select max(Employee_ID) as Employee_ID from TBL_Employee");

                //insert emergency contact

                if (Emergency_name1.Text != "" && Emergency_address1.Text != "" && Emergency_phone1.Text != "" && Emergency_relation1.Text != "")
                {
                    string insert_emergency_contact = "insert into TBL_Employee_Emergency_Contact (Emergency_Name, Emergency_Relation, Emergency_Address, Emergency_Phone, Employee_ID)"
                                                        + "values('" + Cf.StrSql(Emergency_name1.Text) + "','" + Cf.StrSql(Emergency_relation1.Text) + "','" + Cf.StrSql(Emergency_address1.Text) + "','" + Cf.StrSql(Emergency_phone1.Text) + "','" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                    Db.Execute(insert_emergency_contact);

                    if (Emergency_name2.Text != "" && Emergency_address2.Text != "" && Emergency_phone2.Text != "" && Emergency_relation2.Text != "")
                    {
                        string insert_emergency_contact1 = "insert into TBL_Employee_Emergency_Contact (Emergency_Name, Emergency_Relation, Emergency_Address, Emergency_Phone, Employee_ID)"
                                                        + "values('" + Cf.StrSql(Emergency_name2.Text) + "','" + Cf.StrSql(Emergency_relation2.Text) + "','" + Cf.StrSql(Emergency_address2.Text) + "','" + Cf.StrSql(Emergency_phone2.Text) + "','" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                        Db.Execute(insert_emergency_contact1);
                    }
                }

                //insert formal education
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
                                                        + "'" + Cf.StrSql(edu_6.Text) + "','" + Cf.StrSql(edu_name6.Text) + "','" + Cf.StrSql(edu_year6.Text) + "','" + Cf.StrSql(edu_location6.Text) + "','" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                    Db.Execute(insert_formal_education);
                }

                //insert working experience
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
                                                       + "'" + Cf.StrSql(work_exp_name5.Text) + "','" + Cf.StrSql(work_exp_position5.Text) + "','" + Cf.StrSql(work_exp_start5.Text) + "','" + Cf.StrSql(work_exp_end5.Text) + "','" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                    Db.Execute(insert_working_experience);
                }

                //insert dependent
                string insert_dependent = "";
                if (dep_name1.Text != "" && dep_placeofbirth1.Text != "" && dep_relationship1.Text != "" && dep_dateofbirth1.Text != "")
                {
                    insert_dependent = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                            + "values('" + Cf.StrSql(dep_name1.Text) + "','" + dep_gender1.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth1.Text) + "', '" + dep_date1_conv + "', '" + Cf.StrSql(dep_relationship1.Text) + "', '" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                    Db.Execute(insert_dependent);

                    if (dep_name2.Text != "" && dep_placeofbirth2.Text != "" && dep_relationship2.Text != "" && dep_dateofbirth2.Text != "")
                    {
                        insert_dependent = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                            + "values('" + Cf.StrSql(dep_name2.Text) + "','" + dep_gender2.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth2.Text) + "', '" + dep_date2_conv + "', '" + Cf.StrSql(dep_relationship2.Text) + "', '" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                        Db.Execute(insert_dependent);

                        if (dep_name3.Text != "" && dep_placeofbirth3.Text != "" && dep_relationship3.Text != "" && dep_dateofbirth3.Text != "")
                        {
                            insert_dependent = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                            + "values('" + Cf.StrSql(dep_name3.Text) + "','" + dep_gender3.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth3.Text) + "', '" + dep_date3_conv + "', '" + Cf.StrSql(dep_relationship3.Text) + "', '" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                            Db.Execute(insert_dependent);

                            if (dep_name4.Text != "" && dep_placeofbirth4.Text != "" && dep_relationship4.Text != "" && dep_dateofbirth4.Text != "")
                            {
                                insert_dependent = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                                + "values('" + Cf.StrSql(dep_name4.Text) + "','" + dep_gender4.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth4.Text) + "', '" + dep_date4_conv + "', '" + Cf.StrSql(dep_relationship4.Text) + "', '" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                                Db.Execute(insert_dependent);

                                if (dep_name5.Text != "" && dep_placeofbirth5.Text != "" && dep_relationship5.Text != "" && dep_dateofbirth5.Text != "")
                                {
                                    insert_dependent = "insert into TBL_Employee_Dependent (Dependent_Name, Dependent_Gender, Dependent_PlaceofBirth, Dependent_DateofBirth, Dependent_Relationship, Employee_ID) "
                                                    + "values('" + Cf.StrSql(dep_name5.Text) + "','" + dep_gender5.SelectedItem.Value + "','" + Cf.StrSql(dep_placeofbirth5.Text) + "', '" + dep_date5_conv + "', '" + Cf.StrSql(dep_relationship5.Text) + "', '" + idMax.Rows[0]["Employee_ID"].ToString() + "')";
                                    Db.Execute(insert_dependent);
                                }
                            }
                        }
                    }
                }

                //insert info payment / payroll
                string insert_info_payment = "insert into TBL_Employee_Payroll (Payroll_NPWP, Payroll_NPWP_Number, Payroll_OthersAllowance, Payroll_Jamsostek, "
                                                + "Payroll_BPJS_Pribadi, Payroll_BPJS_Pribadi_Value, Payroll_BPJS_Ditanggung, Payroll_SalaryPPH, Payroll_SalaryBPJS, "
                                                + "Payroll_Group, Employee_ID, PTKP_ID)"
                                                + "values('" + sts_npwp + "','" + Cf.StrSql(npwp_number.Text) + "','0','0', "
                                                + "'" + ddl_personal_bpjs.SelectedValue + "','" + Cf.StrSql(personal_bpjs_value.Text) + "','" + ddl_bpjs_ditanggung.SelectedValue + "','" + Cf.StrSql(salary_pph.Text) + "','" + Cf.StrSql(salary_bpjs.Text) + "', "
                                                + "'" + ddl_payroll_group.SelectedValue + "','" + idMax.Rows[0]["Employee_ID"].ToString() + "','" + ddl_status_pajak.SelectedValue + "')";
                Db.Execute(insert_info_payment);

                string encryptionPassword = Param.encryptionPassword;
                string encrypted = Crypto.Encrypt(idMax.Rows[0]["Employee_ID"].ToString(), encryptionPassword);

                //insert info payment / payroll history
                string insert_payment_history = "insert into TBL_HistorySalary (Employee_ID, HistorySalary_Value, HistorySalary_DateChanged)"
                                                + "values('" + encrypted + "', '" + current_salary.Text + "', getdate())";
                Db.Execute2(insert_payment_history);

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
                    string update = "update TBL_Employee set Employee_Photo = '" + get_foto + "' where Employee_ID = '" + idMax.Rows[0]["Employee_ID"].ToString() + "'";
                    Db.Execute(update);
                }
                Response.Redirect(Param.Path_Admin + "data-karyawan/add/?recent=" + idMax.Rows[0]["Employee_ID"].ToString());
            }
            else { Js.Alert(this, "Cek Periode Kontrak"); }
        }
        else { Js.Alert(this, "Email Kantor Sudah Ada!"); }
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
        DataTable rsa = Db.Rs("Select Division_ID, Division_Name from TBL_division Where Department_ID = " + Cf.Int(department) +" order by Division_Name asc");
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




    //////////Selected Index Change
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_division.Items.Clear();
        ddl_directSpv.Items.Clear();
        ddl_directSpv.Items.Add(new ListItem("-- Pilih Direct Supervisor --", ""));
        ddl_division.Items.Add(new ListItem("-- Pilih Divisi --", ""));
        ddl_DivisionPrev(ddl_department.SelectedValue);
        //DataTable selectDivision = Db.Rs("Select * From TBL_Division Where Department_ID = " + ddl_department.SelectedValue);
        //if (selectDivision.Rows.Count > 0)
        //{
        //    for (int i = 0; i < selectDivision.Rows.Count; i++)
        //    {
        //        ddl_division.Items.Add(new ListItem(selectDivision.Rows[i]["Division_Name"].ToString(), selectDivision.Rows[i]["Division_ID"].ToString()));
        //    }
        //}

        //DataTable selectSPV = Db.Rs("select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_ID != 1 and Department_ID = " + ddl_department.SelectedValue+" order by Employee_First_Name asc");
        //if (selectSPV.Rows.Count > 0)
        //{
        //    for (int i = 0; i < selectSPV.Rows.Count; i++)
        //    {
        //        ddl_directSpv.Items.Add(new ListItem(selectSPV.Rows[i]["Employee_First_Name"].ToString(), selectSPV.Rows[i]["Employee_ID"].ToString()));
        //    }
        //}
    }

    protected void ddl_division_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_directSpv.Items.Clear();
        ddl_directSpv.Items.Add(new ListItem("-- Pilih Direct Supervisor --", ""));
        ddl_directSpvPrev(ddl_division.SelectedValue);
        //DataTable selectSPV = Db.Rs("select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_ID != 1 and Division_ID = " + ddl_division.SelectedValue + " order by Employee_First_Name asc");
        //if (selectSPV.Rows.Count > 0)
        //{
        //    for (int i = 0; i < selectSPV.Rows.Count; i++)
        //    {
        //        ddl_directSpv.Items.Add(new ListItem(selectSPV.Rows[i]["Employee_First_Name"].ToString(), selectSPV.Rows[i]["Employee_ID"].ToString()));
        //    }
        //}
    }

    //Radio NPWP
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