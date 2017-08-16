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
using System.Globalization;
using System.Diagnostics;
using System.Security.Cryptography;

public partial class _admin_setup_general : System.Web.UI.Page
{
    protected string status { get { return App.GetStr(this, "status"); } }
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> General", "view");

        tab_datakaryawan.Text = "<a href=\"" + Param.Path_Admin + "setup/import/data-karyawan/\">Data Karyawan</a>";
        tab_absensi.Text = "<a href=\"" + Param.Path_Admin + "setup/import/absensi/\">Absensi</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        link_download_template.Text = "<a class=\"btn btn-primary\" href=\"" + Param.Path_Admin + "setup/import/data-karyawan/template.xlsx\">Download Template</a>";

        if (!IsPostBack)
        {
            import.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            save.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            clean.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            clean.Visible = false;
            save.Visible = false;
            total1.Visible = false;
            total2.Visible = false;
            feed.Text = "";
            view("1", pagesum.Text);
        }

    }
    protected void view(string num, string sum)
    {
        //try
        //{
        DataTable data = Db.Rs("select Temp_ID, User_uploader, Employee_Name, Employee_BCA_AccountNumber, Employee_NPWP_Number, NPWP, PTKP_Status, C.Department_Name, B.Division_Name, D.Admin_Role, Employee_Religion, Employee_BirthDate, Employee_JoinDate, Employee_DateCreate, Employee_DateLastModified, Employee_Inactive, Employee_Gender, Employee_Blood_Type, Employee_NPP_BPJS_Ketenagakerjaan, Employee_NPP_BPJS_Kesehatan, HistorySalary_Value, HistorySalary_DateChanged,salaryPPH,salaryBPJS,salaryBPJSTK, BPJS_Pribadi, BPJS_Pribadi_Value from TBL_Employee_Temp A left join TBL_Division B on A.Employee_Division = B.Division_ID left join TBL_Department C on A.Employee_Department = C.Department_ID left join " + Param.Db2 + ".dbo.TBL_Payroll_Role D ON A.Payroll_Group = D.Role_ID where User_uploader ='" + Cf.StrSql(App.Employee_ID) + "' order by Employee_Name ASC");
        StringBuilder x = new StringBuilder();
        if (data.Rows.Count > 0)
        {
            //App.Failed = "";
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(data.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0")
                {
                    pagenum.Text = num;
                }
                else
                {
                    pagenum.Text = of.ToString();
                }
                pagesum.Text = sum;
            }
            else
            {
            }
            int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
            int per = Cf.Int(pagesum.Text);
            int ke = Cf.Int(pagenum.Text);
            c = ke - 1;
            d = c * per;
            ea = ke * per;


            count_page.Text = of.ToString();
            if (d < data.Rows.Count)
            {
                dari = d + 1;
                if (d == 0) dari = 1;
            }
            if (ea < data.Rows.Count)
            {
                sampai = ea;
            }
            else
            {
                sampai = data.Rows.Count;
            }
            if (dari > 0)
            {
                pagging.Visible = true;
                x.Append("<table style=\"padding:2px; border-collapse: collapse; border:1px solid #000; \" ><tr>"
                + "<td  style=\"padding:2px 8px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">No</td>"
                + "<td  style=\"padding:2px 8px; vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Nama Karyawan</td>"
                + "<td  style=\"padding:2px 8px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Rekening Utama</td>"
                + "<td  style=\" padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Nomor NPWP</td>"
                + "<td   style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Status NPWP</td>"
                + "<td   style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Status PTKP</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Departemen</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Divisi</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Payroll Group</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Agama</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Tanggal Lahir</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Tanggal Bergabung</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Jenis Kelamin</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Golongan Darah</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">NPP BPJS Ketenagakerjaan</td>"
                + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">NPP BPJS Kesehatan</td>"
                  + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Basic Salary</td>"
                    + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Salary Date Changed</td>"
                      + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Gaji PPH</td>"
                        + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Gaji BPJS Kesehatan</td>"
                          + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">Gaji BPJS Ketenagakerjaan</td>"
                           + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">BPJS Pribadi</td>"
                          + "<td  style=\"padding:2px 10px;vertical-align:middle;background: #033363;font-family: 'Helvetica';font-size: 14px;border:1px solid #fff;color:#fff;\">BPJS Pribadi Value</td>"
                + "</tr>");

                for (int a = dari - 1; a < sampai; a++)
                {
                    //religion
                    string religion = data.Rows[a]["Employee_Religion"].ToString();
                    string agama;
                    if (religion.Equals("1")) { agama = "Muslim"; }
                    else if (religion.Equals("2")) { agama = "Kristen"; }
                    else if (religion.Equals("3")) { agama = "Katholik"; }
                    else if (religion.Equals("4")) { agama = "Budha"; }
                    else if (religion.Equals("5")) { agama = "Hindu"; }
                    else if (religion.Equals("6")) { agama = "Konghucu"; }
                    else { agama = "Lainnya"; }

                    //gender
                    string gender = data.Rows[a]["Employee_Gender"].ToString();
                    string jenis_kelamin;
                    if (gender.Equals("L")) { jenis_kelamin = "Laki-Laki"; }
                    else if (gender.Equals("P")) { jenis_kelamin = "Perempuan"; }
                    else { jenis_kelamin = ""; }

                    //blood type
                    string blood_type = data.Rows[a]["Employee_Blood_Type"].ToString();
                    string golongan_darah;
                    if (blood_type.Equals("1")) { golongan_darah = "A"; }
                    else if (blood_type.Equals("2")) { golongan_darah = "B"; }
                    else if (blood_type.Equals("3")) { golongan_darah = "AB"; }
                    else if (blood_type.Equals("4")) { golongan_darah = "O"; }
                    else { golongan_darah = ""; }

                    //string NPWPformat = String.Format("{0:##.###.###.#-###.###}", data.Rows[a]["NPWP"].ToString());

                    x.Append("<tr>"
                        + "<td  style=\"text-align:center;border:1px solid #000;\">" + (a + 1) + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["Employee_Name"].ToString() + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["Employee_BCA_AccountNumber"].ToString() + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:120px;\">" + data.Rows[a]["Employee_NPWP_Number"].ToString() + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:70px;\">" + data.Rows[a]["NPWP"].ToString() + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:50px;\">" + data.Rows[a]["PTKP_Status"].ToString() + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["Department_Name"].ToString() + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:140px;\">" + data.Rows[a]["Division_Name"].ToString() + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000; \"><div style=\"width:200px;\">" + data.Rows[a]["Admin_Role"].ToString() + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + agama + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:80px;\">" + DateTime.Parse(data.Rows[a]["Employee_BirthDate"].ToString()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\"><div style=\"width:80px;\">" + DateTime.Parse(data.Rows[a]["Employee_JoinDate"].ToString()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + "</div></td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + jenis_kelamin + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + golongan_darah + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["Employee_NPP_BPJS_Ketenagakerjaan"].ToString() + "</td>"
                        + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["Employee_NPP_BPJS_Kesehatan"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000; text-align:right;\">" + data.Rows[a]["HistorySalary_Value"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000;\">" + DateTime.Parse(data.Rows[a]["HistorySalary_DateChanged"].ToString()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000; text-align:right;\">" + data.Rows[a]["salaryPPH"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000; text-align:right;\">" + data.Rows[a]["salaryBPJS"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000; text-align:right;\">" + data.Rows[a]["salaryBPJSTK"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000;\">" + data.Rows[a]["BPJS_Pribadi"].ToString() + "</td>"
                         + "<td  style=\"padding:2px;border:1px solid #000; text-align:right;\">" + data.Rows[a]["BPJS_Pribadi_Value"].ToString() + "</td>"
                        + "</tr>");
                }


                x.Append("</table><br/>");

                //y.Append("</table>");
                //Literal tabel_y = new Literal();
                //tabel_y.Text=y.ToString();
                //PH_tr.Controls.Add(tabel_y);
                table.Text = x.ToString();
                clean.Visible = true;
                save.Visible = true;
                import.Visible = false;
                back.Visible = false;
                file.Disabled = true;
                total1.Visible = true;
                total2.Visible = true;

                total2.Text = data.Rows.Count.ToString();
            }
            // page_total.Text = Cf.Int(page_num.Text);
        }
        else { file.Disabled = false; }

        if (!string.IsNullOrEmpty(App.Failed))
        {
            fail.Text = App.Failed;
        }
        else
        {
            fail.Text = "";
        }
        //} catch (Exception ex) { }

    }
    protected void save_Click(object sender, System.EventArgs e)
    {
        DataTable read = Db.Rs("select * from TBL_Employee_Temp where User_uploader = '" + Cf.StrSql(App.Employee_ID) + "'");
        if (read.Rows.Count > 0)
        {
            for (int c = 0; c < read.Rows.Count; c++)
            {
                int value = 0;
                DataTable check_employe = Db.Rs("select max(Employee_ID) as maks from TBL_Employee");
                for (int a = 0; a < check_employe.Rows.Count; a++)
                {
                    if (!string.IsNullOrEmpty(check_employe.Rows[a]["maks"].ToString()))
                    {
                        value = Convert.ToInt32(check_employe.Rows[a]["maks"].ToString()) + 1;
                    }
                    else { value = 1; }

                    DateTime birth = Convert.ToDateTime(read.Rows[c]["Employee_BirthDate"].ToString());
                    string birth_date = birth.ToString("yyyy-MM-dd").ToString();

                    DateTime join = Convert.ToDateTime(read.Rows[c]["Employee_JoinDate"].ToString());
                    DateTime end = join.AddYears(1);
                    string join_date = join.ToString("yyyy-MM-dd").ToString();
                    string end_date = end.ToString("yyyy-MM-dd").ToString();
                    //string saves = "Insert into " + Param.Db + ".dbo.TBL_Employee (Employee_Name, Employee_BCA_AccountNumber, Employee_NPWP_Number, NPWP, PTKP_Status, Employee_Department, Employee_Division, Payroll_Group, Employee_Religion, Employee_BirthDate,  Employee_DateCreate, Employee_DateLastModified, Employee_JoinDate, Employee_Authorized_Date, Employee_Authorized, Employee_Gender, Employee_Blood_Type,Employee_NPP_BPJS_Ketenagakerjaan, Employee_NPP_BPJS_Kesehatan, salaryPPH,salaryBPJS,salaryBPJSTK, BPJS_Pribadi, BPJS_Pribadi_Value, Employee_StartofEmployment ,Employee_EndDate) values ('" + read.Rows[c]["Employee_Name"].ToString() + "','" + read.Rows[c]["Employee_BCA_AccountNumber"].ToString() + "','" + read.Rows[c]["Employee_NPWP_Number"].ToString() + "','" + read.Rows[c]["NPWP"].ToString() + "','" + read.Rows[c]["PTKP_Status"].ToString() + "','" + read.Rows[c]["Employee_Department"].ToString() + "','" + read.Rows[c]["Employee_Division"].ToString() + "','" + read.Rows[c]["Payroll_Group"].ToString() + "','" + read.Rows[c]["Employee_Religion"].ToString() + "','" + birth_date + "',getdate(),getdate(),'" + join_date + "',getdate(),'A','" + read.Rows[c]["Employee_Gender"].ToString() + "','" + read.Rows[c]["Employee_Blood_Type"].ToString() + "', '" + read.Rows[c]["Employee_NPP_BPJS_Ketenagakerjaan"].ToString() + "', '" + read.Rows[c]["Employee_NPP_BPJS_Kesehatan"].ToString() + "','" + read.Rows[c]["salaryPPH"].ToString().Replace(",", ".") + "','" + read.Rows[c]["salaryBPJS"].ToString().Replace(",", ".") + "','" + read.Rows[c]["salaryBPJSTK"].ToString().Replace(",", ".") + "','" + read.Rows[c]["BPJS_Pribadi"].ToString().Replace(",", ".") + "','" + read.Rows[c]["BPJS_Pribadi_Value"].ToString().Replace(",", ".") + "','" + join_date + "','" + end_date + "') ";

                    //SAVE TBL_EMPLOYEE
                    string save = "Insert into " + Param.Db + ".dbo.TBL_Employee (Employee_Full_Name, Employee_Bank_AccNumber_Primary, Department_ID, Division_ID, Religion_ID, Employee_DateOfBirth,  Employee_DateCreate, Employee_DateLastModified, Employee_JoinDate, Employee_Inactive_Date, Employee_Inactive, Employee_Gender, Employee_Blood_Type, Employee_StartofEmployment ,Employee_EndDate) values ('" + read.Rows[c]["Employee_Name"].ToString() + "','" + read.Rows[c]["Employee_BCA_AccountNumber"].ToString() + "','" + read.Rows[c]["Employee_Department"].ToString() + "','" + read.Rows[c]["Employee_Division"].ToString() + "','" + read.Rows[c]["Employee_Religion"].ToString() + "','" + birth_date + "',getdate(),getdate(),'" + join_date + "',getdate(),'1','" + read.Rows[c]["Employee_Gender"].ToString() + "','" + read.Rows[c]["Employee_Blood_Type"].ToString() + "','" + join_date + "','" + end_date + "') ";

                    //SAVE TBL_EMPLOYEE_PAYROLL
                    save += " insert into " + Param.Db + ".dbo.TBL_Employee_Payroll (Employee_ID, Payroll_NPWP_Number, Payroll_NPWP, PTKP_ID, Payroll_Group, Payroll_SalaryPPH, Payroll_SalaryBPJS, Payroll_SalaryBPJSTK, Payroll_BPJS_Pribadi, Payroll_BPJS_Pribadi_Value)"
                             + "values(" + value + ", '" + read.Rows[c]["Employee_NPWP_Number"].ToString().Replace(",", ".") + "' , '1','1','" + read.Rows[c]["Payroll_Group"].ToString().Replace(",", ".") + "','" + read.Rows[c]["salaryPPH"].ToString().Replace(",", ".") + "','" + read.Rows[c]["salaryBPJS"].ToString().Replace(",", ".") + "','" + read.Rows[c]["salaryBPJSTK"].ToString().Replace(",", ".") + "','1','" + read.Rows[c]["BPJS_Pribadi_Value"].ToString().Replace(",", ".") + "')";

                    DateTime Join = Convert.ToDateTime(join_date);


                    save += " insert into TBL_HistorySalary (Employee_ID, HistorySalary_Value, HistorySalary_DateChanged,HistorySalary_Description)"
                             + "values(SCOPE_IDENTITY(), '" + read.Rows[c]["HistorySalary_Value"].ToString().Replace(",", ".") + "' , '" + Join.ToString("yyyy-MM-dd") + "','Hasil Import')";

                    //Response.Redirect(saves);

                    Db.Execute2(save);

                    // GANTI  UPDATE
                    string update = " ", idCek = "";
                    DataTable list = Db.Rs2("select * from TBL_HistorySalary where ISNUMERIC([Employee_ID]) = 1");
                    for (int d = 0; d < list.Rows.Count; d++)
                    {
                        idCek = list.Rows[d]["Employee_id"].ToString();
                        DataTable cek = Db.Rs("select * from TBL_Employee where Employee_id=" + idCek + "");
                        if (cek.Rows.Count > 0)
                        {


                            string encryptionPassword = "BatavianetICCTFHRISpass@word1";
                            string id_emp = Cf.StrSql(idCek);
                            string encrypted = Crypto.Encrypt(id_emp, encryptionPassword);
                            string original = Crypto.Decrypt(encrypted, encryptionPassword);
                            update += " update TBL_HistorySalary set Employee_ID='" + encrypted + "' where Employee_ID='" + original + "' ";
                        }
                    }
                    Db.Execute2(update);



                    string clear_temporary = "delete from TBL_Employee_Temp where Temp_ID = '" + read.Rows[c]["Temp_ID"].ToString() + "'";
                    Db.Execute(clear_temporary);

                    //Response.Redirect(saves);
                }
            }
            Response.Redirect("../data-karyawan/");
        }
    }

    protected void clean_Click(object sender, System.EventArgs e)
    {
        string clean = "Delete from TBL_Employee_Temp where User_uploader = '" + Cf.StrSql(App.Employee_ID) + "'";
        Db.Execute(clean);

        App.Failed = "";

        Response.Redirect("../data-karyawan/");
        //view();
    }

    protected void back_Click(object sender, System.EventArgs e)
    { Response.Redirect("../../"); }

    protected void import_Click(object sender, System.EventArgs e)
    {
        if (!file.PostedFile.FileName.EndsWith(".xlsx"))
        {
            Js.Alert(this, "Proses Upload Gagal.");
        }
        else
        {
            string path = Request.PhysicalApplicationPath
                + "FileUpload\\Excel\\Absen_" + Session.SessionID + ".xlsx";

            Dfc.UploadFile(".xlsx", path, file);

            Cek(path);

            //Hapus file sementara tersebut dari hard-disk server
            Dfc.DeleteFile(path);
        }
    }

    private void Cek(string path)
    {
        string strSql = "SELECT * FROM [Sheet1$]";
        DataTable rs = new DataTable();

        rs = Db.xls(strSql, path);

        if (Rpt.ValidateXls(rs, gagal))
        {
            h2StatusUpload.Visible = true;
            Save(path);
        }
    }

    private void Save(string path)
    {
        int total = 0;

        string strSql = "SELECT * FROM [Sheet1$]";
        DataTable rs = Db.xls(strSql, path);

        for (int i = 0; i < rs.Rows.Count; i++)
        {
            if (!Response.IsClientConnected) break;

            if (Save(rs, i))
            {
                total++;
            }
        }

        feed.Text = "Upload Berhasil";
        if (feed.Text == "Upload Berhasil")
        {
            view("1", pagesum.Text);
        }

    }

    private bool Save(DataTable rs, int i)
    {
        string first_name = Cf.Lower(Cf.Str(rs.Rows[i][0]));
        string bca_account_number = Cf.Str(rs.Rows[i][1]);
        string npwp_number = Cf.Str(rs.Rows[i][2]);
        string status_npwp = Cf.Str(rs.Rows[i][3]);
        string status_ptkp = Cf.Str(rs.Rows[i][4]);
        string department_id = Cf.Str(rs.Rows[i][5]);
        string division_id = Cf.Str(rs.Rows[i][6]);
        string payroll_group_id = Cf.Str(rs.Rows[i][7]);
        string religion = Cf.Str(rs.Rows[i][8]);

        //religion
        //string religion = "";
        //int relig = Convert.ToInt32(Cf.Str(rs.Rows[i][5]));
        //if ((relig < 6))
        //{ religion = relig.ToString(); }
        //else { religion = ""; }

        string birth_date = "";
        if (!string.IsNullOrEmpty(rs.Rows[i][9].ToString()))
        {
            DateTime date = Convert.ToDateTime(rs.Rows[i][9].ToString());
            birth_date = date.ToString("yyyy-MM-dd").ToString();
        }

        string join_date = "";
        if (!string.IsNullOrEmpty(rs.Rows[i][10].ToString()))
        {
            DateTime date = Convert.ToDateTime(rs.Rows[i][10].ToString());
            join_date = date.ToString("yyyy-MM-dd").ToString();
        }

        string gender = Cf.Str(rs.Rows[i][11]);
        string blood_type = Cf.Str(rs.Rows[i][12]);
        string bpjs_ketenagakerjaan = Cf.Str(rs.Rows[i][13]);
        string bpjs_kesehatan = Cf.Str(rs.Rows[i][14]);



        string HistorySalary_DateChanged = "", HistorySalary_Value = "0", salaryPPH = "0";
        if (!string.IsNullOrEmpty(rs.Rows[i][15].ToString()))
        { HistorySalary_Value = Cf.Str(rs.Rows[i][15]); }

        if (!string.IsNullOrEmpty(rs.Rows[i][16].ToString()))
        {
            DateTime date = Convert.ToDateTime(rs.Rows[i][16].ToString());
            HistorySalary_DateChanged = date.ToString("yyyy-MM-dd").ToString();
        }
        if (!string.IsNullOrEmpty(rs.Rows[i][17].ToString()))
        {
            salaryPPH = Cf.Str(rs.Rows[i][17]);
        }

        string salaryBPJS = Cf.Str(rs.Rows[i][18]);
        string salaryBPJSTK = Cf.Str(rs.Rows[i][19]);
        string BPJS_Pribadi = rs.Rows[i][20].ToString();
        string BPJS_Pribadi_Value = "0";

        if (!string.IsNullOrEmpty(rs.Rows[i][21].ToString()))
        {
            BPJS_Pribadi_Value = Cf.Str(rs.Rows[i][21]);
        }

        bool x = false;
        int value = 0;
        DataTable check_temp = Db.Rs("select max(Temp_id) as maks from TBL_Employee_Temp where User_uploader ='" + Cf.StrSql(App.Employee_ID) + "'");
        if (!string.IsNullOrEmpty(check_temp.Rows[0]["maks"].ToString()))
        {
            value = Convert.ToInt32(check_temp.Rows[0]["maks"].ToString()) + 1;
        }
        else { value = 1; }
        try
        {
            //convert date
            //string date_of_birth = "";
            //if (!string.IsNullOrEmpty(rs.Rows[i][4].ToString()))
            //{
            //    string test = rs.Rows[i][4].ToString();
            //    DateTime datez = Convert.ToDateTime(test);
            //    date_of_birth = datez.ToString("yyyy-MM-dd").ToString();
            //}
            //if (!string.IsNullOrEmpty(religion))
            //{
            string insert = "Insert into TBL_Employee_Temp(User_uploader,Employee_Name, Employee_BCA_AccountNumber, Employee_NPWP_Number, NPWP, PTKP_Status, Employee_Department, Employee_Division, Payroll_Group, Employee_Religion, Employee_BirthDate, Employee_JoinDate, Employee_DateCreate, Employee_DateLastModified, Employee_Inactive, Employee_Gender, Employee_Blood_Type, Employee_NPP_BPJS_Ketenagakerjaan, Employee_NPP_BPJS_Kesehatan, Temp_id, HistorySalary_Value,HistorySalary_DateChanged,salaryPPH,salaryBPJS,salaryBPJSTK, BPJS_Pribadi, BPJS_Pribadi_Value)"
            + " values('" + App.Employee_ID + "','" + first_name + "','" + bca_account_number + "','" + npwp_number + "','" + status_npwp + "','" + status_ptkp + "','" + department_id + "','" + division_id + "','" + payroll_group_id + "','" + religion + "','" + birth_date + "','" + join_date + "',getdate(),getdate(),'" + status + "','" + gender + "','" + blood_type + "','" + bpjs_ketenagakerjaan + "','" + bpjs_kesehatan + "','" + value + "' ,'" + HistorySalary_Value + "','" + HistorySalary_DateChanged + "','" + salaryPPH + "','" + salaryBPJS + "','" + salaryBPJSTK + "','" + BPJS_Pribadi + "','" + BPJS_Pribadi_Value + "')";
            //Response.Redirect(insert);
            Db.Execute(insert);
            x = false;
            // }else { Js.Alert(this, "Fail"); x = true; }
        }
        catch (Exception ex)
        {
            App.Failed += first_name + ", ";
        }
        return x;

    }

    protected void previous2_Click(object sender, EventArgs e)
    {
        view("1", pagesum.Text);
    }

    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        view(a.ToString(), pagesum.Text);
    }

    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        view(a.ToString(), pagesum.Text);
    }

    protected void next2_Click(object sender, EventArgs e)
    {
        view("0", pagesum.Text);
    }

    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        view(a.ToString(), pagesum.Text);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        view("1", pagesum.Text);
    }

}