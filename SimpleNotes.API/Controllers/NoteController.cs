using Microsoft.AspNetCore.Mvc;
using SimpleNotes.API.Models;
using SimpleNotes.API.Services;

namespace SimpleNotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<Note>> GetAll()
        {
            return _noteService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetById(int id)
        {
            var note = _noteService.GetById(id);
            if (note == null) return NotFound();
            return note;
        }

        [HttpPost]
        public IActionResult Add(Note note)
        {
            _noteService.Add(note);
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _noteService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

