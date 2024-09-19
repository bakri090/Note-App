using Microsoft.EntityFrameworkCore;

namespace Note.Api.Data
{
	public class NotesDbContext : DbContext
	{
		public NotesDbContext(DbContextOptions options) : base(options)
		{
		}
        public DbSet<Note.Api.Models.Entities.Note> Notes { get; set; }
    }
}
