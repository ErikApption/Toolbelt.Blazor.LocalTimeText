namespace SharedComponents.Models;

public record class Session(
    int Id,
    string Title,
    string Speaker,
    string StartTime,
    string EndTime,
    string Description
);
