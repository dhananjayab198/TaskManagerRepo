
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Task
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public string Status { get; set; }  // Pending, In Progress, Completed

    public DateTime DueDate { get; set; }

    [Required]
    public Guid AssignedTo { get; set; }

    [ForeignKey("AssignedTo")]
    public Employee Employee { get; set; }

    public ICollection<TaskNote> Notes { get; set; }

    public ICollection<TaskAttachment> Attachments { get; set; }
}
