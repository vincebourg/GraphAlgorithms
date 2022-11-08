# GraphAlgorithms
Having fun implementing graph algorithms in C#

## Sources
Started coding following these indications: 
- [10 graph algorithms visually explained](https://towardsdatascience.com/10-graph-algorithms-visually-explained-e57faa1336f3)
- [Breadth-First Search and Shortest Path in C# and .NET Core](https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core)

## Algorithms
Following the examples in the source, I first copied the Breadth-First which was a generic one so I continued with the same pattern for the others. I made the others by myself, with my flaws, using TDD, always :)
- Breadth-First: Tested for shortest path and exploration.
- Depth-First: Tested for loop detection.
- Dijkstra: Tested for shortest path with weighted graph. (weird thing, with generics I had to implement some Add() and comparison functions)