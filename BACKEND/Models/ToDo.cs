using System.ComponentModel.DataAnnotations;

namespace BACKEND.Models;

public class ToDo
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
}