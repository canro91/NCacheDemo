# NCacheDemo

This is the companion repo of my posts about NCache.

## 0. [Working with ASP.NET Core IDistributedCache Provider for NCache](https://canro91.github.io/2022/04/11/DistributedCacheWithNCache/)

It contains two projects:

* [NCacheDemo.Tests](https://github.com/canro91/NCacheDemo/tree/main/NCacheDemo.Tests): It shows NCache basic caching operations in a MSTest Unit Test project.
* [DistributedCacheWithNCache](https://github.com/canro91/NCacheDemo/tree/main/0-DistributedCache/DistributedCacheWithNCache): It shows how to use ASP.NET Core IDistributedCache provider with NCache to speed up an API endpoint that makes a dummy database call.

## 1. [NCache & Full-Text Search](https://canro91.github.io/2022/08/08/FullTextSearchWithNCache/)

It contains two projects:

* [Loader](https://github.com/canro91/NCacheDemo/tree/main/1-DistributedLucene/SearchMovies.Loader): It populates a Distributed Lucene cache with 50 random movies from IMDb using a Console app.
* [Searcher](https://github.com/canro91/NCacheDemo/tree/main/1-DistributedLucene/SearchMovies.Search): It shows how to apply a Lucene query to find movies, again with a Console app.

## 2. [Pub/Sub](https://www.alachisoft.com/blogs/pub-sub-with-ncache/)

It contains two projects:

* [Publisher](https://github.com/canro91/NCacheDemo/tree/main/2-PubSub/MoviePublisher): It publishes new movies releases to a topic
* [Subscriber](https://github.com/canro91/NCacheDemo/tree/main/2-PubSub/MovieSubscriber): It creates a non-durable subscription to listen to new movie releases

## 3. [GeoSpatial Indexes for Distributed Lucene](https://www.alachisoft.com/blogs/geospatial-indexes-for-distributed-lucene-with-ncache/)

* [Landmarks](https://github.com/canro91/NCacheDemo/tree/main/3-GeoSpatialIndexes/Landmarks): It populates a Distributed Lucene cache with 5 or 6 landmarks around Paris. Then, it searches the closest ones to the airport.

## 4. [Entry Processor](https://www.alachisoft.com/blogs/entry-processor-server-side-operations/)

* [RewardPoints](https://github.com/canro91/NCacheDemo/tree/main/4-EntryProcessor/RewardPoints): It doubles rewards points for a list of customers using NCache Bulk methods and Entry Processor to simply check which approach is faster.
* [RewardPoints.Shared](https://github.com/canro91/NCacheDemo/tree/main/4-EntryProcessor/RewardPoints.Shared): It contains a NCache Entry Processor to deploy to an NCache server.

## 5. NCache & Entity Framework Core

* [Movies](https://github.com/canro91/NCacheDemo/tree/main/5-EntityFrameworkCore/Movies): It reads the 10 best rated movies using Entity Framework and caches them.