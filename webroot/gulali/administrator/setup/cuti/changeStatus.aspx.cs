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

public partial class change_status : System.Web.UI.Page
{

    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string x_get { get { return App.GetStr(this, "x"); } }
    protected string y_get { get { return App.GetStr(this, "y"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        App.Init(this);

        int id, x, y;
        string query = "";

        x = Convert.ToInt32(x_get);
        y = Convert.ToInt32(y_get);
        if (x > y)
        {
            DataTable rs = Db.Rs("select * from TBL_Leave_Type where Type_Order < '" + Cf.StrSql(x_get) + "' and Type_Order >= '" + Cf.StrSql(y_get) + "'");

            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string idx = rs.Rows[i]["Type_ID"].ToString();
                    string order = rs.Rows[i]["Type_Order"].ToString();
                    int update = Convert.ToInt32(rs.Rows[i]["Type_Order"].ToString()) + 1;

                    query = "update TBL_Leave_Type set Type_Order = '" + update.ToString() + "' where Type_ID='" + idx + "'";
                    Db.Execute(query);
                }
            }

            query = "update TBL_Leave_Type set Type_Order = '" + Cf.StrSql(y_get) + "' where Type_ID='" + Cf.StrSql(id_get) + "'";
            Db.Execute(query);

        }
        else
        {
            DataTable rs = Db.Rs("select * from TBL_Leave_Type where Type_Order > '" + Cf.StrSql(x_get) + "' and Type_Order <= '" + Cf.StrSql(y_get) + "'");

            if (rs.Rows.Count > 0)
            {
                for (int i = 0; i < rs.Rows.Count; i++)
                {
                    string idx = rs.Rows[i]["Type_ID"].ToString();
                    string order = rs.Rows[i]["Type_Order"].ToString();
                    int update = Convert.ToInt32(rs.Rows[i]["Type_Order"].ToString()) - 1;

                    query = "update TBL_Leave_Type set Type_Order = '" + update.ToString() + "' where Type_ID='" + idx + "'";
                    Db.Execute(query);
                }
            }

            query = "update TBL_Leave_Type set Type_Order = '" + Cf.StrSql(y_get) + "' where Type_ID='" + Cf.StrSql(id_get) + "'";
            Db.Execute(query);
        }

        Response.Redirect(Param.Path_Admin + "setup/cuti/");
    }

}
