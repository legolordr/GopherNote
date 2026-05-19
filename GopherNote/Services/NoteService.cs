using System.Text.Json;
using GopherNote.Models;
using Microsoft.JSInterop;

namespace GopherNote.Services;

public class NoteService(IJSRuntime _js)
{
    private const string StorageKey = "gopher_notes";

    public async Task<List<Note>> GetNotesAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json)) return new List<Note>();

            return JsonSerializer.Deserialize<List<Note>>(json) ?? new List<Note>();
        }
        catch (Exception ex)
        {
            // Вместо того чтобы ронять всё приложение, просто логируем и возвращаем пустой список
            Console.WriteLine($"JS Interop Error: {ex.Message}");
            return new List<Note>();
        }
    }

    public async Task SaveNoteAsync(Note note)
    {
        var notes = await GetNotesAsync();
        var existingNote = notes.FirstOrDefault(n => n.Id == note.Id);

        if (existingNote != null)
        {
            notes.Remove(existingNote);
        }

        notes.Add(note);

        var json = JsonSerializer.Serialize(notes);
        // Вызываем JavaScript метод setItem
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task DeleteNoteAsync(Guid id)
    {
        var notes = await GetNotesAsync();
        var note = notes.FirstOrDefault(n => n.Id == id);

        if (note != null)
        {
            notes.Remove(note);
            var json = JsonSerializer.Serialize(notes);
            await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
    }
}