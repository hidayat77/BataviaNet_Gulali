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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class _admin_setup_usr_management_detail_add : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> User Management", "insert");
        if (!IsPostBack)
        {
            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");

            ddl_groupCMSPrev();
            ddl_Employee_NamePrev();
            update.Update();

            href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/user-management/data-user/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";
        }
    }

    protected void ddl_groupCMSPrev()
    {
        DataTable rsa = Db.Rs("select * from TBL_Role where Role_ID != 1 order by Role_ID asc");

        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_groupCMS.Items.Add(new ListItem(rsa.Rows[i]["Role_Name"].ToString(), rsa.Rows[i]["Role_ID"].ToString()));
            }
        }
    }

    protected void ddl_Employee_NamePrev()
    {
        DataTable rsa = Db.Rs("select Employee_ID, Employee_Full_Name from TBL_Employee where Employee_ID != 1 order by Employee_Full_Name asc");

        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_Employee_Name.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
        }
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(username) ? x : false;
            x = Fv.isComplete(password) ? x : false;
            x = Fv.isComplete(confirm) ? x : false;
            if (password.Text != confirm.Text)
            { x = false; Alert_y.Text = "<span style=\"color: red; font-weight:bold;\">Wrong Ulangi Password.</span>"; }
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        //try
        //{
        if (valid)
        {
            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                DataTable cek_existUsername = Db.Rs("Select * from TBL_User where User_Name = '" + Cf.StrSql(username.Text) + "'");

                if (cek_existUsername.Rows.Count < 1)
                {
                    DataTable check_user = Db.Rs("select top 1 *, (user_ID+1) as userPlus from TBL_User order by User_TglCreate desc");

                    // untuk check double input
                    if (check_user.Rows[0]["User_Name"].ToString() != Cf.StrSql(username.Text))
                    {
                        //convert password
                        string user = Cf.StrLogin2(username.Text);
                        string pass = Cf.StrSql(password.Text);

                        string encryptionPassword = Param.encryptionPassword;
                        string encrypted = Crypto.Encrypt(pass, encryptionPassword);
                        string original = Crypto.Decrypt(encrypted, encryptionPassword);

                        string isAdmin_val = "'3'";
                        if (isAdmin.Checked == true) { isAdmin_val = "'2'"; }

                        string insert = "insert into TBL_User (User_Name, User_Authorized, User_Password, User_LastLogin, User_LastIP, User_LastHostName, User_TglBlokir, User_CountLogin, User_CountSalahPass, User_LastModified, User_TglCreate, Role_ID, Employee_ID, User_IsAdmin)"
                                        + "values('" + Cf.StrSql(username.Text) + "', 'Y', '" + Cf.StrSql(encrypted) + "', getdate(), 0, 0, getdate(), 0, 0, getdate(), getdate(), '" + Cf.StrSql(ddl_groupCMS.SelectedValue) + "', '" + Cf.StrSql(ddl_Employee_Name.SelectedValue) + "', " + isAdmin_val + ")";
                        Db.Execute(insert);
                    }
                    Response.Redirect(Param.Path_Admin + "setup/user-management/data-user/");
                }
                else
                {
                    Js.Alert(this, "Username sudah terdaftar!");
                }
            }
        }
        //}
        //catch (Exception ) { }
    }
}