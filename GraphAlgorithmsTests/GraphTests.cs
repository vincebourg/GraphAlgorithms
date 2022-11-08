namespace GraphAlgorithmsTests
{
    public partial class GraphTests
    {
        public static readonly int[] vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static readonly Tuple<int, int>[] edges = new[]{Tuple.Create(1,2), Tuple.Create(1,3),
                Tuple.Create(2,4), Tuple.Create(3,5), Tuple.Create(3,6),
                Tuple.Create(4,7), Tuple.Create(5,7), Tuple.Create(5,8),
                Tuple.Create(5,6), Tuple.Create(8,9), Tuple.Create(8,10), Tuple.Create(9,10)};
        public static readonly (int Source, int Destination, int Weight)[] edgesWithWeight = new[]{(1,2,1), (1,3,1),
                (2,4,1), (3,5,2), (3,6,3),
                (4,7,1), (5,7,1), (5,8,1),
                (5,6,1), (8,9,1), (8,10,10), (9,10,1)};
    }
}