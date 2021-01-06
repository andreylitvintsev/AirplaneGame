using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class GameObjectsPool : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _initialInstancesCount = 10;
        [SerializeField] private GameObject _prefab;
        [SerializeField, Min(0f)] private float _autoReturnAfterSec = 5f;
        
        private LinkedList<GameObject> _objects = new LinkedList<GameObject>();

        private void OnEnable()
        {
            if (_prefab == null)
            {
                Debug.LogError("'Rockets Pool' must be not null!");
                return;
            }
            
            for (int i = 0; i < _initialInstancesCount; i++)
            {
                var prefabInstance = Instantiate(_prefab, transform);
                prefabInstance.gameObject.SetActive(false);
                _objects.AddLast(prefabInstance);
            }
        }

        private void OnDisable()
        {
            foreach (var @object in _objects)
            {
                Destroy(@object.gameObject);
            }
            _objects.Clear();
        }

        public bool Get(out GameObject result, Transform applyingPositionAndRotation)
        {
            if (_objects.Pop(out var popResult))
            {
                result = popResult;
                result.transform.position = applyingPositionAndRotation.position;
                result.transform.rotation = applyingPositionAndRotation.rotation;
                result.gameObject.SetActive(true);
                AutoPoolReturnable.Attach(this, result, _autoReturnAfterSec);
                return true;
            }
            
            result = default;
            return false;
        }

        public void Return(GameObject targetGameObject)
        {
            Destroy(targetGameObject.GetComponent<AutoPoolReturnable>());
            targetGameObject.SetActive(false);
            _objects.AddLast(targetGameObject);
        }
    }
}