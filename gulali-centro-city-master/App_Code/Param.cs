using System;
using System.Configuration;
using System.Web.Configuration;

/// <summary>
/// Global Parameter dan Web.Config
/// </summary>
public class Param
{
    //Driver
    private static string GetConfig(string key)
    {
        return new AppSettingsReader().GetValue(key, typeof(string)).ToString();
    }
    private static void SetConfig(string key, string value)
    {
        Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
        AppSettingsSection x = (AppSettingsSection)cfg.GetSection("appSettings");
        x.Settings[key].Value = value;
        cfg.Save();
    }
    //Web.Config
    public static bool Debug
    {
        get { return System.Boolean.Parse(GetConfig("Debug")); }
        set { SetConfig("Debug", value.ToString()); }
    }
    public static string DefaultLang
    {
        get { return GetConfig("DefaultLang"); }
        set { SetConfig("DefaultLang", value.ToString()); }
    }
    public static string PathImage
    {
        get { return GetConfig("PathImage"); }
        set { SetConfig("PathImage", value.ToString()); }
    }
    public static string PathPhotoImage
    {
        get { return GetConfig("PathPhotoImage"); }
        set { SetConfig("PathPhotoImage", value.ToString()); }
    }
    public static string PathImageMobile
    {
        get { return GetConfig("PathImageMobile"); }
        set { SetConfig("PathImageMobile", value.ToString()); }
    }
    public static string PathVideo
    {
        get { return GetConfig("PathVideo"); }
        set { SetConfig("PathVideo", value.ToString()); }
    }
    public static string PathFile
    {
        get { return GetConfig("PathFile"); }
        set { SetConfig("PathFile", value.ToString()); }
    }
    public static string PathFileSharing
    {
        get { return GetConfig("PathFileSharing"); }
        set { SetConfig("PathFileSharing", value.ToString()); }
    }
    public static string PathBackup
    {
        get { return GetConfig("PathBackup"); }
        set { SetConfig("PathBackup", value.ToString()); }
    }
    public static string SMTPServer
    {
        get { return GetConfig("SMTPServer"); }
        set { SetConfig("SMTPServer", value.ToString()); }
    }
    public static string SMTPUser
    {
        get { return GetConfig("SMTPUser"); }
        set { SetConfig("SMTPUser", value.ToString()); }
    }
    public static string SMTPPass
    {
        get { return GetConfig("SMTPPass"); }
        set { SetConfig("SMTPPass", value.ToString()); }
    }
    public static string Xml
    {
        get { return GetConfig("PathXml"); }
        set { SetConfig("PathXml", value.ToString()); }
    }
    public static string PathUserFiles
    {
        get { return GetConfig("PathUserFiles"); }
        set { SetConfig("PathUserFiles", value.ToString()); }
    }
    public static string Db
    {
        get { return GetConfig("Db"); }
        set { SetConfig("Db", value.ToString()); }
    }
    public static string Db2
    {
        get { return GetConfig("Db2"); }
        set { SetConfig("Db2", value.ToString()); }
    }
    public static string GenericEmail
    {
        get { return GetConfig("GenericEmail"); }
        set { SetConfig("GenericEmail", value.ToString()); }
    }
    public static string Enable_SSL
    {
        get { return GetConfig("Enable_SSL"); }
        set { SetConfig("Enable_SSL", value.ToString()); }
    }
    public static string SSL_Port
    {
        get { return GetConfig("SSL_Port"); }
        set { SetConfig("SSL_Port", value.ToString()); }
    }
    public static string Default_Credentials
    {
        get { return GetConfig("Default_Credentials"); }
        set { SetConfig("Default_Credentials", value.ToString()); }
    }
    public static Int32 MaxFileSize
    {
        get { return Convert.ToInt32(GetConfig("MaxFileSize")); }
        set { SetConfig("MaxFileSize", value.ToString()); }
    }
    public static Int32 MaxFileSize1
    {
        get { return Convert.ToInt32(GetConfig("MaxFileSize1")); }
        set { SetConfig("MaxFileSize1", value.ToString()); }
    }
    public static Int32 MaxFileSize2
    {
        get { return Convert.ToInt32(GetConfig("MaxFileSize2")); }
        set { SetConfig("MaxFileSize2", value.ToString()); }
    }
    public static string linkSSL
    {
        get { return GetConfig("linkSSL"); }
        set { SetConfig("linkSSL", value.ToString()); }
    }
    public static string SMTP_Timeout
    {
        get { return GetConfig("SMTP_Timeout"); }
        set { SetConfig("SMTP_Timeout", value.ToString()); }
    }
    public static string Path_Admin
    {
        get { return GetConfig("Path_Admin"); }
        set { SetConfig("Path_Admin", value.ToString()); }
    }
    public static string Path_User
    {
        get { return GetConfig("Path_User"); }
        set { SetConfig("Path_User", value.ToString()); }
    }
    public static string Domain
    {
        get { return GetConfig("Domain"); }
        set { SetConfig("Domain", value.ToString()); }
    }
    public static string encryptionPassword
    {
        get { return GetConfig("encryptionPassword"); }
        set { SetConfig("encryptionPassword", value.ToString()); }
    }
}
