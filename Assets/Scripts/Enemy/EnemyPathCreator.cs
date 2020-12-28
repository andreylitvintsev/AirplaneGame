using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(PathCreation.PathCreator))]
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField]
        private Vector3 FromBounds;

        [SerializeField]
        private Vector3 ToBounds;

        [SerializeField, Min(2)]
        private int pointCount = 2;

        // Start is called before the first frame update
        void Start()
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
                Random.Range(FromBounds.x, ToBounds.x),
                Random.Range(FromBounds.y, ToBounds.y),
                Random.Range(FromBounds.z, ToBounds.z)
            );
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
