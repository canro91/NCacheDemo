using SearchMovies.Shared;
using SearchMovies.Shared.Entities;
using SearchMovies.Shared.Services;

var searchService = new SearchService(Config.CacheName);
searchService.LoadMovies(SomeMoviesFromImdb());

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

// This list of movies was taken from IMDb dump
// See: https://www.imdb.com/interfaces/
static IEnumerable<Movie> SomeMoviesFromImdb()
{
    return new List<Movie>
    {
        new Movie("Caged Fury", 1983, 3.8f, 89, new Director("Maurizio Angeloni", 1959), new []{ Genre.Crime,Genre.Drama  }),
        new Movie("Bad Posture", 2011, 6.5f, 93, new Director("Jack Smith", 1932), new []{ Genre.Drama,Genre.Romance  }),
        new Movie("My Flying Wife", 1991, 5.5f, 91, new Director("Franz Bi", 1899), new []{ Genre.Action,Genre.Comedy,Genre.Fantasy  }),
        new Movie("Modern Love", 1990, 5.2f, 105, new Director("Sophie Carlhian", 1962), new []{ Genre.Comedy  }),
        new Movie("Sins", 2012, 2.3f, 84, new Director("Pierre Huyghe", 1962), new []{ Genre.Action, Genre.Thriller  }),
        new Movie("Inferno", 1999, 5.1f, 95, new Director("Richard E. Cunha", 1922), new []{ Genre.Action, Genre.Comedy, Genre.Drama  }),
        new Movie("About Last Night", 1986, 6.2f, 113, new Director("Daniel Cherniavsky", 1933), new []{ Genre.Comedy, Genre.Drama, Genre.Romance  }),
        new Movie("That Was Then... This Is Now", 1985, 6.1f, 102, new Director("Lars Brydesen", 1938), new []{ Genre.Crime, Genre.Drama  }),
        new Movie("Mitsuko Delivers", 2011, 6.2f, 109, new Director("Henry Kingi Jr.", 1970), new []{ Genre.Comedy  }),
        new Movie("The Story of Boys & Girls", 1989, 6.7f, 92, new Director("Jean-Pierre Bekolo", 1966), new []{ Genre.Comedy, Genre.Drama, Genre.Romance  }),
        new Movie("Beep", 1997, 6.8f, 75, new Director("Sabrija Biser", 1930), new []{ Genre.Comedy, Genre.Drama  }),
        new Movie("Baba!", 1983, 7f, 101, new Director("Lou Antonio", 1934), new []{ Genre.Drama  }),
        new Movie("Buttwhistle", 2014, 4.1f, 93, new Director("Branislav Klasnja", 1975), new []{ Genre.Comedy, Genre.Crime, Genre.Drama  }),
        new Movie("Fall Time", 1995, 5.4f, 88, new Director("Isshin Inudô", 1960), new []{ Genre.Crime, Genre.Drama  }),
        new Movie("The Man on the Shore", 1993, 6.5f, 106, new Director("Garnik Arazyan", 1936), new []{ Genre.Drama, Genre.History, Genre.War  }),
        new Movie("Wildflowers", 1999, 5.5f, 93, new Director("Martin Koolhoven", 1969), new []{ Genre.Drama  }),
        new Movie("Streets of Rage", 1993, 3.9f, 84, new Director("Phil Chilvers", 1943), new []{ Genre.Action, Genre.Crime, Genre.Drama  }),
        new Movie("La ingenua, la lesbiana y el travesti", 1983, 5f, 73, new Director("Angelo Bonsignore", 1978), new Genre[]{}),
        new Movie("The Nutcracker", 2009, 6.7f, 120, new Director("Neal Israel", 1945), new []{ Genre.Fantasy, Genre.Music, Genre.Romance  }),
        new Movie("A Rage in Harlem", 1991, 5.9f, 115, new Director("Bernard Campan", 1958), new []{ Genre.Comedy, Genre.Crime  }),
        new Movie("Man on the Moon", 1999, 7.4f, 118, new Director("David Dobkin", 1969), new []{ Genre.Biography, Genre.Comedy, Genre.Drama  }),
        new Movie("Tender Flesh", 1997, 3.8f, 93, new Director("William Ash", 1977), new []{ Genre.Comedy, Genre.Horror  }),
        new Movie("Office Killer", 1997, 5.1f, 82, new Director("Yefim Berezin", 1919), new []{ Genre.Comedy, Genre.Crime, Genre.Horror  }),
        new Movie("Får jag lov: Till den sista dansen?", 2008, 7.2f, 73, new Director("Theodor Halacu-Nicon", 1970), new []{ Genre.Documentary  }),
        new Movie("The Final Option", 1982, 6.4f, 125, new Director("Liv Corfixen", 1973), new []{ Genre.Action, Genre.Thriller  }),
        new Movie("The Delivery", 1999, 5.2f, 105, new Director("Nina Bellotto", 1982), new []{ Genre.Action, Genre.Adventure, Genre.Comedy  }),
        new Movie("Children Metal Divers", 2009, 7.3f, 93, new Director("Bruno Jantoss", 1935), new []{ Genre.Drama  }),
        new Movie("Consenting Adults", 1992, 5.7f, 99, new Director("Robin Byrd", 1955), new []{ Genre.Crime, Genre.Drama, Genre.Mystery  }),
        new Movie("Fortress", 1992, 5.9f, 95, new Director("Marcus Adams", 1966), new []{ Genre.Action, Genre.Crime, Genre.SciFi  }),
        new Movie("Spetters", 1980, 6.6f, 120, new Director("David Seffer", 1980), new []{ Genre.Drama, Genre.Romance, Genre.Sport  }),
        new Movie("O Baiano Fantasma", 1984, 7.2f, 95, new Director("Suze Randall", 1946), new []{ Genre.Drama  }),
        new Movie("Magic Kid II", 1994, 3.8f, 87, new Director("Yaël Abecassis", 1967), new []{ Genre.Comedy, Genre.Family  }),
        new Movie("Mutande pazze", 1992, 3.9f, 97, new Director("Nikolay Aleksandrovich", 1920), new []{ Genre.Comedy  }),
        new Movie("Olympia", 1998, 7.2f, 76, new Director("Gene Bryant", 1897), new []{ Genre.Drama  }),
        new Movie("The Undressing", 1986, 4.7f, 87, new Director("Enzo Biagi", 1920), new []{ Genre.Drama  }),
        new Movie("Bob Roberts", 1992, 7f, 102, new Director("Ragnar Arvedson", 1895), new []{ Genre.Comedy, Genre.Drama  }),
        new Movie("The Clones of Bruce Lee", 1980, 4.2f, 91, new Director("Edward Bernds", 1905), new []{ Genre.Action, Genre.Drama  }),
        new Movie("Night and Day", 1991, 6.4f, 92, new Director("Chris Bernard", 1955), new []{ Genre.Drama  }),
        new Movie("Mouth to Mouth", 1995, 6.4f, 106, new Director("Ram Narayan Gabale", 1914), new []{ Genre.Comedy, Genre.Romance  }),
        new Movie("The Ring of Death", 1980, 6.7f, 92, new Director("Arthur Barnes", 1886), new []{ Genre.Action  }),
        new Movie("Lauderdale", 1989, 3.8f, 91, new Director("Patrick Bokanowski", 1943), new []{ Genre.Comedy  }),
        new Movie("Turnaround", 1998, 4.3f, 105, new Director("Kåre Bergstrøm", 1911), new []{ Genre.Thriller  }),
        new Movie("Backlash", 1986, 5.9f, 89, new Director("William Kissell", 1909), new []{ Genre.Crime, Genre.Drama  }),
        new Movie("Butterfly and Sword", 1993, 5.9f, 88, new Director("Branislav Bastac", 1925), new []{ Genre.Action, Genre.Adventure, Genre.Fantasy  }),
        new Movie("Deadly Dancer", 1990, 5.3f, 99, new Director("Stuart Black", 1923), new []{ Genre.Drama, Genre.Thriller  }),
        new Movie("Kawao tee Bangpleng", 1994, 5.3f, 120, new Director("José Carlos Burle", 1910), new []{ Genre.SciFi  }),
        new Movie("Ruby and Rata", 1990, 6f, 111, new Director("Thomas Bezucha", 1964), new []{ Genre.Comedy, Genre.Drama  }),
        new Movie("The Game", 1997, 7.7f, 129, new Director("Neal Israel", 1945), new []{ Genre.Drama, Genre.Mystery, Genre.Thriller  }),
        new Movie("18 Bronze Girls of Shaolin", 1983, 4.8f, 89, new Director("Jim Brown", 1950), new []{ Genre.Action, Genre.Adventure, Genre.Comedy  }),
        new Movie("Nan yu nu", 1993, 4.4f, 87, new Director("Pablo Larraín", 1976), new []{ Genre.Comedy, Genre.Drama, Genre.Thriller  }),
    };
}