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

public partial class _user_kalender_kehadiran : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Pribadi >> Kalender >> Kehadiran", "view");

        tab_cuti.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/cuti/\">Cuti</a>";
        tab_lembur.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/lembur/\">Lembur</a>";
        tab_kehadiran.Text = "<a href=\"" + Param.Path_User + "personal-data/kalender/kehadiran/\">Kehadiran</a>";
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
                    lineBreak.Text = "<BR /><BR />";
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
    }
}