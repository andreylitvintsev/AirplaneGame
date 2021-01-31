using UnityEngine;

namespace Game.RuntimeSet
{
    [RequireComponent(typeof(Collider)), DisallowMultipleComponent]
    public class EnemyMark : MonoBehaviour
    {
        [SerializeField] private ColliderRuntimeSet _runtimeSet;
    
        private Collider _collider;
    
        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            _runtimeSet.Add(_collider);
        }

        private void OnDisable()
        {
            _runtimeSet.Remove(_collider);
        }
    }
}
