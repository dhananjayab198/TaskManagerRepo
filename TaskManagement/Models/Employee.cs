
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Role { get; set; }  // Admin, Manager, Employee

    public Guid? ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public Employee Manager { get; set; }
    
    public ICollection<Employee> TeamMembers { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
