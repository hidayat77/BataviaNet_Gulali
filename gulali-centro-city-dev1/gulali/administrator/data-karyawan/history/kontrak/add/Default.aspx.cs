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

public partial class _admin_data_karyawan_history : System.Web.UI.Page
{
    protected string id { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Data Karyawan >> Kontrak", "Insert");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "data-karyawan/history/kontrak/?id=" + id + "\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack)
        {
            //check int
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id), out number);
            if (result)
            {
                combo_jabatan();
                DataTable employee_name = Db.Rs("select Employee_Full_Name from TBL_Employee where Employee_ID = " + Cf.StrSql(id) + "");
                nama_employee.Text = employee_name.Rows[0]["Employee_Full_Name"].ToString();
            }
        }
    }
    protected void combo_jabatan()
    {
        DataTable rs = Db.Rs("select * from TBL_Position order by Position_Name asc");
        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                string nama_jabatan = rs.Rows[i]["Position_Name"].ToString();
                string id_jabatan = rs.Rows[i]["Position_ID"].ToString();
                ddl_jabatan.Items.Add(new ListItem(nama_jabatan, id_jabatan));
            }
        }
        else
        {
            ddl_jabatan.Visible = false;
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

                //teaser_image.SaveAs(Server.MapPath("~/images/") + Folder + filename);
                upload_file = filename;

                //string type = "";
                //if (Path.GetExtension(gam.FileName).ToLower() != ".jpg" || Path.GetExtension(gam.FileName).ToLower() != ".png" || Path.GetExtension(gam.FileName).ToLower() != ".jpeg")
                //{
                //    type = "_(CV)_";
                //}
                //else
                //{
                //    type = "_(Profile)_";
                //}

                string large = Server.MapPath("~/file-upload/") + dir + filename;
                string temp = Server.MapPath("~/file-upload/") + dir + filename;

                gam.PostedFile.SaveAs(temp);
                if (resize)
                {
                    Imgh.Crop(temp, large, h, w);
                    File.Delete(temp);
                }
                //File.Delete(temp);
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
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isSelected(ddl_jabatan) ? x : false;
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                try
                {
                    DateTime Join = Convert.ToDateTime(contract_start.Text);
                    DateTime Resign = Convert.ToDateTime(contract_end.Text);
                    if (Join < Resign)
                    {
                        //convert datetime
                        string Join_Conv = Join.ToString("yyyy-MM-dd").ToString();
                        string Resign_Conv = Resign.ToString("yyyy-MM-dd").ToString();

                        //upload doc
                        int h, w;
                        string Folder2 = "kontrak/";
                        h = 298; w = 765;

                        string doc = uploadIncResize(h, w, Folder2, FileUpload_berkas, false);

                        string insert = "insert into TBL_Employee_Contract (Contract_StartPeriode, Contract_EndPeriode, Contract_Title, Contract_DateCreate, Employee_ID, Contract_FileUpload, Contract_Remarks, Contract_UserCreate)"
                            + "values('" + Join_Conv + "', '" + Resign_Conv + "', '" + ddl_jabatan.SelectedValue + "', getdate(), " + id + ", '" + doc + "', '" + Cf.StrSql(remarks.Text) + "', '" + Cf.StrSql(App.Employee_ID) + "')";
                        Db.Execute(insert);

                        string update = "update TBL_Employee "
                                + "set Employee_EndDate = '" + Resign_Conv + "', Position_ID = '" + ddl_jabatan + "' where Employee_ID = " + Cf.StrSql(id);
                        Db.Execute(update);
                    }
                    else
                    {
                        Js.Alert(this, "Invalid Date!");
                    }
                }
                catch (Exception) { }
            }
        }
        Response.Redirect("../?id=" + id + "");
        return;
    }
    protected void BtnCancel(object sender, EventArgs e)
    {
        Response.Redirect("../?id=" + id + "");
    }
}