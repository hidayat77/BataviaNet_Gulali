using System;
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
using AjaxControlToolkit;

public partial class usercontrol_menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable content = Db.Rs("Select General_Content from TBL_General where General_Area = 'Footer'");
            content_footer.Text = content.Rows[0]["General_Content"].ToString();
        }
    }
}