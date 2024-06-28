using System.Collections.Concurrent;

namespace CommandLineGenerator.Caching;

public class ConcurrentCache<T1, T2>
{
    private readonly ConcurrentDictionary<T1, T2> concurrentCache = new ();

    public T2 Get(T1 key, Func<T1, T2> valueCreator)
    {
        if (concurrentCache.TryGetValue(key, out var value))
        {
            return value;
        }

        var createdValue = valueCreator.Invoke(key);
        concurrentCache[key] = createdValue;
        return createdValue;
    }
}