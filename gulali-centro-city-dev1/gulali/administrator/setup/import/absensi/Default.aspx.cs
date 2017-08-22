using GULALI.Absence;
using GULALI.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
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
            Absence_m.m_strQueryTemp = "";
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
        if (Absence_m.m_strQueryTemp.Length == 0)
        {
            try
            {
                data = Absence_f.ViewAbsence();
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
                    + "<th style=\"text-align:center;width:100px;\">No</th>"
                    + "<th style=\"text-align:center;width:100px;\">ID No</th>"
                    + "<th style=\"text-align:center;width:0.5%;\">NIK</th>"
                    + "<th style=\"text-align:center;width:20%;\">Nama</th>"
                    + "<th style=\"text-align:center;width:100px;\">Tanggal</th>"
                    + "<th style=\"text-align:center;width:100px;\">Working Type</th>"
                    + "<th style=\"text-align:center;width:100px;\">Scan Masuk</th>"
                    + "<th style=\"text-align:center;width:100px;\">Scan Pulang</th>"
                    + "<th style=\"text-align:center;width:100px;\">Terlambat</th>"
                    + "<th style=\"text-align:center;width:100px;\">Plg Cepat</th>"
                    + "<th style=\"text-align:center;width:100px;\">Absent</th>"
                    + "<th style=\"text-align:center;width:100px;\">Lembur</th>"
                    + "<th style=\"text-align:center;width:100px;\">Jml Jam Kerja</th>"
                    + "<th style=\"text-align:center;width:100px;\">Day Type</th>"
                    + "<th style=\"text-align:center;width:100px;\">Jml Kehadiran</th>"
                    + "<th style=\"text-align:center;width:100px;\">Lembur Hari Normal</th>"
                    + "<th style=\"text-align:center;width:100px;\">Lembur Akhir Pekan</th>"
                    + "<th style=\"text-align:center;width:100px;\">Lembur Hari Libur</th>"
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
        if (Absence_m._lstAbsenceImportTemp.Count >0)
        {
            try
            {
                
                Absence_f.SaveAbsence();
            }
            catch (Exception Ex)
            {
                Message_m.Show("Err : " + Ex.Message.ToString());
            }
            finally
            {
                Absence_m._lstAbsenceImportTemp = new List<Absence_m.AbsenceImportTemp>();
                table.Text = string.Empty;
                save.Visible = false; 
                clean.Visible = false; 
                import.Visible = true; 
                back.Visible = true;
                paggingImp.Visible = false;
                formfile.Visible = true;
                Message_m.Show("Simpan Data Berhasil.");
                view("1", "10");
            }
        }
    }

    protected void clean_Click(object sender, EventArgs e)
    {
        Absence_m.m_strQueryTemp = string.Empty;
        paggingImp.Visible = false;
        formfile.Visible = true;
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
            //conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
            if (Path.GetExtension(fileUpl.FileName) == ".xlsx" || Path.GetExtension(fileUpl.FileName) == ".xls")
            {
                //string savedFileName = Server.MapPath("~//FileUpload//Excel//" + fileUpl.FileName);
                string savedFileName = Server.MapPath("~//FileUpload//Excel//" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + Path.GetExtension(fileUpl.FileName));

                //Copy file to Server
                fileUpl.SaveAs(savedFileName);
                string path = savedFileName; //System.IO.Path.GetFullPath(Server.MapPath("~/FileUpload/Excel/" + savedFileName));
                //string connString = ConfigurationManager.ConnectionStrings["xlsx"].ConnectionString;
                // Create the connection object
                OleDbConnection oledbConn = new OleDbConnection();
                if (Path.GetExtension(fileUpl.FileName) == ".xlsx")
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;
                    Data Source=" + path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                else
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;
                    Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';");
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
                        int iCheck = Absence_f.iCountAbsence(strAbsences_Id_No, strAbsences_Date);
                        if (iCheck == 1)
                        {
                            //lable.Text = "<p style=\"color:red;\">Data Sudah Ada...</p>";
                            Message_m.Show("Data Sudah Ada.");
                            //fail.Text = "Data Sudah Ada.";
                        }
                        else
                        {
                            //isi lit.table dan Copy Datatablenya...
                            Absence_m.m_dtImport = ds.Tables[0].Copy();
                            fill(Absence_m.m_dtImport, "1", "10", true);
                            save.Visible = true; 
                            clean.Visible = true; 
                            import.Visible = false; 
                            back.Visible = false;
                            formfile.Visible = false;
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

    protected void fill(DataTable dtAbsensi, string num, string sum, bool bIsNew)
    {
        //paging...
        double of = 0;
        if (!string.IsNullOrEmpty(num) && !string.IsNullOrEmpty(sum))
        {
            of = Math.Ceiling(dtAbsensi.Rows.Count / System.Convert.ToDouble(sum));
            if (num != "0")
            {
                pagenumImp.Text = num;
            }
            else
            {
                pagenumImp.Text = of.ToString();
            }
            pagesumImp.Text = sum;
        }
        else
        {
        }
        int dari = 0, sampai = 0, c = 0, d = 0, ea = 0;
        int per = Cf.Int(pagesumImp.Text);
        int ke = Cf.Int(pagenumImp.Text);
        c = ke - 1;
        d = c * per;
        ea = ke * per;

        count_pageImp.Text = of.ToString();
        if (d < dtAbsensi.Rows.Count)
        {
            dari = d + 1;
            if (d == 0) dari = 1;
        }
        if (ea < dtAbsensi.Rows.Count)
        {
            sampai = ea;
        }
        else
        {
            sampai = dtAbsensi.Rows.Count;
        }

        StringBuilder x = new StringBuilder();
        StringBuilder sbQuery = new StringBuilder();
        Absence_m.m_strQueryTemp = string.Empty;
        paggingImp.Visible = true;
        //Header Table
        x.Append("<table id=\"datatable-editable\" class=\"table table-striped table-bordered dataTable no-footer dtr-inline\" role=\"grid\" aria-describedby=\"datatable-buttons_info\" style=\"width:100%\">"
                    + "<thead>"
                        + " <tr role=\"row\">"
                            + "<th style=\"text-align:center;width:100px;\">Emp ID</th>"
                            + "<th style=\"text-align:center;width:0.5%;\">NIK</th>"
                            + "<th style=\"text-align:center;width:20%;\">Nama</th>"
                            + "<th style=\"text-align:center;width:100px;\">Tanggal</th>"
                            + "<th style=\"text-align:center;width:100px;\">Working Type</th>"
                            + "<th style=\"text-align:center;width:100px;\">Scan Masuk</th>"
                            + "<th style=\"text-align:center;width:100px;\">Scan Pulang</th>"
                            + "<th style=\"text-align:center;width:100px;\">Terlambat</th>"
                            + "<th style=\"text-align:center;width:100px;\">Plg Cepat</th>"
                            + "<th style=\"text-align:center;width:100px;\">Absent</th>"
                            + "<th style=\"text-align:center;width:100px;\">Lembur</th>"
                            + "<th style=\"text-align:center;width:100px;\">Jml Jam Kerja</th>"
                            + "<th style=\"text-align:center;width:100px;\">Day Type</th>"
                            + "<th style=\"text-align:center;width:100px;\">Jml Kehadiran</th>"
                            + "<th style=\"text-align:center;width:100px;\">Lembur Hari Normal</th>"
                            + "<th style=\"text-align:center;width:100px;\">Lembur Akhir Pekan</th>"
                            + "<th style=\"text-align:center;width:100px;\">Lembur Hari Libur</th>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>");
        //Isi Table & Data Query
        List<Absence_m.AbsenceImportTemp> lstDataImport = new List<Absence_m.AbsenceImportTemp>();
        int i = 0, iMulai = dari;
        foreach (DataRow item in dtAbsensi.AsEnumerable())
        {
            #region Isi Data ke Table
            //Isi Data ke table...
            if (dari > 0)
            {
                if (iMulai <= sampai)
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
                    iMulai++;
                }
            }
            #endregion Isi Data ke Table

            #region Isi Data Temp buat Query
            //Isi Data Temp yg nanti disimpan ketika btnSave di klik...
            //Dilakukan saat sehabis baca file excel saja...
            if (bIsNew)
            {
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
                CultureInfo ci = new CultureInfo("en-US");
                Absence_m.AbsenceImportTemp dataImport = new Absence_m.AbsenceImportTemp();
                dataImport.Absence_Emp_Id = Convert.ToInt16(item[0].ToString());
                dataImport.Absence_NIK = item[1].ToString();
                dataImport.Absence_Name = item[2].ToString();
                if (item[4].ToString().Length > 0)
                    dataImport.Absence_Date = DateTime.ParseExact(item[4].ToString(), "dd/MM/yyyy", ci);
                dataImport.Absence_Working_Type = item[5].ToString();
                if (item[8].ToString().Length > 0)
                    dataImport.Absence_Scan_In = DateTime.ParseExact(item[8].ToString(), "HH:mm", ci);
                if (item[9].ToString().Length > 0)
                    dataImport.Absence_Scan_Out = DateTime.ParseExact(item[9].ToString(), "HH:mm", ci);
                if (item[11].ToString().Length > 0)
                    dataImport.Absence_Time_Late = DateTime.ParseExact(item[11].ToString(), "HH:mm", ci);
                if (item[12].ToString().Length > 0)
                    dataImport.Absence_Time_Early = DateTime.ParseExact(item[12].ToString(), "HH:mm", ci);
                if (item[13].ToString().ToUpper() == "TRUE") dataImport.Absence_Absent = 1;
                else dataImport.Absence_Absent = 0;
                if (item[14].ToString().Length > 0)
                    dataImport.Absence_Overtime = DateTime.ParseExact(item[14].ToString(), "HH:mm", ci);
                if (item[15].ToString().Length > 0)
                    dataImport.Absence_Total_WorkingHours = DateTime.ParseExact(item[15].ToString(), "HH:mm", ci);
                if (item[18].ToString() == "1")
                    dataImport.Absence_Day_Type = "NORMAL";
                else if (item[19].ToString() == "1")
                    dataImport.Absence_Day_Type = "WEEKEND";
                else if (item[20].ToString() == "1")
                    dataImport.Absence_Day_Type = "HOLIDAY";
                else dataImport.Absence_Day_Type = "";
                if (item[21].ToString().Length > 0)
                    dataImport.Absence_Total_Attendance = DateTime.ParseExact(item[21].ToString(), "HH:mm", ci);
                decimal dTemp = 0;
                decimal.TryParse(item[22].ToString(), out dTemp);
                dataImport.Absence_Overtime_Normal = dTemp;
                dTemp = 0;
                decimal.TryParse(item[23].ToString(), out dTemp);
                dataImport.Absence_Overtime_Weekend = dTemp;
                dTemp = 0;
                decimal.TryParse(item[24].ToString(), out dTemp);
                dataImport.Absence_Overtime_Holiday = dTemp;
                dataImport.Absence_Created_Date = DateTime.Now;
                dataImport.Absence_Created_By = App.Username;

                //Masukan data ke list Absence Import/_lstDataImport
                lstDataImport.Add(dataImport);
            }
            #endregion Isi Data Temp buat Query
            i++;            
        }
        //Simpan ke static list _AbsenceImportTemp untuk di pakai ketika simpan
        //Dilakukan saat sehabis baca file excel saja...
        if (bIsNew)
        {            
            Absence_m._lstAbsenceImportTemp = new List<Absence_m.AbsenceImportTemp>();
            Absence_m._lstAbsenceImportTemp = lstDataImport;
        }
        table.Visible = true;
        x.Append("</tbody></table>");
        table.Text = x.ToString();
    }

    #region Pagging View
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
    #endregion Pagging View

    #region Pagging Import
    protected void prevImp2_Click(object sender, EventArgs e)
    {
        if (Absence_m.m_dtImport.Rows.Count>0)
        fill(Absence_m.m_dtImport, "1", pagesumImp.Text, false);
    }
    protected void prevImp_Click(object sender, EventArgs e)
    {
        if (Absence_m.m_dtImport.Rows.Count > 0)
        {
            int a = Cf.Int(pagenumImp.Text);
            if (a > 1)
                a--;
            fill(Absence_m.m_dtImport, a.ToString(), pagesumImp.Text, false);
        }
    }
    protected void nextImp_Click(object sender, EventArgs e)
    {
        if (Absence_m.m_dtImport.Rows.Count > 0)
        {
            int a = Cf.Int(pagenumImp.Text);
            a++;
            if (a > Convert.ToInt32(count_pageImp.Text)) { a = Convert.ToInt32(count_pageImp.Text); }
            fill(Absence_m.m_dtImport, a.ToString(), pagesumImp.Text, false);
        }
    }
    protected void nextImp2_Click(object sender, EventArgs e)
    {
        if (Absence_m.m_dtImport.Rows.Count > 0)
            fill(Absence_m.m_dtImport, "0", pagesumImp.Text, false);
    }
    protected void page_enter_imp(object sender, EventArgs e)
    {
        if (Absence_m.m_dtImport.Rows.Count > 0)
        {
            int a = Cf.Int(pagenumImp.Text);
            if (a > Convert.ToInt32(count_pageImp.Text)) { a = Convert.ToInt32(count_pageImp.Text); }
            fill(Absence_m.m_dtImport, a.ToString(), pagesumImp.Text, false);
        }
    }
    #endregion Pagging Import

 
}