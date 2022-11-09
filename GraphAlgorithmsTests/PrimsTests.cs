namespace GraphAlgorithmsTests
{
    public partial class GraphTests
    {
        public class PrimsTests
        {
            [Fact]
            public void Prims_can_find_Minimum_spanning_tree()
            {
                // Given
                var graph = new WeightedGraph<int, int>(vertices, edgesWithWeight);
                var expected = new Dictionary<int, int[]> {
                    { 1, new[] { 2,3} },
                    { 2, new[] { 1, 4 } },
                    { 3, new[] { 1, 5, 6 } },
                    { 4, new[] { 2, 7 } },
                    { 5, new[] { 3, 8 } },
                    { 6, new[] { 3 } },
                    { 7, new[] { 4 } },
                    { 8, new[] { 5, 9 } },
                    { 9, new[] { 8, 10 } },
                    { 10, new[] { 9 } },
                };
                // When
                var result = new Prims<int, int>().MST(graph, 1);
                // Then
                Assert.Collection(
                    result.AdjacencyList.OrderBy(a => a.Key),
                    expected.Select(e => new Action<KeyValuePair<int,HashSet<int>>>(v =>
                                                                            {
                                                                                Assert.Equal(e.Key, v.Key);
                                                                                AssertEdges(e.Value.OrderBy(e => e), v.Value.OrderBy(v => v));
                                                                            })
                    ).ToArray());
            }

            public void AssertEdges(IEnumerable<int> expectedEdges, IEnumerable<int> actualEdges)
            {
                Assert.Collection(actualEdges, expectedEdges.Select(e => new Action<int>(v => Assert.Equal(e, v))).ToArray());
            }
        }
    }
}
