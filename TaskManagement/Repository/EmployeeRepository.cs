using Microsoft.EntityFrameworkCore;
using TaskManagement.Interfaces;

namespace TaskManagement.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _dbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(AppDbContext dbContext, ILogger<EmployeeRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }
        public async Task<List<Employee>> GetEmployeesByManagerNameAsync(string managerName)
        {
            try
            { 
                Employee manager = await _dbContext.Employees.Where(m => m.Name == managerName).FirstOrDefaultAsync();
                if (manager == null)
                {
                    return new List<Employee>();
                }
                return await _dbContext.Employees.Where(m => m.Id == manager.Id).ToListAsync();
            }
            catch (Exception ex) {
                _logger.LogInformation(ex.Message.ToString());
                return new List<Employee>();
            }
        }
        public async Task<Employee> GetEmployeeAsync(String name)
        {
            try
            {
                Employee employee;
                employee = await _dbContext.Employees.Where(m => m.Name == name).FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return new Employee();
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {

                List<Employee> employees;
                employees = await _dbContext.Employees.ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return new List<Employee>();
            }
        }
        public async Task<Boolean> AddEmployeeAsync(Employee employee)
        {
            if (employee != null) {
                employee.Id = new Guid();
                _dbContext.Employees.AddAsync(employee);
                _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<Boolean> UpdateEmployeeAsync(Employee e)
        {
            try
            {
                Employee employee = await _dbContext.Employees.Where(m => m.Id == e.Id).FirstOrDefaultAsync();
                if (employee == null)
                {
                    return false;
                }

                employee.Name = e.Name;
                employee.Role = e.Role;
                employee.Email = e.Email;
                employee.Manager = e.Manager;
                employee.Tasks = e.Tasks;
                employee.TeamMembers = e.TeamMembers;
                _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return false;
            }
        }
        public async Task<Boolean> DeleteEmployeeAsync(String Name)
        {
            try
            {
                Employee e = await _dbContext.Employees.Where(m => m.Name == Name).FirstOrDefaultAsync();
                if (e == null)
                {
                    return false;
                }
                _dbContext.Remove(e);
                _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                return false;
            }
        }

    }
}
