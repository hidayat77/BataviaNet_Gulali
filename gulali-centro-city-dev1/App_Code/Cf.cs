using System;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Common Functions
/// </summary>
public class Cf {
	//String Conversion
	public static string StrJs(string Input) {
		return Input.Trim().Replace("\\", "\\\\").Replace("'", "\\'");
	}
	public static string StrSql(string Input) {
		return Input.Replace("'", "''");
	}
    public static string StrLogin(string Input)
    {
        return Cf.StrPtk(Cf.Upper(Input).Replace("<", "&lt;").Replace(">", "&gt;").Replace("SCRIPT", "").Replace("'", "''"));
    }
    public static string StrLogin2(string Input)
    {
        return Cf.StrPtk(Cf.Upper(Input).Replace("<", "&lt;").Replace(">", "&gt;").Replace("SCRIPT", "").Replace("'", "''"));
    }
	public static string StrKet(string Input) {
		return Input.Replace("\n", "<br />");
	}
    public static string StrBlk(string Input)
    {
        return Input.Replace("<br />","\n");
    }
    public static string StrPtk(string Input)
    {
        return Input.Replace("\"","");
    }
	public static string StrCut(string Input, int Length) {
		if (Input.Length <= Length)
			return Input;
		else
			return Input.Substring(0, Length) + "...";
	}

	//Numeric Conversion
	public static string Num(decimal Input) {
		return Input.ToString("N2");
	}
    public static string Num(decimal Input,int val)
    {
        return Input.ToString("N"+val);
    }
	public static string Num(decimal? Input) {
		return Input.HasValue ? Input.Value.ToString("N2") : "0";
	}
	public static string Num(int Input) {
		return Input.ToString("N0");
	}
	public static string Num(long Input) {
		return Input.ToString("N0");
	}
	public static string Kb(decimal Input) {
		return (Input / 1024).ToString("N0") + " kb";
	}
 
    public static int Int(string Input)
    {
        return Fv.isInt(Input) ? Convert.ToInt32(Input) : 0;
    }
    public static double Double(string Input)
    {
        return Fv.isDouble(Input) ? Convert.ToDouble(Input) : 0;
    }

	//Capitalization
	public static string Normal(string Input) {
		return Input.Trim();
	}
	public static string Upper(string Input) {
		return Input.Trim().ToUpper();
	}
	public static string UpperTitle(string Input) {
		System.Globalization.TextInfo x = new System.Globalization.CultureInfo("en-US", false).TextInfo;
		return x.ToTitleCase(Input).Trim();
	}
	public static string UpperFirst(string Input) {
		string s = Input.Trim();

		if (s.Length > 0) {
			StringBuilder x = new StringBuilder(s);
			x[0] = Char.ToUpper(x[0]);
			return x.ToString();
		}
		else return s;
	}
	public static string Lower(string Input) {
		return Input.Trim().ToLower();
	}

	//Date Time Conversion
	public static string Tgl(DateTime Input) {
		return Input.ToString("dd-MM-yyyy");
	}
	public static string Tgl(DateTime? Input) {
		if (Input.HasValue) {
			DateTime x = Input.GetValueOrDefault();
			return x.ToString("dd-MM-yyyy");
		}
		else
			return "";
	}
	public static string Jam(DateTime Input) {
		return Input.ToString("HH:mm:ss");
	}
	public static string Jam(DateTime? Input) {
		if (Input.HasValue) {
			DateTime x = Input.GetValueOrDefault();
			return x.ToString("HH:mm:ss");
		}
		else
			return "";
	}
	public static string TglPeriode(DateTime Input) {
		return Cf.TglNamaBln(Input.Month, false) + " " + Input.Year;
	}
	public static string TglPeriodeDate(DateTime Input) {
		return Cf.TglNamaBln(Input.Month, false) + " " + Input.Day+ ", " + Input.Year;
	}
	public static string TglJam(DateTime Input) {
		return Input.ToString("dd-MM-yyyy HH:mm:ss");
	}
	public static string TglJam(DateTime? Input) {
		if (Input.HasValue) {
			DateTime x = Input.GetValueOrDefault();
			return x.ToString("dd-MM-yyyy HH:mm:ss");
		}
		else
			return "";
	}
	public static string TglSql(DateTime Input) {
		return Input.ToString("yyyyMMdd");
	}
	public static string TglJamSql(DateTime Input) {
		return Input.ToString("yyyyMMddHHmmss");
	}
	public static string TglNamaHari(DateTime Input) {
		string x = String.Empty;

		switch (Input.DayOfWeek) {
			case DayOfWeek.Monday: x = "Monday"; break;
			case DayOfWeek.Tuesday: x = "Tuesday"; break;
			case DayOfWeek.Wednesday: x = "Wednesday"; break;
			case DayOfWeek.Thursday: x = "Thursday"; break;
			case DayOfWeek.Friday: x = "Friday"; break;
			case DayOfWeek.Saturday: x = "Saturday"; break;
			case DayOfWeek.Sunday: x = "Monday"; break;
		}

		return x;
	}
	public static string TglNamaBln(int m, bool Roman) {
		string x = "";
		if (!Roman) {
			switch (m) {
				case 1: x = "January"; break;
				case 2: x = "February"; break;
				case 3: x = "March"; break;
				case 4: x = "April"; break;
				case 5: x = "May"; break;
				case 6: x = "June"; break;
				case 7: x = "July"; break;
				case 8: x = "August"; break;
				case 9: x = "September"; break;
				case 10: x = "October"; break;
				case 11: x = "November"; break;
				case 12: x = "December"; break;
			}
		}
		else {
			switch (m) {
				case 1: x = "I"; break;
				case 2: x = "II"; break;
				case 3: x = "III"; break;
				case 4: x = "IV"; break;
				case 5: x = "V"; break;
				case 6: x = "VI"; break;
				case 7: x = "VII"; break;
				case 8: x = "VIII"; break;
				case 9: x = "IX"; break;
				case 10: x = "X"; break;
				case 11: x = "XI"; break;
				case 12: x = "XII"; break;
			}
		}
		return x;
	}
    public static string TglNamaBln(string duedate , string separator)
    {
        string dd = "", mm = "", yyyy = "";
        dd = duedate.Substring(6, 2);
        mm = duedate.Substring(4, 2);
        yyyy = duedate.Substring(0, 4);
        string tgl ="";
        string x = "";
        switch (Convert.ToInt32(mm))
        {
            case 1: x = "Jan"; break;
            case 2: x = "Feb"; break;
            case 3: x = "Mar"; break;
            case 4: x = "Apr"; break;
            case 5: x = "May"; break;
            case 6: x = "Jun"; break;
            case 7: x = "Jul"; break;
            case 8: x = "Aug"; break;
            case 9: x = "Sep"; break;
            case 10: x = "Oct"; break;
            case 11: x = "Nov"; break;
            case 12: x = "Dec"; break;
        }
        tgl = dd + separator + x + separator + yyyy;
        return tgl;
    }
	
	 public static string Tglddmmyyy(string duedate , string separator)
    {
        string dd = "", mm = "", yyyy = "";
        dd = duedate.Substring(6, 2);
        mm = duedate.Substring(4, 2);
        yyyy = duedate.Substring(0, 4);
        string tgl ="";
       
        tgl = dd + separator + mm + separator + yyyy;
        return tgl;
    }
	public static string NamaBln(int month_angka)
    {
        
        string x = "";
        switch (Convert.ToInt32(month_angka))
        {
            case 1: x = "jan"; break;
            case 2: x = "feb"; break;
            case 3: x = "mar"; break;
            case 4: x = "apr"; break;
            case 5: x = "may"; break;
            case 6: x = "jun"; break;
            case 7: x = "jul"; break;
            case 8: x = "aug"; break;
            case 9: x = "sep"; break;
            case 10: x = "oct"; break;
            case 11: x = "nov"; break;
            case 12: x = "dec"; break;
        }
        
		return x;
    }
    public static string TglSql(string startdate)
    {
        string dd = "", mm = "", yyyy = "";
        dd = startdate.Substring(0, 2);
        mm = startdate.Substring(3, 2);
        yyyy = startdate.Substring(6, 4);
        string tgl = "";
        
        tgl = dd + mm + yyyy;
        return tgl;
    }
    public static string TglSqlPension(string startdate)
    {
        string dd = "", mm = "", yyyy = "";
        dd = startdate.Substring(0, 2);
        mm = startdate.Substring(3, 2);
        yyyy = startdate.Substring(6, 4);
        string tgl = "";

        tgl = yyyy + mm + dd;
        return tgl;
    }
	
	public static string TglPublic(DateTime Input)
	{
		return TglNamaBln(Input.Month, false) + " " + Input.Day + ", " + Input.Year;
	}

	//Periode Function
	public static DateTime AwalMinggu() {
		return AwalMinggu(DateTime.Today);
	}
	public static DateTime AkhirMinggu() {
		return AkhirMinggu(DateTime.Today);
	}
	public static DateTime AwalMinggu(DateTime x) {
		return x.AddDays((Convert.ToInt32(x.DayOfWeek) - 1) * -1);
	}
	public static DateTime AkhirMinggu(DateTime x) {
		return x.AddDays((7 - Convert.ToInt32(x.DayOfWeek)));
	}
	public static DateTime AwalBulan() {
		return AwalBulan(DateTime.Today.Month, DateTime.Today.Year);
	}
	public static DateTime AkhirBulan() {
		return AkhirBulan(DateTime.Today.Month, DateTime.Today.Year);
	}
	public static DateTime AwalBulan(int m, int y) {
		return new DateTime(y, m, 1);
	}
	public static DateTime AkhirBulan(int m, int y) {
		return new DateTime(y, m, DateTime.DaysInMonth(y, m));
	}
	public static DateTime AwalTahun() {
		return AwalTahun(DateTime.Today.Year);
	}
	public static DateTime AkhirTahun() {
		return AkhirTahun(DateTime.Today.Year);
	}
	public static DateTime AwalTahun(int y) {
		return new DateTime(y, 1, 1);
	}
	public static DateTime AkhirTahun(int y) {
		return new DateTime(y, 12, 31);
	}
	public static void SwapTgl(string dari, string sampai, ref DateTime Dari, ref DateTime Sampai) {
		if (dari == null) dari = "";
		if (sampai == null) sampai = "";

		DateTime z;
		if (!DateTime.TryParse(dari, out z)) dari = "";
		if (!DateTime.TryParse(sampai, out z)) sampai = "";

		Dari = dari != "" ?
			Convert.ToDateTime(dari) : new DateTime(1800, 1, 1);

		Sampai = sampai != "" ?
			Convert.ToDateTime(sampai) : new DateTime(2200, 1, 1);

		if (Dari > Sampai) { //swap!
			DateTime x = Dari;
			Dari = Sampai;
			Sampai = x;
		}
	}

	//List Builder
	public static void BindTahun(DropDownList ddl) {
		int y = DateTime.Today.Year;

		for (int i = y - 50; i <= y + 50; i++)
			ddl.Items.Add(new ListItem(i.ToString()));

		ddl.SelectedValue = y.ToString();
	}
    public static void BindTahun2(DropDownList ddl)
    {
        int y = DateTime.Today.Year;
        ddl.Items.Add(new ListItem(" "));

        for (int i = y - 50; i <= y + 50; i++)
            ddl.Items.Add(new ListItem(i.ToString()));

        ddl.SelectedValue = " ";
    }
	public static void BindBulan(DropDownList ddl) {
		for (int i = 1; i <= 12; i++)
			ddl.Items.Add(new ListItem(TglNamaBln(i, false), i.ToString()));

		ddl.SelectedValue = DateTime.Today.Month.ToString();
	}

    public static string Str(object r)
    {
        if (r is DBNull)
            return "";
        else
            return r.ToString().Replace("'", "''").ToUpper().Trim();
    }

    public static string money(double value)
    {
        string xxx = "";
        xxx = String.Format("{0:#,0.00}", value);
        return xxx;
    }
    public static string money(Int32 value)
    {
        string xxx = "";
        xxx = String.Format("{0:#,0.00}", value);
        return xxx;
    }
    public static string money(double value, bool RP) {
        string xxx = "";
        if (RP)
        {
            // xxx = value.ToString("C2");
            xxx = "Rp. " + String.Format("{0:#,0.00}", value);
        }
        else { money(value); }
        return xxx;
    }
    public static string moneymin(double value, bool RP) {
        string xxx = "";
        if (RP)
        {
            xxx = "Rp. - " + string.Format("{0:#,0.00}", value);
        }
        else { money(value); }
        return xxx;
    }
}
