using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Reporting.WebForms;


namespace PDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RdlcToPDFController : ControllerBase
    {
        [HttpGet("GenerateIDCard")]
        public IActionResult GenerateIDCard([FromQuery] string name, [FromQuery] int id)
        {
            // Create LocalReport instance
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "IdCardReport.rdlc");

            // Add parameters to the report
            ReportParameter[] reportParameters = new ReportParameter[]
            {
                new ReportParameter("Name", name),
                new ReportParameter("Id", id.ToString())
            };

            localReport.SetParameters(reportParameters);

            // Render the report to a byte array (PDF format in this example)
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;

            byte[] renderedBytes = localReport.Render(
                "PDF",
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            // Return the generated PDF as a file result
            return File(renderedBytes, "application/pdf", "IdCard.pdf");
        }
    }
}

