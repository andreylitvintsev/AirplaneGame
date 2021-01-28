using System;
using System.Collections.Generic;
using System.Globalization;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class DistanceMarkers : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private ColliderRuntimeSet _enemiesSet = null;
        [SerializeField] private TMP_Text _distanceMarkerPrefab = null;
        [SerializeField] private Image _outOfViewMarkerPrefab = null;

        [SerializeField] private PlayerController _player;
        
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
            var cameraTransform = _camera.transform;
            foreach (var enemy in _enemiesSet)
            {
                var enemyTransform = enemy.transform;
                var enemyPosition = enemyTransform.position;
                var distanceMarker = _distanceMarkerMap[enemy];
                var outOfViewMarker = _outOfViewMarkerMap[enemy];
                
                var distanceVector = enemyPosition - cameraTransform.position;
                if (IsCameraCanSee(enemy))
                {
                    distanceMarker.gameObject.SetActive(true);
                    distanceMarker.text = ((int) distanceVector.magnitude).ToString(CultureInfo.InvariantCulture);
                    distanceMarker.transform.localPosition = _camera.WorldToScreenPoint(enemyPosition);
                    
                    outOfViewMarker.gameObject.SetActive(false);
                }
                else
                {
                    distanceMarker.gameObject.SetActive(false);
                    
                    outOfViewMarker.gameObject.SetActive(true);
                    var vect = Vector3.ProjectOnPlane(distanceVector, _camera.transform.forward);
                    vect = vect.normalized;
                    outOfViewMarker.transform.localPosition = _camera.WorldToScreenPoint(_player.transform.position + vect * 5);

                    var a = outOfViewMarker.transform.rotation;
                    var b = a.eulerAngles;
                    Debug.Log(Vector3.Angle(-cameraTransform.transform.up, vect));
                    b.z = Vector3.SignedAngle(-cameraTransform.transform.up, vect, _camera.transform.forward);
                    a.eulerAngles = b;
                    outOfViewMarker.transform.rotation = a;
                }
            }
        }

        private bool IsCameraCanSee(Collider @object)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_camera), @object.bounds);
        }
    }
}
