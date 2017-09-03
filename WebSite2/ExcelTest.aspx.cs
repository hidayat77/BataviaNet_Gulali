using System;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;

public partial class ExcelTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
            {
                ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                GridView1.DataSource = ExcelPackageExtensions.ToDataTable(package);
                GridView1.DataBind();
            }
        }
    }

}
public static class ExcelPackageExtensions
{
    public static DataTable ToDataTable(this ExcelPackage package)
    {
        DataTable table = new DataTable();
        using (package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();

            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
        }
        return table;
    }
}