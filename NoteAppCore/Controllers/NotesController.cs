using Microsoft.AspNetCore.Mvc;
using NoteAppCore.Models;
using NoteAppCore.Services;

namespace NoteAppCore.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteService _notes;

        public NotesController(INoteService notes) => _notes = notes;

        // GET /Notes
        public IActionResult Index(string? q, string? category, string? sort, string? editId)
        {
            var all = _notes.GetAll();

            if (!string.IsNullOrWhiteSpace(q))
                all = all.Where(n =>
                    n.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    n.Content.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(category) && category != "todas")
                all = all.Where(n => n.Category == category).ToList();

            all = sort switch
            {
                "oldest" => all.OrderBy(n => n.CreatedAt).ToList(),
                "title"  => all.OrderBy(n => n.Title).ToList(),
                _        => all.OrderByDescending(n => n.CreatedAt).ToList()
            };

            var vm = new NotesIndexViewModel
            {
                Notes          = all.ToList(),
                AllNotes       = _notes.GetAll(),
                SearchQuery    = q,
                CategoryFilter = category,
                SortBy         = sort,
                EditingNote    = editId is not null ? _notes.GetById(editId) : null
            };

            return View(vm);
        }

        // POST /Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string? title, string? content, string category)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "La nota debe tener al menos un título o contenido.";
                return RedirectToAction(nameof(Index));
            }

            _notes.Add(title?.Trim() ?? string.Empty, content?.Trim() ?? string.Empty, category);
            return RedirectToAction(nameof(Index));
        }

        // POST /Notes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, string? title, string? content, string category)
        {
            _notes.Update(id, title?.Trim() ?? string.Empty, content?.Trim() ?? string.Empty, category);
            return RedirectToAction(nameof(Index));
        }

        // POST /Notes/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            _notes.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
