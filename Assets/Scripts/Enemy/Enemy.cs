using Common;
using UnityEngine;

namespace Enemy
{
    [DisallowMultipleComponent]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ColliderRuntimeSet _attackTargets;

        private void Start()
        {
            if (_attackTargets == null)
            {
                Debug.LogError("'Attack Targets' must be not null!");
            }
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
            if (found && _attackTargets.Contains(hitInfo.collider))
            {
                // TODO: стреляем ракетой
                Debug.Log("стреляем ракетой");
            }
        }
    }
}