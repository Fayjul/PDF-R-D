using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDF.Models;
using PDF.Services;

namespace PDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employesService)
        {
            _employeeService = employesService;
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<Employee> employees = _employeeService.getEmployee();
            return Ok(employees);
        }

        [HttpGet("ByName")]
        public IActionResult GetEmployesByName()
        {
            List<Employee> employees = _employeeService.SortEmplyeesByName();
            return Ok(employees);
        }

    }
}
