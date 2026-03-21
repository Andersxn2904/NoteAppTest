using NoteAppCore.Models;

namespace NoteAppCore.Services
{
    public interface INoteService
    {
        IReadOnlyList<Note> GetAll();
        Note? GetById(string id);
        Note Add(string title, string content, string category);
        bool Update(string id, string title, string content, string category);
        bool Delete(string id);
    }

    public class NoteService : INoteService
    {
        private readonly List<Note> _notes = [];

        public IReadOnlyList<Note> GetAll() => _notes.AsReadOnly();

        public Note? GetById(string id) =>
            _notes.FirstOrDefault(n => n.Id == id);

        public Note Add(string title, string content, string category)
        {
            var note = new Note { Title = title, Content = content, Category = category };
            _notes.Insert(0, note);
            return note;
        }

        public bool Update(string id, string title, string content, string category)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note is null) return false;
            note.Title = title;
            note.Content = content;
            note.Category = category;
            note.UpdatedAt = DateTime.Now;
            return true;
        }

        public bool Delete(string id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note is null) return false;
            _notes.Remove(note);
            return true;
        }
    }
}
