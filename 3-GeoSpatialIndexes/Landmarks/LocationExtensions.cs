using Lucene.Net.Documents;
using Lucene.Net.Spatial;
using Spatial4n.Core.Context;
using Spatial4n.Core.Distance;
using Spatial4n.Core.Shapes;
using System.Globalization;
using static System.FormattableString;
using Document = Lucene.Net.Documents.Document;

namespace Landmarks;

public static class LocationExtensions
{
    public static SpatialDocument ToSpatialDocument(this Landmark landmark, SpatialStrategy strategy)
    {
        var document = new Document
        {
            new StringField("name", landmark.Name, Field.Store.YES)
        };

        var point = landmark.ToPoint();
        document.Add(new StoredField(strategy.FieldName, Invariant($"{point.X} {point.Y}")));

        return new SpatialDocument
        {
            Document = document,
            Shapes = new[] { point }
        };
    }

    public static IPoint ToPoint(this Landmark landmark)
    {
        return SpatialContext.GEO.MakePoint(landmark.Position.Longitude, landmark.Position.Latitude);
    }

    public static (string Name, double DistanceInKm) ToResponse(this Document document, IPoint startingPoint)
    {
        var name = document.GetField("name").GetStringValue();

        var location = document.GetField("landmarkLocation").GetStringValue();
        var positions = location.Split(' ');
        var x = double.Parse(positions[0], CultureInfo.InvariantCulture);
        var y = double.Parse(positions[1], CultureInfo.InvariantCulture);

        var distanceInDeg = SpatialContext.GEO.CalcDistance(startingPoint, x, y);
        var distanceInKm = distanceInDeg * DistanceUtils.DEG_TO_KM;

        return (name, distanceInKm);
    }
}