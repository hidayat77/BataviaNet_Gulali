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
        DataTable rs = Db.Rs("Select * from TBL_Department");
        if (rs.Rows.Count < 1)
        {
            list2.Text = "There's no Available Employee at the moment";
        }
        else
        {
            string sql_where = "";
            if (!string.IsNullOrEmpty(Cf.StrSql(search_text)))
            {
                sql_where = " where Department_Name like '%" + search_text + "%'";
            }
            DataTable add_list_display = Db.Rs("select Department_Name from TBL_Department " + sql_where + " order by Department_Name asc");
            for (int a = 0; a < add_list_display.Rows.Count; a++)
            {
                list2.Text +=
                    "<tr class=\"contentlist\">" +
                            "<td>" + add_list_display.Rows[a]["Department_Name"].ToString() + "</td>" +
                            "<td><a style=\"text-decoration:none;cursor:pointer\" onclick=\"confirmAdd('" + add_list_display.Rows[a]["Department_Name"].ToString() + "');\"><input type=\"button\" name=\"" + add_list_display.Rows[a]["Department_Name"].ToString() + "\" value=\"Add\" /></a></td>" +
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
