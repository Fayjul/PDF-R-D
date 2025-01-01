using SpreadsheetGear.Themes;
using SpreadsheetGear;

namespace PDF.Services
{
    public class SpreadsheetGenarator
    {
        public void GenarateBaseTemplate()
        {
            IWorkbook workbook = Factory.GetWorkbook();
            IWorksheet worksheet = workbook.ActiveWorksheet;

            IRange range= worksheet.Cells["A1:C3"];

            IFormatConditions conditions = range.FormatConditions;
            IFormatCondition condition = conditions.AddColorScale(2);
            IColorScale colorScale = condition.ColorScale;
            

            workbook.SaveAs(@"C:\Learning\BaseTemplate.xlsx", FileFormat.OpenXMLWorkbook);
        }
    }
}
