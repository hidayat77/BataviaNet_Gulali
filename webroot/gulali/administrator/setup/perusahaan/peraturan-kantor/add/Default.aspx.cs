﻿using System;
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

public partial class _admin_setup_peraturan_kantor_add : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Perusahaan", "insert");
        if (!IsPostBack)
        {
            href_cancel.Text = "<a href=\"" + Param.Path_Admin + "setup/perusahaan/peraturan-kantor/\" Style=\"border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;cursor:pointer;\">Cancel</a>";
        }
    }
    protected bool valid
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(title) ? x : false;
            return x;
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        if (valid)
        {
            if (!IsValid) { return; }
            else
            {
                string insert = "Insert into TBL_Office_Regulations(Regulation_Title, Regulation_Desc) values('" + Cf.StrSql(title.Text) + "', '" + Cf.StrSql(detail.Value) + "')";
                Db.Execute(insert);
                Response.Redirect("../");
            }
        }
    }
}