namespace GraphAlgorithms
{
    public class Prims<VertexType, WeightType> where VertexType : notnull, IEquatable<VertexType>
                                               where WeightType : notnull, IEquatable<WeightType>      
    {
        public Graph<VertexType> MST(WeightedGraph<VertexType, WeightType> graph, VertexType v)
        {
            return new Graph<VertexType>();
        }
    }
}