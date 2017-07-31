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

public partial class _Default_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Cf.StrSql(App.UserID) != "" && App.Username != "")
            {
                string path = "";
                if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path = Param.Path_User; }
                Response.Redirect(path + "dashboard/");
            }

            DataTable content = Db.Rs("Select General_Content from TBL_General where General_Area = 'Logo'");
            logo.Text = "<img src=\"/assets/images/general/" + content.Rows[0]["General_Content"].ToString() + "\" alt=\"logo gulali login page\" style=\"width: 185px; height: auto;\" />";
        }
    }

    protected void BtnSignIn(object sender, EventArgs e)
    {
        if (valid_checked_textbox)
        {
            if (Valid())
            {
                if (!Page.IsValid) { return; }
                else
                {
                    try
                    {
                        string usernm = Cf.StrLogin(Cf.Upper(username.Text));
                        DataTable rs = Db.Rs("Select User_ID, User_Name, User_LastIP, a.Employee_ID, c.Employee_Full_Name, User_LastLogin, User_ResetPass_Status, User_IsAdmin, a.Role_ID, b.Role_Name from TBL_User a join TBL_Role b on a.Role_ID = b.Role_ID join TBL_Employee c on a.Employee_ID = c.Employee_ID where User_Name = '" + usernm + "'");
                        if (rs.Rows.Count > 0)
                        {
                            App.UserID = rs.Rows[0]["User_ID"].ToString();
                            App.Username = rs.Rows[0]["User_Name"].ToString();
                            App.UserIP = rs.Rows[0]["User_LastIP"].ToString();
                            App.Employee_ID = rs.Rows[0]["Employee_ID"].ToString();
                            App.Employee_Name = rs.Rows[0]["Employee_Full_Name"].ToString();
                            App.LastLogin = rs.Rows[0]["User_LastLogin"].ToString();
                            App.UserIsAdmin = rs.Rows[0]["User_IsAdmin"].ToString();
                            App.Role_ID = rs.Rows[0]["Role_ID"].ToString();
                            App.Role_Name = rs.Rows[0]["Role_Name"].ToString();
                            string SessionID = HttpContext.Current.Session.SessionID;

                            Db.Execute("Update TBL_User set User_CountLogin = User_CountLogin + 1, User_LastLogin = getdate(), User_LastIP = '" + Cf.StrSql(App.IP) + "', user_LastHostName = '" + Cf.StrSql(App.userLastHostName) + "' where User_Authorized = 'Y' and User_Name = '" + usernm + "' ");

                            string insert = "Insert into TBL_Log_History(Log_Date, Log_Activity, Log_IP, Log_Hostname, Log_UserID, Log_Username, Log_UserRole, User_IsAdmin, Log_Employee_Name) VALUES "
                                + "(getdate(), 'LOGIN', '" + App.UserIP + "', '" + App.userLastHostName + "', '" + App.UserID + "', '" + App.Username + "', '" + App.Role_Name + "', '" + App.UserIsAdmin + "', '" + App.Employee_Name + "')";
                            Db.Execute(insert);

                            //check reset password
                            string status_reset = rs.Rows[0]["User_ResetPass_Status"].ToString();

                            if (status_reset.Equals("True"))
                            {
                                string path_dashboard = "";
                                if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path_dashboard = Param.Path_Admin + "dashboard/"; } else if (App.UserIsAdmin == "3") { path_dashboard = Param.Path_User + "dashboard/"; }

                                Response.Redirect(path_dashboard);
                            }
                            else
                            {
                                Response.Redirect("/RPassword.aspx");
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                Js.Alert(this, "Username Tidak Ditemukan");
            }
        }
    }

    private bool Valid()
    {
        string user = Cf.StrLogin2(username.Text);
        string pass = Cf.StrSql(password.Text);

        string encryptionPassword = Param.encryptionPassword;

        string encrypted = Crypto.Encrypt(pass, encryptionPassword);
        string original = Crypto.Decrypt(encrypted, encryptionPassword);

        try
        {
            int blok = Db.SingleInteger(" select COUNT(*) from TBL_User where User_Authorized = 'Y' and User_Name = '" + Cf.StrSql(user) + "' and User_TglBlokir > getdate() ");
            if (blok >= 1)
            {
                Js.Alert(this, "UserID telah diblokir, Silahkan Mencoba Beberapa Menit Lagi");
                return false;
            }
            int salah = Db.SingleInteger("Select User_CountSalahPass from TBL_User where User_Authorized = 'Y' and User_Name = '" + Cf.StrSql(user) + "' ");
            if (salah > 3)
            {
                Js.Alert(this, "UserID telah diblokir, Silahkan Mencoba Beberapa Menit Lagi");
                return false;
            }
            int countUser = Db.SingleInteger("select COUNT(*) from TBL_User where User_Authorized = 'Y' and User_Name = '" + Cf.StrSql(user) + "' ");
            if (countUser > 0)
            {
                DataTable rs_user = Db.Rs("Select * from TBL_User where User_Name = '" + Cf.StrSql(user) + "'");
                if (rs_user.Rows.Count > 0)
                {
                    if (rs_user.Rows[0]["User_Password"].ToString() != encrypted)
                    {

                        try
                        {
                            Db.Execute("update TBL_User set User_CountSalahPass = User_CountSalahPass + 1 where User_Name = '" + Cf.StrSql(user) + "' ");
                            if (salah + 1 >= 3)
                            {
                                App.UserBlokir(user);
                            }
                            Js.Alert(this, " Setelah salah password lebih dari 3 x, UserID anda akan diblokir, Invalid Password " + (salah + 1).ToString() + " x ");
                            return false;
                        }
                        catch
                        {
                            Js.Alert(this, "UserID atau Password Salah");
                            return false;
                        }
                    }
                    else
                    {
                        Db.Execute("update TBL_User set User_CountSalahPass = 0 where User_Name = '" + Cf.StrSql(user) + "' ");
                        return true;
                    }
                }
                else
                {
                    Js.Alert(this, "UserID atau Password Salah");
                    return false;
                }
            }
            else
            {
                Js.Alert(this, "Username has been blocked, Please contact Human Resource.");
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    private bool valid_checked_textbox
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(username) ? x : false;
            x = Fv.isComplete(password) ? x : false;
            return x;
        }
    }
}