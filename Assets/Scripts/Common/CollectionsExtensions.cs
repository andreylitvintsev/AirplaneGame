using System.Collections.Generic;

namespace Common
{
    public static class CollectionsExtensions
    {
        public static bool Pop<T>(this LinkedList<T> list, out T result)
        {
            if (list.Count == 0)
            {
                result = default;
                return false;
            }
            
            result = list.First.Value;
            list.RemoveFirst();
            return true;
        }
    }
}