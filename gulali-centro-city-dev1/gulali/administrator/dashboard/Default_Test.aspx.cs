using System;
using System.Data;
using System.Globalization;
using System.Text;

public partial class _DefaultTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);

        DataTable employee = Db.Rs("select Employee_Full_Name, Employee_Sum_LeaveBalance, Employee_Photo, Department_Name, Division_Name from TBL_Employee a left join TBL_Department b on a.Department_ID = b.Department_ID left join TBL_Division c on a.Division_ID = c.Division_ID  where Employee_ID = '" + App.Employee_ID + "' ");

        if (employee.Rows.Count > 0)
        {
            string foto_link = "";
            if (!string.IsNullOrEmpty(employee.Rows[0]["Employee_Photo"].ToString())) { foto_link = "/assets/images/employee-photo/" + employee.Rows[0]["Employee_Photo"].ToString(); }
            else { foto_link = "/assets/images/no-user-image.gif"; }

            link_foto.Text = "<a href=\"" + foto_link + "\" class=\"image-popup\" title=\"Screenshot-1\"><img src=\"" + foto_link + "\" class=\"thumb-img\" alt=\"work-thumbnail\" style=\"max-width:150px;\"></a>";

            nama.Text = employee.Rows[0]["Employee_Full_Name"].ToString();
            departemen.Text = employee.Rows[0]["Department_Name"].ToString();
            divisi.Text = employee.Rows[0]["Division_Name"].ToString();
            balance.Text = employee.Rows[0]["Employee_Sum_LeaveBalance"].ToString();

            data_peraturan_kantor_prev();
            data_hari_libur_prev();
            data_pengumuman_kantor();
        }
    }

    protected void data_peraturan_kantor_prev()
    {
        DataTable rs_peraturan = Db.Rs("select Regulation_Title, Regulation_Desc from TBL_Office_Regulations order by Regulation_ID asc");

        StringBuilder x = new StringBuilder();
        StringBuilder v = new StringBuilder();
        string style_ = "";
        if (rs_peraturan.Rows.Count > 0)
        {
            style_ = "color:red;";
            x.Append("<ul style=\"padding-left: 10px;\">");
            for (int i = 0; i < rs_peraturan.Rows.Count; i++)
            {
                x.Append("<li>" + rs_peraturan.Rows[i]["Regulation_Title"].ToString() + "</li>");
            }
            x.Append("<ul>");

            title_peraturan_kantor.Text = "<font style=\"" + style_ + "\">Peraturan Kantor</font>";
            data_peraturan_kantor.Text = x.ToString();

            v.Append(modal_dialog_allowance("devita", "test"));
            modal.Text = v.ToString();
        }
        else
        {
            div_peraturan_kantor.Visible = false;
        }
    }

    private string modal_dialog_allowance(string id_emp, string month)
    {
        string x = "";
        DataTable tun = Db.Rs("select Regulation_Title, Regulation_Desc from TBL_Office_Regulations order by Regulation_ID asc");

        if (tun.Rows.Count > 0)
        {
            x += (" <div id=\"Regular" + id_emp + "\" class=\"modal fade\" role=\"dialog\">"
                     + "<div class=\"modal-dialog\">"
                     + "<!-- Modal content-->"
                     + "<div class=\"modal-content\">"
                       + "<div class=\"modal-header\">"

                         + "<h4 class=\"modal-title\">Allowance Details</h4>"
                       + "</div>"
                       + "<div class=\"modal-body\"><table>");
            string comName = "", comValue = "";
            for (int y = 0; y < tun.Rows.Count; y++)
            {
                comName = tun.Rows[y]["Regulation_Title"].ToString();
                comValue = tun.Rows[y]["Regulation_Desc"].ToString();
                if (!string.IsNullOrEmpty(comValue))
                {
                    x += ("<tr><td>" + comName + " </td><td>&nbsp; : &nbsp;</td><td align=\"right\">" + comValue + "</td><td></td></tr>");
                }
            }
            x += ("<tr><td colspan=\"3\" align=\"right\">--------------------------------------------------</td><td> &nbsp; &nbsp;+</td></tr>"
                    + "<tr><td align=\"right\">Summary</td><td>&nbsp;  &nbsp;</td><td align=\"right\">"
    + comValue + "</td></tr>"
                    + "</table></div>"
                       + "<div class=\"modal-footer\">"
                       + "</div>"
                     + "</div>"
                   + "</div>"
                 + "</div>");
        }
        return x;
    }

    protected void data_hari_libur_prev()
    {
        DataTable rs_libur = Db.Rs("select Holiday_List_Name, CONVERT(VARCHAR(11),Holiday_Date ,106) as Holiday_Date from TBL_Holiday a join TBL_Holiday_List b on a.Holiday_List_ID = b.Holiday_List_ID where year(Holiday_Date) = '" + DateTime.Now.Year + "' and month(Holiday_Date) = '" + DateTime.Now.Month + "'");

        StringBuilder x = new StringBuilder();
        string style_ = "";
        if (rs_libur.Rows.Count > 0)
        {
            style_ = "color:red;";
            x.Append("<ul style=\"padding-left: 10px;\">");
            for (int i = 0; i < rs_libur.Rows.Count; i++)
            {
                x.Append("<li>" + rs_libur.Rows[i]["Holiday_List_Name"].ToString() + " - " + rs_libur.Rows[i]["Holiday_Date"].ToString() + "</li>");
            }
            x.Append("<ul>");
            title_hari_libur.Text = "<font style=\"" + style_ + "\">Hari Libur</font>";
            data_hari_libur.Text = x.ToString();
        }
        else
        {
            div_hari_libur.Visible = false;
        }
    }

    protected void data_pengumuman_kantor()
    {
        //DataTable rs_pengumuman = Db.Rs("select Announcement_Title, Announcement_Desc from TBL_Announcement order by Announcement_ID asc");
        //StringBuilder x = new StringBuilder();
        //StringBuilder v = new StringBuilder();
        //string style_ = "";
        //if (rs_pengumuman.Rows.Count > 0)
        //{
        //    style_ = "color:red;";
        //    x.Append("<ul style=\"padding-left: 10px;\">");
        //    for (int i = 0; i < rs_pengumuman.Rows.Count; i++)
        //    {
        //        x.Append("<li>" + rs_pengumuman.Rows[i]["Announcement_Title"].ToString() + "</li>");
        //    }
        //    x.Append("<ul>");

        //    title_pengumuman.Text = "<font style=\"" + style_ + "\">Pengumuman</font>";
        //    data_pengumuman.Text = x.ToString();

        //    v.Append(modal_dialog_allowance("devita", "test"));
        //    modal.Text = v.ToString();
        //}
        //else
        //{
        //    div_pengumuman_kantor.Visible = false;
        //}
    }

    protected void btnTest1_Click(object sender, EventArgs e)
    {
        //Get_AbsenceEmployee(string strEmpId, string strSchRoleStatus, DateTime dateFrom, DateTime dateTo)
        DateTime dateFrom = DateTime.ParseExact("25/05/2014",
                  "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dateTo = DateTime.ParseExact("25/07/2014",
                  "dd/MM/yyyy", CultureInfo.InvariantCulture);
        GULALI.Absence.Absence_f.Get_AbsenceEmployee("3", "0", dateFrom, dateTo);

        TimeSpan ts_TelatReg = TimeSpan.FromMinutes(GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_menit_telat_reg);
        TimeSpan ts_TelatShift = TimeSpan.FromMinutes(GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_menit_telat_shift);

        StringBuilder sbQueryResult = new StringBuilder();
        sbQueryResult.AppendFormat(@"
absen_jmlkali_telat_reg     : {1} {0}
absen_menit_telat_reg       : {2} {0}
absen_jmlkali_telat_shift   : {3} {0}
absen_menit_telat_shift     : {4} {0}
absen_cuti_reguler          : {5} {0}
absen_cuti_shift            : {6} {0}
absen_izin_reguler			: {7} {0}
absen_izin_shift            : {8} {0}
absen_sakit_reguler         : {9} {0}
absen_sakit_shift           : {10} {0}
", "<br/>"//Environment.NewLine
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_jmlkali_telat_reg.ToString()
 , ts_TelatReg.ToString(@"hh\:mm")
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_jmlkali_telat_shift.ToString()
 , ts_TelatShift.ToString(@"hh\:mm")
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_cuti_reguler.ToString()
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_cuti_shift.ToString()
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_izin_reguler.ToString()
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_izin_shift.ToString()
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_sakit_reguler.ToString()
 , GULALI.Absence.Absence_m.AbsenceEmployeeStatic.absen_sakit_shift.ToString()
 );
        lit_test1.Text = sbQueryResult.ToString();
    }

    protected void btnTest2_Click(object sender, EventArgs e)
    {
        GULALI.UserDetailsData.UserDetailsData_f.ViewUserDetails("Devita");

        StringBuilder sbQueryResult = new StringBuilder();
        sbQueryResult.AppendFormat(@"
user_divisi              : {1} {0}
user_department          : {2} {0}
user_directLeader        : {3} {0}
user_lamabekerja_aktual  : {4} tahun {5} bulan {6} minggu {7} hari  {0}
user_saldo_cuti			 : {8} {0}
", "<br/>"//Environment.NewLine
, GULALI.UserDetailsData.UserDetailsData_m.UserDataStatic.strUserDivisi.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.UserDataStatic.strUserDepartment.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.UserDataStatic.strDirectSPV.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.LamaBekerjaDataStatic.iYear.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.LamaBekerjaDataStatic.iMonth.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.LamaBekerjaDataStatic.iWeek.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.LamaBekerjaDataStatic.iDay.ToString()
, GULALI.UserDetailsData.UserDetailsData_m.UserDataStatic.dblUserSaldoCuti.ToString()
 );
        lit_test2.Text = sbQueryResult.ToString();
    }
}