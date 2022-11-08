namespace GraphAlgorithmsTests
{
    public partial class GraphTests
    {
        public class BreadthFirstTests
        {
            [Fact]
            public void BFS_visits_all_possible_vertexes()
            {
                // Given
                var graph = new Graph<int>(vertices, edges);
                // When
                var result = BreadthFirst.BFS(graph, 1);
                // Then
                Assert.Collection(result, vertices.Select(v => new Action<int>(t => Assert.Equal(v, t))).ToArray());
            }

            [Theory]
            [InlineData(1, new int[] { 1 })]
            [InlineData(2, new int[] { 1, 2 })]
            [InlineData(3, new int[] { 1, 3 })]
            [InlineData(4, new int[] { 1, 2, 4 })]
            [InlineData(5, new int[] { 1, 3, 5 })]
            [InlineData(6, new int[] { 1, 3, 6 })]
            [InlineData(7, new int[] { 1, 2, 4, 7 })]
            [InlineData(8, new int[] { 1, 3, 5, 8 })]
            [InlineData(9, new int[] { 1, 3, 5, 8, 9 })]
            [InlineData(10, new int[] { 1, 3, 5, 8, 10 })]
            public void BFS_shortestPath_from1(int destination, int[] expectedPath)
            {
                // Given
                var graph = new Graph<int>(vertices, edges);
                // When
                var result = BreadthFirst.ShortestPathFunction(graph, 1)(destination);
                // Then
                Assert.Collection(result, expectedPath.Select(e => new Action<int>(v => Assert.Equal(e, v))).ToArray());
            }
        }
    }

}
