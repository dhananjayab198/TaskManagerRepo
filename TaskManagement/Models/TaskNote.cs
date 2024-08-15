
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskNote
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid TaskId { get; set; }

    [ForeignKey("TaskId")]
    public Task Task { get; set; }

    [Required]
    public string Note { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    public Employee Employee { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
