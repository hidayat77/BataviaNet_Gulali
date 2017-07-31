using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Form Validation
/// </summary>
public class Fv {
	//Regex String
	public static string Pk { get { return @"^([\w\.\-\:\/]+)"; } }
	public static string Email { get { return @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"; } }

	//Marker System
	public static void Mark(TextBox tb, bool x) {
		if (!x) MarkError(tb); else ClrError(tb);
	}
	public static void MarkError(TextBox tb) {
		tb.Attributes["style"] += "background-color:pink;";
	}
	public static void ClrError(TextBox tb) {
		if (tb.Attributes["style"] != null)
			tb.Attributes["style"] = tb.Attributes["style"].Replace("background-color:pink;", "");
	}
    //kyok{
    public static void MarkFU(FileUpload fu, bool x)
    {
        if (!x) MarkErrorFU(fu); else ClrErrorFU(fu);
    }
    public static void MarkErrorFU(FileUpload fu)
    {
        fu.Attributes["style"] += "background-color:pink;";
    }
    public static void ClrErrorFU(FileUpload fu)
    {
        if (fu.Attributes["style"] != null)
            fu.Attributes["style"] = fu.Attributes["style"].Replace("background-color:pink;", "");
    }

    public static void MarkDDL(DropDownList ddl, bool x)
    {
        if (!x) MarkErrorDDL(ddl); else ClrErrorDDL(ddl);
    }
    public static void MarkErrorDDL(DropDownList ddl)
    {
        ddl.Attributes["style"] += "background-color:pink;";
    }
    public static void ClrErrorDDL(DropDownList ddl)
    {
        if (ddl.Attributes["style"] != null)
            ddl.Attributes["style"] = ddl.Attributes["style"].Replace("background-color:pink;", "");
    }
    //kyok}

	//Type Validator
	public static bool cekTgl(string t) {
		bool x = true;
		DateTime z;

		x = DateTime.TryParse(t, out z) ? x : false;

		return x;
	}
	public static bool cekDecimal(string t) {
		bool x = true;
		decimal z;

		x = System.Decimal.TryParse(t, out z) ? x : false;

		return x;
	}
	public static bool cekByte(string t) {
		bool x = true;
		byte z;

		x = System.Byte.TryParse(t, out z) ? x : false;

		return x;
	}
	public static bool cekShort(string t) {
		bool x = true;
		short z;

		x = System.Int16.TryParse(t, out z) ? x : false;

		return x;
	}
	public static bool cekInt(string t) {
		bool x = true;
		int z;

		x = System.Int32.TryParse(t, out z) ? x : false;

		return x;
	}

	//Business Validator
	public static bool isComplete(TextBox tb) {
		bool x = true;

		if (tb.Text == "") x = false;

		Mark(tb, x);
		return x;
	}
	public static bool isTgl(TextBox dari, TextBox sampai) {
		bool x = true;
		DateTime z;

		if (dari.Text != "")
			x = DateTime.TryParse(dari.Text, out z) ? x : false;
		if (sampai.Text != "")
			x = DateTime.TryParse(sampai.Text, out z) ? x : false;

		return x;
	}
	public static bool isTgl(TextBox tb) {
		bool x = cekTgl(tb.Text);

		Mark(tb, x);
		return x;
	}
	public static bool isDecimal(TextBox tb) {
		bool x = cekDecimal(tb.Text);

		Mark(tb, x);
		return x;
	}
	public static bool isByte(TextBox tb) {
		bool x = true;
		byte z;

		x = System.Byte.TryParse(tb.Text, out z) ? x : false;
		x = z >= 0 ? x : false;

		Mark(tb, x);
		return x;
	}
	public static bool isInt(TextBox tb) {
		bool x = true;
		int z;

		x = System.Int32.TryParse(tb.Text, out z) ? x : false;
		x = z >= 0 ? x : false;

		Mark(tb, x);
		return x;
	}
    public static bool isInt(string t)
    {
        bool x = true;
        int z;

        x = System.Int32.TryParse(t, out z) ? x : false;

        return x;
    }
    public static bool isDouble(string t)
    {
        bool x = true;
        double z;

        x = System.Double.TryParse(t, out z) ? x : false;

        return x;
    }
	public static bool isEmail(TextBox tb) {
		bool x = true;

        if (tb.Text != "")
        {
            Regex re = new Regex(Email);
            x = re.IsMatch(tb.Text);
        }
        else { x = false; }

		Mark(tb, x);
		return x;
	}
    //kyok{
    private  static string getExtention(string file_name)
    { 
        string ext= Cf.Lower(file_name);
        string[] m= ext.Split('.');
        int n= m.Length;
        return m[n-1].ToString() ;
    }
    public static bool isUploadImage (FileUpload fu)
    {
        bool x = true;
        //int z;
      
        if (fu.PostedFile.FileName != "" )
            {
                //get extention
                string ext = getExtention(fu.FileName.ToString());

                if (ext != "jpeg" && ext != "pjpeg" && ext != "jpg" && ext != "png" && ext != "bmp" && ext != "gif")
                    x = false;
            }

        MarkFU(fu, x);
        //if (!x) Js.AlertUploadJPG(this);
        return x;
    }

    public static bool isValidFileSize_200kb(FileUpload fu)
    {
        bool x = true;
        //int z;
        long ukuran_File = fu.FileBytes.LongLength;
        if (ukuran_File >= Param.MaxFileSize)
        {
              x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isValidFileSize_400kb(FileUpload fu)
    {
        bool x = true;
        //int z;
        long ukuran_File = fu.FileBytes.LongLength;
        if (ukuran_File >= Param.MaxFileSize1)
        {
            x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isValidFileSize_2000kb(FileUpload fu)
    {
        bool x = true;
        //int z;
        long ukuran_File = fu.FileBytes.LongLength;
        if (ukuran_File >= Param.MaxFileSize2)
        {
            x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isUploadDoc_Xls_pdf(FileUpload fu)
    {
        bool x = true;
        int z;

        if (fu.PostedFile.FileName != "")
        {
            //get extention
            string ext = getExtention(fu.FileName.ToString());

            if (ext != "doc" && ext != "docx" && ext != "xls" && ext != "xlsx" && ext != "pdf")
                x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isUploadpdf(FileUpload fu)
    {
        bool x = true;
        int z;

        if (fu.PostedFile.FileName != "")
        {
            //get extention
            string ext = getExtention(fu.FileName.ToString());

            if (ext != "pdf")
                x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isUpload_file_compress_zip(FileUpload fu)
    {
        bool x = true;
        int z;

        if (fu.PostedFile.FileName != "")
        {
            //get extention
            string ext = getExtention(fu.FileName.ToString());

            if (ext != "zip" && ext != "rar" && ext != "7z")
                x = false;
        }

        MarkFU(fu, x);
        return x;
    }
    public static bool isCompleteUpload(FileUpload fu)
    {
        bool x = true;
        if (fu.PostedFile.FileName == "") x = false;

        MarkFU(fu, x);
        return x;
    }
    public static bool isSelected(DropDownList ddl)
    {
        bool x = true;
        if (ddl.SelectedIndex == 0 ) x = false;

        MarkDDL(ddl, x);
        return x;
    }


    //kyok}
}
