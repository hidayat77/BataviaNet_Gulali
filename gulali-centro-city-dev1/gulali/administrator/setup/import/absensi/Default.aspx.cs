using GULALI.Absence;
using GULALI.Message;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

//using GULALI.Absence;
//using GULALI.Message;

public partial class _admin_setup_general : System.Web.UI.Page
{
    protected string status { get { return App.GetStr(this, "status"); } }
    protected string id_get { get { return App.GetStr(this, "id"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            App.CekPageUser(this);
            App.ProtectPageGulali(this, "Setup >> General", "view");

            //tab_datakaryawan.Text = "<a href=\"/\" onclick='goToURL(); return false;'>DataKaryawan</a>";
            tab_absensi.Text = "<a href=\"" + Param.Path_Admin + "setup/import/absensi/\" >Absensi</a>";

            link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
            Absence_m.strQueryTemp = "";
            //import.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            save.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            clean.OnClientClick = string.Format("return confirm('Apakah Anda Yakin?');", "kodeee");
            clean.Visible = false;
            save.Visible = false;
            total1.Visible = false;
            total2.Visible = false;
            feed.Text = "";
            view("1", "10");
            //Js.AlertProses(this.Page, "TEst");
        }
    }

    protected void view(string num, string sum)
    {
        DataTable data = new DataTable();
        //1. Ambil Data di Database...
        if (Absence_m.strQueryTemp.Length == 0)
        {
            try
            {
                data = Absence_m.ViewAbsence();
            }
            catch (Exception Ex)
            {
                Message_m.Show("Err : " + Ex.Message.ToString());
            }
        }
        //2. Buat table html nya...
        StringBuilder x = new StringBuilder();
        if (data.Rows.Count > 0)
        {
            double of = 0;
            if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
            {
                of = Math.Ceiling(data.Rows.Count / System.Convert.ToDouble(sum));
                if (num != "0")
                {
                    pagenum.Text = num;
                }
                else
                {
                    pagenum.Text = of.ToString();
                }
                pagesum.Text = sum;
            }
            else
            {
            }
            int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
            int per = Cf.Int(pagesum.Text);
            int ke = Cf.Int(pagenum.Text);
            c = ke - 1;
            d = c * per;
            ea = ke * per;

            count_page.Text = of.ToString();
            if (d < data.Rows.Count)
            {
                dari = d + 1;
                if (d == 0) dari = 1;
            }
            if (ea < data.Rows.Count)
            {
                sampai = ea;
            }
            else
            {
                sampai = data.Rows.Count;
            }
            if (dari > 0)
            {
                pagging.Visible = true;
                x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
            + "<thead>"
                + " <tr role=\"row\">"
                    + "<th style=\"text-align:center;\">No</th>"
                    + "<th style=\"text-align:center;\">ID_No</th>"
                    + "<th style=\"text-align:center;\">NIK</th>"
                    + "<th style=\"text-align:center;\">Nama</th>"
                    + "<th style=\"text-align:center;\">Tanggal</th>"
                    + "<th style=\"text-align:center;\">Working_Type</th>"
                    + "<th style=\"text-align:center;\">Scan_Masuk</th>"
                    + "<th style=\"text-align:center;\">Scan_Pulang</th>"
                    + "<th style=\"text-align:center;\">Terlambat</th>"
                    + "<th style=\"text-align:center;\">Plg_Cepat</th>"
                    + "<th style=\"text-align:center;\">Absent</th>"
                    + "<th style=\"text-align:center;\">Lembur</th>"
                    + "<th style=\"text-align:center;\">Jml_Jam_Kerja</th>"
                    + "<th style=\"text-align:center;\">Day_Type</th>"
                    + "<th style=\"text-align:center;\">Jml_Kehadiran</th>"
                    + "<th style=\"text-align:center;\">Lembur_Hari_Normal</th>"
                    + "<th style=\"text-align:center;\">Lembur_Akhir_Pekan</th>"
                    + "<th style=\"text-align:center;\">Lembur_Hari_Libur</th>"
                + "</tr>"
            + "</thead>"
            + "<tbody>");

                for (int a = dari - 1; a < sampai; a++)
                {
                    string link_edit = "", classes = "odd";

                    if (a % 2 == 0) { classes = "even"; }

                    x.Append("<tr class=\"" + classes + "\">");
                    x.Append("<td  style=\"text-align:center;\">" + (a + 1) + "</td>");//Utk No. Urut
                    for (int b = 1; b < 18; b++)
                    {
                        //Filter berdasarkan index kolom jika mau ubah stylenya..
                        x.Append("<td  style=\"padding:2px;\">" + data.Rows[a][b].ToString() + "</td>");
                    }
                    x.Append("</tr>");
                }

                x.Append("</tbody></table><br/>");
                table.Text = x.ToString();
                clean.Visible = false;
                save.Visible = false;
                import.Visible = true;
                back.Visible = true;
                total1.Visible = true;
                total2.Visible = true;

                total2.Text = data.Rows.Count.ToString();
            }
            // page_total.Text = Cf.Int(page_num.Text);
        }
        else
        {
            table.Text = "Data tidak ada.";
        }

        if (!string.IsNullOrEmpty(App.Failed))
        {
            fail.Text = App.Failed;
        }
        else
        {
            fail.Text = "";
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (Absence_m.strQueryTemp.Length > 0)
        {
            try
            {
                string strQuery = Absence_m.strQueryTemp;
                Absence_m.SaveAbsence(strQuery);
            }
            catch (Exception Ex)
            {
                Message_m.Show("Err : " + Ex.Message.ToString());
            }
            finally
            {
                Absence_m.strQueryTemp = string.Empty;
                table.Text = string.Empty;
                save.Visible = false; clean.Visible = false; import.Visible = true; back.Visible = true;
                Message_m.Show("Simpan Data Berhasil.");
                view("1", "10");
            }
        }
    }

    protected void clean_Click(object sender, EventArgs e)
    {
        Absence_m.strQueryTemp = string.Empty;
        table.Text = string.Empty;
        save.Visible = false;
        clean.Visible = false;
        import.Visible = true;
        back.Visible = true;
    }

    protected void import_Click(object sender, EventArgs e)
    {
        if (fileUpl.HasFile)
        {
            if (Path.GetExtension(fileUpl.FileName) == ".xlsx")
            {
                //string savedFileName = Server.MapPath("~//FileUpload//Excel//" + fileUpl.FileName);
                string savedFileName = Server.MapPath("~//FileUpload//Excel//" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx");

                //Copy file to Server
                fileUpl.SaveAs(savedFileName);
                string path = savedFileName; //System.IO.Path.GetFullPath(Server.MapPath("~/FileUpload/Excel/" + savedFileName));
                //string connString = ConfigurationManager.ConnectionStrings["xlsx"].ConnectionString;
                // Create the connection object
                OleDbConnection oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;
                    Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();

                // OleDbConnection oledbConn = new OleDbConnection(connString);
                try
                {
                    // Open connection
                    oledbConn.Open();

                    //Sheeat1
                    // Create OleDbCommand object and select data from worksheet Sheet1
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                    // Create new OleDbDataAdapter
                    OleDbDataAdapter oleda = new OleDbDataAdapter();

                    oleda.SelectCommand = cmd;

                    // Fill the DataSet from the data extracted from the worksheet.
                    oleda.Fill(ds, "Absensi");

                    // Bind the data to the GridView
                    // GridView1.DataSource = ds.Tables[0].DefaultView;
                    // GridView1.DataBind();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //check data sudah ada belum???
                        string strAbsences_Id_No = ds.Tables[0].Rows[0]["No# ID"].ToString();   //"2";
                        string strAbsences_Date = ds.Tables[0].Rows[0]["Tanggal"].ToString();   //"25/06/2014";
                        fail.Text = "";
                        int iCheck = Absence_m.iCountAbsence(strAbsences_Id_No, strAbsences_Date);
                        if (iCheck == 1)
                        {
                            //lable.Text = "<p style=\"color:red;\">Data Sudah Ada...</p>";
                            Message_m.Show("Data Sudah Ada.");
                            //fail.Text = "Data Sudah Ada.";
                        }
                        else
                        {
                            //isi lit.table

                            fill(ds.Tables["Absensi"]);
                            save.Visible = true; clean.Visible = true; import.Visible = false; back.Visible = false;
                        }
                    }
                    ds.Dispose();
                }
                catch (Exception Ex)
                {
                    Message_m.Show("Err : " + Ex.Message.ToString());
                    oledbConn.Close();
                }
                finally
                {
                    // Close connection
                    oledbConn.Close();
                    if (System.IO.File.Exists(savedFileName))
                    {
                        System.IO.File.Delete(savedFileName);
                    }
                }
            }
        }
    }

    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/gulali/administrator/setup/default.aspx");
        this.Page.Dispose();
    }

    protected void fill(DataTable dtAbsensi)
    {
        StringBuilder x = new StringBuilder();
        StringBuilder sbQuery = new StringBuilder();
        Absence_m.strQueryTemp = string.Empty;
        pagging.Visible = false;
        //Header Table
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;\">Emp_ID</th>"
                            + "<th style=\"text-align:center;\">NIK</th>"
                            + "<th style=\"text-align:center;\">Nama</th>"
                            + "<th style=\"text-align:center;\">Tanggal</th>"
                            + "<th style=\"text-align:center;\">Working_Type</th>"
                            + "<th style=\"text-align:center;\">Scan_Masuk</th>"
                            + "<th style=\"text-align:center;\">Scan_Pulang</th>"
                            + "<th style=\"text-align:center;\">Terlambat</th>"
                            + "<th style=\"text-align:center;\">Plg_Cepat</th>"
                            + "<th style=\"text-align:center;\">Absent</th>"
                            + "<th style=\"text-align:center;\">Lembur</th>"
                            + "<th style=\"text-align:center;\">Jml_Jam_Kerja</th>"
                            + "<th style=\"text-align:center;\">Day_Type</th>"
                            + "<th style=\"text-align:center;\">Jml_Kehadiran</th>"
                            + "<th style=\"text-align:center;\">Lembur_Hari_Normal</th>"
                            + "<th style=\"text-align:center;\">Lembur_Akhir_Pekan</th>"
                            + "<th style=\"text-align:center;\">Lembur_Hari_Libur</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");
        //Isi Table & Data Query
        int i = 0;
        foreach (DataRow item in dtAbsensi.AsEnumerable())
        {
            string link_edit = "", classes = "odd";

            if (i % 2 == 0) { classes = "even"; }

            x.Append("<tr class=\"" + classes + "\">");
            StringBuilder sbKolom = new StringBuilder();
            sbKolom.Append("<td>" + item[0].ToString() + "</td>");
            sbKolom.Append("<td>" + item[1].ToString() + "</td>");
            sbKolom.Append("<td>" + item[2].ToString() + "</td>");
            sbKolom.Append("<td>" + item[4].ToString() + "</td>");
            sbKolom.Append("<td>" + item[5].ToString() + "</td>");
            sbKolom.Append("<td>" + item[8].ToString() + "</td>");
            sbKolom.Append("<td>" + item[9].ToString() + "</td>");
            sbKolom.Append("<td>" + item[11].ToString() + "</td>");
            sbKolom.Append("<td>" + item[12].ToString() + "</td>");
            sbKolom.Append("<td>" + item[13].ToString() + "</td>");
            sbKolom.Append("<td>" + item[14].ToString() + "</td>");
            sbKolom.Append("<td>" + item[15].ToString() + "</td>");
            if (item[18].ToString() == "1") sbKolom.Append("<td>" + "NORMAL" + "</td>");
            if (item[19].ToString() == "1") sbKolom.Append("<td>" + "AKHIR PEKAN" + "</td>");
            if (item[20].ToString() == "1") sbKolom.Append("<td>" + "LIBUR" + "</td>");
            if (item[18].ToString() == "" && item[19].ToString() == "" && item[20].ToString() == "") sbKolom.Append("<td></td>");
            sbKolom.Append("<td>" + item[21].ToString() + "</td>");
            sbKolom.Append("<td>" + item[22].ToString() + "</td>");
            sbKolom.Append("<td>" + item[23].ToString() + "</td>");
            sbKolom.Append("<td>" + item[24].ToString() + "</td>");
            x.Append(sbKolom.ToString() + "</tr>");

            //Data Query
            // <Absences_ID_No, bigint,>
            // ,<Absences_NIK, varchar(50),>
            // ,<Absences_Name, varchar(200),>
            // ,<Absences_Date, datetime,>
            // ,<Absences_Working_Type, varchar(100),>
            // ,<Absences_Scan_In, time(7),>
            // ,<Absences_Scan_Out, time(7),>
            // ,<Absences_Time_Late, time(7),>
            // ,<Absences_Time_Early, time(7),>
            // ,<Absences_Absent, bit,>
            // ,<Absences_Overtime, time(7),>
            // ,<Absences_Total_WorkingHours, time(7),>
            // ,<Absences_Day_Type, varchar(50),>
            // ,<Absences_Total_Attendance, time(7),>
            // ,<Absences_Overtime_Normal, decimal(20,2),>
            // ,<Absences_Overtime_Weekend, decimal(20,2),>
            // ,<Absences_Overtime_Holiday, decimal(20,2),>
            // ,<Absences_Created_Date, datetime,>
            // ,<Absences_Created_By, varchar(100),>
            if (i > 0) sbQuery.Append(",(");
            else sbQuery.Append("(");
            sbQuery.Append(item[0].ToString());
            sbQuery.Append(",'" + item[1].ToString() + "'");
            sbQuery.Append(",'" + item[2].ToString() + "'");
            //sbQuery.Append(item[3].ToString());
            sbQuery.Append(",Convert(datetime,'" + item[4].ToString() + "',103)");
            sbQuery.Append(",'" + item[5].ToString() + "'");
            //sbQuery.Append(item[6].ToString());
            //sbQuery.Append(item[7].ToString());
            if (item[8].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[8].ToString() + ":00')");
            else sbQuery.Append(",Null");
            if (item[9].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[9].ToString() + ":00')");
            else sbQuery.Append(",Null");
            //sbQuery.Append(item[10].ToString());
            if (item[11].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[11].ToString() + ":00')");
            else sbQuery.Append(",Null");
            if (item[12].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[12].ToString() + ":00')");
            else sbQuery.Append(",Null");
            if (item[13].ToString().ToUpper() == "TRUE") sbQuery.Append(",1");//Absen = 0/1
            else sbQuery.Append(",0");//Absen = 0/1
            if (item[14].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[14].ToString() + ":00')");
            else sbQuery.Append(",Null");
            if (item[15].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[15].ToString() + ":00')");
            else sbQuery.Append(",Null");
            //sbQuery.Append(item[16].ToString());
            //sbQuery.Append(item[17].ToString());
            if (item[18].ToString() == "1") sbQuery.Append(",'NORMAL'");//day_type = Normal/Weekend/Holiday
            else if (item[19].ToString() == "1") sbQuery.Append(",'WEEKEND'");//day_type = Normal/Weekend/Holiday
            else if (item[20].ToString() == "1") sbQuery.Append(",'HOLIDAY'");//day_type = Normal/Weekend/Holiday
            else sbQuery.Append(",Null");
            if (item[21].ToString().Length > 0) sbQuery.Append(",Convert([time](7),'" + item[21].ToString() + ":00')");
            else sbQuery.Append(",Null");
            if (item[22].ToString().Length > 0) sbQuery.Append(",Convert([decimal](20,2),replace('" + item[22].ToString() + "', ',', '.'))");
            else sbQuery.Append(",Null");
            if (item[23].ToString().Length > 0) sbQuery.Append(",Convert([decimal](20,2),replace('" + item[23].ToString() + "', ',', '.'))");
            else sbQuery.Append(",Null");
            if (item[24].ToString().Length > 0) sbQuery.Append(",Convert([decimal](20,2),replace('" + item[24].ToString() + "', ',', '.'))");
            else sbQuery.Append(",Null");

            sbQuery.Append(",GETDATE()");
            sbQuery.Append(",'" + App.Username + "'");

            //Tutup Query
            sbQuery.Append(" ) ");

            i++;
        }

        table.Visible = true;
        x.Append("</tbody></table>");
        table.Text = x.ToString();
        Absence_m.strQueryTemp = sbQuery.ToString();
    }

    protected void next_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        a++;
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        view(a.ToString(), pagesum.Text);
    }

    protected void next2_Click(object sender, EventArgs e)
    {
        view("0", pagesum.Text);
    }

    protected void previous_Click(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > 1)
            a--;
        view(a.ToString(), pagesum.Text);
    }

    protected void previous2_Click(object sender, EventArgs e)
    {
        view("1", pagesum.Text);
    }

    protected void page_enter(object sender, EventArgs e)
    {
        int a = Cf.Int(pagenum.Text);
        if (a > Convert.ToInt32(count_page.Text)) { a = Convert.ToInt32(count_page.Text); }
        view(a.ToString(), pagesum.Text);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        view("1", pagesum.Text);
    }
}