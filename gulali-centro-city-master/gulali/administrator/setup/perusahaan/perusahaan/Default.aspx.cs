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

public partial class _admin_setup_perusahaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "update");

        tab_perusahaan.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/perusahaan/\">Perusahaan</a>";
        tab_organisasi.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\">Organisasi</a>";
        tab_posisi.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/posisi/\">Posisi</a>";
        tab_peraturan_kantor.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/peraturan-kantor/\">Peraturan Kantor</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/perusahaan/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";

        if (!IsPostBack)
        {
            DataTable preferences1 = Db.Rs("Select General_Content from TBL_General where General_ID=1");
            DataTable preferences2 = Db.Rs("Select General_Content from TBL_General where General_ID=2");
            DataTable preferences3 = Db.Rs("Select General_Content from TBL_General where General_ID=3");
            DataTable preferences4 = Db.Rs("Select General_Content from TBL_General where General_ID=4");
            DataTable preferences5 = Db.Rs("Select General_Content from TBL_General where General_ID=5");
            DataTable preferences6 = Db.Rs("Select General_Content from TBL_General where General_ID=6");

            company_name.Text = preferences1.Rows[0]["General_Content"].ToString();
            about.Value = preferences2.Rows[0]["General_Content"].ToString();

            if (!string.IsNullOrEmpty(preferences3.Rows[0]["General_Content"].ToString())) { logo_gulali_img.ImageUrl = "/assets/images/general/" + preferences3.Rows[0]["General_Content"] + ""; }
            else { logo_gulali_img.ImageUrl = "/assets/images/no-image.png"; }

            if (!string.IsNullOrEmpty(preferences4.Rows[0]["General_Content"].ToString())) { logo_dark_img.ImageUrl = "/assets/images/general/" + preferences4.Rows[0]["General_Content"] + ""; }
            else { logo_dark_img.ImageUrl = "/assets/images/no-image.png"; }

            if (!string.IsNullOrEmpty(preferences5.Rows[0]["General_Content"].ToString())) { ikon_img.ImageUrl = "/assets/images/general/" + preferences5.Rows[0]["General_Content"] + ""; }
            else { ikon_img.ImageUrl = "/assets/images/no-image.png"; }

            footer_content.Value = preferences6.Rows[0]["General_Content"].ToString();
        }
    }

    // SO upload image
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

        return fileStatus;
    }

    protected string uploadIncResize(int h, int w, string Folder2, FileUpload gam, bool resize)
    {
        string upload_file = "";
        if (gam.PostedFile.FileName.EndsWith(".jpeg") || gam.PostedFile.FileName.EndsWith(".jpg") || gam.PostedFile.FileName.EndsWith(".png") || gam.PostedFile.FileName.EndsWith(".gif") || gam.PostedFile.FileName.EndsWith(".ico"))
        {
            string filename = Path.GetFileName(gam.FileName);
            string dir = Param.PathImage + Folder2;
            bool status = true;

            while (status)
            {
                status = FileExists(filename, Server.MapPath("~/assets/" + Folder2));
                if (status)
                    filename = getRandomFileName() + System.IO.Path.GetExtension(gam.PostedFile.FileName);
            }

            upload_file = filename;

            string temp = dir + "(temp)" + filename;
            string large = dir + filename;

            gam.PostedFile.SaveAs(temp);
            if (resize)
            {
                Imgh.Crop(temp, large, h, w);
            }
            else
            {
                gam.PostedFile.SaveAs(large);
            }
            File.Delete(temp);
        }
        else
        {
            Js.Alert(this, "Upload status: Only JPEG, PNG or GIF files are allowed!");
        }
        return upload_file;
    }

    protected string uploadIncResize2(int h, int w, string Folder2, FileUpload gam, bool resize)
    {
        string upload_file = "";
        if (gam.PostedFile.FileName.EndsWith(".jpeg") || gam.PostedFile.FileName.EndsWith(".jpg") || gam.PostedFile.FileName.EndsWith(".png") || gam.PostedFile.FileName.EndsWith(".gif") || gam.PostedFile.FileName.EndsWith(".ico"))
        {
            string filename = Path.GetFileName(gam.FileName);
            string dir = Param.PathImage + Folder2;
            bool status = true;

            while (status)
            {
                status = FileExists(filename, Server.MapPath("~/assets/" + Folder2));
                if (status)
                    filename = getRandomFileName() + System.IO.Path.GetExtension(gam.PostedFile.FileName);
            }

            upload_file = filename;

            string temp = dir + "(temp)" + filename;
            string large = dir + filename;

            gam.PostedFile.SaveAs(temp);
            if (resize)
            {
                Imgh.Crop(temp, large, h, w);
            }
            else
            {
                gam.PostedFile.SaveAs(large);
            }
            File.Delete(temp);
        }
        else
        {
            Js.Alert(this, "Upload status: Only JPEG, PNG or GIF files are allowed!");
        }
        return upload_file;
    }

    protected string uploadIncResize3(int h, int w, string Folder2, FileUpload gam, bool resize)
    {
        string upload_file = "";
        if (gam.PostedFile.FileName.EndsWith(".jpeg") || gam.PostedFile.FileName.EndsWith(".jpg") || gam.PostedFile.FileName.EndsWith(".png") || gam.PostedFile.FileName.EndsWith(".gif") || gam.PostedFile.FileName.EndsWith(".ico"))
        {
            string filename = Path.GetFileName(gam.FileName);
            string dir = Param.PathImage + Folder2;
            bool status = true;

            while (status)
            {
                status = FileExists(filename, Server.MapPath("~/assets/" + Folder2));
                if (status)
                    filename = getRandomFileName() + System.IO.Path.GetExtension(gam.PostedFile.FileName);
            }

            upload_file = filename;

            string temp = dir + "(temp)" + filename;
            string large = dir + filename;

            gam.PostedFile.SaveAs(temp);
            if (resize)
            {
                Imgh.Crop(temp, large, h, w);
            }
            else
            {
                gam.PostedFile.SaveAs(large);
            }
            File.Delete(temp);
        }
        else
        {
            Js.Alert(this, "Upload status: Only JPEG, PNG or GIF files are allowed!");
        }
        return upload_file;
    }

    protected void btnSubmit(object sender, EventArgs e)
    {
        int h, w; string Folder = "/images/general/";
        h = 298; w = 765;

        DataTable rs = Db.Rs(" Select * from TBL_General where General_ID=3");
        DataTable rs2 = Db.Rs(" Select * from TBL_General where General_ID=4");
        DataTable rs3 = Db.Rs(" Select * from TBL_General where General_ID=5");

        string image_path = rs.Rows[0]["General_Content"].ToString();
        string image_path2 = rs2.Rows[0]["General_Content"].ToString();
        string image_path3 = rs3.Rows[0]["General_Content"].ToString();

        string dir = Param.PathImage + Folder;
        bool flag_image = false; bool flag_image2 = false; bool flag_image3 = false;
        if (logo_gulali_file_upload.HasFile) { image_path = uploadIncResize(h, w, Folder, logo_gulali_file_upload, false); flag_image = true; }
        if (logo_dark_file_upload.HasFile) { image_path2 = uploadIncResize2(h, w, Folder, logo_dark_file_upload, false); flag_image2 = true; }
        if (ikon_file_upload.HasFile) { image_path3 = uploadIncResize3(h, w, Folder, ikon_file_upload, false); flag_image3 = true; }

        if (rs.Rows[0]["General_Type"].ToString() == "image")
        {
            if (flag_image && image_path != "")
            {
                string update = "update TBL_General set General_Content = '" + image_path + "' where General_ID = 3";
                Db.Execute(update);

                bool status_upload;
                if (rs.Rows[0]["General_Content"].ToString() != "")
                {
                    status_upload = FileExists(rs.Rows[0]["General_Content"].ToString(), Server.MapPath("~/assets/" + Folder));
                    if (status_upload)
                    {
                        File.Delete(dir + rs.Rows[0]["General_Content"].ToString());
                    }
                }
            }

            if (rs2.Rows[0]["General_Type"].ToString() == "image")
            {
                if (flag_image2 && image_path2 != "")
                {
                    string update = "update TBL_General set General_Content = '" + image_path2 + "' where General_ID = 4";
                    Db.Execute(update);

                    bool status_upload;
                    if (rs2.Rows[0]["General_Content"].ToString() != "")
                    {
                        status_upload = FileExists(rs2.Rows[0]["General_Content"].ToString(), Server.MapPath("~/assets/" + Folder));
                        if (status_upload)
                        {
                            File.Delete(dir + rs2.Rows[0]["General_Content"].ToString());
                        }
                    }
                }
            }

            if (rs3.Rows[0]["General_Type"].ToString() == "image")
            {
                if (flag_image3 && image_path3 != "")
                {
                    string update = "update TBL_General set General_Content = '" + image_path3 + "' where General_ID = 5";
                    Db.Execute(update);

                    bool status_upload;
                    if (rs3.Rows[0]["General_Content"].ToString() != "")
                    {
                        status_upload = FileExists(rs3.Rows[0]["General_Content"].ToString(), Server.MapPath("~/assets/" + Folder));
                        if (status_upload)
                        {
                            File.Delete(dir + rs3.Rows[0]["General_Content"].ToString());
                        }
                    }
                }
            }

            Db.Execute("update TBL_General set General_Content = '" + Cf.StrSql(company_name.Text) + "' where General_ID = 1");
            Db.Execute("update TBL_General set General_Content = '" + Cf.StrSql(about.Value) + "' where General_ID = 2");
            Db.Execute("update TBL_General set General_Content = '" + Cf.StrSql(footer_content.Value) + "' where General_ID = 6");

            Response.Redirect(Param.Path_Admin + "setup/perusahaan/perusahaan/");
        }
    }
}