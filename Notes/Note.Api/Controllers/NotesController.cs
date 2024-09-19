using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Note.Api.Data;
using Note.Api.Models.Entities;

namespace Note.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotesController : Controller
	{
		private readonly NotesDbContext _dbContext;

		public NotesController(NotesDbContext dbContext)
        {
			this._dbContext = dbContext;
		}

		[HttpGet]
        public async Task<IActionResult> GetAllNotes()
		{
			return Ok(await _dbContext.Notes.ToArrayAsync());
		}
		[HttpGet]
		[Route("{id:Guid}")]
		[ActionName("GetNoteById")]
		public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
		{
			var note = await _dbContext.Notes.FindAsync(id);

			if(note == null)
			{
				return NotFound();
			}
			return Ok(note);
		}

		[HttpPost]
		public async Task<IActionResult> AddNote(Note.Api.Models.Entities.Note note)
		{
			note.Id = Guid.NewGuid();
			await _dbContext.AddAsync(note);
			await _dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(GetNoteById),new { id=note.Id },note);
		}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> UpdateNode([FromRoute] Guid id, [FromBody] Note.Api.Models.Entities.Note updateNote)
		{
			var existingNote = _dbContext.Notes.Find(id);
			if (existingNote == null)
			{
				return NotFound();
			}
			existingNote.Title = updateNote.Title;
			existingNote.Description = updateNote.Description;
			existingNote.IsVisible = updateNote.IsVisible;
			await _dbContext.SaveChangesAsync();
			return Ok(existingNote);
		}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> RemoveNote([FromRoute]Guid id)
		{
			var removeNode = await _dbContext.Notes.FindAsync(id);
            if (removeNode == null)
			{
				return NotFound();	
			}
			_dbContext.Notes.Remove(removeNode);
			await _dbContext.SaveChangesAsync();
			return Ok(removeNode);
        }
	}
}
