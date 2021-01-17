using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Collider))]
    public class RocketController : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _relativeSpeed = 0f;

        public Collider ParentCollider { get; set; }
        public float ParentSpeed { get; set; }

        private void Update()
        {
            var speed = ParentSpeed + _relativeSpeed;
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ParentCollider != other)
            {
                Debug.Log("Атакует"); // TODO: убийство
            }
        }
    }
}
