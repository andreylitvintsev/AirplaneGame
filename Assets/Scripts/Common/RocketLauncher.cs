using Common.Pool;
using UnityEngine;

namespace Common
{
    public class RocketLauncher
    {
        private IRocketLauncherOwner _rocketLauncherOwner;
        
        private GameObjectsPool _cachedRocketsPool = null;
        private Transform _cachedOwnerTransform = null;
        private float _spendTimeFromLastAttack = 0f;

        public RocketLauncher(IRocketLauncherOwner rocketLauncherOwner)
        {
            _rocketLauncherOwner = rocketLauncherOwner;
            
            _cachedRocketsPool = rocketLauncherOwner.RocketsPool;
            _cachedOwnerTransform = rocketLauncherOwner.RocketLauncherOwner.transform;
            _spendTimeFromLastAttack = rocketLauncherOwner.ReloadDelayInSeconds;
        }

        public void Update()
        {
            _spendTimeFromLastAttack += Time.deltaTime;
        }
        
        public void TryLaunchRocket()
        {
            if (_spendTimeFromLastAttack < _rocketLauncherOwner.ReloadDelayInSeconds)
            {
                return;
            } 
            
            if (_cachedRocketsPool.Get(out var result, _cachedOwnerTransform))
            {
                _spendTimeFromLastAttack = 0f;
                
                var rocketInstance = result.GetComponent<RocketController>();
                rocketInstance.ParentSpeed = _rocketLauncherOwner.Speed;
                rocketInstance.ParentCollider = _rocketLauncherOwner.RocketLauncherOwner.GetComponent<Collider>();
            }
        }
    }
}