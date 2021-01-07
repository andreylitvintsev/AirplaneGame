using Common.Pool;
using UnityEngine;

namespace Common
{
    public interface IRocketLauncherOwner
    {
        GameObjectsPool RocketsPool { get; }
        
        Component RocketLauncherOwner { get; }
        
        float Speed { get; }
        
        float ReloadDelayInSeconds { get; }
    }
}