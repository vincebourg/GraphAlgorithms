namespace GraphAlgorithms
{
    public class WeightedGraph<VertexType, WeightType> where VertexType : notnull
                                                        where WeightType : notnull, IEquatable<WeightType>
    {
        public WeightedGraph() { }
        public WeightedGraph(IEnumerable<VertexType> vertices, IEnumerable<(VertexType Source, VertexType Destination, WeightType Weight)> edges)
        {
            foreach (var vertex in vertices)
                AddVertex(vertex);

            foreach (var edge in edges)
                AddEdge(edge);
        }

        public Dictionary<VertexType, Dictionary<VertexType, WeightType>> AdjacencyList { get; } = new Dictionary<VertexType, Dictionary<VertexType, WeightType>>();

        public void AddVertex(VertexType vertex)
        {
            AdjacencyList[vertex] = new Dictionary<VertexType, WeightType>();
        }

        public void AddEdge((VertexType Source, VertexType Destination, WeightType Weight) edge)
        {
            if (AdjacencyList.ContainsKey(edge.Source) && AdjacencyList.ContainsKey(edge.Destination))
            {
                AdjacencyList[edge.Source].Add(edge.Destination, edge.Weight);
                AdjacencyList[edge.Destination].Add(edge.Source, edge.Weight);
            }
        }
    }
}
