using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Common
{
    public class DistanceMarkers : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        
        [SerializeField] private ColliderRuntimeSet _enemiesSet = null;
        [SerializeField] private TMP_Text _markerPrefab = null;
        
        private Dictionary<Collider, TMP_Text> _markerMap = new Dictionary<Collider, TMP_Text>();
        
        private void Start()
        {
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
            foreach (var enemy in _enemiesSet)
            {
                var marker = _markerMap[enemy];
                var distanceVector = enemy.transform.position - Camera.main.transform.position;
                if (Vector3.Dot(distanceVector.normalized, Camera.main.transform.forward) > 0)
                {
                    marker.enabled = true;
                    marker.text = ((int)distanceVector.magnitude).ToString(CultureInfo.InvariantCulture);
                    marker.transform.localPosition =
                        Camera.main.WorldToScreenPoint(enemy.transform.position);
                }
                else
                {
                    marker.enabled = false;
                }
            }
        }
    }
}
