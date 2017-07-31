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

public partial class _del : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
			App.CekPageUser(this);
			
            // try
            // {
                string del_flag = "delete from TBL_Payroll_Flag where Flag_ID = '" + id + "'";
                Db.Execute2(del_flag);

                string del_pay = "delete from TBL_Payroll where Flag_ID = '" + id + "'";
                Db.Execute2(del_pay);

                string del_det_allowance = "delete from TBL_Payroll_det_allowance where det_allowance_Flag_ID = '" + id + "'";
                Db.Execute2(del_det_allowance);
			// }
            // catch (Exception ) { }
                Response.Redirect("../");
        }
    }
}