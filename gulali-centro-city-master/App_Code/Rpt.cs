using System;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

	/// <summary>
	/// Reporting common functions
	/// </summary>
	public class Rpt
	{
		

		#region public static void ToExcel(System.Web.UI.Page p, Table rpt)
		public static void ToExcel(System.Web.UI.Page p, Table rpt, string filename)
		{
            //string filename = System.IO.Path.GetFileNameWithoutExtension(
            //    System.Web.HttpContext.Current.Request.PhysicalPath);

			p.Response.Clear();
			
			p.Response.AddHeader("content-disposition","attachment;filename="+filename+".xls");
			p.Response.ContentType = "application/ms-excel";
			
			System.IO.StringWriter sw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
			rpt.RenderControl(hw);
			p.Response.Write(sw.ToString());
			
			p.Response.End();
		}
		#endregion
		#region public static void ToExcel(System.Web.UI.Page p, PlaceHolder rpt)
        public static void ToExcel(System.Web.UI.Page p, PlaceHolder rpt, string filename)
		{
            //string filename = System.IO.Path.GetFileNameWithoutExtension(
            //    System.Web.HttpContext.Current.Request.PhysicalPath);

			p.Response.Clear();
			
			p.Response.AddHeader("content-disposition","attachment;filename="+filename+".xls");
			p.Response.ContentType = "application/ms-excel";
			
			System.IO.StringWriter sw = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
			rpt.RenderControl(hw);
			p.Response.Write(sw.ToString());
			
			p.Response.End();
		}
        public static bool ValidateXls(DataTable rs, Table gagal)
        {
            bool x = true;
            gagal.Rows.Clear();

            if (rs.Columns.Count == 0)
            {
                Gagal(gagal, "File Excel yang di-upload tidak menggunakan format standard.<br>"
                    + "Silakan download template standard terlebih dahulu.");
                x = false;
            }

            return x;
        }
        private static void Gagal(Table gagal, string ket)
        {
            TableRow r = new TableRow();
            TableCell c = new TableCell();
            c.Text = ket;
            c.Attributes["style"] = "padding-left:50";
            r.Cells.Add(c);
            gagal.Rows.Add(r);
        }
		#endregion
	}