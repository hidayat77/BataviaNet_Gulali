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

public partial class _admin_setup_organisasi_add : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan >> Struktur Organisasi", "insert");

        if (!IsPostBack)
        {
               href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";
                link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/organisasi/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        cek_button();
           
            
        }
    }

    protected void cek_button()
    {
        DataTable preferences7 = Db.Rs("Select General_Content from TBL_General where General_ID=7");

        string foto_link = "";

        if (!string.IsNullOrEmpty(preferences7.Rows[0]["General_Content"].ToString())) { foto_link = "/assets/images/general/" + preferences7.Rows[0]["General_Content"].ToString(); }
        else { foto_link = "/assets/images/no-image.png"; }

        link_foto.Text = "<a href=\"" + foto_link + "\" class=\"image-popup\" title=\"Screenshot-1\"><img src=\"" + foto_link + "\" class=\"thumb-img\" alt=\"work-thumbnail\" style=\"max-width:180px;\"></a>";
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
        string image_path = rs.Rows[0]["General_Content"].ToString();

        string dir = Param.PathImage + Folder;
        bool flag_image = false;

        if (struktur_organisasi_file_upload.HasFile) { image_path = uploadIncResize(h, w, Folder, struktur_organisasi_file_upload, false); flag_image = true; }

        if (rs.Rows[0]["General_Type"].ToString() == "image")
        {
            if (flag_image && image_path != "")
            {
                string update = "update TBL_General set General_Content = '" + image_path + "' where General_ID = 7";
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

            Response.Redirect(Param.Path_Admin + "setup/perusahaan/organisasi/");
            
        }  
    }


}