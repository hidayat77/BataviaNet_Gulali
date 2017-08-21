using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Xml;

/// <summary>
/// ADO.Net Database Driver
/// </summary>
public class Db
{
    //Connection String
    public static string CnnString
    { get { return ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString; } }

    public static string CnnString2 {
        get { return ConfigurationManager.ConnectionStrings["CnnStr2"].ConnectionString; } }

    public static string CnnString3 {
        get { return ConfigurationManager.ConnectionStrings["CnnStr3"].ConnectionString; } }

    //Data Table Builder
    public static DataTable Rs(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable rs = new DataTable();
        rs = objDS.Tables["data"];

        return rs;
    }
    public static DataTable Rs2(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString2);
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable rs = new DataTable();
        rs = objDS.Tables["data"];

        return rs;
    }

    public static DataTable Rs3(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString3);
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable rs = new DataTable();
        rs = objDS.Tables["data"];

        return rs;
    }

    //Scalar Function
    public static string SingleString(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        string x = "";
        x = (string)sqlCmd.ExecuteScalar();
        sqlCnn.Close();

        return x;
    }
    public static int SingleInteger(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        int x = (int)sqlCmd.ExecuteScalar();
        //int x = sqlCmd.ExecuteScalar();
        sqlCnn.Close();

        return x;
    }

    public static decimal SingleDecimal(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        decimal x = Convert.ToDecimal(sqlCmd.ExecuteScalar());
        sqlCnn.Close();

        return x;
    }
    public static bool SingleBool(string strSql)
    {
        bool x = false;
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        try
        {
            x = (bool)sqlCmd.ExecuteScalar();
        }
        catch
        {
            x = false;
        }
        sqlCnn.Close();

        return x;
    }
    public static byte SingleByte(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        byte x = (byte)sqlCmd.ExecuteScalar();
        sqlCnn.Close();

        return x;
    }
    public static System.DateTime SingleTime(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        DateTime x = (DateTime)sqlCmd.ExecuteScalar();
        sqlCnn.Close();

        return x;
    }

    //Execute Command
    public static void Execute(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCnn.Close();
    }
    public static void Execute2(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString2);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCnn.Close();
    }
    public static XmlNodeList Xml(string strPath, string strNodes)
    {
        XmlDataDocument x = new XmlDataDocument();
        x.Load(strPath);

        XmlNodeList y = x.SelectNodes(strNodes);
        return y;
    }
    public static string CnnStringC
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["CnnStrC"].ConnectionString;
        }
    }

    public static DataTable RsC(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnStringC);
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable rs = new DataTable();
        rs = objDS.Tables["data"];

        return rs;
    }

    public static void ExecuteC(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnStringC);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCnn.Close();
    }

    public static void Execute3(string strSql)
    {
        SqlConnection sqlCnn = new SqlConnection(CnnString3);
        SqlCommand sqlCmd = new SqlCommand(strSql, sqlCnn);
        sqlCnn.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCnn.Close();
    }

    public static DataTable xls(string strSql, string path)
    {
        string cnn = "Provider=Microsoft.ACE.OLEDB.12.0;"
            + "Data Source=" + path + ";"
            + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";

        OleDbConnection sqlCnn = new OleDbConnection(cnn);
        OleDbDataAdapter sqlAdapter = new OleDbDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable rst = new DataTable();
        rst = objDS.Tables["data"];

        return rst;
    }
}