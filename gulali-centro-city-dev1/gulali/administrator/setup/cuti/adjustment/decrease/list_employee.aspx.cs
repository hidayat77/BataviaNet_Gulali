using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;
//using System.Linq.Dynamic;



public partial class list : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        if (!IsPostBack)
        {
            fill(text_search.Text);
        }
    }

    public void fill(string search_text)
    {
        list2.Text = "";
        DataTable rs = Db.Rs("Select * from TBL_Employee where Employee_ID != '1' and Employee_ID != '" + Cf.StrSql(id_get) + "'");
        if (rs.Rows.Count < 1)
        {
            list2.Text = "There's no Available Employee at the moment";
        }
        else
        {
            string sql_where = "";
            if (!string.IsNullOrEmpty(Cf.StrSql(search_text)))
            {
                sql_where = " and (B.Department_Name like '%" + search_text + "%' or C.Division_Name like '%" + search_text + "%' or D.Position_Name like '%" + search_text + "%' or A.Employee_Full_Name like '%" + search_text + "%')";
            }
            //DataTable add_list_display = Db.Rs("select t.Position_Name, e.Employee_Full_Name from TBL_Employee e, TBL_Title t where e.Title_ID=t.Title_ID and e.Employee_ID != '1' and e.Employee_ID != '" + Cf.StrSql(id_get) + "' order by t.Position_Name asc");
            DataTable add_list_display = Db.Rs("select A.Employee_ID as 'id', B.Department_Name as 'dep_Name', "
            + "C.Division_Name as 'Div_name', D.Position_Name as 'Position_Name', A.Employee_Full_Name as 'Employee_Full_Name', A.Employee_Last_Name as 'employee_last_name' from TBL_Employee A "
            + "left join TBL_Department B on A.Department_ID = B.Department_ID "
            + "left join TBL_Division C on A.Division_ID = C.Division_ID "
            + "left join TBL_Position D on A.Position_ID = D.Position_ID "
            + "where A.Employee_ID != '1' " + sql_where + ""
            + "order by B.Department_Name asc, "
            + "C.Division_Name asc, "
            + "D.Position_Name asc, A.Employee_Full_Name asc");
            for (int a = 0; a < add_list_display.Rows.Count; a++)
            {
                list2.Text +=
                    "<tr class=\"contentlist\">" +
                            "<td>" + add_list_display.Rows[a]["dep_Name"].ToString() + "</td>" +
                            "<td>" + add_list_display.Rows[a]["Div_name"].ToString() + "</td>" +
                            "<td>" + add_list_display.Rows[a]["Position_Name"].ToString() + "</td>" +
                            "<td>" + add_list_display.Rows[a]["Employee_Full_Name"].ToString() + "</td>" +
                            "<td><a style=\"text-decoration:none;cursor:pointer\" onclick=\"confirmAdd('" + add_list_display.Rows[a]["Employee_Full_Name"].ToString() + "');\"><input type=\"button\" name=\"" + add_list_display.Rows[a]["Employee_Full_Name"].ToString() + "\" value=\"Add\" /></a></td>" +
                         "</tr>";

            }
            list2.Text += "</ul>";

        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        fill(text_search.Text);
    }
}
