using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using System.Net;

/// <summary>
/// Download Driver
/// </summary>
public class Dh
{
	//MIME Handler
	public static void Text(Page p, string Text)
	{
		p.Response.Clear();
		p.Response.ContentType = "text/plain";

		if (Text.Length == 0) Text = " ";
		p.Response.Write(Text);

		p.Response.End();
		p.Response.Flush();
	}
	public static void Img(Page p, string File)
	{
		FileInfo finfo = new FileInfo(File);

		p.Response.Clear();
		p.Response.AddHeader("Content-Disposition", "inline; filename=\"" + finfo.Name + "\"");
		p.Response.AddHeader("Content-Length", finfo.Length.ToString());
		p.Response.CacheControl = "no-cache";

		switch (finfo.Extension.ToLower())
		{
			case ".jpg":
			case ".jpeg":
			case ".jpe":
				p.Response.ContentType = "image/jpeg";
				break;
		}

		p.Response.WriteFile(File);
		p.Response.End();
		p.Response.Flush();
	}
	public static void Ext(Page p, string File)
	{
		FileInfo finfo = new FileInfo(File);

		p.Response.Clear();
		p.Response.AddHeader("Content-Disposition", "inline; filename=\"" + finfo.Name + "\"");
		p.Response.AddHeader("Content-Length", finfo.Length.ToString());
		p.Response.CacheControl = "no-cache";

		switch (finfo.Extension.ToLower())
		{
			case ".pdf":
				p.Response.ContentType = "application/pdf";
				break;
            case ".xls":
                p.Response.ContentType = "application/vnd.ms-excel";
                break;
            case ".html":
                p.Response.ContentType = "text/html";
                break;
            case ".swf":
                p.Response.ContentType = "application/x-shockwave-flash";
                break;
			case ".zip":
				switch (p.Request.Browser.Browser)
				{
					case "IE": 
						p.Response.ContentType = "application/x-zip-compressed";
						break;
					case "Firefox":
					case "Opera":
						p.Response.ContentType = "application/zip";
						break;
					case "AppleMAC-Safari":
						p.Response.ContentType = "application/octet-stream";
						break;
				}
                break;
		}
		p.Response.WriteFile(File);
		p.Response.End();
		p.Response.Flush();
	}

    
	//Physical Download
	public static void Download(Page p, string Src)
	{
		FileInfo finfo = new FileInfo(Src);

		string ContentType = "";
		switch (finfo.Extension.ToLower())
		{
			case ".txt":
			case ".sql":
				ContentType = "text/plain";
				break;
			case ".xls":
			case ".xlsx":
				ContentType = "application/ms-excel";
				break;
			case ".doc":
			case ".docx":
				ContentType = "application/ms-word";
				break;
		}

		Download(p, Src, finfo.Name, finfo.Length, ContentType);
	}
	public static void Download(Page p, string Src, string FileName, decimal FileSize, string ContentType)
	{
		p.Response.Clear();
		p.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + FileName + "\"");
		p.Response.CacheControl = "no-cache";
		p.Response.ContentType = ContentType;
		p.Response.WriteFile(Src);
		p.Response.End();
		p.Response.Flush();
	}

	//Physical Upload
	public static void UploadImg(FileUpload img, string Folder, string ID)
	{
		string dir = Param.PathImage + Folder;

		string temp = dir + ID + ".jpg";
		img.PostedFile.SaveAs(temp);
	}
	public static void UploadImg(FileUpload img, string Folder, string ID, bool thumbnail)
	{
		string dir = Param.PathImage + Folder;

		string large = dir + ID + ".jpg";
		string thumb = dir + ID + "(sm).jpg";

		img.PostedFile.SaveAs(large);
		Imgh.Crop(large, thumb, 100, false);
	}
    public static void UploadImgResize(FileUpload img, string Folder, string ID)
    {
        string dir = Param.PathImage + Folder;

        string large = dir + ID + "(sm).jpg";
        string thumb = dir + ID + ".jpg";

        img.PostedFile.SaveAs(large);
        Imgh.Crop(large, thumb, 250, false);
    }
    public static void UploadFoto(string ID, FileUpload foto, string Folder)
    {
        string dir = Param.PathImage + Folder;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string temp = dir + ID + "(temp).jpg";
        string large = dir + ID + ".jpg";
        string thumb = dir + ID + "(sm).jpg";

        foto.PostedFile.SaveAs(temp);
        Imgh.Crop(temp, large, 448, true);
        Imgh.Crop(temp, thumb, 86, 154);
        File.Delete(temp);
        //foto.PostedFile.SaveAs(large);
    }
	public static void DeleteImg(string Folder, string ID)
	{
		string dir = Param.PathImage + Folder;

		string temp = dir + ID + ".jpg";

		if (File.Exists(dir)) File.Delete(temp);
	}
	public static void DeleteImg(string Folder, string ID, bool thumbnail)
	{
		string dir = Param.PathImage + Folder;

		string large = dir + ID + ".jpg";
		string thumb = dir + ID + "(sm).jpg";
        
		if (File.Exists(large)) File.Delete(large);
		if (File.Exists(thumb)) File.Delete(thumb);
	}
	
	public static string SrcImg(string Folder, string FileName)
	{
		string x = "";
		string dir = Param.PathImage + Folder;

		if (File.Exists(dir + FileName))
			x = "/common/img.aspx?folder=" + Folder + "&file=" + FileName;
		else
			x = "/images/blank.jpg";

		return x;
	}
	public static string SrcFile(string Folder, string FileName)
	{
		string x = "";
		string dir = Param.PathFile + Folder;

		if (File.Exists(dir + FileName))
			x = "/common/file.aspx?folder=" + Folder + "&file=" + FileName;

		return x;
	}
    public static string SrcFileSharing(string FileName, string ext)
    {
        string x = "";
        string dir = Param.PathFileSharing + "document_sharing\\";

        //if (File.Exists(dir + FileName + ext))
            x = "/common/fileSharing.aspx?file=" + FileName;

        return x;
    }
	public static void UploadFileSharing(FileUpload file, string Folder, string ID, string ext)
	{
		string dir = Param.PathFileSharing + Folder;

		string temp = dir + ID + ext;
		file.PostedFile.SaveAs(temp);
	}
	public static void DeleteFileSharing(string Folder, string ID, string ext)
	{
		string dir = Param.PathFileSharing + Folder;

		string temp = dir + ID + ext;
		if (File.Exists(temp)) File.Delete(temp);
	}
	public static void UploadFile(FileUpload file, string Folder, string ID, string ext)
	{
		string dir = Param.PathFile + Folder;

		string temp = dir + ID + ext;
		file.PostedFile.SaveAs(temp);
	}
	public static void DeleteFile(string Folder, string ID, string ext)
	{
		string dir = Param.PathFile + Folder;

		string temp = dir + ID + ext;
		if (File.Exists(temp)) File.Delete(temp);
	}
	

	
}
