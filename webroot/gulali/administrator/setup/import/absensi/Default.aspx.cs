using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

public partial class _admin_setup_general : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        App.CekPageUser(this);
        App.ProtectPageGulali(this, "Setup >> General", "view");

        tab_datakaryawan.Text = "<a href=\"" + Param.Path_Admin + "setup/import/data-karyawan/\">Data Karyawan</a>";
        tab_absensi.Text = "<a href=\"" + Param.Path_Admin + "setup/import/absensi/\">Absensi</a>";

        link_back.Text = "<a href=\"" + Param.Path_Admin + "setup/\" class=\"btn btn-custom dropdown-toggle waves-effect waves-light\">Kembali <span class=\"m-l-5\"><i class=\"fa fa-backward\"></i></span></a>";
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (lable.Text.Length == 0 && litHdn.Text.Length > 0)
        {
            try
            {
                //Db.Execute(litHdn.Text);
                Absense.SaveAbsense(litHdn.Text);
            }
            catch (Exception Ex)
            {
                lable.Text = Ex.Message.ToString();
            }
            finally
            {
                lable.Text = string.Empty;
                litHdn.Text = string.Empty;
                table.Text = string.Empty;
                save.Visible = false; import.Visible = true;
            }
        }
    }

    protected void clean_Click(object sender, EventArgs e)
    {
        lable.Text = string.Empty;
        litHdn.Text = string.Empty;
        table.Text = string.Empty;
        save.Visible = false; clean.Visible = false; import.Visible = true;
    }

    protected void import_Click(object sender, EventArgs e)
    {
        if (fileUpl.HasFile)
        {
            if (Path.GetExtension(fileUpl.FileName) == ".xlsx")
            {
                //string savedFileName = Server.MapPath("~//FileUpload//Excel//" + fileUpl.FileName);
                string savedFileName = Server.MapPath("~//FileUpload//Excel//" + DateTime.Now.ToString("yyyyMMdd_hhmmss")+".xlsx");
                
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
                        int iCheck =  Db.SingleInteger(@"select count(1)
                        FROM [Gulali_HRIS].[dbo].[TBL_Absences]
                        where [Absences_ID_No] = 2 and Convert(varchar,[Absences_Date],103) like  Convert(varchar,Convert(datetime,'25/06/2014',103),103) ");
                        if (iCheck == 1)
                        {
                            lable.Text = "Data Sudah ada";

                        }
                        else {
                            //isi lit.table
                            //fill(ds.Tables["Absensi"]);
                            lable.Text = string.Empty;
                            save.Visible = true; clean.Visible = true; import.Visible = false;
                        }
                       
                    }
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    lable.Text = ex.Message.ToString();
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

        //Header Table
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
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
        ////Simpan Query...
        //sbQuery.Append("INSERT INTO [Gulali_HRIS].[dbo].[TBL_Absences] (");
        //sbQuery.Append(" [Absences_ID_No] ");
        //sbQuery.Append(",[Absences_NIK] ");
        //sbQuery.Append(",[Absences_Name] ");
        //sbQuery.Append(",[Absences_Date] ");
        //sbQuery.Append(",[Absences_Working_Type] ");
        //sbQuery.Append(",[Absences_Scan_In] ");
        //sbQuery.Append(",[Absences_Scan_Out] ");
        //sbQuery.Append(",[Absences_Time_Late] ");
        //sbQuery.Append(",[Absences_Time_Early] ");
        //sbQuery.Append(",[Absences_Absent] ");
        //sbQuery.Append(",[Absences_Overtime] ");
        //sbQuery.Append(",[Absences_Total_WorkingHours] ");
        //sbQuery.Append(",[Absences_Day_Type] ");
        //sbQuery.Append(",[Absences_Total_Attendance] ");
        //sbQuery.Append(",[Absences_Overtime_Normal] ");
        //sbQuery.Append(",[Absences_Overtime_Weekend] ");
        //sbQuery.Append(",[Absences_Overtime_Holiday] ");
        //sbQuery.Append(",[Absences_Created_Date] ");
        //sbQuery.Append(",[Absences_Created_By] ");
        //sbQuery.Append(" ) VALUES ");

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

            sbQuery.Append(",Convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103)");
            sbQuery.Append(",'" + App.Username + "'");

            //Tutup Query
            sbQuery.Append(" ) ");

            i++;
        }

        table.Visible = true;
        x.Append("</tbody></table>");
        table.Text = x.ToString();
        lable.Text = "";
        litHdn.Text = sbQuery.ToString();
    }
}