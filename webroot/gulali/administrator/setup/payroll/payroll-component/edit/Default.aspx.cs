using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using AjaxControlToolkit;
using System.Globalization;

public partial class _admin_payroll_component_edit : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Payroll", "update");

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/payroll/payroll-component/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
        if (!IsPostBack) { }
        preview();
        preview_2();
    }

    protected void preview()
    {
        int element_row = 0;
        DataTable preview = Db.Rs2("Select * from TBL_Payroll_Role where Admin_Role = '" + Cf.StrSql(id_get) + "' ");
        name.Text = preview.Rows[0]["Admin_Role"].ToString();

        Literal hChapter = new Literal();
        hChapter.Text = "<div style=\"float:left;width:50%;\"><table class=\"table \" style=\"border: 1px solid #dddddd;\" >"
                    + "<thead>"
                            + " <tr align=\"center\" class=\"table_head\">"
                                + "<th style=\"text-align:center;\">No</th>"
                                + "<th>Regular Income</th>"
                                + "<th style=\"text-align:center;\">Choose</th>"
                                + "<th style=\"text-align:center;\">Value</th>"

                            + "</tr>"
                    + "</thead>";
        PH_component.Controls.Add(hChapter);

        DataTable rs = Db.Rs2("select r.role_id, r.admin_role, pp.privilege_id, pp.privilege_choose, mc.component_name, mc.component_id, privilege_value from TBL_payroll_role r left join TBL_Payroll_Privilege pp on r.admin_role=pp.admin_role left join TBL_MasterComponent mc on pp.component_id=mc.component_id where r.admin_role not in ('superadmin') and mc.component_type='Regular Income' and pp.admin_role='" + Cf.StrSql(id_get) + "' and component_name not in ('Gaji', 'Gaji terdaftar BPJS')");

        if (rs.Rows.Count > 0)
        {
            for (int i = 0; i < rs.Rows.Count; i++)
            {
                element_row++;

                HiddenField hid_id = new HiddenField();
                hid_id.ID = "component_id1_" + element_row;
                hid_id.Value = rs.Rows[i]["component_id"].ToString();
                PH_component.Controls.Add(hid_id);

                Literal lit1 = new Literal();
                lit1.Text = "<tr>"
                        + "<td style=\"text-align:center;vertical-align: middle;\" class=\"field_name\">" + (i + 1).ToString() + "</td>"
                        + "<td style=\"vertical-align: middle;\" class=\"field_name\"><strong>" + rs.Rows[i]["component_name"].ToString() + "</strong></td>"
                        + "<td style=\"text-align:center;vertical-align: middle;\">";
                PH_component.Controls.Add(lit1);

                CheckBox choose1 = new CheckBox();
                choose1.ID = "choose1_" + element_row;
                if (rs.Rows[i]["privilege_choose"].ToString() == "1")
                {
                    choose1.Checked = true;
                }
                else { choose1.Checked = false; }
                PH_component.Controls.Add(choose1);

                Literal lit2 = new Literal();
                lit2.Text = "</td><td>";
                PH_component.Controls.Add(lit2);

                TextBox component_name1 = new TextBox();
                component_name1.ID = "component_name1_" + element_row;
                component_name1.CssClass = "form-control";
                component_name1.Attributes.Add("onkeyup", "javascript:this.value=MoneyFormat(this.value);");
                //component_name1.Text = rs.Rows[i]["privilege_value"].ToString();
                if (!string.IsNullOrEmpty(rs.Rows[i]["privilege_value"].ToString()))
                {
                    decimal txtmoney = Convert.ToDecimal(rs.Rows[i]["privilege_value"].ToString());
                    component_name1.Text = (txtmoney).ToString("#,##0");
                }

                PH_component.Controls.Add(component_name1);

                Literal lit3 = new Literal();
                lit3.Text = "</td></tr>";
                PH_component.Controls.Add(lit3);
            }
            Literal lit4 = new Literal();
            lit4.Text = "</table></div>";
            PH_component.Controls.Add(lit4);
        }
    }

    protected void preview_2()
    {
        int element_row = 0;

        Literal hChapter = new Literal();
        hChapter.Text = "<div style=\"float:left;width:50%;\"><table class=\"table \" style=\"border: 1px solid #dddddd;\" >"
                   + "<thead>"
                           + " <tr align=\"center\" class=\"table_head\">"
                               + "<th style=\"text-align:center;\">No</th>"
                               + "<th>Irregular Income</th>"
                               + "<th style=\"text-align:center;\">Choose</th>"
                               + "<th style=\"text-align:center;\">Value</th>"
                           + "</tr>"
                   + "</thead>";
        PH_component.Controls.Add(hChapter);

        DataTable rs2 = Db.Rs2("select r.role_id,r.admin_role,pp.privilege_id,pp.privilege_choose, mc.component_name, mc.component_id, privilege_value from TBL_payroll_role r left join TBL_Payroll_Privilege pp on r.admin_role=pp.admin_role left join TBL_MasterComponent mc on pp.component_id=mc.component_id where r.admin_role not in ('superadmin') and mc.component_type='Irregular Income' and pp.admin_role='" + Cf.StrSql(id_get) + "'");

        if (rs2.Rows.Count > 0)
        {
            for (int i = 0; i < rs2.Rows.Count; i++)
            {
                element_row++;

                HiddenField hid_id_2 = new HiddenField();
                hid_id_2.ID = "component_id1_2_" + element_row;
                hid_id_2.Value = rs2.Rows[i]["component_id"].ToString();
                PH_component.Controls.Add(hid_id_2);

                Literal lit1_2 = new Literal();
                lit1_2.Text = "<tr>"
                        + "<td style=\"text-align:center;vertical-align: middle;\" class=\"field_name\">" + (i + 1).ToString() + "</td>"
                        + "<td style=\"vertical-align: middle;\" class=\"field_name\"><strong>" + rs2.Rows[i]["component_name"].ToString() + "</strong></td>"
                        + "<td style=\"text-align:center;vertical-align: middle;\">";
                PH_component.Controls.Add(lit1_2);

                CheckBox choose1_2 = new CheckBox();
                choose1_2.ID = "choose1_2_" + element_row;
                if (rs2.Rows[i]["privilege_choose"].ToString() == "1")
                {
                    choose1_2.Checked = true;
                }
                else { choose1_2.Checked = false; }
                PH_component.Controls.Add(choose1_2);

                Literal lit2_2 = new Literal();
                lit2_2.Text = "</td><td>";
                PH_component.Controls.Add(lit2_2);

                TextBox component_name1_2 = new TextBox();
                component_name1_2.ID = "component_name1_2_" + element_row;
                component_name1_2.CssClass = "form-control";
                component_name1_2.Attributes.Add("onkeyup", "javascript:this.value=MoneyFormat(this.value);");

                if (!string.IsNullOrEmpty(rs2.Rows[i]["privilege_value"].ToString()))
                {
                    decimal txtmoney = Convert.ToDecimal(rs2.Rows[i]["privilege_value"].ToString());
                    component_name1_2.Text = (txtmoney).ToString("#,##0");
                }

                PH_component.Controls.Add(component_name1_2);

                Literal lit3_2 = new Literal();
                lit3_2.Text = "</td></tr>";
                PH_component.Controls.Add(lit3_2);
            }
            Literal lit4_2 = new Literal();
            lit4_2.Text = "</table></div>";
            PH_component.Controls.Add(lit4_2);
        }
    }

    protected void BtnSubmit(object sender, EventArgs e)
    {
        int element_row = 0;

        DataTable rs = Db.Rs2("select r.role_id, r.admin_role, pp.privilege_id, pp.privilege_choose, mc.component_name, mc.component_id, privilege_value from TBL_payroll_role r left join TBL_Payroll_Privilege pp on r.admin_role=pp.admin_role left join TBL_MasterComponent mc on pp.component_id=mc.component_id where r.admin_role not in ('superadmin') and mc.component_type='Regular Income' and pp.admin_role='" + Cf.StrSql(id_get) + "' and component_name not in ('Gaji', 'Gaji terdaftar BPJS')");

        if (rs.Rows.Count > 0)
        {
            for (int i = 1; i <= rs.Rows.Count; i++)
            {
                element_row++;
                HiddenField component_id = ((HiddenField)PH_component.FindControl("component_id1_" + (i).ToString()));
                CheckBox choose1 = ((CheckBox)PH_component.FindControl("choose1_" + (i).ToString()));
                TextBox component_name1 = ((TextBox)PH_component.FindControl("component_name1_" + (i).ToString()));

                int choose_1 = 0;
                if (choose1.Checked == true)
                {
                    choose_1 = 1;
                }

                string sql = "UPDATE TBL_Payroll_Privilege SET Privilege_Choose = '" + choose_1 + "', privilege_value = '" + component_name1.Text.ToString().Replace(".", "") + "'  WHERE Component_id = '" + component_id.Value + "' and upper(Admin_Role) = '" + Cf.Upper(Cf.StrSql(id_get)) + "'";
                Db.Execute2(sql);
            }
        }

        DataTable rs2 = Db.Rs2("select r.role_id,r.admin_role,pp.privilege_id,pp.privilege_choose, mc.component_name, mc.component_id from TBL_payroll_role r left join TBL_Payroll_Privilege pp on r.admin_role=pp.admin_role left join TBL_MasterComponent mc on pp.component_id=mc.component_id where r.admin_role not in ('superadmin') and mc.component_type='Irregular Income' and pp.admin_role='" + Cf.StrSql(id_get) + "'");
        if (rs2.Rows.Count > 0)
        {
            for (int i = 1; i <= rs2.Rows.Count; i++)
            {
                element_row++;
                HiddenField component_id = ((HiddenField)PH_component.FindControl("component_id1_2_" + (i).ToString()));
                CheckBox choose1_2 = ((CheckBox)PH_component.FindControl("choose1_2_" + (i).ToString()));
                TextBox component_name1_2 = ((TextBox)PH_component.FindControl("component_name1_2_" + (i).ToString()));

                int choose_2 = 0;
                if (choose1_2.Checked == true)
                {
                    choose_2 = 1;
                }

                string sql = "UPDATE TBL_Payroll_Privilege SET Privilege_Choose = '" + choose_2 + "', privilege_value = '" + component_name1_2.Text.ToString().Replace(".", "") + "'  WHERE Component_id = '" + component_id.Value + "' and upper(Admin_Role) = '" + Cf.Upper(Cf.StrSql(id_get)) + "'";
                Db.Execute2(sql);
            }
        }

        DataTable rs3 = Db.Rs2("select r.role_id,r.admin_role,pp.privilege_id,pp.privilege_choose, mc.component_name, mc.component_id from TBL_payroll_role r left join TBL_Payroll_Privilege pp on r.admin_role=pp.admin_role left join TBL_MasterComponent mc on pp.component_id=mc.component_id where r.admin_role not in ('superadmin') and mc.component_type='Deduction' and pp.admin_role='" + Cf.StrSql(id_get) + "'");
        if (rs3.Rows.Count > 0)
        {
            for (int i = 1; i <= rs3.Rows.Count; i++)
            {
                element_row++;
                HiddenField component_id = ((HiddenField)PH_component.FindControl("component_id1_3_" + (i).ToString()));
                CheckBox choose1_3 = ((CheckBox)PH_component.FindControl("choose1_3_" + (i).ToString()));
                TextBox component_name1_3 = ((TextBox)PH_component.FindControl("component_name1_3_" + (i).ToString()));

                int choose_3 = 0;
            }
        }

        Response.Redirect(Param.Path_Admin + "setup/payroll/payroll-component/");
    }
}