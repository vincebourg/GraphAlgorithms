using System.Linq.Expressions;

namespace GraphAlgorithms
{
    public class Prims<VertexType, WeightType> where VertexType : notnull, IEquatable<VertexType>
                                               where WeightType : notnull, IEquatable<WeightType>      
    {
        private Func<WeightType, WeightType, WeightType> _add = null;
        private Func<WeightType, WeightType, bool> _lessThan = null;
        private Func<WeightType, WeightType, bool> _lessThanOrEqual = null;

        public Graph<VertexType> MST(WeightedGraph<VertexType, WeightType> graph, VertexType source)
        {
            var candidates = new LinkedList<(VertexType vertex, WeightType weight)>();
            candidates.AddFirst((source, default));
            var previous = new Dictionary<VertexType, (VertexType previous, WeightType weight)> { { source, default } };
            bool bContinue = true;
            while (bContinue)
            {
                bContinue = ExploreNextCandidate(graph, candidates, previous);
            }
            var vertices = previous.Keys.Concat(previous.Values.Select(v => v.previous))
                                        .Distinct()
                                        .Where(v => !v.Equals(default));
            var edges = previous.Select(p => new Tuple<VertexType,VertexType>(p.Key, p.Value.previous));
            return new Graph<VertexType>(vertices, edges);
        }

        public bool ExploreNextCandidate(WeightedGraph<VertexType, WeightType> weightedGraph, LinkedList<(VertexType vertex, WeightType weight)> candidates, Dictionary<VertexType, (VertexType, WeightType)> previous)
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
            return candidates.Any();
        }

        private static void UpdateCandidates(LinkedList<(VertexType vertex, WeightType weight)> candidates, KeyValuePair<VertexType, WeightType> newCandidate, LinkedListNode<(VertexType vertex, WeightType weight)>? previousCandidate)
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