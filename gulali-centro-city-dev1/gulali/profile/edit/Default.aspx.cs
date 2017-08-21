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

public partial class _update_profile : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);

        if (!IsPostBack)
        {
            if (mode == "1")
            {
                note.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                + "<h4 class=\"page-title\">Updated Success!</h4>"
                + "</div>";
            }
            else if (mode == "2")
            {
                note.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                + "<h4 class=\"page-title\">Password Successfully Changed!</h4>"
                + "</div>";
            }

            Submit.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            DataTable db = Db.Rs("select a.User_Name, b.Employee_ID, b.Employee_Full_Name, b.Employee_Phone_Number_Primary, b.Employee_Company_Email, b.Employee_Personal_Email, a.User_Photo from TBL_User a left join TBL_Employee b on a.Employee_ID = b.Employee_ID where a.User_ID = '" + App.UserID + "' ");
            user_name.Text = db.Rows[0]["User_Name"].ToString();
            full_name.Text = db.Rows[0]["Employee_Full_Name"].ToString();
            phone_number.Text = db.Rows[0]["Employee_Phone_Number_Primary"].ToString();

            if (!string.IsNullOrEmpty(db.Rows[0]["User_Photo"].ToString()))
            {
                Image1.ImageUrl = "/assets/images/user-profile/" + db.Rows[0]["User_Photo"].ToString();
            }
            else
            {
                Image1.ImageUrl = "/assets/images/no-user-image.gif";
            }
            update_profile.Update();
        }
    }

    protected void check_password(object sender, EventArgs e)
    {
        if (checkbox_password.Checked)
        {
            tr_password.Visible = true;
            tr_conf_password.Visible = true;
            tr_old_password.Visible = true;
        }
        else
        {
            tr_password.Visible = false;
            tr_conf_password.Visible = false;
            tr_old_password.Visible = false;
        }
        update_profile.Update();
    }

    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(oldpassword) ? x : false;
            x = Fv.isComplete(password) ? x : false;
            x = Fv.isComplete(confirm) ? x : false;
            //note.Text = "fail";
            note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                   + "<h4 class=\"page-title\">Please Complete This Textbox!</h4>"
                               + "</div>";
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        //try
        //{
        if (checkbox_password.Checked)
        {
            if (valid)
            {
                string pass = Cf.StrSql(oldpassword.Text);
                string pass_new = Cf.StrSql(password.Text);

                string encryptionPassword = Param.encryptionPassword;
                string encrypted = Crypto.Encrypt(pass, encryptionPassword);
                string encrypted_new = Crypto.Encrypt(pass_new, encryptionPassword);

                DataTable check_pass = Db.Rs("Select User_Password from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");
                DataTable cek_employee = Db.Rs("Select Employee_ID from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");

                if (encrypted == check_pass.Rows[0]["User_Password"].ToString())
                {
                    if (Employee_Photo.PostedFile != null && Employee_Photo.PostedFile.FileName != "")
                    {
                        DataTable employeeID = Db.Rs("select Employee_ID from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");
                        byte[] imageSize = new byte[Employee_Photo.PostedFile.ContentLength];
                        HttpPostedFile uploadedImage = Employee_Photo.PostedFile;
                        uploadedImage.InputStream.Read(imageSize, 0, (int)Employee_Photo.PostedFile.ContentLength);

                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

                        // Create SQL Command 

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Update TBL_User set User_Photo = @User_Photo where Employee_ID = " + employeeID.Rows[0]["Employee_ID"].ToString() + "";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;


                        SqlParameter UploadedImage = new SqlParameter("@User_Photo", SqlDbType.Image, imageSize.Length);
                        UploadedImage.Value = imageSize;
                        cmd.Parameters.Add(UploadedImage);
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        if (password.Text == confirm.Text)
                        {
                            string update = "update TBL_User "
                                + "set User_LastModified = getdate(), User_Password = '" + encrypted_new + "' where User_ID = " + Cf.StrSql(App.UserID);
                            Db.Execute(update);
                            note.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                                + "<h4 class=\"page-title\">Password Successfully Changed!</h4>"
                            + "</div>";
                        }
                        else
                        {
                            note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                               + "<h4 class=\"page-title\">Invalid New Password or Confirm Password!</h4>"
                           + "</div>";
                        }
                    }
                }
                else
                {
                    note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                               + "<h4 class=\"page-title\">Invalid Old Password!</h4>"
                           + "</div>";
                }

                //catch (Exception ) { }
            }
        }
        else
        {
            if (Employee_Photo.PostedFile != null && Employee_Photo.PostedFile.FileName != "")
            {
                DataTable employeeID = Db.Rs("select Employee_ID from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");
                byte[] imageSize = new byte[Employee_Photo.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = Employee_Photo.PostedFile;
                uploadedImage.InputStream.Read(imageSize, 0, (int)Employee_Photo.PostedFile.ContentLength);

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

                // Create SQL Command 

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update TBL_User set User_Photo = @User_Photo where Employee_ID = " + employeeID.Rows[0]["Employee_ID"].ToString() + "";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;


                SqlParameter UploadedImage = new SqlParameter("@User_Photo", SqlDbType.Image, imageSize.Length);
                UploadedImage.Value = imageSize;
                cmd.Parameters.Add(UploadedImage);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
            }
            note.Text = "<div style=\"background-color: #10c469; text-align: center;\">"
                            + "<h4 class=\"page-title\">Updated Success!</h4>"
                        + "</div>";
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (Fv.isValidFileSize_200kb(Employee_Photo)) //200kb convert dalam bentuk Byte
        {
            if (Employee_Photo.PostedFile != null && Employee_Photo.PostedFile.FileName != "")
            {
                //images//
                int w = 500; int h = 500;
                string Folder = "images/user-profile/";
                //delete old image if image update//
                DataTable rs = Db.Rs("select Employee_ID from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");
                DataTable fotouser = Db.Rs("select User_Photo from TBL_User where Employee_ID = '" + rs.Rows[0]["Employee_ID"].ToString() + "'");
                string nfoto = fotouser.Rows[0]["User_Photo"].ToString();
                string fto = Server.MapPath("~/assets/") + Folder + nfoto;
                if (nfoto != "")
                {
                    File.Delete(fto);
                }
                //images//
                string banner_path = "";
                if (Employee_Photo.HasFile)
                {
                    banner_path = uploadIncResize(h, w, Folder, Employee_Photo, true);
                }
                string get_foto = App.UserID + "_(Profile)_" + Cf.StrSql(banner_path);
                string update = "update TBL_User set User_Photo = '" + get_foto + "' where Employee_ID = '" + rs.Rows[0]["Employee_ID"].ToString() + "'";
                string updatelastmodified = "update TBL_User set User_LastModified = getdate() where User_ID = " + Cf.StrSql(App.UserID);
                Db.Execute(update);
                Db.Execute(updatelastmodified);
            }
            if (checkbox_password.Checked)
            {
                if (valid)
                {
                    string pass = Cf.StrSql(oldpassword.Text);
                    string pass_new = Cf.StrSql(password.Text);
                    string encryptionPassword = Param.encryptionPassword;
                    string encrypted = Crypto.Encrypt(pass, encryptionPassword);
                    string encrypted_new = Crypto.Encrypt(pass_new, encryptionPassword);

                    DataTable check_pass = Db.Rs("Select User_Password from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");
                    DataTable cek_employee = Db.Rs("Select Employee_ID from TBL_User where User_ID = '" + Cf.StrSql(App.UserID) + "'");

                    if (encrypted == check_pass.Rows[0]["User_Password"].ToString())
                    {

                        if (password.Text == confirm.Text)
                        {
                            string updatepass = "update TBL_User "
                                + "set User_LastModified = getdate(), User_Password = '" + encrypted_new + "' where User_ID = " + Cf.StrSql(App.UserID);
                            Db.Execute(updatepass);
                            Response.Redirect("/gulali/profile/edit/?mode=2");
                        }
                        else
                        {
                            note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                + "<h4 class=\"page-title\">Invalid New Password or Confirm Password!</h4>"
                            + "</div>";
                        }
                    }
                    else
                    {
                        note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                   + "<h4 class=\"page-title\">Invalid Old Password!</h4>"
                               + "</div>";
                    }
                }
            }
            else
            {
                Response.Redirect("/gulali/profile/edit?mode=1");
            }
        }
        else
        {
            note.Text = "<div style=\"background-color: #ff4242; text-align: center;\">"
                                   + "<h4 class=\"page-title\">File Image is Large</h4>"
                               + "</div>";
        }
    }

    protected string uploadIncResize(int h, int w, string Folder, FileUpload img, bool resize)
    {
        string upload_file = "";
        try
        {
            if (Path.GetExtension(img.FileName).ToLower() != ".jpg" || Path.GetExtension(img.FileName).ToLower() != ".png" || Path.GetExtension(img.FileName).ToLower() != ".jpeg")
            {
                string filename = Path.GetFileName(img.FileName);
                string dir = Folder;
                bool status = true;

                while (status)
                {
                    status = FileExists(filename, Server.MapPath("~/assets/" + Folder));
                    if (status)
                        filename = getRandomFileName() + System.IO.Path.GetExtension(img.PostedFile.FileName);
                }

                //teaser_image.SaveAs(Server.MapPath("~/images/") + Folder + filename);

                upload_file = filename;

                string large = Server.MapPath("~/assets/") + dir + App.UserID + "_(Profile)_" + filename;
                string temp = Server.MapPath("~/assets/") + dir + "test" + filename;

                img.PostedFile.SaveAs(temp);
                if (resize)
                {
                    Imgh.Crop(temp, large, h, w);
                    File.Delete(temp);
                }
                File.Delete(temp);
            }
            else
            {
                Js.Alert(this, "Upload status: Only JPEG , PNG or GIF files are allowed!");
            }

        }
        catch (Exception ex)
        {
            Js.Alert(this, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
        }
        return upload_file;
    }

    protected string getRandomFileName()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        int newFile = 0;
        newFile = rand.Next(1, 9999999);

        return newFile.ToString();
    }

    protected bool FileExists(string filename, string path)
    {
        FileInfo imageFile = new FileInfo(path + filename);
        bool fileStatus = imageFile.Exists;
        //Js.Alert(this,getRandomFileName() + System.IO.Path.GetExtension(teaser_image.PostedFile.FileName));

        return fileStatus;
    }

    protected void BtnCancel(object sender, EventArgs e)
    {
        Response.Redirect("/gulali/profile/");
    }
}