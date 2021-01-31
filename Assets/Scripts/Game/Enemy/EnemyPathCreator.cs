using Game.Variable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    [RequireComponent(typeof(PathCreation.PathCreator))]
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _fromBounds = null;
        [SerializeField] private Vector3Variable _toBounds = null;
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
            var fromBoundsValue = _fromBounds.Value;
            var toBoundsValue = _toBounds.Value;
            return new Vector3(
                Random.Range(fromBoundsValue.x, toBoundsValue.x),
                Random.Range(fromBoundsValue.y, toBoundsValue.y),
                Random.Range(fromBoundsValue.z, toBoundsValue.z)
            );
        }

        private void OnDrawGizmosSelected()
        {
            var gizmoCubeBounds = new Bounds();
            gizmoCubeBounds.Encapsulate(_fromBounds.Value);
            gizmoCubeBounds.Encapsulate(_toBounds.Value);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gizmoCubeBounds.center, gizmoCubeBounds.size);
        }
    }
}
