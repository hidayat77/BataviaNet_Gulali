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

public partial class _admin_surat_peringatan_add : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Surat Peringatan", "Insert");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/history/surat-peringatan/?id=" + id + "\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id) + "");
                nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();
            }
        }
    }

    protected string uploadIncResize(int h, int w, string Folder, FileUpload gam, bool resize)
    {
        string upload_file = "";
        try
        {
            if (gam.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || gam.PostedFile.ContentType == "application/msword" || gam.PostedFile.ContentType == "image/jpeg" || gam.PostedFile.ContentType == "image/png" || gam.PostedFile.ContentType == "image/gif" || gam.PostedFile.ContentType == "application/pdf")
            {
                string filename = Path.GetFileName(gam.FileName);
                string dir = Folder;
                bool status = true;

                while (status)
                {
                    status = FileExists(filename, Server.MapPath("~/file-upload/" + Folder));
                    if (status)
                        filename = getRandomFileName() + System.IO.Path.GetExtension(gam.PostedFile.FileName);
                }

                upload_file = filename;

                string large = Server.MapPath("~/file-upload/") + dir + filename;
                string temp = Server.MapPath("~/file-upload/") + dir + filename;

                gam.PostedFile.SaveAs(temp);
                if (resize)
                {
                    Imgh.Crop(temp, large, h, w);
                    File.Delete(temp);
                }
            }
            else

                Js.Alert(this, "Upload status: Only JPEG , PNG or GIF files are allowed");
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
        return fileStatus;
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        { return; }
        else
        {
            int h, w;
            string Folder2 = "surat-peringatan/";
            h = 298; w = 765;

            string doc = uploadIncResize(h, w, Folder2, FileUpload_berkas, false);

            string insert = "insert into TBL_Employee_Warning (Warning_Type, Warning_FileTermination, Employee_ID, Warning_CreateDate, Warning_Remarks, Employee_UserCreate)"
                + "values('" + ddl_type_warning.SelectedValue + "', '" + doc + "','" + id + "',getdate(), '" + Cf.StrSql(remarks.Text) + "', '" + Cf.StrSql(App.Employee_ID) + "')";
            Db.Execute(insert);
            Response.Redirect("../?id=" + id + "");
            return;
        }
    }

    protected void BtnCancel(object sender, EventArgs e) { Response.Redirect("../?id=" + id + ""); }
}