namespace Movies.Shared.Entities;

[Serializable]
public record Movie(string Name, int ReleaseYear, float? Rating, int DurationInMinutes, Genre[] Genres)
{
    public override string ToString()
    {
        return $"Movie: [{Name}] ({ReleaseYear})";
    }
}

public enum Genre
{
    Action,
    Adventure,
    Biography,
    Comedy,
    Crime,
    Documentary,
    Drama,
    Family,
    Fantasy,
    History,
    Horror,
    Music,
    Musical,
    Mystery,
    Romance,
    SciFi,
    Sport,
    Thriller,
    War
}