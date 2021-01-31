using UnityEngine;

namespace Game.Extensions
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