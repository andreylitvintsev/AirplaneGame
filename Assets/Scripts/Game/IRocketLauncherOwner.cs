using Game.Pool;
using UnityEngine;

namespace Game
{
    public interface IRocketLauncherOwner
    {
        GameObjectsPool RocketsPool { get; }
        
        Component RocketLauncherOwner { get; }
        
        float Speed { get; }
        
        float ReloadDelayInSeconds { get; }
    }
}