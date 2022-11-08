namespace GraphAlgorithmsTests
{
    public partial class GraphTests
    {
        public class DepthFirstTests
        {
            [Fact]
            public void DFS_visits_all_possible_vertexes()
            {
                // Given
                var graph = new Graph<int>(vertices, edges);
                var expectedOrder = new[] { 1, 2, 4, 7, 5, 3, 6, 8, 9, 10 };
                // When
                var result = DepthFirst.DFS(graph, 1);
                // Then
                Assert.Collection(result, expectedOrder.Select(v => new Action<int>(t => Assert.Equal(v, t))).ToArray());
            }

            [Fact]
            public void DFS_finds_all_loops()
            {
                // Given
                var graph = new Graph<int>(vertices, edges);
                var expectedLoops = new[] {
                new [] { 1, 3, 5, 7, 4, 2 },
                new [] { 5, 6, 3 },
                new [] { 8, 10, 9 },
                };
                // When
                var result = DepthFirst.FindLoopsWithDFS(graph, 1);
                // Then
                Assert.NotEmpty(result);
                Assert.Collection(result, expectedLoops.Select(e => new Action<IEnumerable<int>>(v => AssertLoop(e, v))).ToArray());
            }

            public void AssertLoop<T>(IEnumerable<T> expected, IEnumerable<T> actual)
            {
                Assert.Collection(actual, expected.Select(e => new Action<T>(v => Assert.Equal(v, e))).ToArray());
            }
        
        }
    
    }
}
