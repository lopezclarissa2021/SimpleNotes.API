using Xunit;
using SimpleNotes.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using SimpleNotes.API.Models;
using System.Net.Http;

namespace SimpleNotes.Tests
{
    public class NoteApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public NoteApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task Can_Create_And_Get_Note()
        {
            // Create note
            var noteJson = new StringContent(
                JsonSerializer.Serialize(new { Content = "Integration Test Note" }),
                Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/note", noteJson);
            createResponse.EnsureSuccessStatusCode();

            // Get notes
            var getResponse = await _client.GetAsync("/api/note");
            getResponse.EnsureSuccessStatusCode();

            var json = await getResponse.Content.ReadAsStringAsync();
            var notes = JsonSerializer.Deserialize<List<Note>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert note exists
            Assert.Contains(notes, n => n.Content == "Integration Test Note");
        }
    }

    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
    }

}