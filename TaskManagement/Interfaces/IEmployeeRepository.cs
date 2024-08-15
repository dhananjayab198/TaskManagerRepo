using System;

namespace TaskManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesByManagerNameAsync(String name);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(String name);
        Task<Boolean> AddEmployeeAsync(Employee employee);

        Task<Boolean> UpdateEmployeeAsync(Employee employee);
        Task<Boolean> DeleteEmployeeAsync(String name);
    }
}
