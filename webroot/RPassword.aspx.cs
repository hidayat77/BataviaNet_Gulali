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

public partial class _rpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        username.Text = App.Username;
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(old_password) ? x : false;
            x = Fv.isComplete(new_password) ? x : false;
            x = Fv.isComplete(confirm_new_password) ? x : false;
            if (new_password.Text != confirm_new_password.Text)
            { x = false; note.Text = "<span style=\"color: red; font-weight:bold;\">Wrong Confirm New Password.</span>"; }
            return x;
        }
    }

    protected void BtnReset(object sender, EventArgs e)
    {
        if (valid)
        {
            if (old_password.Text != "")
            {
                string pass = Cf.StrSql(old_password.Text);
                string pass_new = Cf.StrSql(new_password.Text);
                string encryptionPassword = Param.encryptionPassword;
                string encrypted = Crypto.Encrypt(pass, encryptionPassword);
                string encrypted_new = Crypto.Encrypt(pass_new, encryptionPassword);

                DataTable check_pass = Db.Rs("Select User_Password from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");

                if (encrypted == check_pass.Rows[0]["User_Password"].ToString())
                {
                    string update = "update TBL_User "
                        + "set User_LastModified = getdate(), User_ResetPass_Date = getdate(), User_Password = '" + encrypted_new + "', User_ResetPass_Status = '1' where User_ID = " + Cf.StrSql(App.UserID);
                    Db.Execute(update);

                    this.Session.Abandon();
                    string path = "";
                    if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2") { path = Param.Path_Admin; } else if (App.UserIsAdmin == "3") { path = Param.Path_User; }
                    Response.Redirect(path + "dashboard/");
                }
                else { Js.Alert(this, "Invalid Old Password!"); }
            }
            else { Js.Alert(this, "Old Password is Empty!"); }
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
    }
}