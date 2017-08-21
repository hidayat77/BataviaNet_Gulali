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
using System.Data.SqlClient;
using ClosedXML.Excel;

public partial class _admin_laporan_karyawan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Karyawan", "view");

        fill("1", pagesum.Text, search_text.Text);
    }

    protected void fill(string num, string sum, string search)
    {
        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">No</th>"
                            + "<th>Nama</th>"
                            + "<th>Departemen</th>"
                            + "<th>Divisi</th>"
                            + "<th>Status</th>"
                        + "</tr>"
                    + "</thead>"
                    + "</tbody>");

        string sql_where = " where 1=1";
        if (search != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Full_Name") { sql_where += " and Employee_Full_Name like '%" + search + "%' or Employee_First_Name like '%" + search + "%' or Employee_Middle_Name like '%" + search + "%' or Employee_Last_Name like '%" + search + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Address") { sql_where += " and Employee_Domicile_Address like '%" + search + "%' or Employee_IDCard_Address like '%" + search + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Department_Name")
            {
                sql_where += " and c.Department_Name like '%" + search + "%' ";
            }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Division_Name")
            {
                sql_where += " and b.Division_Name like '%" + search + "%' ";
            }
        }

        DataTable rs = Db.Rs("select a.Employee_ID, a.Employee_Inactive, a.Employee_IDCard_Number, a.Employee_Full_Name, Employee_Last_Name, a.Employee_Company_Email, a.Employee_Personal_Email, b.Division_Name, c.Department_Name from TBL_Employee a left join TBL_Division b on a.Division_ID = b.Division_ID left join TBL_Department c on c.Department_ID = a.Department_ID  " + sql_where + " and Employee_ID != 1 order by Employee_Inactive,Employee_Full_Name asc ");

        lable.Text = "";
        table.Visible = true;
        if (rs.Rows.Count > 0)
        {
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0") { pagenum.Text = num; }
                else { pagenum.Text = of.ToString(); }
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
            if (d < rs.Rows.Count) { dari = d + 1; if (d == 0) dari = 1; }
            if (ea < rs.Rows.Count) { sampai = ea; }
            else { sampai = rs.Rows.Count; }
            if (dari > 0)
            {
                for (int i = dari - 1; i < sampai; i++)
                {
                    string id = rs.Rows[i]["Employee_ID"].ToString();

                    string status = rs.Rows[i]["Employee_Inactive"].ToString(), stat, style;
                    if (status == "2") { stat = "Inactive"; } else { stat = "Active"; }

                    if (status == "2") { style = "style=\"color:blue;font-weight:bold;\""; }
                    else { style = ""; }

                    x.Append("<tr " + style + ">"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td width=\"35%\"><a href=\"" + Param.Path_Admin + "data-karyawan/detail/?id=" + id + "\">" + Cf.Upper(rs.Rows[i]["Employee_Full_Name"].ToString()) + "</a></td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Department_Name"].ToString() + "</td>"
                                    + "<td style=\"width:20%\">" + rs.Rows[i]["Division_Name"].ToString() + "</td>"
                                    + "<td>" + stat + "</td>"
                        //+ "</td>"
                                + "</tr>");

                }
                x.Append("</tbody></table>");
                table.Text = x.ToString();
            }
        }
        else
        {
            lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>";
            table.Visible = false;
        }
    }


    protected void btn_search_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous2_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text, search_text.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        //if (a > 1)
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text, search_text.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text, search_text.Text);
    }

    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        string sql_where = "";
        if (search_text.Text != "")
        {
            if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Full_Name") { sql_where += " and A.Employee_Full_Name like '%" + search_text.Text + "%' or A.Employee_First_Name like '%" + search_text.Text + "%' or A.Employee_Middle_Name like '%" + search_text.Text + "%' or A.Employee_Last_Name like '%" + search_text.Text + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Employee_Address") { sql_where += " and A.Employee_Domicile_Address like '%" + search_text.Text + "%' or A.Employee_IDCard_Address like '%" + search_text.Text + "%' "; }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Department_Name")
            {
                sql_where += " and B.Department_Name like '%" + search_text.Text + "%' ";
            }
            else if (ddl_search.SelectedValue.ToString().Trim() == "Division_Name")
            {
                sql_where += " and C.Division_Name like '%" + search_text.Text + "%' ";
            }
        }
        string before = "", after = "";
        try { App.LogHistory("Generate : Laporan > Karyawan", before, after, sql_where); }
        catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

        string query = "Select "
            + " A.Employee_NIK as 'No. Induk Karyawan' ,"
            + " A.Employee_Full_Name as 'Nama Lengkap',"
            + " A.Employee_First_Name as 'Nama Depan',"
            + " A.Employee_Middle_Name as 'Nama Tengah',"
            + " A.Employee_Last_Name as 'Nama Belakang',"
            + " A.Employee_Alias_Name as 'Nama Alias',"
            + " IIF(A.Employee_Gender like '%L%', 'Male', IIF(A.Employee_Gender like '%P%', 'Female', '')) as 'Jenis Kelamin',"
            + " A.Employee_PlaceOfBirth as 'Tempat Lahir',"
            + " A.Employee_DateOfBirth as 'Tanggal Lahir',"
            + " E.Religion_Name_ID as 'Agama',"
            + " IIF(A.Employee_Blood_Type = 1, 'A', "
            + " IIF(A.Employee_Blood_Type = 2, 'B', "
            + " IIF(A.Employee_Blood_Type = 3, 'AB', "
            + " IIF(A.Employee_Blood_Type = 4, 'O', '')))) as 'Golongan Darah',"
            + " A.Employee_Phone_Number_Primary as 'No. Handphone 1 (WA)',"
            + " A.Employee_Phone_Number as 'No. Handphone 2',"
            + " A.Employee_IDCard_Number as 'No. Identitas (KTP)' ,"
            + " A.Employee_IDCard_Address as 'Alamat (Sesuai KTP)', "
            + " A.Employee_Domicile_Address as 'Alamat (Domisili)',"
            + " IIF(A.Employee_Marital_Status = 1, 'Belum Menikah', "
            + " IIF(A.Employee_Marital_Status = 2, 'Menikah', "
            + " IIF(A.Employee_Marital_Status = 3, 'Bercerai', ''))) as 'Status Pernikahan',"
            + " A.Employee_Spouse_Name as 'Nama Pasangan',"
            + " A.Employee_SIM_A as 'SIM A',"
            + " A.Employee_SIM_C as 'SIM C',"
            + " A.Employee_Company_Email as 'Email (Kantor)', "
            + " A.Employee_Personal_Email as 'Email (Pribadi)', "
            + " IIF(A.Employee_Education = 1, 'High School', "
            + " IIF(A.Employee_Education = 2, 'Bachelor Degree', "
            + " IIF(A.Employee_Education = 3, 'Master Degree', "
            + " IIF(A.Employee_Education = 4, 'Doctoral Degree', '')))) as 'Pendidikan Terakhir',"
            + " A.Employee_Education_Major as 'Jurusan', "
            + " B.Department_Name as 'Departemen',"
            + " C.Division_Name as 'Divisi', "
            + " H.Employee_Full_Name as 'Direct Supervisor',"
            + " D.Position_Name as 'Jabatan', "
            + " A.Employee_StartofEmployment as 'Mulai Bekerja', "
            + " A.Employee_JoinDate as 'Periode Kontrak (Dari)', "
            + " A.Employee_EndDate as 'Periode Kontrak (Sampai)', "
            + " IIF(A.Employee_Inactive like '1', 'Aktif', IIF(A.Employee_Inactive like '2', 'Tidak Aktif', '')) as 'Status Karyawan',"
            + " A.Employee_Bank_AccName_Primary as 'Rekening 1 (Atas Nama)', "
            + " A.Employee_Bank_AccNumber_Primary as 'Rekening 1 (No. Rekening)', "
            + " A.Employee_Bank_AccName as 'Rekening 2 (Atas Nama)', "
            + " A.Employee_Bank_AccNumber as 'Rekening 2 (No. Rekening)',"
            + " F.Payroll_NPWP as 'NPWP',"
            + " F.Payroll_NPWP_Number as 'No NPWP',"
            + " F.Payroll_BPJS_Pribadi as 'BPJS Pribadi',"
            + " F.Payroll_BPJS_Pribadi_Value as 'Jumlah BPJS Pribadi',"
            + " F.Payroll_BPJS_Ditanggung as 'Payroll BPJS Ditanggung',"
            + " F.Payroll_SalaryPPH as 'Gaji PPH',"
            + " F.Payroll_SalaryBPJS as 'Gaji BPJS',"
            + " I.Admin_Role as 'Grup Payroll',"
            + " G.PTKP_Description as 'Status Pajak',"
            + " IIF(DATEDIFF(day,A.Employee_JoinDate,getdate()) > 365 , DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365, '') as 'Tahun',"
            + " IIF((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) > 30 , (DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) / 30, '') as 'Bulan',"
            + " IIF((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) - (30 * ((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) / 30)) > 7 ,((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) - (30 * ((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) / 30)))/7, '') as 'Minggu',"
            + " (DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) - (30 * ((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) / 30)) "
            + " - (7 * (((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) - (30 * ((DATEDIFF(day,A.Employee_JoinDate,getdate()) - (365 * (DATEDIFF(day,A.Employee_JoinDate,getdate()) / 365))) / 30)))/7)) as 'Hari'"
            + " from TBL_Employee A"
            + " left join TBL_Department B on A.Department_ID = B.Department_ID"
            + " left join TBL_Division C on A.Division_ID = C.Division_ID"
            + " left join TBL_Position D on A.Position_ID = D.Position_ID"
            + " left join TBL_Religion E on A.Religion_ID = E.Religion_ID"
            + " left join TBL_Employee_Payroll F on A.Employee_ID = F.Employee_ID"
            + " left join " + Param.Db2 + ".dbo.TBL_PTKP G on G.PTKP_ID = F.PTKP_ID"
            + " left join TBL_Employee H on A.Employee_DirectSpv = H.Employee_ID"
            + " left join " + Param.Db2 + ".dbo.TBL_Payroll_Role I on F.Payroll_Group = I.Role_ID"
            + " where 1=1 " + sql_where + " and A.Employee_ID != 1 order by A.Employee_Full_Name asc";

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
                        ds.Tables[0].TableName = "Karyawan";

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
                            Response.AddHeader("content-disposition", "attachment;filename=Karyawan " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
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

}