using System;
using Common;
using Common.Pool;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IRocketLauncherOwner, IDamagable
    {
        [Header("Speed")]
        [SerializeField, Min(0f)] private float _longitudinalRollSpeed = 0f;
        [SerializeField, Min(0f)] private float _lateralRollSpeed = 0f;
        [SerializeField, Min(0f)] private float _defaultSpeed = 0f;
        [SerializeField, Min(0f)] private float _acceleratedSpeed = 0f;
        
        private float _speed = 0f;

        [Header("Movement")]
        [SerializeField] private bool _invertHorizontal = false;
        [SerializeField] private bool _invertVertical = false;
        
        [Header("Attack")]
        [SerializeField] private ColliderRuntimeSet _attackTargets;
        [SerializeField, Min(0f)] private float _reloadDelayInSeconds = 0f;
        [SerializeField] private GameObjectsPool _rocketsPool;

        private RocketLauncher _rocketLauncher = null;
        
        public event Action<float, float> AfterInputAdjusted;

        private void Awake()
        {
            _rocketLauncher = new RocketLauncher(this);
        }

        private void Update() // TODO: разбить на методы
        {
            _rocketLauncher.Update();
            
            var deltaTime = Time.deltaTime;
            var invertHorizontal = _invertHorizontal ? -1 : 1;
            var invertVertical = _invertVertical ? -1 : 1;
            var verticalInput = Input.GetAxis("Vertical");
            var horizontalInput = Input.GetAxis("Horizontal");
            var rotateAngles = new Vector3(
                invertVertical * _longitudinalRollSpeed * verticalInput * deltaTime,
                0f,
                invertHorizontal * _lateralRollSpeed * horizontalInput * deltaTime
            );
            var cachedTransform = transform;
            cachedTransform.Rotate(rotateAngles);
            cachedTransform.position += cachedTransform.forward * _speed * deltaTime;

            TryAttack();
            ManipulateAcceleration();

            AfterInputAdjusted?.Invoke(verticalInput, horizontalInput);
        }

        private void TryAttack()
        {
            if (Input.GetButton("Fire"))
            {
                _rocketLauncher.TryLaunchRocket();
            }
        }

        private void ManipulateAcceleration()
        {
            _speed = Input.GetButton("Accelerate") ? _acceleratedSpeed : _defaultSpeed;
        }

        public void Damage()
        {
            Debug.Log("Конец игры");
        }

        public GameObjectsPool RocketsPool => _rocketsPool;
        
        public Component RocketLauncherOwner => this;
        
        public float Speed => _speed;
        
        public float ReloadDelayInSeconds => _reloadDelayInSeconds;
    }
}
