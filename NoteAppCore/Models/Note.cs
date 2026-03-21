namespace NoteAppCore.Models
{
    public class Note
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = "personal";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
