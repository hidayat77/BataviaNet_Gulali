using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace GULALI.Message
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    public class Message_m
    {
        public Message_m()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public static void Show(string message)
        {
            //string cleanMessage = message.Replace("'", "\'");
            string cleanMessage = message.Replace("'", "\\\'");
            Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }
    }
}