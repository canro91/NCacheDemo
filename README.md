# NCacheDemo

This is the companion repo of my posts about NCache.

## 0. [Working with ASP.NET Core IDistributedCache Provider for NCache](https://canro91.github.io/2022/04/11/DistributedCacheWithNCache/)

It contains two projects:

* [NCacheDemo.Tests](https://github.com/canro91/NCacheDemo/tree/main/NCacheDemo.Tests): It shows NCache basic caching operations in a MSTest Unit Test project.
* [DistributedCacheWithNCache](https://github.com/canro91/NCacheDemo/tree/main/0-DistributedCache/DistributedCacheWithNCache): It shows how to use ASP.NET Core IDistributedCache provider with NCache to speed up an API endpoint that makes a dummy database call.

## 1. NCache & Full-Text Search

It contains two projects:

* [Loader](https://github.com/canro91/NCacheDemo/tree/main/1-DistributedLucene/SearchMovies.Loader): It populates a Distributed Lucene cache with 50 random movies from IMDb using a Console app.
* [Searcher](https://github.com/canro91/NCacheDemo/tree/main/1-DistributedLucene/SearchMovies.Search): It shows how to apply a Lucene query to find movies, again with a Console app.
