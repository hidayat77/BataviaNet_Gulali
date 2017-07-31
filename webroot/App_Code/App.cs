using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;

using System.Collections;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// Application Function
/// </summary>
public class App
{
    //Initial Page Setup
    public static void Init(Page p)
    {
        p.Response.CacheControl = "no-cache";

        InitValidator(p);
    }
    public static string Path(Page p)
    {
        if (p.Request.ApplicationPath != "/") { return p.Request.ApplicationPath; }
        else { return ""; }
    }
    public static void InitValidator(Page p)
    {
        foreach (BaseValidator bv in p.Validators)
        {
            bv.SetFocusOnError = true;
            bv.Display = ValidatorDisplay.Dynamic;

            if (bv is RequiredFieldValidator)
            {
                bv.ControlToValidate = bv.ID.Substring(0, bv.ID.Length - 1);
            }

            if (bv is RegularExpressionValidator)
            {
                bv.ControlToValidate = bv.ID.Substring(0, bv.ID.Length - 2);

                switch (bv.SkinID.ToLower())
                {
                    case "pk":
                        ((RegularExpressionValidator)bv).ValidationExpression = Fv.Pk;
                        break;
                    case "email":
                        ((RegularExpressionValidator)bv).ValidationExpression = Fv.Email;
                        break;
                }
            }

            if (bv is CompareValidator)
            {
                bv.ControlToValidate = bv.ID.Substring(0, bv.ID.Length - 1);

                switch (bv.SkinID.ToLower())
                {
                    case "pass":
                        ((CompareValidator)bv).ControlToCompare = "pass1";
                        ((CompareValidator)bv).ControlToValidate = "pass2";
                        break;
                    case "tgl":
                        ((CompareValidator)bv).Type = ValidationDataType.Date;
                        ((CompareValidator)bv).Operator = ValidationCompareOperator.DataTypeCheck;
                        break;
                    case "int":
                        ((CompareValidator)bv).Type = ValidationDataType.Integer;
                        ((CompareValidator)bv).Operator = ValidationCompareOperator.GreaterThanEqual;
                        ((CompareValidator)bv).ControlToCompare = "nol";
                        break;
                }
            }
        }
    }
    public static void InitGrid(GridView tb)
    {
        foreach (BoundField f in tb.Columns)
            f.HtmlEncode = false;
    }
    //Administrator Section
    public static string IP
    {
        get { return HttpContext.Current.Request.UserHostAddress; }
    }

    #region public static string HostName
    public static string HostName
    {
        get { return System.Net.Dns.GetHostName(); }
    }
    #endregion

    public static void ProtectPageGulali(Page p, String Menu_Name, String PrivName)
    {
        //Debug Mode
        if (Employee_ID == "") { if (Param.Debug) { Role_ID = "1"; } }
        else
        {
            if (Role_ID != "1")
            {
                DataTable priv;
                if (PrivName == "view")
                {
                    priv = Db.Rs("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_View = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");

                    if (priv.Rows.Count == 0) { p.Response.Redirect("/gulali/page/Privilege.aspx?priv=1"); }
                }
                else if (PrivName == "insert")
                {
                    priv = Db.Rs("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Insert = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");

                    if (priv.Rows.Count > 0)
                    {
                        bool x = Db.SingleBool("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Insert = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");
                    }
                    else { p.Response.Redirect("/gulali/page/Privilege.aspx?priv=2"); }
                }
                else if (PrivName == "update")
                {
                    priv = Db.Rs("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Update = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");

                    if (priv.Rows.Count > 0)
                    {
                        bool x = Db.SingleBool("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Update = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");
                    }
                    else { p.Response.Redirect("/gulali/page/Privilege.aspx?priv=3"); }
                }
                else if (PrivName == "delete")
                {
                    priv = Db.Rs("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Delete = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");

                    if (priv.Rows.Count > 0)
                    {
                        bool x = Db.SingleBool("SELECT hm.Menu_Name, hu.Role_ID FROM TBL_Privilege hp join TBL_Menu hm on hp.Menu_ID = hm.Menu_ID join TBL_Role hu on hp.Role_ID = hu.Role_ID WHERE Privilege_Delete = 1 and hu.Role_ID = '" + Role_ID + "' and hm.Menu_Name = '" + Menu_Name + "'");
                    }
                    else { p.Response.Redirect("/gulali/page/Privilege.aspx?priv=4"); }
                }
            }
        }
    }

    public static void Del(Page p)
    {
        p.Server.Transfer("del.aspx");
    }
    public static void NoDel(Page p)
    {
        p.Server.Transfer("del.aspx?nodel=1");
    }

    //Reload System
    public static void Reload(Page p)
    {
        Reload(p, new string[] { });
    }
    public static void Reload(Page p, string ID)
    {
        Reload(p, new string[] { "id=" + ID });
    }
    public static void Reload(Page p, string[] Qstr)
    {
        string url = p.Request.Path + "?d=1";

        foreach (string qstr in Qstr)
            url += "&" + qstr;
        p.Response.Redirect(url);
    }

    //Feedback System
    public static void Feed(Page p, HtmlGenericControl div)
    {
        if (!p.IsPostBack && p.Request.QueryString["d"] != null)
            div.Visible = true;
        else
            div.Visible = false;
    }

    //Hyper-Link Marker
    public static void LinkMarker(HtmlAnchor a)
    {
        a.Attributes["class"] = "aktif";
    }
    public static void LinkMarker(HtmlGenericControl li)
    {
        li.Attributes["class"] = "aktif";
    }
    public static void LinkMarker(Page p, HtmlAnchor a, string Href)
    {
        if (p.Request.Url.PathAndQuery.IndexOf(Href) != -1) LinkMarker(a);
    }
    public static void LinkMarker(Page p, HtmlGenericControl li, string Href)
    {
        if (p.Request.Url.PathAndQuery.IndexOf(Href) != -1) LinkMarker(li);
    }

    //QueryString Validator
    public static int GetInt(Page p)
    {
        return GetInt(p, "id");
    }
    public static int GetInt(Page p, string qstr)
    {
        int x = 0;
        try { x = Convert.ToInt32(p.Request.QueryString[qstr]); }
        catch { }
        return x;
    }
    public static string GetStr(Page p)
    {
        return GetStr(p, "id");
    }
    public static string GetStr(Page p, string qstr)
    {
        return p.Request.QueryString[qstr] != null ?
            Cf.Upper(p.Request.QueryString[qstr]) : "";
    }
    public static string GetNormal(Page p)
    {
        return GetNormal(p, "id");
    }
    public static string GetNormal(Page p, string qstr)
    {
        return p.Request.QueryString[qstr] != null ?
            Cf.Normal(p.Request.QueryString[qstr]) : "";
    }

    public static string Normal(decimal value)
    {
        string t;
        return t = value.ToString("#,##0.00");
    }

    //List Control
    public static void Select(DropDownList wc, string v)
    {
        string t = "Now : " + v;
        wc.Items.Add(new ListItem(t, v));
        wc.SelectedValue = v;
    }
    public static void NoData(Table tb, int Count)
    {
        if (Count == 0)
        {
            TableRow tr = new TableRow();
            tb.Controls.Add(tr);

            tr.CssClass = "kosong";

            TableCell c = new TableCell();
            c.Text = "Data for the selected criteria is unavailable.";
            c.ColumnSpan = 30;
            tr.Cells.Add(c);
        }
    }
    public static void NoData(PlaceHolder list, Button b, int Count)
    {
        if (Count == 0)
        {
            HtmlTableRow tr;
            HtmlTableCell c;

            tr = new HtmlTableRow();
            list.Controls.Add(tr);

            tr.Attributes["class"] = "kosong";

            c = new HtmlTableCell();
            c.InnerHtml = "Data for the selected criteria is unavailable.";
            c.ColSpan = 30;
            tr.Cells.Add(c);

            b.Enabled = false;
        }
    }

    //Data List Function
    public static string SearchSql(DropDownList field)
    {
        string w = "";

        if (field.SelectedIndex == 0)
        {
            for (int i = 1; i < field.Items.Count; i++)
            {
                w += field.Items[i].Value + ".Contains(@0)";
                if (i < field.Items.Count - 1) w += " OR ";
            }
        }
        else w = field.SelectedValue + ".Contains(@0)";

        return w == "" ? w : "(" + w + ")";
    }

    //Session Driver
    public static string GetSession(string key)
    {
        if (HttpContext.Current.Session[key] != null)
            return HttpContext.Current.Session[key].ToString();
        else
            return "";
    }
    public static void SetSession(string key, string value)
    {
        HttpContext.Current.Session[key] = value;
    }

    //Application Settings
    public static string GetConfig(string key)
    {
        return new AppSettingsReader().GetValue(key, typeof(string)).ToString();
    }
    public static void SetConfig(string key, string value)
    {
        Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
        AppSettingsSection x = (AppSettingsSection)cfg.GetSection("appSettings");
        x.Settings[key].Value = value;
        cfg.Save();
    }

    //System.Web
    public static bool CustomErrors
    {
        get
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            CustomErrorsSection section = (CustomErrorsSection)config.GetSection("system.web/customErrors");
            if (section != null)
                return section.Mode == CustomErrorsMode.Off ? false : true;
            else return false;
        }
        set
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            CustomErrorsSection section = (CustomErrorsSection)config.GetSection("system.web/customErrors");
            if (section != null)
            {
                section.Mode = value ? CustomErrorsMode.RemoteOnly : CustomErrorsMode.Off;
                config.Save();
            }
        }
    }
    public static int MaxRequestLength
    {
        get
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            HttpRuntimeSection section = (HttpRuntimeSection)config.GetSection("system.web/httpRuntime");
            if (section != null)
                return section.MaxRequestLength;
            else return 0;
        }
    }
    public static bool check_Filemime(FileUpload file)
    {
        bool x = false;
        if (file.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || file.PostedFile.ContentType == "application/msword" || file.PostedFile.ContentType == "image/jpeg" || file.PostedFile.ContentType == "image/png" || file.PostedFile.ContentType == "image/gif")
        { x = true; }
        return x;
    }
    public static void SetGrid(GridView tb)
    {
        foreach (BoundField f in tb.Columns)
            f.HtmlEncode = false;
    }
    //Menu

    //Language
    public static string Language
    {
        get
        {
            if (GetSession("Lang") == "")
                return Param.DefaultLang;
            else
                return GetSession("Lang");
        }
        set { SetSession("Lang", value); }
    }
    public static void SetLanguage(Page p)
    {
        if (App.GetStr(p, "lang") != "") App.Language = App.GetStr(p, "lang");
    }
    public static string RandomGUID(int length)
    {
        string x = System.Guid.NewGuid().ToString();

        x = x.Replace("-", String.Empty);

        return x.Substring(0, length);
    }
    // Log History
    public static void LogHistory(string aktivitas, string before, string after, string desc)
    {
        string SessionID = HttpContext.Current.Session.SessionID;

        string insert = "Insert into TBL_Log_History (Log_Date, Log_Activity, Log_Desc, Log_IP, Log_Hostname, Log_Before, Log_After, Log_UserID, Log_Username, Log_UserRole, Log_Employee_Name, User_IsAdmin) VALUES "
                        + "("
                        + "getdate()"
                        + ",'" + Cf.StrSql(aktivitas) + "'"
                        + ",'" + Cf.StrSql(desc) + "'"
                        + ",'" + Cf.StrSql(App.IP) + "'"
                        + ",'" + Cf.StrSql(App.HostName) + "'"
                        + ",'" + Cf.StrSql(before) + "'"
                        + ",'" + Cf.StrSql(after) + "'"
                        + ",'" + Cf.StrSql(App.UserID) + "'"
                        + ",'" + Cf.StrSql(App.Username) + "'"
                        + ",'" + Cf.StrSql(App.Role_Name) + "'"
                        + ",'" + Cf.StrSql(App.Employee_Name) + "'"
                        + ",'" + Cf.StrSql(App.UserIsAdmin) + "')";
        Db.Execute(insert);
    }
    public static string LastLogin { get { return GetSession("LastLogin"); } set { SetSession("LastLogin", value); } }
    public static string LastIPLogin { get { return GetSession("LastIPLogin"); } set { SetSession("LastIPLogin", value); } }
    public static string LastPage { get { return GetSession("LastPage"); } set { SetSession("LastPage", value); } }
    public static string UserID { get { return GetSession("UserID"); } set { SetSession("UserID", value); } }
    public static string Employee_ID { get { return GetSession("Employee_ID"); } set { SetSession("Employee_ID", value); } }
    public static string Employee_Name { get { return GetSession("Employee_Name"); } set { SetSession("Employee_Name", value); } }
    public static string Role_ID { get { return GetSession("Role_ID"); } set { SetSession("Role_ID", value); } }
    public static string Role_Name { get { return GetSession("Role_Name"); } set { SetSession("Role_Name", value); } }
    public static string Username { get { return GetSession("Username"); } set { SetSession("Username", value); } }
    public static string UserIP { get { return GetSession("UserIP"); } set { SetSession("UserIP", value); } }
    public static string UserIsAdmin { get { return GetSession("UserIsAdmin"); } set { SetSession("UserIsAdmin", value); } }
    public static string userLastHostName { get { return System.Net.Dns.GetHostName(); } }

    public static string Failed
    {
        get { return GetSession("Failed"); }
        set { SetSession("Failed", value); }
    }

    public static void UserBlokir(string userID)
    {
        Db.Execute(" Update TBL_User set user_CountSalahPass = 0, User_TglBlokir = DATEADD(minute, 10, GETDATE()) where User_Authorized = 'Y' and User_Name = '" + userID + "' ");
    }
    public static void CekPageUser(Page p)
    {
        if (App.Username == "" || App.UserID == "") { p.Response.Redirect("/gulali/sign-up/"); }

        var Request = HttpContext.Current.Request;
        string url = Request.Url.AbsoluteUri;
        if (App.UserIsAdmin == "1" || App.UserIsAdmin == "2")
        {
            if (url.Contains(Param.Path_User)) { p.Response.Redirect(Param.Path_Admin + "dashboard/"); }
        }
        else if (App.UserIsAdmin == "3")
        {
            if (url.Contains(Param.Path_Admin)) { p.Response.Redirect(Param.Path_User + "dashboard/"); }
        }
    }
}