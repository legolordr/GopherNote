namespace GopherNote.Models;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public NoteType Type { get; set; } = NoteType.Text;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsPinned { get; set; } = false;
    public List<string> Tags { get; set; } = new List<string>();

    public string Color { get; set; } = "#ffffff";
}