﻿using Common;
using PathCreation.Examples;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(PathFollower)), DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private ColliderRuntimeSet _attackTargets;
        [SerializeField, Min(0f)] private float _reloadDelayInSeconds = 0f;

        private float _spendTimeFromLastAttack;

        [SerializeField] private RocketController _rocketPrefab;

        private PathFollower _pathFollower;
        
        private void Start()
        {
            if (_attackTargets == null)
            {
                Debug.LogError("'Attack Targets' must be not null!");
            }
            
            if (_rocketPrefab == null)
            {
                Debug.LogError("'Rocket prefab' must be not null!");
            }

            _pathFollower = GetComponent<PathFollower>();
            
            _spendTimeFromLastAttack = _reloadDelayInSeconds;
        }

        private void Update()
        {
            TryFindAndAttack();
        }

        private void TryFindAndAttack()
        {
            var cachedTransform = transform;
            var raycastRay = new Ray(cachedTransform.position, cachedTransform.forward);
            var found = Physics.Raycast(raycastRay, out var hitInfo);

            _spendTimeFromLastAttack += Time.deltaTime; 
            if (found && _attackTargets.Contains(hitInfo.collider) && _spendTimeFromLastAttack >= _reloadDelayInSeconds)
            {
                _spendTimeFromLastAttack = 0f;
                LaunchRocket();
            }
        }

        private void LaunchRocket()
        {
            var cachedTransform = transform;
            var rocketInstance = Instantiate(
                _rocketPrefab, cachedTransform.position, cachedTransform.rotation);
            rocketInstance.ParentSpeed = _pathFollower.speed;
        }
    }
}