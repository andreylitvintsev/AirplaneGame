using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Common
{
    public class DistanceMarkers : MonoBehaviour
    {
        [SerializeField] private Camera _camera = null;
        
        [SerializeField] private Canvas _canvas = null;
        
        [SerializeField] private ColliderRuntimeSet _enemiesSet = null;
        [SerializeField] private TMP_Text _markerPrefab = null;
        
        private Dictionary<Collider, TMP_Text> _markerMap = new Dictionary<Collider, TMP_Text>();
        
        private void Start()
        {
            if (_camera == null)
            {
                Debug.LogError("'Camera' must be not null!");
            }
            if (_canvas == null)
            {
                Debug.LogError("'Canvas' must be not null!");
            }
            if (_enemiesSet == null)
            {
                Debug.LogError("'Enemies Set' must be not null!");
            }
            if (_markerPrefab == null)
            {
                Debug.LogError("'Mark Prefab' must be not null!");
            }

            CreateMarkers();
        }

        private void CreateMarkers()
        {
            foreach (var enemy in _enemiesSet)
            {
                var markerPrefab = Instantiate(_markerPrefab, transform);
                _markerMap.Add(enemy, markerPrefab);
            }
        }

        private void Update()
        {
            var cameraTransform = _camera.transform;
            foreach (var enemy in _enemiesSet)
            {
                var enemyTransform = enemy.transform;
                var marker = _markerMap[enemy];
                var distanceVector = enemyTransform.position - cameraTransform.position;
                if (Vector3.Dot(distanceVector.normalized, cameraTransform.forward) > 0)
                {
                    marker.enabled = true;
                    marker.text = ((int) distanceVector.magnitude).ToString(CultureInfo.InvariantCulture);
                    marker.transform.localPosition = _camera.WorldToScreenPoint(enemyTransform.position);
                }
                else
                {
                    marker.enabled = false;
                }
            }
        }
    }
}
