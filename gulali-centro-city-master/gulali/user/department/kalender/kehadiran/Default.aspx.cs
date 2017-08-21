using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class _admin_kalender_kehadiran : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Departemen >> Kalender >> Kehadiran", "view");

        tab_cuti.Text = "<a href=\"" + Param.Path_User + "department/kalender/cuti/\">Cuti</a>";
        tab_lembur.Text = "<a href=\"" + Param.Path_User + "department/kalender/lembur/\">Lembur</a>";
        tab_kehadiran.Text = "<a href=\"" + Param.Path_User + "department/kalender/kehadiran/\">Kehadiran</a>";

        //fill("1", pagesum.Text, search_text.Text); 

        if (!IsPostBack)
        {
            ddl_employee_show(); ddl_jadwal_show(); legend();
            //fill("1", pagesum.Text, search_text.Text); 
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

    ///calendar
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

        /////////////////
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
        string where_jadwal ;
        if (ddl_jadwal.SelectedValue == "")
        {
            where_jadwal = "";
        }
        else
        {
            where_jadwal = " And Schedule_Role_ID = '" + ddl_jadwal.SelectedValue + "'";
        }

        DataTable Schedule = Db.Rs("Select Distinct CONVERT(VARCHAR(10),Schedule_Date ,105) as Schedule_Date, Schedule_Role_ID from TBL_Schedule_Kalendar A left join TBL_Employee B on A.Employee_ID = B.Employee_ID where B.Employee_DirectSpv = '" + App.Employee_ID + "' " + where_employee_id + "" + where_jadwal + "");

        DataTable Schedule_ID = Db.Rs("Select Distinct Schedule_ID from TBL_Schedule_Kalendar A left join TBL_Employee B on A.Employee_ID = B.Employee_ID where B.Employee_DirectSpv = '" + App.Employee_ID + "' " + where_employee_id + "" + where_jadwal + "");

        if (Schedule.Rows.Count > 0)
        {
            for (int i = 0; i < Schedule.Rows.Count; i++)
            {
                if (Schedule.Rows[i]["Schedule_Date"].ToString() == e.Day.Date.ToString("dd-MM-yyyy").ToString())
                {
                    Literal lineBreaks = new Literal();
                    lineBreaks.Text = "<BR />";
                    e.Cell.Controls.Add(lineBreaks);

                    Label bs = new Label();
                    bs.Font.Size = 8;
                    bs.Font.Bold = true;
                    bs.ForeColor = System.Drawing.ColorTranslator.FromHtml("white");

                    DateTime tanggal_sekarang = DateTime.Now.AddDays(-1);
                    DateTime tanggal_schedule = Convert.ToDateTime(Schedule.Rows[i]["Schedule_Date"].ToString());

                    string outputs = "";
                    string tanggal = tanggal_schedule.ToString("dd-MM-yy");

                    // check warna shift
                    DataTable Schedule_warna_shift = Db.Rs("Select Schedule_Role_Color from TBL_Schedule_Role where Schedule_Role_ID = '" + Schedule.Rows[i]["Schedule_Role_ID"].ToString() + "'");

                    string warna_shift = Schedule_warna_shift.Rows[0]["Schedule_Role_Color"].ToString();
                    //string id_shift = Schedule_warna_shift.Rows[0]["Schedule_ID"].ToString();
                    
                    if (tanggal_schedule < tanggal_sekarang)
                    {
                        e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#d8d8d8");
                        outputs = "<a style=\"background-color: " + warna_shift + " !important; border: 1px solid " + warna_shift + " !important; padding: 0px 78px; color: white !important;\"></a>";
                    }
                    else if (ddl_employee.SelectedValue == "")
                    {
                        outputs = "<a style=\"background-color: " + warna_shift + " !important; border: 1px solid " + warna_shift + " !important; padding: 0px 78px; color: white !important;\"></a>";
                    }
                    else
                    {
                        outputs = "<a href=\"edit/?id=" + Cf.Int(Schedule_ID.Rows[i]["Schedule_ID"].ToString()) + "\" style=\"background-color: " + warna_shift + " !important; border: 1px solid " + warna_shift + " !important; padding: 0px 60px; color: white !important;\"><i class=\"fa fa-pencil\"></i> Edit</a>";
                    }

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
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("../kehadiran");
    }
}