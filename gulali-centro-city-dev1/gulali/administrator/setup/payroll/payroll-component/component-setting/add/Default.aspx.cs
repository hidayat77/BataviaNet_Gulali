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

public partial class _admin_component_setting_add : System.Web.UI.Page
{
    protected string mode { get { return App.GetStr(this, "mode"); } }
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "insert");
        if (!IsPostBack)
        {
            switch (mode)
            {
                case "1": { txtComponenttype.Text = "Regular Income"; break; }
                case "2":
                    {
                        txtComponenttype.Text = "Irregular Income";
                        div_mode1.Visible = false;
                        div_mode2.Visible = true; break;
                    }
                case "3":
                    {
                        txtComponenttype.Text = "Deduction";
                        div_mode1.Visible = false;
                        div_mode3.Visible = true; break;
                    }
                default: { Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/"); break; }
            }
        }
    }
    protected bool valid1
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(txtComponentcode) ? x : false;
            x = Fv.isComplete(txtComponentname) ? x : false;
            return x;
        }
    }

    protected void Submit1_Click(object sender, EventArgs e)
    {
        if (valid1)
        {
            if (!IsValid) { return; }
            else
            {
                string type = "";
                switch (mode)
                {
                    case "1": { type = "Regular Income"; break; }
                    case "2": { type = "Irregular Income"; break; }
                    case "3": { type = "Deduction"; break; }
                    default: { Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/"); break; }
                }
                DataTable chn = Db.Rs2("Select * from TBL_MasterComponent where component_name = '" + Cf.StrSql(txtComponentname.Text) + "' and component_type='" + type + "'");
                if (chn.Rows.Count > 0)
                {
                    return;
                }
                else
                {
                    string kind = "";
                    if (mode == "1") { kind = ddl_kind_mode1.SelectedValue; }
                    else if (mode == "2") { kind = txtComponentkind.Text; }
                    else if (mode == "3") { kind = "Potongan"; }

                    string insert = "Insert into TBL_MasterComponent(component_code,component_name,component_kind,component_type) values('" + Cf.StrSql(txtComponentcode.Text) + "','" + Cf.StrSql(txtComponentname.Text) + "','" + Cf.StrSql(kind) + "','" + Cf.StrSql(txtComponenttype.Text) + "')";
                    Db.Execute2(insert);

                    DataTable max_componentID = Db.Rs2("select max(component_id) as max from TBL_MasterComponent");

                    DataTable loop_role = Db.Rs2("select distinct Admin_Role from TBL_Payroll_Role");
                    for (int i = 0; i < loop_role.Rows.Count; i++)
                    {
                        Db.Execute2("INSERT INTO TBL_Payroll_Privilege (Component_ID, Admin_Role ,Privilege_Choose ,privilege_value) VALUES ('" + max_componentID.Rows[0]["max"].ToString() + "','" + Cf.StrSql(loop_role.Rows[i]["Admin_Role"].ToString()) + "','0', '0')");
                    }

                    Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
                }
            }
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/component-setting/");
    }
}