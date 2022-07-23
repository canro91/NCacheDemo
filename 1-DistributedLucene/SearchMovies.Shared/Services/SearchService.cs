using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using SearchMovies.Shared.Entities;
using SearchMovies.Shared.Extensions;
using SearchMovies.Shared.Responses;

namespace SearchMovies.Shared.Services;

public class SearchService
{
    private const string IndexName = "movies";
    private const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;

    private readonly string _cacheName;

    public SearchService(string cacheName)
    {
        _cacheName = cacheName;
    }

    public void LoadMovies(IEnumerable<Movie> movies)
    {
        using var indexDirectory = NCacheDirectory.Open(_cacheName, IndexName);

        var standardAnalyzer = new StandardAnalyzer(luceneVersion);
        var indexConfig = new IndexWriterConfig(luceneVersion, standardAnalyzer)
        {
            OpenMode = OpenMode.CREATE
        };
        using var writer = new IndexWriter(indexDirectory, indexConfig);

        foreach (var movie in movies)
        {
            var doc = movie.MapToLuceneDocument();
            writer.AddDocument(doc);
        }

        writer.Commit();
    }

    public IEnumerable<MovieResponse> SearchByNames(string searchQuery)
    {
        using var indexDirectory = NCacheDirectory.Open(_cacheName, IndexName);
        using var reader = DirectoryReader.Open(indexDirectory);
        var searcher = new IndexSearcher(reader);

        var analyzer = new StandardAnalyzer(luceneVersion);
        var parser = new QueryParser(luceneVersion, "name", analyzer);
        var query = parser.Parse(searchQuery);

        var documents = searcher.Search(query, 10);

        var result = new List<MovieResponse>();
        for (int i = 0; i < documents.TotalHits; i++)
        {
            var document = searcher.Doc(documents.ScoreDocs[i].Doc);
            result.Add(document.MapToMovieResponse());
        }

        return result;
    }
}