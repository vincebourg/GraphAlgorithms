namespace GraphAlgorithms
{
    public class DepthFirst
    {
        public static HashSet<T> DFS<T>(Graph<T> graph, T start) where T : notnull
        {
            var visited = new HashSet<T>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            VisitRecursively(graph, start, visited);

            return visited;
        }

        private static void VisitRecursively<T>(Graph<T> graph, T vertex, ICollection<T> visited) where T : notnull
        {
            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    VisitRecursively(graph, neighbor, visited);
                }
            }
        }

        public static HashSet<HashSet<T>> FindLoopsWithDFS<T>(Graph<T> graph, T start) where T : notnull, IEquatable<T>
        {
            var visited = new Dictionary<T, T>();
            var foundLoops = new HashSet<HashSet<T>>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return foundLoops;

            FindLoopsRecursively(graph, start, default, visited, foundLoops);

            return foundLoops;
        }

        private static void FindLoopsRecursively<T>(Graph<T> graph, T vertex, T parent, IDictionary<T, T> visited, HashSet<HashSet<T>> foundLoops) where T : notnull, IEquatable<T>
        {
            if (!visited.ContainsKey(vertex))
            {
                visited.Add(vertex, parent);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (!neighbor.Equals(parent))
                    {
                        FindLoopsRecursively(graph, neighbor, vertex, visited, foundLoops);
                    }
                }
            }
            else // loop found
            {
                if (!foundLoops.Any(f => f.Contains(vertex) && f.Contains(parent))) // if loop with these exacts two vertexes is not already
                {
                    var currentVertex = parent;
                    var newLoop = new HashSet<T>
                        {
                            vertex, currentVertex
                        };
                    var parentVertex = visited[currentVertex];
                    while (!parentVertex.Equals(vertex))
                    {
                        newLoop.Add(parentVertex);
                        currentVertex = parentVertex;
                        parentVertex = visited[currentVertex];
                    }
                    foundLoops.Add(newLoop);
                }
            }
        }
    }
}
