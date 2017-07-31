using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace General
{
    /// <summary>
    /// Summary description for Session
    /// </summary>
    public class Session
    {
        public Session()
        {
        }

        /// <summary>Gets or sets user id from session</summary>
        /// <remarks>Author: Ehsan kayani</remarks>
        public static string UserID
        {
            get { return HttpContext.Current.Session["UserID"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["UserID"] = value;  }
        }

        public static string prize1
        {
            get { return HttpContext.Current.Session["prize1"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prize1"] = value; }
        }

        public static string prize2
        {
            get { return HttpContext.Current.Session["prize2"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prize2"] = value; }
        }

        public static string prize3
        {
            get { return HttpContext.Current.Session["prize3"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prize3"] = value; }
        }

        public static string prize4
        {
            get { return HttpContext.Current.Session["prize4"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prize4"] = value; }
        }

        public static string prize5
        {
            get { return HttpContext.Current.Session["prize5"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prize5"] = value; }
        }


        public static string prizeID1
        {
            get { return HttpContext.Current.Session["prizeID1"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prizeID1"] = value; }
        }
        public static string prizeID2
        {
            get { return HttpContext.Current.Session["prizeID2"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prizeID2"] = value; }
        }
        public static string prizeID3
        {
            get { return HttpContext.Current.Session["prizeID3"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prizeID3"] = value; }
        }
        public static string prizeID4
        {
            get { return HttpContext.Current.Session["prizeID4"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prizeID4"] = value; }
        }
        public static string prizeID5
        {
            get { return HttpContext.Current.Session["prizeID5"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["prizeID5"] = value; }
        }


        public static string newSaleID
        {
            get { return HttpContext.Current.Session["newSaleID"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["newSaleID"] = value; }
        }

                
        public static string UserName
        {
            get { return HttpContext.Current.Session["UserName"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static string UserEmail
        {
            get { return HttpContext.Current.Session["UserEmail"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["UserEmail"] = value; }
        }

        public static string retailer
        {
            get { return HttpContext.Current.Session["retailer"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["retailer"] = value; }
        }

        public static string raceStart
        {
            get { return HttpContext.Current.Session["raceStart"] as string ?? String.Empty; }
            set { HttpContext.Current.Session["raceStart"] = value; }
        }


        public enum SessionName { UserID, UserName, useerRole, raceStart, retailer, UserEmail, newSaleID }
        public static void SetNull()
        {

            if (HttpContext.Current.Session["UserID"] != null)
                HttpContext.Current.Session["UserID"] = null;

            if (HttpContext.Current.Session["UserName"] != null)
                HttpContext.Current.Session["UserName"] = null;

            if (HttpContext.Current.Session["raceStart"] != null)
                HttpContext.Current.Session["raceStart"] = null;

            if (HttpContext.Current.Session["retailer"] != null)
                HttpContext.Current.Session["retailer"] = null;

            if (HttpContext.Current.Session["UserEmail"] != null)
                HttpContext.Current.Session["UserEmail"] = null;

            if (HttpContext.Current.Session["newSaleID"] != null)
                HttpContext.Current.Session["newSaleID"] = null;



        }
       
    }
}