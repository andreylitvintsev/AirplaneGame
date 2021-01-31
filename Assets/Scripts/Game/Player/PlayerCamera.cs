using Game.Extensions;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController = null;
        [SerializeField, Min(0f)] private float _longitudinalAngleBound = 0f;
        [SerializeField, Min(0f)] private float _followingDistance = 0f;
        [SerializeField] private Animator _animator = null;
        
        private static readonly int IsAcceleratedAnimatorProperty = Animator.StringToHash("IsAccelerated");

        private void AfterPlayerInputAdjusted(float verticalInput, float horizontalInput) // TODO: разбить на методы
        {
            var playerTransform = _playerController.transform;
            var newCameraPosition = playerTransform.TransformPoint(
                Vector3.back.RotateByEulerX(-verticalInput * _longitudinalAngleBound) * _followingDistance);
            transform.position = newCameraPosition;
            
            transform.LookAt(playerTransform, playerTransform.up);

            HandleAccelerateInput();
        }

        private void HandleAccelerateInput()
        {
            _animator.SetBool(IsAcceleratedAnimatorProperty, Input.GetButton("Accelerate"));
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
            _playerController.LogIfNull(nameof(_playerController));
            _playerController.AfterInputAdjusted += AfterPlayerInputAdjusted;
            
            _animator.LogIfNull(nameof(_animator));
        }

        private void OnDisable()
        {
            if (_playerController != null)
            {
                _playerController.AfterInputAdjusted -= AfterPlayerInputAdjusted;
            }
        }
    }
}
