
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskAttachment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid TaskId { get; set; }

    [ForeignKey("TaskId")]
    public Task Task { get; set; }

    [Required]
    public string FilePath { get; set; }

    [Required]
    public Guid UploadedBy { get; set; }

    [ForeignKey("UploadedBy")]
    public Employee Employee { get; set; }

    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
}
