using SimpleNotes.API.Models;

namespace SimpleNotes.API.Services
{
    public interface INoteService
    {
        List<Note> GetAll();
        Note? GetById(int id);
        void Add(Note note);
        bool Delete(int id);
    }
}
