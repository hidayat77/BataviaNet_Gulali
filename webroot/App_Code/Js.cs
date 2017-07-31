using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

/// <summary>
/// JavaScript Functions
/// </summary>
public class Js
{
	//Common
	public static void Close(Page p)
	{
		Close(p, "");
	}
	public static void Close(Page p, string url)
	{
		string x = url == "" ? "" : "opener.location.href='" + url + "';";

		StringBuilder js = new StringBuilder();
		js.Append("<script language=\"javascript\" type=\"text/javascript\">");
		js.Append("window.close();");
		js.Append(x);
		js.Append("</script>");

		p.ClientScript.RegisterStartupScript(p.GetType(), "closeScript", js.ToString());
	}

	//Confirmation to Submit
	public static void Confirm(Button b, string txt)
	{
		StringBuilder js = new StringBuilder();
		js.Append("if (typeof(Page_ClientValidate) == 'function') {");
		js.Append("		var oldPage_IsValid = Page_IsValid;");
		js.Append("		var oldPage_BlockSubmit = Page_BlockSubmit;");
		js.Append("		if (Page_ClientValidate('" + b.ValidationGroup + "') == false) {");
		js.Append("			Page_IsValid = oldPage_IsValid;");
		js.Append("			Page_BlockSubmit = oldPage_BlockSubmit;");
		js.Append("			return false;");
		js.Append("		}");
		js.Append(" }");
		js.Append("return confirm('" + txt + "');");

		b.Attributes.Add("onclick", js.ToString());
	}
	public static void ConfirmDel(Button b)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Proceed with : DELETE ?\\n\\n");
		t.Append("The data will be permanently deleted.\\n");

		Confirm(b, t.ToString());
	}
	public static void ConfirmLama(Button b, string action)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Proceed with : " + action + " ?\\n\\n");
		t.Append("The process may take time.\\n");
		t.Append("Please do not close the browser window while it is being processed.");

		Confirm(b, t.ToString());
	}

	//Alert Pop-Up
	public static void Alert(Page p, string alert)
	{
		StringBuilder js = new StringBuilder();
		js.Append("<script language=\"javascript\" type=\"text/javascript\">");
		js.Append("alert(unescape('" + alert + "'))");
		js.Append("</script>");

		p.ClientScript.RegisterStartupScript(p.GetType(), "alertScript", js.ToString());
	}
	public static void AlertInvalid(Page p)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process Failed\\n");
		t.Append("Cause : Invalid Data Format\\n\\n");
		t.Append("Please fill in all required fields and\\n");
		t.Append("recheck the data format.");

		Alert(p, t.ToString());
	}
    public static void AlertPassword(Page p)
    {
        StringBuilder t = new StringBuilder();
        t.Append("Change Password failed.\\n");
        t.Append("Cause : Password must contain at least 1 uppercase and 1 number\\n\\n");

        Alert(p, t.ToString());
    }
	public static void AlertConflict(Page p, string kolom)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process Failed\\n");
		t.Append("Cause : Value Conflict (" + kolom + ")\\n\\n");
		t.Append("The column must contain UNIQUE value.\\n");
		t.Append("Please recheck the value to avoid duplicates.\\n");

		Alert(p, t.ToString());
	}
	public static void AlertReferensi(Page p, string kolom)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process Failed\\n");
		t.Append("Cause : Reference Conflict (" + kolom + ")\\n\\n");
		t.Append("The column is being referenced by another data.\\n");
		t.Append("Please choose a valid reference value.\\n");

		Alert(p, t.ToString());
	}
	public static void AlertProses(Page p, string proses)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process Success\\n");
		t.Append("Process : " + proses + "\\n\\n");

		Alert(p, t.ToString());
	}
	public static void AlertKosong(Page p, string data)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process failed\\n");
		t.Append("Cause : Invalid Value (" + data + ")\\n\\n");
		t.Append("Value cannot be null.\\n");
		t.Append("Please fill in the required field.");

		Alert(p, t.ToString());
	}
	public static void AlertCompare(Page p, string data1, string data2)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process failed\\n");
		t.Append("Cause : Invalid Value (" + data1 + " dan " + data2 + ")\\n\\n");
		t.Append("The specified passwords do not match.\\n");

		Alert(p, t.ToString());
	}
	public static void AlertUpload(Page p, string txt)
	{
		StringBuilder t = new StringBuilder();
		t.Append("Process Failed\\n");
		t.Append("Cause : File Format\\n\\n");
		t.Append("Files that can be processed :\\n");
		t.Append(txt);

		Alert(p, t.ToString());
	}
	public static void AlertUploadJPG(Page p)
	{
		AlertUpload(p, "Image file with JPG format.");
	}
	public static void AlertUploadSWF(Page p)
	{
		AlertUpload(p, "File with SWF format.");
	}
	public static void AlertUploadPDF(Page p)
	{
		AlertUpload(p, "File with PDF format.");
	}
	public static void AlertUploadZIP(Page p)
	{
		AlertUpload(p, "File with ZIP format.");
	}

    public static void AlertCodeExists(Page p)
    {
        Alert(p, "Fund Code already exists.");
    }

    public static void AlertRoleExists(Page p)
    {
        Alert(p, "Role already exists.");
    }

    public static void AlertAdminExists(Page p)
    {
        Alert(p, "Admin ID already exists.");
    }

	//Form
	public static void NumberFormat(TextBox t)
	{
		t.Attributes["onblur"] = "NumberFormat(this);";
	}
}
