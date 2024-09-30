using ECommerceAPI.Model;
using ECommerceAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
       {
            return Ok(_employee.GetAllEmp());
        }
        [HttpGet("GetById")]
        public IActionResult GetEmployee(int id)
        {
            return Ok(_employee.GetById(id));
        }
        [HttpPost]
        public IActionResult SaveEmployee(Employee employee)
        {
            _employee.CreateEmp(employee);
            return Ok();
        }
        [HttpPatch]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _employee.UpdateEmp(employee);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            _employee.DeleteEmp(id);
            return Ok();
        }

    }
}
