using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Game.Extensions;
using Game.Player;
using Game.RuntimeSet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DistanceMarkers : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private ColliderRuntimeSet _enemiesSet = null;
        [SerializeField] private TMP_Text _distanceMarkerPrefab = null;
        [SerializeField] private Image _outOfViewMarkerPrefab = null;
        [SerializeField] private PlayerController _player = null;
        [SerializeField, Min(0f)] private float _outOfViewMarkerRadius = 5f;
        
        private Dictionary<Collider, TMP_Text> _distanceMarkerMap = new Dictionary<Collider, TMP_Text>();
        private Dictionary<Collider, Image> _outOfViewMarkerMap = new Dictionary<Collider, Image>();

        private void Start()
        {
            _camera.LogIfNull(nameof(_camera));
            _canvas.LogIfNull(nameof(_canvas));
            _enemiesSet.LogIfNull(nameof(_enemiesSet));
            _distanceMarkerPrefab.LogIfNull(nameof(_distanceMarkerPrefab));
            _outOfViewMarkerPrefab.LogIfNull(nameof(_outOfViewMarkerPrefab));
            _player.LogIfNull(nameof(_player));

            _enemiesSet.Changed += OnEnemySetChanged;

            CreateMarkers();
        }

        private void CreateMarkers()
        {
            foreach (var enemy in _enemiesSet)
            {
                _distanceMarkerMap.Add(enemy, Instantiate(_distanceMarkerPrefab, transform));
                _outOfViewMarkerMap.Add(enemy, Instantiate(_outOfViewMarkerPrefab, transform));
            }
        }

        private void Update()
        {
            foreach (var enemy in _enemiesSet)
            {
                var distanceMarker = _distanceMarkerMap[enemy];
                var outOfViewMarker = _outOfViewMarkerMap[enemy];
                
                if (IsCameraCanSee(enemy))
                {
                    distanceMarker.gameObject.SetActive(true);
                    SetupDistanceMarker(distanceMarker, enemy);
                    outOfViewMarker.gameObject.SetActive(false);
                }
                else
                {
                    distanceMarker.gameObject.SetActive(false);
                    outOfViewMarker.gameObject.SetActive(true);
                    SetupOutOfViewMarker(outOfViewMarker, enemy);
                }
            }
        }

        private void SetupDistanceMarker(TMP_Text distanceMarker, Collider enemy)
        {
            var enemyPosition = enemy.transform.position;
            var distanceVector = enemyPosition - _camera.transform.position;

            distanceMarker.text = ((int) distanceVector.magnitude).ToString(CultureInfo.InvariantCulture);
            distanceMarker.transform.localPosition = _camera.WorldToScreenPoint(enemyPosition);
        }

        private void SetupOutOfViewMarker(Image outOfViewMarker, Collider enemy)
        {
            var cameraTransform = _camera.transform;
            var distanceVector = enemy.transform.position - cameraTransform.position;
            var cameraForward = cameraTransform.forward;
            var projectedVectorOnScreenPlane = Vector3.ProjectOnPlane(
                distanceVector, cameraForward).normalized;
            var outOfViewMarkerTransform = outOfViewMarker.transform;
            
            outOfViewMarkerTransform.localPosition = _camera.WorldToScreenPoint(
                _player.transform.position + projectedVectorOnScreenPlane * _outOfViewMarkerRadius);
            outOfViewMarkerTransform.SetEuler( z: Vector3.SignedAngle(
                -cameraTransform.transform.up, projectedVectorOnScreenPlane, cameraForward));
        }

        private bool IsCameraCanSee(Collider @object)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_camera), @object.bounds);
        }

        private void OnEnemySetChanged()
        {
            try
            {
                var killedEnemy = _distanceMarkerMap
                .FirstOrDefault(mapEntry => !mapEntry.Key.gameObject.activeSelf).Key;
            
                _distanceMarkerMap[killedEnemy].gameObject.SetActive(false);
                _distanceMarkerMap.Remove(killedEnemy);
                _outOfViewMarkerMap[killedEnemy].gameObject.SetActive(false);
                _outOfViewMarkerMap.Remove(killedEnemy);
            }
            catch (Exception a)
            {
                // ignored
            }
        }
    }
}
