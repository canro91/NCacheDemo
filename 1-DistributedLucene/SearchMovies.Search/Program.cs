using SearchMovies.Shared;
using SearchMovies.Shared.Services;

Console.WriteLine("Enter a Lucene query. For example: 'name:nu* or directorName:David'");
var query = Console.ReadLine();

var searchService = new SearchService(Config.CacheName);
var result = searchService.SearchByNames(query);

foreach (var item in result)
{
    Console.WriteLine(item);
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();
