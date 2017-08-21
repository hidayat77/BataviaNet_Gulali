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

public partial class _notifikasi : System.Web.UI.Page
{
    protected string link { get { return App.GetStr(this, "link"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        left_menu_notification();
    }

    protected void left_menu_notification()
    {
        DataTable employee = Db.Rs("select b.Employee_ID from TBL_User a left join TBL_Employee b on a.Employee_ID = b.Employee_ID where a.User_ID = '" + App.UserID + "' ");
        string employee_id = employee.Rows[0]["Employee_ID"].ToString();

        view_inbox(employee_id);

        DataTable notifcount = Db.Rs("select Notif_StatusPage from TBL_Notification where Notif_Employee_ID = '" + Cf.Int(employee_id) + "' and Notif_StatusPage = '0'");

        string count_notif = "", class_ = "";
        if (notifcount.Rows.Count > 0) { count_notif = "<span>(" + notifcount.Rows.Count + ")</span>"; class_ = "class=\"active\""; }
        inbox_left_menu.Text = "<li " + class_ + " style=\"text-align:center;\"><a href=\"javascript:void(0);\">Notifikasi " + count_notif + "</a>";
    }

    protected void view_inbox(string employee_id)
    {
        DataTable notif = Db.Rs("select * from TBL_Notification hn join TBL_Employee he on hn.Notif_UserCreate = he.Employee_ID where Notif_Employee_ID = '" + Cf.Int(employee_id) + "' order by Notif_DateCreate desc");
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
                string date_year = convert_date.ToString("yyyy");

                DateTime year = Convert.ToDateTime(DateTime.Now);
                string now_year = year.ToString("yyyy");

                string time = "";
                if (date_year == now_year)
                {
                    string date = convert_date.ToString("dd MMM");
                    string now = year.ToString("dd MMM");

                    if (date == now) { time = convert_date.ToString("HH:mm"); }
                    else { time = date; }
                }
                else { time = convert_date.ToString("dd/mm/yyyy"); }

                string foto = "";
                if (!string.IsNullOrEmpty(notif_data_employee.Rows[0]["Employee_Photo"].ToString()))
                {
                    foto = "/images/user-profile/" + notif_data_employee.Rows[0]["Employee_Photo"].ToString();
                }
                else { foto = "/images/no-user-image.gif"; }

                string class_ = "", href = "<a href=\"" + link + "\" style=\"color: #797979 !important;\">";
                if (notif.Rows[i]["Notif_StatusPage"].ToString() == "0")
                {
                    class_ = "class=\"unread\"";
                    href = "<a href=\"" + link + "\" style=\"font-weight: 600 !important; color: #555 !important;\">";
                }

                notifikasi.Text += "<li " + class_ + ">"
                                    + href
                                        + "<div class=\"col col-1\"><p class=\"title\">" + full_name + "</p></div>"
                                    + "</a>"
                                        + "<div class=\"col col-2\" style=\"left: 320px !important;\">"
                                    + href
                                            + "<div class=\"subject\">"
                                            + "<span class=\"teaser\">" + desc + "</span>"
                                            + "</div>"
                                            + "<div class=\"date\" style=\"padding-left: 0px; !important\">" + time + "</div>"
                                    + "</a>"
                                            + "<div class=\"date\" style=\"padding-left: 140px; !important\">"
                                                + "<a onClick=\"confirmdel('delete/?id=" + notif.Rows[i]["Notif_ID"].ToString() + "');\" style=\"padding-right:20px;\">"
                                                        + "<i style=\"font-size:22px; color:#ff5b5b;cursor:pointer;\" class=\"fa fa-trash-o\"></i>"
                                                + "</a>"
                                            + "</div>"
                                        + "</div>"
                                    + "</a>"
                                + "</li><hr style=\"margin: 0px !important;\" />";
            }
        }
        else { notifikasi.Text = "<li class=\"list-group-item\">Tidak Ada Notifikasi</li>"; }
    }
}