using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TreeTest : System.Web.UI.Page
{
    public static string CnnString
    { get { return ConfigurationManager.ConnectionStrings["CnnStr3"].ConnectionString; } }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append(@"
SELECT [TREE_ID]
      ,[TREE_NAME]
      ,[TREE_PARENT_ID]
  FROM [MyDB].[dbo].[TBL_TEST_TREE]
");
        try
        {
            DataTable dt = GetDataTable(sbQuery.ToString(), CnnString);
            Load_tree(dt);
        }
        catch
        {
        }
    }
    public static DataTable GetDataTable(string strSql, string strConn)
    {
        SqlConnection sqlCnn = new SqlConnection(strConn);
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strSql, sqlCnn);
        DataSet objDS = new DataSet();
        sqlAdapter.Fill(objDS, "data");
        sqlCnn.Close();

        DataTable dt = new DataTable();
        dt = objDS.Tables["data"];

        return dt;
    }
    public void Load_tree(DataTable dtData)
    {

        TreeView1.Nodes.Clear();
        foreach (DataRow dr in dtData.Rows)
        {
            if ((int)dr["TREE_PARENT_ID"] == 0)
            {
                TreeNode tnParent = new TreeNode();
                tnParent.Text = dr["TREE_NAME"].ToString();
                string value = dr["TREE_ID"].ToString();
                tnParent.Expand();
                TreeView1.Nodes.Add(tnParent);
                FillChild(dtData, tnParent, value);
            }
        }
    }

    public int FillChild(DataTable dtData,TreeNode parent, string iID)
    {
        int iCheck = Convert.ToInt32(iID);
        
        var lst = (from item in dtData.AsEnumerable()
                            where item.Field<int>("TREE_PARENT_ID") == iCheck
                            select item).ToList();
        if (lst.Count > 0)
        {
            DataTable dtChild = (from item in dtData.AsEnumerable()
                                 where item.Field<int>("TREE_PARENT_ID") == iCheck
                                 select item).CopyToDataTable();
            if (dtChild.Rows.Count > 0)
            {
                foreach (DataRow dr in dtChild.Rows)
                {
                    TreeNode tnChild = new TreeNode();
                    tnChild.Text = dr["TREE_NAME"].ToString().Trim();
                    string temp = dr["TREE_ID"].ToString();
                    tnChild.Collapse();
                    parent.ChildNodes.Add(tnChild);
                    FillChild(dtData, tnChild, temp);
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    
}