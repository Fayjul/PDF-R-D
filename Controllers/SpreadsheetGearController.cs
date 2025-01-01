using Microsoft.AspNetCore.Mvc;
using PDF.Models;
using SpreadsheetGear;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpreadsheetGearController : ControllerBase
    {
        [HttpPost]
        public void Post()
        {
            /*IWorkbook workbook = Factory.GetWorkbook(@"C:\Learning\SpreadsheetGear-Sample-Color-Scale.xlsx");
            IWorksheet worksheet = workbook.ActiveWorksheet;

            IRange dataRange = worksheet.Cells["A2:B22"];
            IFormatCondition cf = dataRange.FormatConditions.Add(
                FormatConditionType.Expression,
                FormatConditionOperator.None,
                @"=ISNUMBER(SEARCH(""November"",$B2))", null);
            workbook.SaveAs(@"C:\Learning\Sample.xlsx", FileFormat.OpenXMLWorkbook);
            cf.Interior.ThemeColor = ColorSchemeIndex.Accent4;
            cf.Interior.TintAndShade = 0.8;

            workbook.SaveAs(@"C:\Learning\SpreadsheetGear-Sample.xlsx", FileFormat.OpenXMLWorkbook);*/

            /*IWorkbook workbook = Factory.GetWorkbook();
            IWorksheet worksheet = workbook.ActiveWorksheet;

            IRange range = worksheet.Cells["A1:C3"];

            IFormatConditions conditions = range.FormatConditions;
            IFormatCondition condition = conditions.AddColorScale(2);
            IColorScale colorScale = condition.ColorScale;


            workbook.SaveAs(@"C:\Learning\BaseTemplate.xlsx", FileFormat.OpenXMLWorkbook);*/

            // From SpreadsheetGear
            IWorkbook workbook = Factory.GetWorkbook();
            IWorksheet worksheet = workbook.ActiveWorksheet;
            IRange cells = worksheet.Cells;

            // Add a title to a cell.
            cells["A1"].Formula = "Template Administration";
            IRange range = cells["A1:C1"];
            range.Interior.Color = Colors.Blue;
            range.Font.Color = Colors.White;
            range.NumberFormat = "@";

            cells["A3"].Formula = "General Properties";
            IRange range2 = cells["A3:C3"];
            range2.Interior.Color = Colors.Blue;
            range2.Font.Color = Colors.White;
            range2.NumberFormat = "@";

            List<TemplateInfo> templates = new List<TemplateInfo>
            {
                new TemplateInfo { Id = 1, Name = "Template Type", Value = "GMT" },
                new TemplateInfo { Id = 2, Name = "Template Version", Value = "3.0.1" },
                new TemplateInfo { Id = 3, Name = "Template Version Date", Value = "9/23/2024" }
            };
            cells["B:B"].ColumnWidth = 10; // Set column B to width 10
            cells["C:C"].ColumnWidth = 25; // Set column C to width 25
            cells["D:D"].ColumnWidth = 15; // Set column D to width 15

            cells[$"B5"].Value = "ID";
            cells[$"C5"].Value = "Property Name";
            cells[$"D5"].Value = "Value";

            int startRow = 6;

            for (int i = 0; i < templates.Count; i++)
            {
                var template = templates[i];
                cells[$"B{startRow + i}"].Value = template.Id;
                cells[$"C{startRow + i}"].Value = template.Name;
                cells[$"D{startRow + i}"].Value = template.Value;
            }


            workbook.SaveAs(@"C:\Learning\BaseTemplate5.xlsx", FileFormat.OpenXMLWorkbook);

        }
    }
}
