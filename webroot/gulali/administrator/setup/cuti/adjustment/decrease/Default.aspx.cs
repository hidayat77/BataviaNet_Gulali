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
using System.Security.Cryptography;
using AjaxControlToolkit;
using System.Globalization;

public partial class _admin_setting_cuti_tambah : System.Web.UI.Page
{
    protected string from_date { get { return App.GetStr(this, "from_date"); } }
    protected string to_date { get { return App.GetStr(this, "to_date"); } }
    protected string month { get { return App.GetStr(this, "month"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> Cuti", "Add");

        link_href.Text = "<a href=\"" + Param.Path_Admin + "setup/cuti/adjustment/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        if (!IsPostBack)
        {
            Button1.Enabled = false;
            Select_1.Visible = false;
            Select_2.Visible = false;
            Select_3.Visible = false;
            tr_department.Visible = false;
            tr_division.Visible = false;
            tr_employee.Visible = false;
            tr_radio.Visible = false;
            radio_department.Checked = true;
            combo_department();
            combo_division(ddl_department.SelectedValue);
            combo_employee(ddl_division.SelectedValue);
        }
    }
    protected bool valid1
    {
        get
        {
            bool x = true;
            x = Fv.isComplete(txt_desc) ? x : false;
            x = Fv.isDecimal(txt_value) ? x : false;
            return x;
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (valid1)
        {
            if (!IsValid)
            {
                return;
            }
            else
            {
                string employee_id = "";
                if (Button1.Enabled.Equals(false))
                {
                    employee_id = "select Employee_ID, Department_ID, Employee_Sum_LeaveBalance, Division_ID FROM TBL_Employee where Employee_ID != 1";
                    DataTable rs_employee_id = Db.Rs(employee_id);
                    if (rs_employee_id.Rows.Count > 0)
                    {
                        string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));
                        for (int i = 0; i < rs_employee_id.Rows.Count; i++)
                        {
                            string ID_employee = rs_employee_id.Rows[i]["Employee_ID"].ToString();
                            string Dep_employee = rs_employee_id.Rows[i]["Department_ID"].ToString();
                            string Div_employee = rs_employee_id.Rows[i]["Division_ID"].ToString();
                            string insert = "Insert into TBL_History_Leave_Balance (employee_id, division_id, department_id, balance_value, Balance_Create_By, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Div_employee + "', '" + Dep_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',2)";
                            Db.Execute(insert);
                            Hitung_cuti(ID_employee, txtvalue);
                        }
                    }
                }
                else if (Button2.Enabled.Equals(false))
                {
                    int a = Cf.Int(ddl_department.SelectedValue);
                    int b = Cf.Int(ddl_division.SelectedValue);
                    int c = Cf.Int(ddl_employee.SelectedValue);
                    if ((a != 0) && (b == 0) && (c == 0))
                    {
                        employee_id = "select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Department_ID = '" + Cf.StrSql(ddl_department.SelectedValue) + "' and Employee_ID != 1";
                    }
                    else if ((a != 0) && (b != 0) && (c == 0))
                    {
                        employee_id = "select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Division_ID = '" + Cf.StrSql(ddl_division.SelectedValue) + "' and Employee_ID != 1";
                    }
                    else if ((a != 0) && (b != 0) && (c != 0))
                    {
                        employee_id = "select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Employee_ID = '" + Cf.StrSql(ddl_employee.SelectedValue) + "' and Employee_ID != 1";
                    }
                    else
                    {
                        Response.Redirect("../");
                    }
                    DataTable rs_employee_id = Db.Rs(employee_id);
                    if (rs_employee_id.Rows.Count > 0)
                    {
                        string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));
                        for (int i = 0; i < rs_employee_id.Rows.Count; i++)
                        {
                            string ID_employee = rs_employee_id.Rows[i]["Employee_ID"].ToString();
                            string Dep_employee = rs_employee_id.Rows[i]["Department_ID"].ToString();
                            string Div_employee = rs_employee_id.Rows[i]["Division_ID"].ToString();
                            string insert = "Insert into TBL_History_Leave_Balance (employee_id, division_id, department_id, balance_value, Balance_Create_By, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Div_employee + "', '" + Dep_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',2)";
                            Db.Execute(insert);
                            Hitung_cuti(ID_employee, txtvalue);
                        }
                    }
                }
                else if (Button3.Enabled.Equals(false))
                {
                    if (radio_department.Checked.Equals(true))
                    {
                        string participant2 = txt_multiple_department.Text.Replace(",", "','");
                        DataTable data_department = Db.Rs("select a.Employee_ID as 'Employee_ID', a.Department_ID as 'Department_ID', a.Division_ID as 'Division_ID', b.Department_ID as department_id from TBL_Employee a join TBL_Department b on a.Department_ID = b.Department_ID where b.Department_Name in ('" + participant2 + "') and Employee_ID != 1");
                        if (data_department.Rows.Count > 0)
                        {
                            string id_department = "";
                            for (int i = 0; i < data_department.Rows.Count; i++)
                            {
                                id_department = data_department.Rows[i]["department_id"].ToString();
                                DataTable rs_multiply = Db.Rs("select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Department_ID = '" + Cf.StrSql(id_department) + "'");
                                if (rs_multiply.Rows.Count > 0)
                                {
                                    string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));
                                    string ID_employee = data_department.Rows[i]["Employee_ID"].ToString();
                                    string Dep_employee = data_department.Rows[i]["Department_ID"].ToString();
                                    string Div_employee = data_department.Rows[i]["Division_ID"].ToString();
                                    string insert = "Insert into TBL_History_Leave_Balance (employee_id, division_id, department_id, balance_value, Balance_Create_By, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Div_employee + "', '" + Dep_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',2)";
                                    Db.Execute(insert);
                                    Hitung_cuti(ID_employee, txtvalue);
                                }

                            }
                        }
                    }
                    else if (radio_division.Checked.Equals(true))
                    {
                        string participant2 = txt_multiple_division.Text.Replace(",", "','");
                        DataTable data_division = Db.Rs("select a.Employee_ID as 'Employee_ID', a.Department_ID as 'Department_ID', a.Division_ID as 'Division_ID', b.Division_ID as division_id from TBL_Employee a join TBL_Division b on a.Division_ID = b.Division_ID where b.Division_Name in ('" + participant2 + "') and Employee_ID != 1");
                        if (data_division.Rows.Count > 0)
                        {
                            string id_division = "";
                            for (int i = 0; i < data_division.Rows.Count; i++)
                            {
                                id_division = data_division.Rows[i]["division_id"].ToString();
                                DataTable rs_multiply = Db.Rs("select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Division_ID = '" + Cf.StrSql(id_division) + "'");
                                if (rs_multiply.Rows.Count > 0)
                                {
                                    string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));
                                    string ID_employee = data_division.Rows[i]["Employee_ID"].ToString();
                                    string Dep_employee = data_division.Rows[i]["Department_ID"].ToString();
                                    string Div_employee = data_division.Rows[i]["Division_ID"].ToString();
                                    string insert = "Insert into TBL_History_Leave_Balance (employee_id, division_id, department_id, balance_value, Balance_Create_By, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Div_employee + "', '" + Dep_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',2)";
                                    Db.Execute(insert);
                                    Hitung_cuti(ID_employee, txtvalue);
                                }
                            }
                        }
                    }
                    else if (radio_employee.Checked.Equals(true))
                    {
                        string participant2 = txt_multiple_employee.Text.Replace(",", "','");
                        DataTable data_employee = Db.Rs("select Employee_ID, Department_ID, Division_ID, Employee_Full_Name, Employee_Last_Name from TBL_Employee where (Employee_Full_Name) in ('" + participant2 + "')");
                        if (data_employee.Rows.Count > 0)
                        {
                            string id_employee = "";
                            for (int i = 0; i < data_employee.Rows.Count; i++)
                            {
                                id_employee = data_employee.Rows[i]["Employee_ID"].ToString();
                                DataTable rs_multiply = Db.Rs("select Employee_ID, Department_ID, Division_ID FROM TBL_Employee where Employee_ID = '" + Cf.StrSql(id_employee) + "'");
                                if (rs_multiply.Rows.Count > 0)
                                {
                                    string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));
                                    string ID_employee = data_employee.Rows[i]["Employee_ID"].ToString();
                                    string Dep_employee = data_employee.Rows[i]["Department_ID"].ToString();
                                    string Div_employee = data_employee.Rows[i]["Division_ID"].ToString();
                                    string insert = "Insert into TBL_History_Leave_Balance (employee_id, division_id, department_id, balance_value, Balance_Create_By, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Div_employee + "', '" + Dep_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',2)";
                                    Db.Execute(insert);
                                    Hitung_cuti(ID_employee, txtvalue);
                                }
                            }
                        }
                    }
                }

                //string ID_employee = rs_employee_id.Rows[0]["Employee_ID"].ToString();
                //string txtvalue = Cf.StrSql(txt_value.Text.Replace(",", "."));

                //string insert = "Insert into TBL_History_Leave_Balance (employee_id, balance_value, balance_create_by_id, balance_create_date, balance_remarks, balance_status) values('" + ID_employee + "', '" + Cf.StrSql(txtvalue) + "','" + Cf.StrSql(App.Employee_ID) + "',GETDATE(),'" + Cf.StrSql(txt_desc.Text) + "',1)";
                //Db.Execute(insert);

                ////////////////////////////////////////////////
                //string potongcuti = Cf.StrSql(txt_value.Text);
                //decimal datacut = Int32.Parse(data_cuti);
                //decimal potongcuti = Int32.Parse(txtvalue);
                //////////////////////////////////////////////

                Response.Redirect("../");
            }
        }
    }
    public void Hitung_cuti(string employee_id, string value)
    {
        //RUMUS PENGURANGAN CUTI DI TBL_EMPLOYEE
        DataTable rss = Db.Rs("select Employee_Sum_LeaveBalance from TBL_Employee where Employee_ID = '" + employee_id + "'");
        if (rss.Rows.Count > 0)
        {
            string data_cuti = rss.Rows[0]["Employee_Sum_LeaveBalance"].ToString();
            decimal datacut = Convert.ToDecimal(data_cuti);
            decimal kurangcuti = Convert.ToDecimal(value);

            int cuti = Convert.ToInt32(datacut);
            int kurang = Convert.ToInt32(kurangcuti);
            int sisa = cuti - kurang;
            string jumlah = sisa.ToString();

            string update = "Update TBL_Employee set Employee_Sum_LeaveBalance ='" + Cf.StrSql(jumlah) + "' where Employee_ID = '" + Cf.StrSql(employee_id) + "'";
            Db.Execute(update);
        }
    }
    protected void combo_department()
    {
        //DEPARTMENT
        DataTable rs_department = Db.Rs("select * from TBL_Department order by Department_Name asc");

        if (rs_department.Rows.Count > 0)
        {
            for (int i = 0; i < rs_department.Rows.Count; i++)
            {
                ddl_department.Items.Add(new ListItem(rs_department.Rows[i]["Department_Name"].ToString(), rs_department.Rows[i]["Department_ID"].ToString()));
            }
        }
    }
    protected void combo_division(string id_department)
    {
        //DIVISION
        Select_2.Visible = false;
        Select_3.Visible = false;
        ddl_division.Items.Clear();
        ddl_employee.Items.Clear();
        DataTable rs_division = Db.Rs("select * from TBL_Division where Department_ID = '" + Cf.Int(id_department) + "'order by Division_Name asc");

        if (rs_division.Rows.Count > 0)
        {
            ddl_division.Items.Add(new ListItem("-- Pilih Divisi --", "0"));
            for (int i = 0; i < rs_division.Rows.Count; i++)
            {
                ddl_division.Items.Add(new ListItem(rs_division.Rows[i]["Division_Name"].ToString(), rs_division.Rows[i]["Division_ID"].ToString()));
            }
        }
        updatePanel1.Update();
    }
    protected void combo_employee(string id_division)
    {
        //EMPLOYEE
        Select_3.Visible = false;
        ddl_employee.Items.Clear();
        DataTable rsa = Db.Rs("select * from TBL_Employee where Employee_ID != 1 and Employee_ID != '" + App.Employee_ID + "' and Division_ID = '" + Cf.Int(id_division) + "'  order by Employee_Full_Name asc");

        if (rsa.Rows.Count > 0)
        {
            ddl_employee.Items.Add(new ListItem("-- Pilih Karyawan --", "0"));
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_employee.Items.Add(new ListItem(rsa.Rows[i]["Employee_Full_Name"].ToString(), rsa.Rows[i]["Employee_ID"].ToString()));
            }
            updatePanel1.Update();
        }
    }
    protected void selected_department(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_department.SelectedValue);
        if (a.Equals(0))
        {
            combo_division(ddl_department.SelectedValue);
            Select_2.Visible = false;
        }
        else
        {
            combo_division(ddl_department.SelectedValue);
            Select_2.Visible = true;
        }
    }
    protected void selected_division(object sender, EventArgs e)
    {
        int a = Cf.Int(ddl_division.SelectedValue);
        if (a.Equals(0))
        {
            combo_employee(ddl_division.SelectedValue);
            Select_3.Visible = false;
        }
        else
        {
            combo_employee(ddl_division.SelectedValue);
            Select_3.Visible = true;
        }
    }
    /////////////////////////////////////////////////
    protected void btnview_Click_department(object sender, EventArgs e)
    {
        OpenNewWindow("list_department.aspx");
    }
    protected void btnview_Click_division(object sender, EventArgs e)
    {
        OpenNewWindow("list_division.aspx");
    }
    protected void btnview_Click_employee(object sender, EventArgs e)
    {
        OpenNewWindow("list_employee.aspx");
    }

    public void OpenNewWindow(string url)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format(
            "<script>window.open('{0}','Link','toolbar=no,statusbar=no,scrollbars=yes,width=700,height=400');</script>", url));
    }
    /////////////////////////////////////////////////
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../");
    }
    protected void selected_1(object sender, EventArgs e)
    {
        Button1.Enabled = false;
        Button2.Enabled = true;
        Button3.Enabled = true;
        Select_1.Visible = false;
        Select_2.Visible = false;
        Select_3.Visible = false;
        tr_department.Visible = false;
        tr_division.Visible = false;
        tr_employee.Visible = false;
        tr_radio.Visible = false;
        radio_department.Checked = true;
        radio_division.Checked = false;
        radio_employee.Checked = false;
        updatePanel1.Update();
    }
    protected void selected_2(object sender, EventArgs e)
    {
        Button1.Enabled = true;
        Button2.Enabled = false;
        Button3.Enabled = true;
        Select_1.Visible = true;
        tr_department.Visible = false;
        tr_division.Visible = false;
        tr_employee.Visible = false;
        tr_radio.Visible = false;
        radio_department.Checked = true;
        radio_division.Checked = false;
        radio_employee.Checked = false;
        updatePanel1.Update();
    }
    protected void selected_3(object sender, EventArgs e)
    {
        Button1.Enabled = true;
        Button2.Enabled = true;
        Button3.Enabled = false;
        Select_1.Visible = false;
        Select_2.Visible = false;
        Select_3.Visible = false;
        tr_department.Visible = true;
        tr_radio.Visible = true;
        radio_department.Checked = true;
        radio_division.Checked = false;
        radio_employee.Checked = false;
        updatePanel1.Update();
    }
    protected void radio_1(object sender, EventArgs e)
    {
        tr_department.Visible = true;
        tr_division.Visible = false;
        tr_employee.Visible = false;
        updatePanel1.Update();
    }
    protected void radio_2(object sender, EventArgs e)
    {
        tr_department.Visible = false;
        tr_division.Visible = true;
        tr_employee.Visible = false;
        updatePanel1.Update();
    }
    protected void radio_3(object sender, EventArgs e)
    {
        tr_department.Visible = false;
        tr_division.Visible = false;
        tr_employee.Visible = true;
        updatePanel1.Update();
    }
    public string[] RemoveDuplicates(string[] inputArray)
    {
        List<string> distinctArray = new List<string>();
        foreach (string element in inputArray)
        {
            if (!distinctArray.Contains(element))
                distinctArray.Add(element);
        }

        return distinctArray.ToArray<string>();
    }
}
