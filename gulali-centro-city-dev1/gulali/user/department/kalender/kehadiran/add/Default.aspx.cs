using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Kehadiran", "Insert");

        BtnClear.OnClientClick = string.Format("return confirm('Apakah Anda Yakin Ingin Membersihkan Data ? ');", "kodeee");
        BtnSave.OnClientClick = string.Format("return confirm('Apakah Anda Yakin Ingin Menyimpan Data ? Pastikan data yang di Input Sudah Lengkap');", "kodeee");

        link_href.Text = "<a href=\"" + Param.Path_User + "department/kalender/kehadiran/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            ddl_employee_show(); ddl_jadwal_show(); legend();
            //fill("1", pagesum.Text, search_text.Text); 
        }
    }
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isSelected(ddl_employee) ? x : false;
            x = Fv.isSelected(ddl_jadwal) ? x : false;
            //x = Fv.isComplete(tanggal) ? x : false;
            //x = Fv.isComplete(tanggal_mulai) ? x : false;
            //x = Fv.isComplete(tanggal_selesai) ? x : false;
            return x;
        }
    }
    protected void ddl_employee_show()
    {
        DataTable rsa = Db.Rs("Select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_DirectSpv = '" + App.Employee_ID + "' order by Employee_Full_Name asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_employee.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
        }
    }
    protected void ddl_jadwal_show()
    {
        DataTable rsa = Db.Rs("Select Schedule_Role_ID, Schedule_Role_Nama from TBL_Schedule_Role where Schedule_Role_Status = '1' order by Schedule_Role_Nama asc");
        for (int i = 0; i < rsa.Rows.Count; i++)
        {
            ddl_jadwal.Items.Add(new ListItem(rsa.Rows[i]["Schedule_Role_Nama"].ToString(), rsa.Rows[i]["Schedule_Role_ID"].ToString()));
        }
    }
    protected void radio_1(object sender, EventArgs e)
    {
        div_tgl_perperiode.Visible = false;
        div_tgl_perhari.Visible = true;
        tanggal.Text = "";
        tanggal_mulai.Text = "";
        tanggal_selesai.Text = "";
        update.Update();
    }
    protected void radio_2(object sender, EventArgs e)
    {
        div_tgl_perperiode.Visible = true;
        div_tgl_perhari.Visible = false;
        tanggal.Text = "";
        tanggal_mulai.Text = "";
        tanggal_selesai.Text = "";
        //npwp_number.Text = "00.000.000.0-000.000";
        update.Update();
    }
    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                //DataTable chn = Db.Rs("Select Schedule_Temp_ID, Employee_ID, Schedule_Role_ID, Schedule_Temp_Date, Schedule_Temp_CreateDate, Schedule_Temp_CreateBy from TBL_Schedule_Kalendar_Temp where Schedule_Role_Status = 1 AND (Schedule_Role_Code = '" + Cf.StrSql(Kode_Kehadiran.Text) + "' or Schedule_Role_Nama = '" + Cf.StrSql(nama_kehadiran.Text) + "')");
                //if (chn.Rows.Count > 0)
                //{
                //    return;
                //}
                //else
                //{
                if (radio_hari.Checked)
                {
                    DateTime tgl = Convert.ToDateTime(tanggal.Text);
                    string date = tgl.ToString("yyyy-MM-dd");

                    string insert = "Insert into TBL_Schedule_Kalendar_Temp(Employee_ID, Schedule_Role_ID, Schedule_Temp_Date, Schedule_Temp_CreateDate, Schedule_Temp_CreateBy) values('" + Cf.Int(ddl_employee.SelectedValue) + "','" + Cf.Int(ddl_jadwal.SelectedValue) + "','" + date + "',getdate(),'" + App.Employee_ID + "')";
                    Db.Execute(insert);
                }
                else if (radio_periode.Checked) 
                {
                    DateTime tglmulai = Convert.ToDateTime(tanggal_mulai.Text);
                    DateTime tglselesai = Convert.ToDateTime(tanggal_selesai.Text);

                    for (var i = tglmulai; i <= tglselesai; i=i.AddDays(1))
                    {

                        //if (i.DayOfWeek==DayOfWeek.Saturday)
                        //{
                        //    continue;
                        //}
                        string insert = "Insert into TBL_Schedule_Kalendar_Temp(Employee_ID, Schedule_Role_ID, Schedule_Temp_Date, Schedule_Temp_CreateDate, Schedule_Temp_CreateBy) values('" + Cf.Int(ddl_employee.SelectedValue) + "','" + Cf.Int(ddl_jadwal.SelectedValue) + "','" + i.ToString("yyyy-MM-dd") + "',getdate(),'" + App.Employee_ID + "')";
                        
                        
                        Db.Execute(insert);
                    }
                }
                    Response.Redirect(Param.Path_User + "department/kalender/kehadiran/add/");
                //}
            }
        }
    }
    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect(Param.Path_User + "/department/kalender/kehadiran/"); }
    protected void ddl_jadwal_SelectedTextChanged(object sender, EventArgs e)
    {
        if (ddl_jadwal.SelectedValue == "")
        {
            txt_masuk.Text = "";
            txt_keluar.Text = "";
        }
        else
        {
            DataTable rsa = Db.Rs("Select Schedule_Role_ID, Schedule_Role_JamMasuk, Schedule_Role_JamKeluar from TBL_Schedule_Role where Schedule_Role_Status = '1' and Schedule_Role_ID = '" + Cf.Int(ddl_jadwal.SelectedValue) + "' order by Schedule_Role_Nama asc");

            txt_masuk.Text = rsa.Rows[0]["Schedule_Role_JamMasuk"].ToString();
            txt_keluar.Text = rsa.Rows[0]["Schedule_Role_JamKeluar"].ToString();
        }
    }


    protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
    {
        DataTable rs = Db.Rs("SELECT CONVERT(VARCHAR(10),Absen_Date ,105) as Absen_Date, CONVERT(VARCHAR(8), Absen_In,108) as Absen_In, CONVERT(VARCHAR(8), Absen_Out,108) as Absen_Out FROM TBL_Absen ha join TBL_Employee he on upper((CASE WHEN 0 = CHARINDEX(' ', Absen_Employee) then Absen_Employee ELSE SUBSTRING(Absen_Employee, 1, CHARINDEX(' ', Absen_Employee)) end)) = upper((CASE WHEN 0 = CHARINDEX(' ', Employee_Full_Name) then Employee_Full_Name ELSE SUBSTRING(Employee_Full_Name, 1, CHARINDEX(' ', Employee_Full_Name)) end)) where Employee_ID = '" + Cf.StrSql(App.Employee_ID) + "'");

        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                if (rs.Rows[i]["Absen_Date"].ToString() == e.Day.Date.ToString("dd-MM-yyyy").ToString() || rs.Rows[i]["Absen_Out"].ToString() == e.Day.Date.ToString("dd-MM-yyyy").ToString())
                {
                    Literal lineBreak = new Literal();
                    lineBreak.Text = "<br/>";
                    e.Cell.Controls.Add(lineBreak);
                    //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f3f3f3");

                    Label b = new Label();
                    b.Font.Size = 8;
                    b.Font.Bold = true;
                    b.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff8acc");
                    string output = "";
                    if (rs.Rows[i]["Absen_Date"].ToString() != "") { output = "IN : " + rs.Rows[i]["Absen_In"].ToString(); }
                    if (rs.Rows[i]["Absen_Date"].ToString() != "" && output != "") { output += "<br />OUT : " + rs.Rows[i]["Absen_Out"].ToString(); }
                    else if (rs.Rows[i]["Absen_Date"].ToString() != "" && output == "") { output = "OUT : " + rs.Rows[i]["Absen_Out"].ToString(); }
                    b.Text = output.ToString();
                    e.Cell.Controls.Add(b);
                }
            }
        }

        DataTable holiday = Db.Rs("Select CONVERT(VARCHAR(10),Holiday_Date ,105) as Holiday_Date, Holiday_List_Name from TBL_Holiday a join TBL_Holiday_List b on a.Holiday_List_ID = b.Holiday_List_ID");
        if (holiday.Rows.Count > 0)
        {
            for (int i = 0; i < holiday.Rows.Count; i++)
            {
                if (holiday.Rows[i]["Holiday_Date"].ToString() == e.Day.Date.ToString("dd-MM-yyyy").ToString())
                {
                    Literal lineBreak = new Literal();
                    lineBreak.Text = "<BR />";
                    e.Cell.Controls.Add(lineBreak);
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff5b5b");

                    Label b = new Label();
                    b.Font.Size = 8;
                    b.Font.Bold = true;
                    b.ForeColor = System.Drawing.ColorTranslator.FromHtml("white");
                    string output = "- " + holiday.Rows[i]["Holiday_List_Name"].ToString();
                    b.Text = output.ToString();
                    e.Cell.Controls.Add(b);
                }
            }
        }

        //Show Schedule where Karyawan
        string where_employee_id;
        if (ddl_employee.SelectedValue == "")
        {
            where_employee_id = "";
        }
        else
        {
            where_employee_id = " And B.Employee_ID = '" + ddl_employee.SelectedValue + "'";
        }

        //Show Schedule where Jadwal
        string where_jadwal;
        if (ddl_jadwal.SelectedValue == "")
        {
            where_jadwal = "";
        }
        else
        {
            where_jadwal = " And Schedule_Role_ID = '" + ddl_jadwal.SelectedValue + "'";
        }

        DataTable Schedule = Db.Rs("Select Distinct CONVERT(VARCHAR(10),Schedule_Temp_Date ,105) as Schedule_Temp_Date, Schedule_Role_ID from TBL_Schedule_Kalendar_Temp A left join TBL_Employee B on A.Employee_ID = B.Employee_ID where B.Employee_DirectSpv = '" + App.Employee_ID + "' " + where_employee_id + "" + where_jadwal + "");

        if (Schedule.Rows.Count > 0)
        {
            for (int i = 0; i < Schedule.Rows.Count; i++)
            {
                if (Schedule.Rows[i]["Schedule_Temp_Date"].ToString() == e.Day.Date.ToString("dd-MM-yyyy").ToString())
                {
                    Literal lineBreaks = new Literal();
                    lineBreaks.Text = "<BR />";
                    e.Cell.Controls.Add(lineBreaks);

                    Label bs = new Label();
                    bs.Font.Size = 8;
                    bs.Font.Bold = true;
                    bs.ForeColor = System.Drawing.ColorTranslator.FromHtml("white");

                    DateTime tanggal_sekarang = DateTime.Now.AddDays(-1);
                    DateTime tanggal_schedule = Convert.ToDateTime(Schedule.Rows[i]["Schedule_Temp_Date"].ToString());

                    string outputs = "";
                    string tanggal = tanggal_schedule.ToString("dd-MM-yy");

                    if (tanggal_schedule < tanggal_sekarang)
                    {
                        e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#d8d8d8");
                    }

                    // check warna shift
                    DataTable Schedule_warna_shift = Db.Rs("Select Schedule_Role_Color from TBL_Schedule_Role where Schedule_Role_ID = '" + Schedule.Rows[i]["Schedule_Role_ID"].ToString() + "'");

                    string warna_shift = Schedule_warna_shift.Rows[0]["Schedule_Role_Color"].ToString();
                    outputs = "<a href=\"jadwal/?date=" + tanggal + "\" style=\"background-color: " + warna_shift + " !important; border: 1px solid " + warna_shift + " !important; padding: 0px 38px; color: white !important;\"></a>";

                    bs.Text = outputs.ToString();
                    e.Cell.Controls.Add(bs);
                }
            }
        }
    }

    protected void legend()
    {
        // check warna legend
        DataTable Schedule_warna_legend = Db.Rs("Select Schedule_Role_Color, Schedule_Role_Nama from TBL_Schedule_Role where Schedule_Role_Status = 1");

        for (int l = 0; l < Schedule_warna_legend.Rows.Count; l++)
        {
            string warna_legend = Schedule_warna_legend.Rows[l]["Schedule_Role_Color"].ToString();
            string nama_legend = Schedule_warna_legend.Rows[l]["Schedule_Role_Nama"].ToString();

            legend_calendar.Text += "<a style=\"background-color: " + warna_legend + " !important; border: 1px solid " + warna_legend + " !important; padding: 0px 10px; color: white !important;\">" + nama_legend + "</a>"
                                   + "<br>";
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        string Delete = "delete from TBL_Schedule_Kalendar_Temp where Schedule_Temp_CreateBy ='" + App.Employee_ID + "'";
        Db.Execute(Delete);
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataTable Schedule = Db.Rs("SELECT Employee_ID, Schedule_Role_ID, Schedule_Temp_Date FROM TBL_Schedule_Kalendar_Temp where Schedule_Temp_CreateBy ='" + App.Employee_ID + "'");

        for (int i = 0; i < Schedule.Rows.Count; i++)
        {
            DateTime tanggal_schedule = Convert.ToDateTime(Schedule.Rows[i]["Schedule_Temp_Date"].ToString());
            string tanggal = tanggal_schedule.ToString("yyyy-MM-dd");
            //if (i.DayOfWeek==DayOfWeek.Saturday)
            //{
            //    continue;
            //}
            string insert = "INSERT INTO TBL_Schedule_Kalendar (Employee_ID, Schedule_Role_ID, Schedule_Date, Schedule_CreateDate, Schedule_CreateBy) values('" + Schedule.Rows[i]["Employee_ID"].ToString() + "','" + Schedule.Rows[i]["Schedule_Role_ID"].ToString() + "','" + tanggal + "',getdate(),'" + App.Employee_ID + "')";
            Db.Execute(insert);
        }


        //string insert = "INSERT INTO TBL_Schedule_Kalendar (Employee_ID, Schedule_Role_ID, Schedule_Date, Schedule_CreateDate = getdate()) SELECT Employee_ID, Schedule_Role_ID, Schedule_Temp_Date FROM TBL_Schedule_Kalendar_Temp where Schedule_Temp_CreateBy ='" + App.Employee_ID + "'";
        //Db.Execute(insert);

        string Delete = "delete from TBL_Schedule_Kalendar_Temp where Schedule_Temp_CreateBy ='" + App.Employee_ID + "'";
        Db.Execute(Delete);

        Response.Redirect(Param.Path_User + "department/kalender/kehadiran/");
    }
}