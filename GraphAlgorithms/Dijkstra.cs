using System.Linq.Expressions;

namespace GraphAlgorithms
{
    public class Dijkstra<VerticeType, WeightType> where VerticeType : notnull, IEquatable<VerticeType>
                                                   where WeightType : notnull, IEquatable<WeightType>
    {
        private Func<WeightType, WeightType, WeightType> _add = null;
        private Func<WeightType, WeightType, bool> _lessThan = null;
        private Func<WeightType, WeightType, bool> _lessThanOrEqual = null;
        public HashSet<VerticeType> ShortestPath(WeightedGraph<VerticeType, WeightType> weightedGraph, VerticeType source, VerticeType destination)
        {
            var candidates = new LinkedList<(VerticeType vertex, WeightType weight)>();
            candidates.AddFirst((source, default));
            var previous = new Dictionary<VerticeType, (VerticeType previous, WeightType weight)> { { source, default } };
            var lastCandidate = source;
            while (!destination.Equals(lastCandidate) && candidates.Any())
            {
                lastCandidate = ExploreNextCandidate(weightedGraph, candidates, previous);
            }
            var result = new HashSet<VerticeType>();
            if (previous.ContainsKey(destination))
            {
                result.Add(destination);
                var currentVertex = destination;
                while (previous.TryGetValue(currentVertex, out var previousVertex) && !currentVertex.Equals(source))
                {
                    result.Add(previousVertex.previous);
                    currentVertex = previousVertex.previous;
                }
                result.Add(source);
            }
            return result.Reverse().ToHashSet();
        }

        public VerticeType ExploreNextCandidate(WeightedGraph<VerticeType, WeightType> weightedGraph, LinkedList<(VerticeType vertex, WeightType weight)> candidates, Dictionary<VerticeType, (VerticeType, WeightType)> previous)
        {
            var nextCandidate = candidates.First;
            candidates.RemoveFirst();
            foreach (var newCandidate in weightedGraph.AdjacencyList[nextCandidate.Value.vertex])
            {
                var newWeight = Add(newCandidate.Value, previous[nextCandidate.Value.vertex].Item2);
                var previousCandidate = candidates.Find(candidates.LastOrDefault(c => LessThanOrEqual(c.weight, newCandidate.Value)));
                if (!previous.ContainsKey(newCandidate.Key))
                {
                    previous.Add(newCandidate.Key, (nextCandidate.Value.vertex, newWeight));
                    UpdateCandidates(candidates, newCandidate, previousCandidate);
                }
                else if (LessThan(newWeight, previous[newCandidate.Key].Item2))
                {
                    previous[newCandidate.Key] = (nextCandidate.Value.vertex, newWeight);
                    UpdateCandidates(candidates, newCandidate, previousCandidate);
                }
            }
            return nextCandidate.Value.vertex;
        }

        private static void UpdateCandidates(LinkedList<(VerticeType vertex, WeightType weight)> candidates, KeyValuePair<VerticeType, WeightType> newCandidate, LinkedListNode<(VerticeType vertex, WeightType weight)>? previousCandidate)
        {
            if (previousCandidate == null)
            {
                candidates.AddFirst((newCandidate.Key, newCandidate.Value));
            }
            else
            {
                candidates.AddAfter(previousCandidate, (newCandidate.Key, newCandidate.Value));
            }
        }

        public WeightType Add(WeightType a, WeightType b)
        {

            if (_add == null)
            {
                _add = GenerateAddFunction();
            }
            // Call it
            return _add(a, b);
        }

        public bool LessThan(WeightType a, WeightType b)
        {

            if (_lessThan == null)
            {
                _lessThan = GenerateLowerThanFunction();
            }
            // Call it
            return _lessThan(a, b);
        }

        public bool LessThanOrEqual(WeightType a, WeightType b)
        {

            if (_lessThanOrEqual == null)
            {
                _lessThanOrEqual = GenerateLowerThanOrEqualFunction();
            }
            // Call it
            return _lessThanOrEqual(a, b);
        }

        private Func<WeightType, WeightType, WeightType> GenerateAddFunction()
        {
            // Declare the parameters
            var paramA = Expression.Parameter(typeof(WeightType));
            var paramB = Expression.Parameter(typeof(WeightType));

            // Add the parameters together
            BinaryExpression body = Expression.Add(paramA, paramB);

            // Compile it
            return Expression.Lambda<Func<WeightType, WeightType, WeightType>>(body, paramA, paramB).Compile();
        }

        private Func<WeightType, WeightType, bool> GenerateLowerThanFunction()
        {
            // Declare the parameters
            var paramA = Expression.Parameter(typeof(WeightType));
            var paramB = Expression.Parameter(typeof(WeightType));

            // Add the parameters together
            BinaryExpression body = Expression.LessThan(paramA, paramB);

            // Compile it
            return Expression.Lambda<Func<WeightType, WeightType, bool>>(body, paramA, paramB).Compile();
        }

        private Func<WeightType, WeightType, bool> GenerateLowerThanOrEqualFunction()
        {
            // Declare the parameters
            var paramA = Expression.Parameter(typeof(WeightType));
            var paramB = Expression.Parameter(typeof(WeightType));

            // Add the parameters together
            BinaryExpression body = Expression.LessThanOrEqual(paramA, paramB);

            // Compile it
            return Expression.Lambda<Func<WeightType, WeightType, bool>>(body, paramA, paramB).Compile();
        }
    }
}