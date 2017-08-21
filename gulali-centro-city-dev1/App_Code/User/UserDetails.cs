using System;
using System.Data;
using System.Text;

namespace GULALI.UserDetailsData
{
    /// <summary>
    /// Schema Data dari UserDetails
    /// </summary>
    public class UserDetailsData_m
    {
        //Example dari Excel
        //string	user_divisi
        //string	user_department
        //string	user_directLeader
        //int	    user_lamabekerja_aktual
        //double	user_saldo_cuti


        public class UserDataTemp
        {
            public string strEmpID { get; set; }
            public string strFirstName { get; set; }
            public string strLastName { get; set; }
            public string strFullName { get; set; }
            public string strDirectSPV { get; set; }
            public string strUserDepartment { get; set; }
            public string strUserDivisi { get; set; }
            public double dblUserSaldoCuti { get; set; }
        }

        public class UserDataStatic
        {
            public static string strEmpID { get; set; }
            public static string strFirstName { get; set; }
            public static string strLastName { get; set; }
            public static string strFullName { get; set; }
            public static string strDirectSPV { get; set; }
            public static string strUserDepartment { get; set; }
            public static string strUserDivisi { get; set; }
            public static double dblUserSaldoCuti { get; set; }         
        }

        public void UserObj(UserDataTemp _UserDataTemp)
        {
            UserDataStatic.strEmpID = _UserDataTemp.strEmpID;
            UserDataStatic.strFirstName = _UserDataTemp.strFirstName;
            UserDataStatic.strLastName = _UserDataTemp.strLastName;
            UserDataStatic.strFullName = _UserDataTemp.strFullName;
            UserDataStatic.strDirectSPV = _UserDataTemp.strDirectSPV;
            UserDataStatic.strUserDepartment = _UserDataTemp.strUserDepartment;
            UserDataStatic.strUserDivisi = _UserDataTemp.strUserDivisi;
            UserDataStatic.dblUserSaldoCuti = _UserDataTemp.dblUserSaldoCuti;          
        }

        // Untuk LamaBekerja kita pisah karena object ini mempunyai nilai lebih dari satu ...
        public class LamaBekerjaDataStatic
        {
            public static int iYear { get; set; }
            public static int iMonth { get; set; }
            public static int iWeek { get; set; }
            public static int iDay { get; set; }
        }

        public class LamaBekerjaDataTemp
        {
            public int iYear { get; set; }
            public int iMonth { get; set; }
            public int iWeek { get; set; }
            public int iDay { get; set; }
        }

        public void LamaBekerjaObj(LamaBekerjaDataTemp _LamaBekerjaDataTemp)
        {
            LamaBekerjaDataStatic.iYear = _LamaBekerjaDataTemp.iYear;
            LamaBekerjaDataStatic.iMonth = _LamaBekerjaDataTemp.iMonth;
            LamaBekerjaDataStatic.iWeek = _LamaBekerjaDataTemp.iWeek;
            LamaBekerjaDataStatic.iDay = _LamaBekerjaDataTemp.iDay;
        }
    }

    /// <summary>
    /// Fungsi-fungsi UserDetails
    /// </summary>
    public class UserDetailsData_f
    {
        public static void ViewUserDetails(string strUserName)
        {
            UserDetailsData_m userDet = new UserDetailsData_m();
            DataTable dtQuery = new DataTable();
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT			TBL_User.Employee_ID, TBL_User.User_Name, TBL_Employee.Employee_Full_Name, TBL_Employee.Employee_First_Name, TBL_Employee.Employee_Middle_Name, TBL_Employee.Employee_Last_Name
				,TBL_Employee.Department_ID, TBL_Employee.Employee_Sum_LeaveBalance, TBL_Employee.Employee_JoinDate, TBL_Department.Department_Name, TBL_Employee.Division_ID, TBL_Division.Division_Name
				,DirectSPV.Employee_Full_Name Employee_DirectSPV
FROM            TBL_User
INNER JOIN
				TBL_Employee ON TBL_User.Employee_ID = TBL_Employee.Employee_ID
INNER JOIN
                TBL_Department ON TBL_Employee.Department_ID = TBL_Department.Department_ID
INNER JOIN
                TBL_Division ON TBL_Employee.Division_ID = TBL_Division.Division_ID
LEFT JOIN
(SELECT A.Employee_Full_Name, A.Employee_DirectSpv, A.Employee_ID FROM TBL_Employee A) DirectSPV on DirectSPV.Employee_ID = TBL_Employee.Employee_DirectSpv
WHERE TBL_User.User_Name like UPPER('{0}')", strUserName.ToUpper());
            try
            {
                dtQuery = Db.Rs(sbQuery.ToString());
            }
            catch
            {
                dtQuery = null;
            }
            //Check Apakah hasil Query ada Datanya dan Cuman 1 Data ???
            if (dtQuery.Rows.Count > 0 && dtQuery.Rows.Count == 1)
            {
                //declare variable temp... biar enak diliat...
                DateTime dtJoinDate = new DateTime();
                string strJoinDate = string.Empty;
                double dblUserSaldoCuti = 0;
                string strUserSaldoCuti = string.Empty;

                //Isi data nya ...
                UserDetailsData_m.UserDataTemp _UserDataTemp = new UserDetailsData_m.UserDataTemp();
                _UserDataTemp.strEmpID = dtQuery.Rows[0]["Employee_ID"].ToString();
                _UserDataTemp.strFirstName = dtQuery.Rows[0]["Employee_First_Name"].ToString();
                _UserDataTemp.strLastName = dtQuery.Rows[0]["Employee_Last_Name"].ToString();
                _UserDataTemp.strFullName = dtQuery.Rows[0]["Employee_Full_Name"].ToString();
                _UserDataTemp.strDirectSPV = dtQuery.Rows[0]["Employee_DirectSPV"].ToString();
                _UserDataTemp.strUserDepartment = dtQuery.Rows[0]["Department_Name"].ToString();
                _UserDataTemp.strUserDivisi = dtQuery.Rows[0]["Division_Name"].ToString();
                strJoinDate = dtQuery.Rows[0]["Employee_JoinDate"].ToString();
                strUserSaldoCuti = dtQuery.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
                if (strUserSaldoCuti.Length > 0)
                {
                    double value;
                    if (Double.TryParse(strUserSaldoCuti, out value)) dblUserSaldoCuti = value;
                }
                _UserDataTemp.dblUserSaldoCuti = dblUserSaldoCuti;

                //Isi Data Object Public ...
                userDet.UserObj(_UserDataTemp);
                if (strJoinDate.Length > 0)
                {
                    dtJoinDate = Convert.ToDateTime(strJoinDate);
                    //Perhitungan estimasi Tahun,Bulan,Minggu dan Hari ...
                    UserDetailsData_m.LamaBekerjaDataTemp objTemp = Get_LamaBekerja(dtJoinDate);
                    //Isi Data Object Public - Lama Bekerja ...
                    userDet.LamaBekerjaObj(objTemp);
                }
            }
        }

        public static UserDetailsData_m.LamaBekerjaDataTemp Get_LamaBekerja(DateTime dtJoinDate)
        {
            UserDetailsData_m.LamaBekerjaDataTemp lb = new UserDetailsData_m.LamaBekerjaDataTemp();

            int diffDays = (DateTime.Now - dtJoinDate).Days;
            int iYear = 0; int iMonth = 0; int iWeek = 0; int iDay = 0;
            //Estimasi bahwa 1 tahun 365(365/366) hari ...
            if (diffDays > 365)
            {
                int count_year = diffDays / 365;
                iYear = count_year;
                diffDays -= count_year * 365;
            }
            //Estimasi bahwa 1 bulan 30(31/30/29/28) hari ...
            if (diffDays > 30)
            {
                int count_month = diffDays / 30;
                iMonth = count_month;
                diffDays -= count_month * 30;
            }
            if (diffDays > 7)
            {
                int count_week = diffDays / 7;
                iWeek = count_week;
                diffDays -= count_week * 7;
            }
            iDay = diffDays;
            //Isi temp.data ...
            lb.iYear = iYear;
            lb.iMonth = iMonth;
            lb.iWeek = iWeek;
            lb.iDay = iDay;

            return lb;
        }
    }
}