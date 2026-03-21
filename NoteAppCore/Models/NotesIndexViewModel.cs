namespace NoteAppCore.Models
{
    public class NotesIndexViewModel
    {
        public IReadOnlyList<Note> Notes { get; set; } = [];
        public IReadOnlyList<Note> AllNotes { get; set; } = [];
        public string? SearchQuery { get; set; }
        public string? CategoryFilter { get; set; }
        public string? SortBy { get; set; }
        public Note? EditingNote { get; set; }

        public static readonly string[] Categories =
            ["personal", "trabajo", "ideas", "urgente", "otro"];

        // Counts are always based on all notes (not filtered view)
        public int TotalCount => AllNotes.Count;
        public int CountFor(string category) =>
            AllNotes.Count(n => n.Category == category);
    }
}
