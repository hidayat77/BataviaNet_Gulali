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

public partial class usercontrol_header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable employee = Db.Rs("select b.Employee_ID, a.User_Photo from TBL_User a left join TBL_Employee b on a.Employee_ID = b.Employee_ID where a.User_ID = '" + App.UserID + "' ");
            if (employee.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(employee.Rows[0]["User_Photo"].ToString()))
                { Image1.ImageUrl = "/assets/images/user-profile/" + employee.Rows[0]["User_Photo"].ToString(); }
                else { Image1.ImageUrl = "/assets/images/no-user-image.gif"; }
            }

            if (App.UserIsAdmin == "1") { implementor.Visible = true; }

            string path_notif = "";
            if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path_notif = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path_notif = Param.Path_User; }

            name.Text = App.Employee_Name;

            DataTable content = Db.Rs("Select General_Content from TBL_General where General_Area = 'Logo Dark'");
            logo.Text = "<a href=\"" + path_notif + "/dashboard\" class=\"logo\"><img src=\"/assets/images/general/" + content.Rows[0]["General_Content"].ToString() + "\" height=\"40\"></a>";

            DataTable notif = Db.Rs("select top 7 * from TBL_Notification hn join TBL_Employee he on hn.Notif_UserCreate = he.Employee_ID where Notif_Employee_ID = '" + employee.Rows[0]["Employee_ID"].ToString() + "' and notif_link like '" + path_notif + "%' order by Notif_DateCreate desc");
            if (notif.Rows.Count > 0)
            {
                for (int i = 0; i < notif.Rows.Count; i++)
                {
                    DataTable notif_data_employee = Db.Rs("select Employee_Full_Name, Employee_Photo from TBL_Employee where Employee_ID = '" + notif.Rows[i]["Notif_UserCreate"].ToString() + "'");
                    string full_name = notif_data_employee.Rows[0]["Employee_Full_Name"].ToString();
                    string link = notif.Rows[i]["Notif_Link"].ToString();

                    string desc = "";
                    //notification_user_leave_req
                    if (notif.Rows[i]["Leave_ID"].ToString() != "0" && (notif.Rows[i]["Notif_Remarks"].ToString().Contains("-> User)") || notif.Rows[i]["Notif_Remarks"].ToString() == "Create Leave Request(User)"))
                    {
                        DataTable SPV = Db.Rs("select e.Employee_Full_Name from TBL_Employee e join TBL_Leave l on e.Employee_ID=l.Employee_ID where l.Leave_ID = " + notif.Rows[i]["Leave_ID"].ToString() + "");

                        if (notif.Rows[i]["Notif_UserCreate"].ToString() == Cf.StrSql(App.Employee_ID))
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " create Leave Request";
                        }
                        else if (notif.Rows[i]["Notif_Remarks"].ToString() == "Reject Leave Request(Leave Monitor -> User)" || notif.Rows[i]["Notif_Remarks"].ToString() == "Reject Leave Request (Supervisor -> User)")
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " has Rejected " + SPV.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request";
                        }
                        else
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " has Approved " + SPV.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request";
                        }
                    }

                    //notification_spv
                    if (notif.Rows[i]["Notif_Remarks"].ToString().Contains("User -> Supervisor") || notif.Rows[i]["Notif_Remarks"].ToString().Contains("Leave Monitor -> Supervisor"))
                    {
                        DataTable LM = Db.Rs("select e.Employee_Full_Name, Leave_StatusLeave from TBL_Employee e join TBL_Leave l on e.Employee_ID = l.Employee_ID where Leave_ID = '" + notif.Rows[i]["Leave_ID"].ToString() + "'");

                        string decision = "";
                        if (notif.Rows[i]["Notif_Remarks"].ToString().Contains("Leave Monitor -> Supervisor)"))
                        {
                            if (LM.Rows[0]["Leave_StatusLeave"].ToString() == "2") { decision = "Approve"; }
                            else if (LM.Rows[0]["Leave_StatusLeave"].ToString() == "3") { decision = "Reject"; }

                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " has " + decision + " " + LM.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request";
                        }
                        else if (notif.Rows[i]["Notif_Remarks"].ToString().Contains("User -> Supervisor)"))
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " create Leave Request";
                        }
                    }

                    //notification_usermonitor
                    if (notif.Rows[i]["Notif_Remarks"].ToString().Contains("-> Leave Monitor)"))
                    {
                        DataTable SPV = Db.Rs("select e.Employee_Full_Name from TBL_Employee e join TBL_Leave l on e.Employee_ID=l.Employee_ID where Leave_ID = " + notif.Rows[i]["Leave_ID"].ToString() + "");

                        if (notif.Rows[i]["Notif_Remarks"].ToString() == "Create Leave Request(User -> Leave Monitor)")
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " create Leave Request";
                        }
                        else if (notif.Rows[i]["Notif_Remarks"].ToString() == "Reject Leave Request (Supervisor -> Leave Monitor)")
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " has Rejected " + SPV.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request";
                        }
                        else
                        {
                            desc = notif.Rows[i]["Employee_Full_Name"].ToString() + " has Approved " + SPV.Rows[0]["Employee_Full_Name"].ToString() + "'s Leave Request";
                        }
                    }

                    //date
                    DateTime convert_date = Convert.ToDateTime(notif.Rows[i]["Notif_DateCreate"].ToString());
                    DateTime now = DateTime.Now;
                    string date = convert_date.ToString("dd/MM/yyyy");

                    int days = (now.Date - convert_date.Date).Days;
                    int bagi = 0;
                    string time = "";
                    if (days >= 365)
                    {
                        bagi = days / 365;
                        time = bagi.ToString() + " Year Ago";
                    }
                    else if (days >= 30)
                    {
                        bagi = days / 30;
                        time = bagi.ToString() + " Month Ago";
                    }
                    else if (days >= 7)
                    {
                        bagi = days / 7;
                        time = bagi.ToString() + " Week Ago";
                    }
                    else if (days < 7)
                    {
                        if (days == 0)
                        {
                            //string dates = convert_date.ToString();
                            DateTime h_now = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                            DateTime h_last = Convert.ToDateTime(convert_date.ToString("HH:mm:ss"));
                            var lalu = (h_now - h_last).TotalSeconds;
                            int second = Convert.ToInt32(lalu);

                            if (second >= 3600)
                            {
                                bagi = second / 3600;
                                time = bagi.ToString() + " Hours Ago";
                            }
                            else if (second >= 60)
                            {
                                bagi = second / 60;
                                time = bagi.ToString() + " Minutes Ago";
                            }
                            else
                            {
                                bagi = second;
                                time = bagi.ToString() + " Second Ago";
                            }
                        }
                        else
                        {
                            bagi = days;
                            time = bagi.ToString() + " Days Ago";
                        }
                    }

                    string foto = "";
                    if (!string.IsNullOrEmpty(notif_data_employee.Rows[0]["Employee_Photo"].ToString()))
                    {
                        foto = "/images/user-profile/" + notif_data_employee.Rows[0]["Employee_Photo"].ToString();
                    }
                    else
                    {
                        foto = "/images/no-user-image.gif";
                    }

                    ////////////////////////////////
                    if (notif.Rows[i]["Notif_StatusPage"].ToString() == "0")
                    {
                        notifikasi.Text += " <li class=\"list-group-item active\">"
                              + "<a href=\"" + link + "\" class=\"user-list-item\">"
                                  + "<div class=\"avatar\">"
                                      + "<img src=\"/assets" + foto + "\" alt=\"\">"
                                  + "</div>"
                                  + "<div class=\"user-desc\">"
                                      + "<span class=\"name\">" + full_name + "</span>"
                                      + "<span class=\"desc\">" + desc + "</span>"
                                      + "<span class=\"time\">" + time + " " + date + "</span>"
                                  + "</div>"
                              + "</a>"
                          + "</li>";
                        dot_notif.Visible = true;
                        //dot_notif.Text = "<div class=\"noti-dot\"><span class=\"dot\"></span><span class=\"pulse\"></span></div>";
                    }
                    else
                    {
                        notifikasi.Text += " <li class=\"list-group-item\">"
                              + "<a href=\"" + link + "\" class=\"user-list-item\">"
                                  + "<div class=\"avatar\">"
                                      + "<img src=\"/assets" + foto + "\" alt=\"\">"
                                  + "</div>"
                                  + "<div class=\"user-desc\">"
                                      + "<span class=\"name\">" + full_name + "</span>"
                                      + "<span class=\"desc\">" + desc + "</span>"
                                      + "<span class=\"time\">" + time + " " + date + "</span>"
                                  + "</div>"
                              + "</a>"
                          + "</li>";
                        //dot_notif.Text = "";
                    }
                }
            }
            else
            {
                notifikasi.Text = "<li class=\"list-group-item\">Tidak Ada Notifikasi</li>";
                //dot_notif.Text = "";
            }

        }
    }
}