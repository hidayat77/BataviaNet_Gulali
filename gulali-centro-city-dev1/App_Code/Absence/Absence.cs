using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GULALI.Absence
{
    /// <summary>
    /// Summary description for Absence
    /// </summary>
    ///

    public class Absence_m
    {
        public static string m_strQueryTemp { get; set; }
        public static DataTable m_dtImport { get; set; }
        private Db m_objDb;

        public Absence_m()
        {
            m_objDb = new Db();
        }

        public static List<AbsenceImportTemp> _lstAbsenceImportTemp { get; set; }

        public class AbsenceImportTemp
        {
            public Nullable<int> Absence_Emp_Id { get; set; }
            public string Absence_NIK { get; set; }
            public string Absence_Name { get; set; }
            public Nullable<DateTime> Absence_Date { get; set; }
            public string Absence_Working_Type { get; set; }
            public Nullable<DateTime> Absence_Scan_In { get; set; }
            public Nullable<DateTime> Absence_Scan_Out { get; set; }
            public Nullable<DateTime> Absence_Time_Late { get; set; }
            public Nullable<DateTime> Absence_Time_Early { get; set; }
            public Nullable<int> Absence_Absent { get; set; }
            public Nullable<DateTime> Absence_Overtime { get; set; }
            public Nullable<DateTime> Absence_Total_WorkingHours { get; set; }
            public string Absence_Day_Type { get; set; }
            public Nullable<DateTime> Absence_Total_Attendance { get; set; }
            public Nullable<decimal> Absence_Overtime_Normal { get; set; }
            public Nullable<decimal> Absence_Overtime_Weekend { get; set; }
            public Nullable<decimal> Absence_Overtime_Holiday { get; set; }
            public Nullable<DateTime> Absence_Created_Date { get; set; }
            public string Absence_Created_By { get; set; }
            public Nullable<DateTime> Absence_Modified_Date { get; set; }
            public string Absence_Modified_By { get; set; }
        }

        public class AbsenceEmployeeStatic
        {
            public static int absen_jmlkali_telat_reg { get; set; }
            public static int absen_menit_telat_reg { get; set; }
            public static int absen_jmlkali_telat_shift { get; set; }
            public static int absen_menit_telat_shift { get; set; }
            public static int absen_cuti_reguler { get; set; }
            public static int absen_cuti_shift { get; set; }
            public static decimal absen_izin_reguler { get; set; }
            public static decimal absen_izin_shift { get; set; }
            public static decimal absen_sakit_reguler { get; set; }
            public static decimal absen_sakit_shift { get; set; }
        }

        public class AbsenceEmployeeTemp
        {
            public int absen_jmlkali_telat_reg { get; set; }
            public int absen_menit_telat_reg { get; set; }
            public int absen_jmlkali_telat_shift { get; set; }
            public int absen_menit_telat_shift { get; set; }
            public int absen_cuti_reguler { get; set; }
            public int absen_cuti_shift { get; set; }
            public decimal absen_izin_reguler { get; set; }
            public decimal absen_izin_shift { get; set; }
            public decimal absen_sakit_reguler { get; set; }
            public decimal absen_sakit_shift { get; set; }
        }

        public static void AbsenceEmployeeObj(AbsenceEmployeeTemp AbsentEmpTemp)
        {
            AbsenceEmployeeStatic.absen_jmlkali_telat_reg = AbsentEmpTemp.absen_jmlkali_telat_reg;
            AbsenceEmployeeStatic.absen_menit_telat_reg = AbsentEmpTemp.absen_menit_telat_reg;
            AbsenceEmployeeStatic.absen_jmlkali_telat_shift = AbsentEmpTemp.absen_jmlkali_telat_shift;
            AbsenceEmployeeStatic.absen_menit_telat_shift = AbsentEmpTemp.absen_menit_telat_shift;
            AbsenceEmployeeStatic.absen_cuti_reguler = AbsentEmpTemp.absen_cuti_reguler;
            AbsenceEmployeeStatic.absen_cuti_shift = AbsentEmpTemp.absen_cuti_shift;
            AbsenceEmployeeStatic.absen_izin_reguler = AbsentEmpTemp.absen_izin_reguler;
            AbsenceEmployeeStatic.absen_izin_shift = AbsentEmpTemp.absen_izin_shift;
            AbsenceEmployeeStatic.absen_sakit_reguler = AbsentEmpTemp.absen_sakit_reguler;
            AbsenceEmployeeStatic.absen_sakit_shift = AbsentEmpTemp.absen_sakit_shift;
        }
    }

    /// <summary>
    /// Fungsi-fungsi Absence
    /// </summary>
    public class Absence_f
    {
        public static string f_strQueryTemp { get; set; }
        private Db f_objDb;

        public Absence_f()
        {
            f_objDb = new Db();
        }

        public static DataTable ViewAbsence()
        {
            DataTable dtRtn = new DataTable();
            dtRtn = Db.Rs(@"SELECT [Absence_ID],[Absence_Emp_Id],[Absence_NIK],[Absence_Name],CONVERT(VARCHAR, [Absence_Date], 103) [Absence_Date],[Absence_Working_Type],[Absence_Scan_In],[Absence_Scan_Out]
,[Absence_Time_Late],[Absence_Time_Early],[Absence_Absent],[Absence_Overtime],[Absence_Total_WorkingHours],[Absence_Day_Type],[Absence_Total_Attendance]
,[Absence_Overtime_Normal],[Absence_Overtime_Weekend],[Absence_Overtime_Holiday],[Absence_Created_Date],[Absence_Created_By],[Absence_Modified_Date],[Absence_Modified_By]
FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import]");
            return dtRtn;
        }

        //public static void SaveAbsence(string strQuery)
        //{
        //    //Simpan Data Absent
        //    StringBuilder sbQuery = new StringBuilder();
        //    //Simpan Query...
        //    sbQuery.Append("INSERT INTO [Gulali_HRIS].[dbo].[TBL_Absence_Import] (");
        //    sbQuery.Append(" [Absence_Emp_Id] ");
        //    sbQuery.Append(",[Absence_NIK] ");
        //    sbQuery.Append(",[Absence_Name] ");
        //    sbQuery.Append(",[Absence_Date] ");
        //    sbQuery.Append(",[Absence_Working_Type] ");
        //    sbQuery.Append(",[Absence_Scan_In] ");
        //    sbQuery.Append(",[Absence_Scan_Out] ");
        //    sbQuery.Append(",[Absence_Time_Late] ");
        //    sbQuery.Append(",[Absence_Time_Early] ");
        //    sbQuery.Append(",[Absence_Absent] ");
        //    sbQuery.Append(",[Absence_Overtime] ");
        //    sbQuery.Append(",[Absence_Total_WorkingHours] ");
        //    sbQuery.Append(",[Absence_Day_Type] ");
        //    sbQuery.Append(",[Absence_Total_Attendance] ");
        //    sbQuery.Append(",[Absence_Overtime_Normal] ");
        //    sbQuery.Append(",[Absence_Overtime_Weekend] ");
        //    sbQuery.Append(",[Absence_Overtime_Holiday] ");
        //    sbQuery.Append(",[Absence_Created_Date] ");
        //    sbQuery.Append(",[Absence_Created_By] ");
        //    sbQuery.Append(" ) VALUES ");
        //    sbQuery.Append(strQuery);
        //    Db.Execute(sbQuery.ToString());
        //}
        public static void SaveAbsence()
        {
            if (Absence_m._lstAbsenceImportTemp.Count > 0)
            {
                //Simpan Data Absent
                StringBuilder sbQuery = new StringBuilder();
                //Simpan Query...
                sbQuery.Append("INSERT INTO [Gulali_HRIS].[dbo].[TBL_Absence_Import] (");
                sbQuery.Append(" [Absence_Emp_Id] ");
                sbQuery.Append(",[Absence_NIK] ");
                sbQuery.Append(",[Absence_Name] ");
                sbQuery.Append(",[Absence_Date] ");
                sbQuery.Append(",[Absence_Working_Type] ");
                sbQuery.Append(",[Absence_Scan_In] ");
                sbQuery.Append(",[Absence_Scan_Out] ");
                sbQuery.Append(",[Absence_Time_Late] ");
                sbQuery.Append(",[Absence_Time_Early] ");
                sbQuery.Append(",[Absence_Absent] ");
                sbQuery.Append(",[Absence_Overtime] ");
                sbQuery.Append(",[Absence_Total_WorkingHours] ");
                sbQuery.Append(",[Absence_Day_Type] ");
                sbQuery.Append(",[Absence_Total_Attendance] ");
                sbQuery.Append(",[Absence_Overtime_Normal] ");
                sbQuery.Append(",[Absence_Overtime_Weekend] ");
                sbQuery.Append(",[Absence_Overtime_Holiday] ");
                sbQuery.Append(",[Absence_Created_Date] ");
                sbQuery.Append(",[Absence_Created_By] ");
                sbQuery.Append(" ) VALUES ");
                int i = 0;
                foreach (Absence_m.AbsenceImportTemp item in Absence_m._lstAbsenceImportTemp)
                {
                    //Menggunakan (condition) ? [true path] : [false path];
                    if (i > 0) sbQuery.Append(",(");
                    else sbQuery.Append("(");
                    if (item.Absence_Emp_Id.HasValue) sbQuery.Append(item.Absence_Emp_Id.ToString()); else sbQuery.Append("Null");
                    if (!item.Absence_NIK.Equals(null)) sbQuery.Append(",'" + item.Absence_NIK.ToString() + "'"); else sbQuery.Append(",Null");
                    if (!item.Absence_Name.Equals(null)) sbQuery.Append(",'" + item.Absence_Name.ToString() + "'"); else sbQuery.Append(",Null");
                    if (item.Absence_Date.HasValue) sbQuery.Append(",Convert(datetime,'" + item.Absence_Date.Value.ToString("dd/MM/yyyy") + "',103)"); else sbQuery.Append(",Null");
                    if (!item.Absence_Working_Type.Equals(null)) sbQuery.Append(",'" + item.Absence_Working_Type.ToString() + "'"); else sbQuery.Append(",Null");
                    if (item.Absence_Scan_In.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Scan_In.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Scan_Out.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Scan_Out.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Time_Late.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Time_Late.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Time_Early.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Time_Early.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Absent.HasValue) sbQuery.Append("," + item.Absence_Absent.ToString()); else sbQuery.Append(",Null");
                    if (item.Absence_Overtime.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Overtime.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Total_WorkingHours.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Total_WorkingHours.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (!item.Absence_Day_Type.Equals(null)) sbQuery.Append(",'" + item.Absence_Day_Type.ToString() + "'"); else sbQuery.Append(",Null");
                    if (item.Absence_Total_Attendance.HasValue) sbQuery.Append(",Convert([time](7),replace('" + item.Absence_Total_Attendance.Value.ToString("HH:mm:ss") + "', '.', ':'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Overtime_Normal.HasValue) sbQuery.Append(",Convert([decimal](20,2),replace('" + item.Absence_Overtime_Normal.ToString() + "', ',', '.'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Overtime_Weekend.HasValue) sbQuery.Append(",Convert([decimal](20,2),replace('" + item.Absence_Overtime_Weekend.ToString() + "', ',', '.'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Overtime_Holiday.HasValue) sbQuery.Append(",Convert([decimal](20,2),replace('" + item.Absence_Overtime_Holiday.ToString() + "', ',', '.'))"); else sbQuery.Append(",Null");
                    if (item.Absence_Created_Date.HasValue) sbQuery.Append(",GETDATE()"); else sbQuery.Append(",Null");
                    if (!item.Absence_Created_By.Equals(null)) sbQuery.Append(",'" + item.Absence_Created_By.ToString() + "'"); else sbQuery.Append(",Null");
                    sbQuery.Append(")");
                    i++;
                }
                Db.Execute(sbQuery.ToString());
            }
        }

        public static int iCountAbsence(string strID_No, string strAbsenceDate)
        {
            //check data sudah ada belum...
            int iRet = 0;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
            SELECT COUNT(1)
            FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import]
            WHERE [Absence_Emp_Id] = {0} and Convert(varchar,[Absence_Date],103) LIKE Convert(varchar,Convert(datetime,'{1}',103),103) ", strID_No, strAbsenceDate);
            try
            {
                iRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iRet = 0;
            }

            return iRet;
        }

        public static void Get_AbsenceEmployee(string strEmpId, string strSchRoleStatus, DateTime dateFrom, DateTime dateTo)
        {
            Absence_m.AbsenceEmployeeTemp dtEmp = new Absence_m.AbsenceEmployeeTemp();

            #region 1.absen_jmlkali_telat_reg

            //1.absen_jmlkali_telat_reg
            int iQueryRet = 0;
            decimal dQueryRet = 0;
            strSchRoleStatus = "0";
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT ISNULL(COUNT(1),0) as 'Berapa_Kali_Telat_Absen'
FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import] A
JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Absence_Emp_Id
WHERE
A.Absence_Emp_Id = {0}
AND
A.[Absence_Time_Late] is not null
AND
A.Absence_Date BETWEEN Convert(datetime,'{1}',103) AND Convert(datetime,'{2}',103)
AND
SCH.Schedule_Role_Status = {3}
", strEmpId, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), strSchRoleStatus);
            try
            {
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_jmlkali_telat_reg = iQueryRet;
            }

            #endregion 1.absen_jmlkali_telat_reg

            #region 2.absen_jmlkali_telat_shift

            //2.absen_jmlkali_telat_shift
            iQueryRet = 0;
            strSchRoleStatus = "1";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT ISNULL(COUNT(1),0) as 'Berapa_Kali_Telat_Absen'
FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import] A
JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Absence_Emp_Id
WHERE
A.Absence_Emp_Id = {0}
AND
A.[Absence_Time_Late] is not null
AND
A.Absence_Date BETWEEN Convert(datetime,'{1}',103) AND Convert(datetime,'{2}',103)
AND
SCH.Schedule_Role_Status = {3}
", strEmpId, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), strSchRoleStatus);
            try
            {
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_jmlkali_telat_shift = iQueryRet;
            }

            #endregion 2.absen_jmlkali_telat_shift

            #region 3.absen_menit_telat_reg

            //3.absen_menit_telat_reg
            iQueryRet = 0;
            strSchRoleStatus = "0";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT ISNULL(SUM(Convert(int,DATEPART(minute,[Absence_Time_Late]),103)),0) as 'Berapa_Menit_Telat_Absen'
FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import] A
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Absence_Emp_Id
WHERE
A.Absence_Emp_Id = 3
AND
A.[Absence_Date] BETWEEN Convert(datetime,'{1}',103) AND Convert(datetime,'{2}',103)
AND
A.[Absence_Time_Late] is not null
AND
SCH.Schedule_Role_Status = {3}
", strEmpId, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), strSchRoleStatus);
            try
            {
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_menit_telat_reg = iQueryRet;
            }

            #endregion 3.absen_menit_telat_reg

            #region 4.absen_menit_telat_shift

            //4.absen_menit_telat_shift
            iQueryRet = 0;
            strSchRoleStatus = "1";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT ISNULL(SUM(Convert(int,DATEPART(minute,[Absence_Time_Late]),103)),0) as 'Berapa_Menit_Telat_Absen'
FROM [Gulali_HRIS].[dbo].[TBL_Absence_Import] A
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Absence_Emp_Id
WHERE
A.Absence_Emp_Id = 3
AND
A.[Absence_Date] BETWEEN Convert(datetime,'{1}',103) AND Convert(datetime,'{2}',103)
AND
A.[Absence_Time_Late] is not null
AND
SCH.Schedule_Role_Status = {3}
", strEmpId, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"), strSchRoleStatus);
            try
            {
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_menit_telat_shift = iQueryRet;
            }

            #endregion 4.absen_menit_telat_shift

            #region 5.absen_cuti_reguler

            //5.absen_cuti_reguler
            iQueryRet = 0;
            strSchRoleStatus = "0";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
    ISNULL((DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1),0) JML_HARI
FROM		TBL_Employee A
INNER JOIN	TBL_Leave AS B ON B.Employee_ID = A.Employee_ID
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Employee_ID
WHERE		(A.Employee_ID = {0})
AND
(B.Type_ID = '1') --Cuti/ annualleave
AND
(B.Leave_StatusLeave = 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_cuti_reguler = iQueryRet;
            }

            #endregion 5.absen_cuti_reguler

            #region 6.absen_cuti_shift

            //6.absen_cuti_shift
            iQueryRet = 0;
            strSchRoleStatus = "1";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
    ISNULL((DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1),0) JML_HARI
FROM		TBL_Employee A
INNER JOIN	TBL_Leave AS B ON B.Employee_ID = A.Employee_ID
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = {0}
) SCH ON SCH.Employee_ID = A.Employee_ID
WHERE		(A.Employee_ID = {0})
AND
(B.Type_ID = '1') --Cuti/ annualleave
AND
(B.Leave_StatusLeave = 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                iQueryRet = Db.SingleInteger(sbQuery.ToString());
            }
            catch
            {
                iQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_cuti_shift = iQueryRet;
            }

            #endregion 6.absen_cuti_shift

            #region 7.absen_izin_reguler

            //7.absen_izin_reguler
            iQueryRet = 0;
            dQueryRet = 0;
            strSchRoleStatus = "0";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
SUM(
	ISNULL(
		IIF(
			B.Leave_Half = 0
			,(DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1)
			,B.Leave_Half*0.5
		)
		,0
	)
)
FROM		TBL_Leave AS B
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = '{0}'
) SCH ON SCH.Employee_ID = B.Employee_ID
WHERE		(B.Employee_ID = '{0}')
AND
(B.Type_ID  in ('5')) --izin/lainnya
AND
(B.Leave_StatusLeave= 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                dQueryRet = Db.SingleDecimal(sbQuery.ToString());
            }
            catch
            {
                dQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_izin_reguler = dQueryRet;
            }

            #endregion 7.absen_izin_reguler

            #region 8.absen_izin_shift

            //8.absen_izin_shift
            iQueryRet = 0;
            dQueryRet = 0;
            strSchRoleStatus = "1";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
SUM(
	ISNULL(
		IIF(
			B.Leave_Half = 0
			,(DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1)
			,B.Leave_Half*0.5
		)
		,0
	)
)
FROM		TBL_Leave AS B
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = '{0}'
) SCH ON SCH.Employee_ID = B.Employee_ID
WHERE		(B.Employee_ID = '{0}')
AND
(B.Type_ID  in ('5')) --izin/lainnya
AND
(B.Leave_StatusLeave= 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                dQueryRet = Db.SingleDecimal(sbQuery.ToString());
            }
            catch
            {
                dQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_izin_shift = dQueryRet;
            }

            #endregion 8.absen_izin_shift

            #region 9.absen_sakit_reguler

            //9.absen_sakit_reguler
            iQueryRet = 0;
            dQueryRet = 0;
            strSchRoleStatus = "0";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
SUM(
	ISNULL(
		IIF(
			B.Leave_Half = 0
			,(DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1)
			,B.Leave_Half*0.5
		)
		,0
	)
)
JML_HARI
FROM TBL_Leave AS B
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = '{0}'
) SCH ON SCH.Employee_ID = B.Employee_ID
WHERE		(B.Employee_ID = '{0}')
AND
(B.Type_ID in ('2','3')) -- Sakit dengan surat dan tanpa surat
AND
(B.Leave_StatusLeave= 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                dQueryRet = Db.SingleDecimal(sbQuery.ToString());
            }
            catch
            {
                dQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_sakit_reguler = dQueryRet;
            }

            #endregion 9.absen_sakit_reguler

            #region 10.absen_sakit_shift

            //10.absen_sakit_shift
            iQueryRet = 0;
            dQueryRet = 0;
            strSchRoleStatus = "1";
            sbQuery = new StringBuilder();
            sbQuery.AppendFormat(@"
SELECT
SUM(
	ISNULL(
		IIF(
			B.Leave_Half = 0
			,(DATEDIFF(DAY, CONVERT(DATETIME,B.Leave_RequestDateFrom,103),CONVERT(DATETIME,B.Leave_RequestDateTo,103)) + 1)
			,B.Leave_Half*0.5
		)
		,0
	)
)
JML_HARI
FROM TBL_Leave AS B
INNER JOIN	TBL_Leave_Type AS C ON C.Type_ID = B.Type_ID
INNER JOIN (
SELECT DISTINCT C.Schedule_Role_Status, B.Employee_ID FROM [Gulali_HRIS].[dbo].TBL_Schedule_Kalendar B
INNER JOIN [Gulali_HRIS].[dbo].TBL_Schedule_Role C ON C.Schedule_Role_ID = B.Schedule_Role_ID
WHERE B.Employee_ID = '{0}'
) SCH ON SCH.Employee_ID = B.Employee_ID
WHERE		(B.Employee_ID = '{0}')
AND
(B.Type_ID in ('2','3')) -- Sakit dengan surat dan tanpa surat
AND
(B.Leave_StatusLeave= 2)
AND
(SCH.Schedule_Role_Status = {1})
AND
B.[Leave_RequestDateFrom] BETWEEN Convert(datetime,'{2}',103) AND Convert(datetime,'{3}',103)
", strEmpId, strSchRoleStatus, dateFrom.ToString("dd/MM/yyyy"), dateTo.ToString("dd/MM/yyyy"));
            try
            {
                //DataTable dtResult = Db.Rs(sbQuery.ToString());
                //if (dtResult.Rows.Count > 0) iQueryRet = Convert.ToInt16(dtResult.Rows[0][0].ToString());
                //else iQueryRet = 0;
                dQueryRet = Db.SingleDecimal(sbQuery.ToString());
            }
            catch
            {
                dQueryRet = 0;
            }
            finally
            {
                dtEmp.absen_sakit_shift = dQueryRet;
            }

            #endregion 10.absen_sakit_shift

            //Akhirnya simpan di Object
            Absence_m.AbsenceEmployeeObj(dtEmp);
        }
    }
}