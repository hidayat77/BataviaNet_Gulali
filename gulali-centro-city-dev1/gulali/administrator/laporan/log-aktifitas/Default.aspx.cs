using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Globalization;

public partial class _admin_laporan_log_aktifitas : System.Web.UI.Page
{
    protected string id_get { get { return App.GetStr(this, "id"); } }
    protected string from_date { get { return App.GetStr(this, "from"); } }
    protected string to_date { get { return App.GetStr(this, "to"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Laporan >> Log Aktifitas", "view");
        if (!IsPostBack)
        {
            fill("1", pagesum.Text);
            ddl_username_prev();
            if (!string.IsNullOrEmpty(id_get) || !string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
            {
                from.Text = Convert.ToDateTime(from_date).ToString("dd-MM-yyyy");
                to.Text = Convert.ToDateTime(to_date).ToString("dd-MM-yyyy");
                ddl_username.SelectedValue = id_get;
            }
            else
            {
                from.Text = Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year);
                to.Text = Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year);
            } updatePanelTable.Update();
        }
    }

    protected void ddl_username_prev()
    {
        string where = "where 1=1";
        if (App.UserIsAdmin == "2") { where += " and User_IsAdmin != 1"; }
        DataTable rsa = Db.Rs("select * from TBL_User " + where + " order by User_Name asc");

        if (rsa.Rows.Count > 0)
        {
            for (int i = 0; i < rsa.Rows.Count; i++)
            {
                ddl_username.Items.Add(new ListItem(rsa.Rows[i]["User_Name"].ToString(), rsa.Rows[i]["User_ID"].ToString()));
            }
        }
    }

    protected void BtnGo(object sender, EventArgs e)
    {
        DateTime From = Convert.ToDateTime(from.Text);
        DateTime To = Convert.ToDateTime(to.Text);
        string From_Conv = From.ToString("yyyy-MM-dd").ToString();
        string To_Conv = To.ToString("yyyy-MM-dd").ToString();

        Response.Redirect(Param.Path_Admin + "laporan/log-aktifitas/?id=" + ddl_username.SelectedValue + "&from=" + From_Conv + "&to=" + To_Conv + "");
    }

    protected void fill(string num, string sum)
    {
        link_back.Text = "<a href=\"" + Param.Path_Admin + "laporan/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";

        StringBuilder x = new StringBuilder();
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                   + "<thead>"
                       + " <tr role=\"row\">"
                           + "<th style=\"text-align:center;\">No</th>"
                           + "<th style=\"width:15%;\">Username</th>"
                           + "<th style=\"width:15%;\">Role</th>"
                           + "<th style=\"width:10%;\">is Admin ?</th>"
                           + "<th>Aktifitas</th>"
                           + "<th>Tanggal</th>"
                           + "<th style=\"width:10%;text-align:center;\">Aksi</th>"
                       + "</tr>"
                   + "</thead><tbody>");

        if (!string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
        {
            DateTime From = Convert.ToDateTime(from_date);
            DateTime To = Convert.ToDateTime(to_date);
            string From_Conv = From.ToString("yyyy-MM-dd").ToString();
            string To_Conv = To.ToString("yyyy-MM-dd").ToString();

            string where = "where 1=1", user_filter;
            if (App.UserIsAdmin == "1") { where += ""; }
            else if (App.UserIsAdmin == "2") { where += " and User_IsAdmin != 1 "; }

            if (id_get == "0") { user_filter = ""; } else { user_filter = " and Log_UserID = " + Cf.StrSql(id_get) + " "; }

            DataTable rs = Db.Rs("SELECT Log_ID, Log_Activity, Log_UserID, Log_Username, Log_UserRole, User_IsAdmin, Log_Date FROM TBL_Log_History " + where + " and (Log_Date between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') " + user_filter + " order by Log_Date desc");

            lable.Text = "";
            table.Visible = true;
            if (rs.Rows.Count > 0)
            {
                double of = 0;
                if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
                {
                    of = Math.Ceiling(rs.Rows.Count / System.Convert.ToDouble(sum));
                    if (num != "0") { pagenum.Text = num; }
                    else { pagenum.Text = of.ToString(); }
                    pagesum.Text = sum;
                }
                int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
                int per = Cf.Int(pagesum.Text);
                int ke = Cf.Int(pagenum.Text);
                c = ke - 1;
                d = c * per;
                ea = ke * per;

                count_page.Text = of.ToString();
                if (d < rs.Rows.Count)
                { dari = d + 1; if (d == 0) dari = 1; }
                if (ea < rs.Rows.Count) { sampai = ea; }
                else { sampai = rs.Rows.Count; }
                if (dari > 0)
                {
                    for (int i = dari - 1; i < sampai; i++)
                    {
                        string id = rs.Rows[i]["Log_ID"].ToString(), isAdmin = "No";
                        if (rs.Rows[i]["User_IsAdmin"].ToString() == "1" || rs.Rows[i]["User_IsAdmin"].ToString() == "2") { isAdmin = "yes"; }
                        x.Append("<tr>"
                                    + "<td width=\"5%\" style=\"text-align:center;\">" + (i + 1).ToString() + "</td>"
                                    + "<td>" + rs.Rows[i]["Log_Username"].ToString() + "</td>"
                                    + "<td>" + rs.Rows[i]["Log_UserRole"].ToString() + "</td>"
                                    + "<td>" + isAdmin + "</td>"
                                    + "<td>" + rs.Rows[i]["Log_Activity"].ToString() + "</td>"
                                    + "<td>" + DateTime.Parse(rs.Rows[0]["Log_Date"].ToString()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture) + "</td>"
                                    + "<td style=\"text-align:center;\"><a href=\"/gulali/log-activity/detail/?id=" + id + "\"><i class=\"fa fa-eye\"></i></a></td>"
                                + "</tr>");
                    }
                    x.Append("</tbody></table>");
                    table.Text = x.ToString();
                }
            }
            else { lable.Text = "<div><h3 style=\"text-align:center;\"><i>Data Tidak Ditemukan</i></h3></div>"; table.Visible = false; }
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text);
    }
    protected void previous2_Click(object sender, EventArgs e)
    {
        fill("1", pagesum.Text);
    }
    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        fill(a.ToString(), pagesum.Text);
    }
    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        //if (a > 1)
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text);
    }
    protected void next2_Click(object sender, EventArgs e)
    {
        fill("0", pagesum.Text);
    }
    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        fill(a.ToString(), pagesum.Text);
    }

    protected void BtnExportExcel_New(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        if (!string.IsNullOrEmpty(from_date) || !string.IsNullOrEmpty(to_date))
        {
            DateTime From = Convert.ToDateTime(from_date);
            DateTime To = Convert.ToDateTime(to_date);
            string From_Conv = From.ToString("yyyy-MM-dd").ToString();
            string To_Conv = To.ToString("yyyy-MM-dd").ToString();

            string where = "where 1=1", user_filter;
            if (App.UserIsAdmin == "1") { where += ""; }
            else if (App.UserIsAdmin == "2") { where += " and User_IsAdmin != 1 "; }

            if (id_get == "0") { user_filter = ""; } else { user_filter = " and Log_UserID = " + Cf.StrSql(id_get) + " "; }

            string query = "SELECT Log_Employee_Name as 'Nama Karyawan', Log_Username as Username, Log_UserRole as Role, Log_Activity as Aktifitas, Log_Desc as Keterangan, case when User_IsAdmin = 1 or User_IsAdmin = 2 then 'Yes' else 'No' end as 'Is Admin ?' , Log_Date as Tanggal FROM TBL_Log_History " + where + " and (Log_Date between '" + From_Conv + " 00:01' and '" + To_Conv + " 23:59') " + user_filter + " order by Log_Date desc";

            string before = "", after = "";
            try { App.LogHistory("Generate : Laporan > Aktifitas", before, after, "Dari tanggal : " + From_Conv + " 00:01 s/d " + To_Conv + " 23:59"); }
            catch { Js.Alert(this, "Error Log Activities.\\rPlease Contact Administrator"); }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);

                            //Set Name of DataTables.
                            ds.Tables[0].TableName = "Aktifitas";

                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                foreach (DataTable dt in ds.Tables)
                                {
                                    //Add DataTable as Worksheet.
                                    wb.Worksheets.Add(dt);
                                }
                                //Export the Excel file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                //Response.ContentType = " application/vnd.ms-excel";
                                Response.AddHeader("content-disposition", "attachment;filename=Aktifitas " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}