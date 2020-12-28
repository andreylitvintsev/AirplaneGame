using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController = null;
        [SerializeField, Min(0f)] private float _longitudinalAngleBound = 0f;
        [SerializeField, Min(0f)] private float _followingDistance = 0f;

        private void AfterPlayerInputHandled(float verticalInput, float horizontalInput)
        {
            var playerTransform = _playerController.transform;
            var newCameraPosition = playerTransform.TransformPoint(
                Vector3.back.RotateByEulerX(-verticalInput * _longitudinalAngleBound) * _followingDistance);
            transform.position = newCameraPosition;
            
            // TODO: расчитать up вектор (отложить)
            transform.LookAt(playerTransform, playerTransform.up);
        }

        private void OnDrawGizmosSelected()
        {
            var playerTransform = _playerController.transform;
            var playerPosition = playerTransform.position;
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, playerPosition);
                
            var upAngleBoundVector = playerTransform.TransformPoint(
                Vector3.back.RotateByEulerX(_longitudinalAngleBound) * _followingDistance);
            var downAngleBoundVector = playerTransform.TransformPoint(
                Vector3.back.RotateByEulerX(-_longitudinalAngleBound) * _followingDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(playerPosition, upAngleBoundVector);
            Gizmos.DrawLine(playerPosition, downAngleBoundVector);
        }

        private void OnEnable()
        {
            if (_playerController != null)
            {
                _playerController.AfterInputHandled += AfterPlayerInputHandled;
            }
            else
            {
                Debug.LogError("Player Controller is null", this);
            }
        }

        private void OnDisable()
        {
            if (_playerController != null)
            {
                _playerController.AfterInputHandled -= AfterPlayerInputHandled;
            }
        }
    }
}
