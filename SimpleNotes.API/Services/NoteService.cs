using SimpleNotes.API.Models;

namespace SimpleNotes.API.Services
{
    public class NoteService : INoteService
    {
        private readonly List<Note> _notes = new();
        private int _nextId = 1;

        public List<Note> GetAll() => _notes;

        public Note? GetById(int id) => _notes.FirstOrDefault(n => n.Id == id);

        public void Add(Note note)
        {
            note.Id = _nextId++;
            _notes.Add(note);
        }

        public bool Delete(int id)
        {
            var note = GetById(id);
            if (note == null) return false;
            _notes.Remove(note);
            return true;
        }
    }
}
