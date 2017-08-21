using System;
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

public partial class _admin_component_setting_edit : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "update");
        if (!IsPostBack)
        { //Pokok
            DataTable rs1 = Db.Rs2("select component_type, component_code,component_id from TBL_MasterComponent where component_id='" + id_get + "'");

            if (rs1.Rows.Count > 0)
            {
               string type = "", typeid = "";


                for (int i = 0; i < rs1.Rows.Count; i++)
                {
                    switch (rs1.Rows[i]["component_type"].ToString())
                    {
                        case "Regular Income":
                            {
                                type = "Regular Income"; typeid = "1";
                                break;
                            }
                        case "Irregular Income":
                            {
                                type = "Irregular Income"; typeid = "2";
                                div_mode1.Visible = false;
                                div_mode2.Visible = true; break;
                            }
                        case "Deduction":
                            {
                                type = "Deduction"; typeid = "3";
                                div_mode1.Visible = false;
                                div_mode3.Visible = true; break;
                            }
                        default: { break; }
                    }
                }
            }
            int number;
            bool result = Int32.TryParse(Cf.StrSql(id_get), out number);
            if (result)
            {
                DataTable dba = Db.Rs2("Select * from TBL_MasterComponent where Component_ID = " + Cf.StrSql(id_get) + " and component_id not in ('18', '19', '20', '29', '30', '31')");
                if (dba.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_name"].ToString()))
                    {
                        txtComponentname.Text = dba.Rows[0]["component_name"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_code"].ToString()))
                    {
                        txtcode.Text = dba.Rows[0]["component_code"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_kind"].ToString()))
                    {
                        string component_kind = dba.Rows[0]["component_kind"].ToString();
                        if (component_kind.Equals("Pokok")) { ddl_kind_mode1.SelectedValue = component_kind; }
                        else if (component_kind.Equals("Tunjangan")) { ddl_kind_mode1.SelectedValue = component_kind; }
                        else { txtkind.Text = dba.Rows[0]["component_kind"].ToString(); }
                    }
                    if (!string.IsNullOrEmpty(dba.Rows[0]["component_type"].ToString())) { txttype.Text = dba.Rows[0]["component_type"].ToString(); }
                }
                else { Response.Redirect("/gulali/page/404.aspx"); }
            }
            else { Response.Redirect("/gulali/page/404.aspx"); }
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        DataTable dba = Db.Rs2("Select * from TBL_MasterComponent where Component_ID = " + Cf.StrSql(id_get) + " and component_id not in ('18', '19', '20', '29', '30', '31')");
        string component_type = dba.Rows[0]["component_type"].ToString();
        if (component_type.Equals("Regular Income"))
        {
            string update = "Update TBL_MasterComponent set Component_Code='" + Cf.StrSql(txtcode.Text) + "', Component_Name='" + Cf.StrSql(txtComponentname.Text) + "', Component_Kind='" + Cf.StrSql(ddl_kind_mode1.SelectedValue) + "' where component_ID = '" + Cf.StrSql(id_get) + "'";
            Db.Execute2(update);
            Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
        }
        else if (component_type.Equals("Irregular Income"))
        {
            string update = "Update TBL_MasterComponent set Component_Code='" + Cf.StrSql(txtcode.Text) + "', Component_Name='" + Cf.StrSql(txtComponentname.Text) + "', Component_Kind='" + Cf.StrSql(txtkind.Text) + "' where component_ID = '" + Cf.StrSql(id_get) + "'";
            Db.Execute2(update);
            Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
        }
        else if (component_type.Equals("Deduction"))
        {
            string update = "Update TBL_MasterComponent set Component_Code='" + Cf.StrSql(txtcode.Text) + "', Component_Name='" + Cf.StrSql(txtComponentname.Text) + "' where component_ID = '" + Cf.StrSql(id_get) + "'";
            Db.Execute2(update);
            Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
        }
        else { Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/"); }
    }

    protected void Cancel_Click(object sender, EventArgs e) { Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/"); }
}