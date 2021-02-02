using Game.Extensions;
using Game.Pool;
using Game.RuntimeSet;
using PathCreation.Examples;
using UnityEngine;

namespace Game.Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PathFollower))]
    public class EnemyController : MonoBehaviour, IRocketLauncherOwner, IDamagable
    {
        [SerializeField] private ColliderRuntimeSet _attackTargets;
        [SerializeField, Min(0f)] private float _reloadDelayInSeconds = 0f;
        [SerializeField] private GameObjectsPool _rocketsPool;
        [SerializeField] private Animator _animator;
        [SerializeField, Min(0f)] private float _sphereCastRadiusForEnemyCheck = 0.5f;
        
        private PathFollower _pathFollower;
        private AnimationEventReceiver _animationEventReceiver;

        private RocketLauncher _rocketLauncher = null;
        private static readonly int IsDestroyed = Animator.StringToHash("IsDestroyed");

        public bool Killed { get; private set; } = false;
        
        private void Awake()
        {
            _rocketLauncher = new RocketLauncher(this);
        }

        private void Start() // TODO: может awake?
        {
            _attackTargets.LogIfNull(nameof(_attackTargets));
            _rocketsPool.LogIfNull(nameof(_rocketsPool));
            
            _pathFollower = GetComponent<PathFollower>();
            _animationEventReceiver = GetComponent<AnimationEventReceiver>();
        }

        private void Update()
        {
            _rocketLauncher.Update();
            TryFindAndAttack();
        }
        private void TryFindAndAttack()
        {
            var cachedTransform = transform;
            var raycastRay = new Ray(cachedTransform.position, cachedTransform.forward);
            if (Physics.SphereCast(raycastRay, _sphereCastRadiusForEnemyCheck, out var hit) 
                && _attackTargets.Contains(hit.collider))
            {
                _rocketLauncher.TryLaunchRocket();
            }
        }

        public void Damage()
        {
            if (Killed) return;
            Killed = true;
            _animator.SetBool(IsDestroyed, true);
        }
        
        // called from Animation receiver
        public void OnDestroyAnimationEnd()
        {
            gameObject.SetActive(false);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sphereCastRadiusForEnemyCheck);
        }

        public GameObjectsPool RocketsPool => _rocketsPool;

        public Component RocketLauncherOwner => this;

        public float Speed => _pathFollower.speed;

        public float ReloadDelayInSeconds => _reloadDelayInSeconds;
    }
}