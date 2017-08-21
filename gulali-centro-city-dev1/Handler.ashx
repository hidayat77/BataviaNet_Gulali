<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        // Create SQL Command 

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select Employee_Photo from TBL_Employee where Employee_ID = @Employee_ID";
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;

        SqlParameter ImageID = new SqlParameter("@Employee_ID", System.Data.SqlDbType.Int);
        ImageID.Value = context.Request.QueryString["Employee_ID"];
        cmd.Parameters.Add(ImageID);
        con.Open();
        SqlDataReader dReader = cmd.ExecuteReader();
        dReader.Read();
        context.Response.BinaryWrite((byte[])dReader["Employee_Photo"]);
        dReader.Close();
        con.Close();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
 
    

}