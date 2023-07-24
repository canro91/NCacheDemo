namespace Movies;

[Serializable]
public class Movie
{
    public Movie(string name, int releaseYear, decimal rating)
    {
        Name = name;
        ReleaseYear = releaseYear;
        Rating = rating;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int ReleaseYear { get; set; }

    public decimal Rating { get; set; }
}