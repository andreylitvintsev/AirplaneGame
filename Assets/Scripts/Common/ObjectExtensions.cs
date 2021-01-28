using UnityEngine;

namespace Common
{
    public static class ComponentExtensions
    {
        public static void LogIfNull(this Object component, string variableName)
        {
            if (component == null)
            {
                Debug.LogError($"'{variableName}' must be not null!");
            }
        }
    }
}