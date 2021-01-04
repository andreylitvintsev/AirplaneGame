using UnityEngine;

namespace Common
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _relativeSpeed = 0f;

        public float ParentSpeed { get; set; }

        private void Update()
        {
            var speed = ParentSpeed + _relativeSpeed;
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
    }
}
