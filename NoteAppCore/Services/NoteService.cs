using Microsoft.EntityFrameworkCore;
using NoteAppCore.Data;
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
        private readonly AppDbContext _db;

        public NoteService(AppDbContext db) => _db = db;

        public IReadOnlyList<Note> GetAll() =>
            _db.Notes.OrderByDescending(n => n.CreatedAt).ToList();

        public Note? GetById(string id) =>
            _db.Notes.FirstOrDefault(n => n.Id == id);

        public Note Add(string title, string content, string category)
        {
            var note = new Note { Title = title, Content = content, Category = category };
            _db.Notes.Add(note);
            _db.SaveChanges();
            return note;
        }

        public bool Update(string id, string title, string content, string category)
        {
            var note = _db.Notes.FirstOrDefault(n => n.Id == id);
            if (note is null) return false;
            note.Title = title;
            note.Content = content;
            note.Category = category;
            note.UpdatedAt = DateTime.Now;
            _db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            var note = _db.Notes.FirstOrDefault(n => n.Id == id);
            if (note is null) return false;
            _db.Notes.Remove(note);
            _db.SaveChanges();
            return true;
        }
    }
}
