using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField, Min(0f)] private float _longitudinalRollSpeed = 0f;
        [SerializeField, Min(0f)] private float _lateralRollSpeed = 0f;
        [SerializeField, Min(0f)] private float _speed = 0f;

        [Header("Control")]
        [SerializeField] private bool _invertHorizontal = false;
        [SerializeField] private bool _invertVertical = false;

        public event Action<float, float> AfterInputHandled;

        private void Update()
        {
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
        
            AfterInputHandled?.Invoke(verticalInput, horizontalInput);
        }
    }
}
