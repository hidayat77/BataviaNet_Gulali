using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);

        DataTable employee = Db.Rs("select Employee_Full_Name, Employee_Sum_LeaveBalance, Employee_Photo, Department_Name, Division_Name from TBL_Employee a left join TBL_Department b on a.Department_ID = b.Department_ID left join TBL_Division c on a.Division_ID = c.Division_ID  where Employee_ID = '" + App.Employee_ID + "' ");

        if (employee.Rows.Count > 0)
        {
            string foto_link = "";
            if (!string.IsNullOrEmpty(employee.Rows[0]["Employee_Photo"].ToString())) { foto_link = "/assets/images/employee-photo/" + employee.Rows[0]["Employee_Photo"].ToString(); }
            else { foto_link = "/assets/images/no-user-image.gif"; }

            link_foto.Text = "<a href=\"" + foto_link + "\" class=\"image-popup\" title=\"Screenshot-1\"><img src=\"" + foto_link + "\" class=\"thumb-img\" alt=\"work-thumbnail\" style=\"max-width:150px;\"></a>";

            nama.Text = employee.Rows[0]["Employee_Full_Name"].ToString();
            departemen.Text = employee.Rows[0]["Department_Name"].ToString();
            divisi.Text = employee.Rows[0]["Division_Name"].ToString();
            balance.Text = employee.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
        }
    }
}