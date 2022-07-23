using Lucene.Net.Documents;
using SearchMovies.Shared.Entities;
using SearchMovies.Shared.Responses;

namespace SearchMovies.Shared.Extensions;

public static class MoviesExtensions
{
    public static Document MapToLuceneDocument(this Movie self)
    {
        return new Document
        {
            new TextField("name", self.Name, Field.Store.YES),
            new TextField("directorName", self.Director.Name, Field.Store.YES)
        };
    }

    public static MovieResponse MapToMovieResponse(this Document self)
    {
        return new MovieResponse(self.Get("name"), self.Get("directorName"));
    }
}