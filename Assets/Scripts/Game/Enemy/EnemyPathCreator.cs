using Game.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    [RequireComponent(typeof(PathCreation.PathCreator))]
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField] private Vector3 _fromBounds = Vector3.zero;
        [SerializeField] private Vector3 _toBounds = Vector3.zero;
        [SerializeField, Min(2)] private int pointCount = 2;

        private void Start()
        {
            var bezierPathCreator = GetComponent<PathCreation.PathCreator>().bezierPath;
            for (int i = 0; i < pointCount; ++i)
            {
                bezierPathCreator.AddSegmentToEnd(RandomPoint());
            }
        
            // удаляем первоначально сгенерированные библиотекой сегменты
            bezierPathCreator.DeleteSegment(0);
            bezierPathCreator.DeleteSegment(1);

            bezierPathCreator.IsClosed = true;
        }

        private Vector3 RandomPoint()
        {
            return new Vector3(
                Random.Range(_fromBounds.x, _toBounds.x),
                Random.Range(_fromBounds.y, _toBounds.y),
                Random.Range(_fromBounds.z, _toBounds.z)
            );
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var gizmoCubeBounds = _fromBounds.ToBounds(_toBounds);
            Gizmos.DrawWireCube(gizmoCubeBounds.center, gizmoCubeBounds.size);
        }
    }
}
