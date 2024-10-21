using System.IO;
using Aspose.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdfConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HtmlToPdfController : ControllerBase
    {
        [HttpPost("convert")]
        public IActionResult ConvertHtmlToPdf(IFormFile htmlFile)
        {
            if (htmlFile == null || htmlFile.Length == 0)
            {
                return BadRequest("Please upload a valid HTML file.");
            }

            try
            {
                // Load the uploaded HTML file into a stream
                using var inputStream = htmlFile.OpenReadStream();
                using var memoryStream = new MemoryStream();
                string localFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedPdfs");
                Directory.CreateDirectory(localFolderPath);
                string outputPdfPath = Path.Combine(localFolderPath, "converted.pdf");

                // Load HTML into Aspose.PDF Document object
                var loadOptions = new HtmlLoadOptions
                {
                    PageInfo = new PageInfo
                    {
                        Width = PageSize.A4.Width,
                        Height = PageSize.A4.Height,
                        Margin = new MarginInfo(10, 10, 10, 10)
                    }
                };

                // Convert HTML to PDF
                var pdfDocument = new Document(inputStream, loadOptions);
                pdfDocument.Save(outputPdfPath);
                memoryStream.Position = 0;

                // Return the PDF as a file response
                return Ok(new { Message = "HTML file has been successfully converted to PDF.", FilePath = outputPdfPath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}