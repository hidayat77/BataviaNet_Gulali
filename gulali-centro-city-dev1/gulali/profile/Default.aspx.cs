using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;

public partial class _profile : System.Web.UI.Page
{
    protected string notif { get { return App.GetStr(this, "notif"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        DataTable employee = Db.Rs("select b.Employee_ID, a.User_Photo from TBL_User a left join TBL_Employee b on a.Employee_ID = b.Employee_ID where a.User_ID = '" + App.UserID + "' ");

        if (!string.IsNullOrEmpty(employee.Rows[0]["User_Photo"].ToString()))
        {
            Image1.ImageUrl = "/assets/images/user-profile/" + employee.Rows[0]["User_Photo"].ToString();
        }
        else { Image1.ImageUrl = "/assets/images/no-user-image.gif"; }

        view();
    }

    protected void view()
    {
        DataTable db = Db.Rs("select * from TBL_User a left join TBL_Employee b on a.Employee_ID = b.Employee_ID where a.User_ID = '" + App.UserID + "' ");
        if (db.Rows.Count > 0)
        {
            user_name.Text = db.Rows[0]["User_Name"].ToString();
            full_name.Text = db.Rows[0]["Employee_Full_Name"].ToString();
            phone_number.Text = db.Rows[0]["Employee_Phone_Number_Primary"].ToString();
            string divisi = db.Rows[0]["Division_ID"].ToString();
            string idpegawai = db.Rows[0]["Employee_ID"].ToString();

            DataTable fr = Db.Rs("select a.Employee_ID, a.Employee_Full_Name, c.User_Photo, b.Position_Name from TBL_Employee a left join TBL_Position b on a.Position_ID = b.Position_ID join TBL_User c on c.Employee_ID = a.Employee_ID where a.Division_ID = '" + divisi + "' and a.Employee_ID not in('1', '" + idpegawai + "') ");

            StringBuilder x = new StringBuilder();
            if (fr.Rows.Count > 0)
            {
                for (int i = 0; i < fr.Rows.Count; i++)
                {
                    string avatar = "/assets/images/no-user-image.gif"; ;
                    if (!string.IsNullOrEmpty(fr.Rows[i]["User_Photo"].ToString()))
                    { avatar = "/assets/images/user-profile/" + fr.Rows[i]["User_Photo"].ToString(); }

                    x.Append("<li class=\"list-group-item2\" style=\"height: 60px;\">"
                                + "<a href=\"#\" class=\"user-list-item\">"
                                + "<div class=\"avatar\">"
                                    + "<img src=\"" + avatar + "\" alt=\"\" />"
                                + "</div>"
                                + "<div class=\"user-desc\">"
                                    + "<span class=\"name\">" + fr.Rows[i]["Employee_Full_Name"].ToString() + "</span>"
                                    + "<span class=\"desc\">" + fr.Rows[i]["Position_Name"].ToString() + "</span>"
                                + "</div>"
                                + "</a>"
                                + "</li>");
                }
                isi.Text = x.ToString();
            }
            else
            {
                isi.Text = "Data Tidak Ditemukan!";
            }
        }
    }
}