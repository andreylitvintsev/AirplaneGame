using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(PathCreation.PathCreator))]
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField] private Vector3 _fromBounds;

        [SerializeField]
        private Vector3 _toBounds;

        [SerializeField, Min(2)]
        private int pointCount = 2;

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

        private void OnDrawGizmosSelected() // TODO: можно вынести в partial class
        {
            var gizmoCubeBounds = new Bounds();
            gizmoCubeBounds.Encapsulate(_fromBounds);
            gizmoCubeBounds.Encapsulate(_toBounds);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gizmoCubeBounds.center, gizmoCubeBounds.size);
        }
    }
}
