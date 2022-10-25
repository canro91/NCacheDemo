using DistributedLucene.Net.Spatial;
using Landmarks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Spatial;
using Lucene.Net.Spatial.Prefix;
using Lucene.Net.Spatial.Prefix.Tree;
using Lucene.Net.Spatial.Queries;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Spatial4n.Core.Context;
using Spatial4n.Core.Distance;

const LuceneVersion LuceneVersion = LuceneVersion.LUCENE_48;
const string CacheName = "DemoLuceneCache";
const string IndexName = "landmarks";
const string NameFieldName = "name";
const string LocationFieldName = "landmarkLocation";

// 1. Let's index some locations around Paris
var favoriteLandmarks = new Landmark[]
{
    new Landmark("Eiffel Tower", new Location(48.858093, 2.294694)),
    new Landmark("Sacre Coeur", new Location(48.886452, 2.343121)),
    new Landmark("Louvre Museum", new Location(48.860294, 2.338629)),
    new Landmark("Palace of Versailles", new Location(48.804722, 2.121782)),
    new Landmark("Disneyland Paris", new Location(48.867374, 2.784018)),
    new Landmark("Arc de Triomphe", new Location(48.873756, 2.294946))
};
IndexLandmarks(favoriteLandmarks);

// 2. Let's find five landmarks, 30Km around the airport
/// and print them
var airport = new Landmark("Charles de Gaulle Airport", new Location(49.009724, 2.547778));
SearchAround(5, airport, 30);

Console.ReadKey();

static void IndexLandmarks(IEnumerable<Landmark> landmarks)
{
    var strategy = GetStrategy();

    using var indexDirectory = NCacheDirectory.Open(CacheName, IndexName);
    var config = new IndexWriterConfig(LuceneVersion, new StandardAnalyzer(LuceneVersion))
    {
        OpenMode = OpenMode.CREATE
    };
    using var writer = new IndexWriter(indexDirectory, config);

    foreach (var landmark in landmarks)
    {
        var document = landmark.ToSpatialDocument(strategy);
        writer.AddDocument(document, strategy);
    }
    writer.Commit();
}

static void SearchAround(int landmarkCount, Landmark around, int distanceInKm)
{
    using var indexDirectory = NCacheDirectory.Open(CacheName, IndexName);
    using var reader = DirectoryReader.Open(indexDirectory);
    var searcher = new IndexSearcher(reader);

    var startingPoint = around.ToPoint();

    var sortByName = new Sort(new SortField(NameFieldName, SortFieldType.STRING));
    var spatialArgs = new SpatialArgs(
                            SpatialOperation.Intersects,
                            SpatialContext.GEO.MakeCircle(startingPoint, DistanceUtils.Dist2Degrees(distanceInKm, DistanceUtils.EARTH_MEAN_RADIUS_KM)));

    var strategy = GetStrategy();
    var filter = strategy.MakeFilter(spatialArgs);
    var documents = searcher.Search(new MatchAllDocsQuery(), filter, landmarkCount, sortByName);

    foreach (var scoreDoc in documents.ScoreDocs)
    {
        var document = searcher.Doc(scoreDoc.Doc);
        var (name, awayInKm) = document.ToResponse(startingPoint);

        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Distance: {awayInKm}");
    }
}

static SpatialStrategy GetStrategy()
{
    var context = SpatialContext.GEO;
    var strategy = new RecursivePrefixTreeStrategy(
                    new GeohashPrefixTree(context, maxLevels: 11),
                    fieldName: LocationFieldName);
    return strategy;
}

public record Location(double Latitude, double Longitude);
public record Landmark(string Name, Location Position);