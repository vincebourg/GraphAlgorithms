
using GraphAlgorithms;

namespace GraphAlgorithmsTests
{
    public partial class GraphTests
    {
        public class DijkstraTests
        {
            [Fact]
            public void Dijkstra_can_find_shortest_path()
            {
                // Given
                var weightedGraph = new WeightedGraph<int, int>(vertices, edgesWithWeight);
                var expectedResult = new[] { 1, 3, 5, 8, 9, 10 };
                var dijkstra = new Dijkstra<int, int>();
                // When
                var result = dijkstra.ShortestPath(weightedGraph, 1, 10);
                // Then
                Assert.Collection(result, expectedResult.Select(e => new Action<int>(r => Assert.Equal(r,e))).ToArray());
            }

            [Fact]
            public void Dijkstra_can_find_best_next_candidate()
            {
                // given
                var previous = new Dictionary<int, (int, int)> { {4, (2,2) }, { 2, (1, 1) } };
                var weightedGraph = new WeightedGraph<int, int>(vertices, edgesWithWeight);
                var candidates = new LinkedList<(int, int)>();
                candidates.AddLast((4, 2));
                candidates.AddLast((5, 3));
                candidates.AddLast((6,4));
                var expectedNewcandidates = new List<KeyValuePair<int, int>>{ new(7, 1), new(5, 3), new(6,4)  };
                var expectedNewPrevious = new (int current, (int previous, int weight) value)[] { ( 4, (2, 2) ), ( 2, (1, 1) ), (7, (4,3)) };
                var dijkstra = new Dijkstra<int, int>();
                // When
                var result = dijkstra.ExploreNextCandidate(weightedGraph, candidates, previous);

                //Then
                Assert.Equal(4, result);
                Assert.Collection(previous, expectedNewPrevious.Select(e => new Action<KeyValuePair<int, (int previous, int weight) >>(v => {
                                                                                                    Assert.Equal(e.current, v.Key);
                                                                                                    Assert.Equal(e.value.previous, v.Value.previous);
                                                                                                    Assert.Equal(e.value.weight, v.Value.weight);
                                                                })).ToArray());

                Assert.Collection(candidates, expectedNewcandidates.Select(e => new Action<(int, int)>(v => {
                    Assert.Equal(e.Key, v.Item1);
                    Assert.Equal(e.Value, v.Item2);
                })).ToArray());

            }

            [Fact]
            public void Dijkstra_can_add_from_generics()
            {
                // Given
                var expectedResult = 7;
                var dijkstra = new Dijkstra<int, int>();
                // When
                var result = dijkstra.Add(3, 4);
                // Then
                Assert.Equal(expectedResult, result);
            }

            [Fact]
            public void Dijkstra_can_compare_less_than_from_generics()
            {
                // Given
                var dijkstra = new Dijkstra<int, int>();
                // When
                var result = dijkstra.LessThan(3, 4);
                // Then
                Assert.True(result);
            }

            [Fact]
            public void Dijkstra_can_compare_less_than_or_equal_from_generics()
            {
                // Given
                var dijkstra = new Dijkstra<int, int>();
                // When
                var result = dijkstra.LessThanOrEqual(4, 4);
                // Then
                Assert.True(result);
            }
        }

    }
}
