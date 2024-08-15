// Controllers/EmployeesController.cs
using Microsoft.AspNetCore.Mvc; 
using TaskManagement.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        List<Employee> employees = await _employeeRepository.GetAllEmployeesAsync();
        if (employees == null)
        {
            return NotFound();
        }
        return Ok(employees);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Employee>> GetEmployee([FromRoute]string name)
    {
        Employee employee = await _employeeRepository.GetEmployeeAsync(name);

        if (employee == null)
        {
            return NotFound();
        }

        return employee;
    }

    [HttpGet("manager/{managerName}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByManagerName([FromRoute] string managerName)
    {
        List<Employee> employees = await _employeeRepository.GetEmployeesByManagerNameAsync(managerName);

        if (employees == null)
        {
            return NotFound();
        }

        return employees;
    }

    [HttpPost]
    public async Task<ActionResult> AddEmployee([FromBody]Employee employee)
    {
        if(employee == null)
        {
            return BadRequest("Need minimum data");
        }
        var isEmployeeAdded = await _employeeRepository.AddEmployeeAsync(employee);
        if (!isEmployeeAdded)
        {
            return BadRequest("Failed to create employee");
        }
        return Ok("Employee Added Successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute]Guid id,[FromBody] Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        var isUpdatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);

        if (!isUpdatedEmployee)
        {
            return BadRequest("Update Failure");
        }
         
        return Ok("Updated successfully");
    }

    [HttpDelete("{employeeName}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] string employeeName)
    {
        var employee = await _employeeRepository.DeleteEmployeeAsync(employeeName);
        if (!employee)
        {
            return NotFound("Employee not found");
        }
        return Ok("Deleted successfully");        
    }
}
