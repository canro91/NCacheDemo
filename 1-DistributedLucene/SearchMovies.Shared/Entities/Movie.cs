namespace SearchMovies.Shared.Entities;

public record Movie(string Name, int ReleaseYear, float Rating, int DurationInMinutes, Director Director, Genre[] Genres);

public record Director(string Name, int BirthYear);

public enum Genre
{
    Crime,
    Drama,
    Romance,
    Action,
    Comedy,
    Fantasy,
    Thriller,
    Documentary,
    Mystery,
    Family,
    SciFi,
    Adventure,
    Horror,
    Biography,
    Sport,
    Music,
    History,
    War
}