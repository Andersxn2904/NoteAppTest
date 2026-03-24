using Microsoft.EntityFrameworkCore;
using NoteAppCore.Models;

namespace NoteAppCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Note> Notes => Set<Note>();
    }
}
