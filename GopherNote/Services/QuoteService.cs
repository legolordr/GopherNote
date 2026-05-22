using GopherNote.Models;

namespace GopherNote.Services;

public class QuoteService
{
    public string GetContextualQuote(List<Note> notes)
    {
        if (notes == null || !notes.Any())
            return "Чистый лист — начало великих идей. Создайте свою первую заметку.";

        var now = DateTime.Now;
        var notesCreatedToday = notes.Count(n => (now - n.CreatedAt).TotalDays < 1);
        var daysSinceLastNote = (now - notes.Max(n => n.CreatedAt)).TotalDays;

        if (notesCreatedToday > 10)
        {
            return "Вы создали очень много заметок сегодня. Не забывайте об отдыхе — выгорание не дремлет.";
        }
            
        if (daysSinceLastNote > 7)
        {
            return "Прокрастинация — вор времени. Давно мы не видели новых записей!";
        }

        var pinnedCount = notes.Count(n => n.IsPinned);
        if (pinnedCount > 5)
        {
            return "Слишком много приоритетов означает отсутствие приоритетов. Почистите закрепленные заметки.";
        }

        return "Продуктивность — это не делать больше, а делать то, что важно.";
    }
}