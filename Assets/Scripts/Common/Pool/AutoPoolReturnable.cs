using UnityEngine;

namespace Common.Pool
{
    public class AutoPoolReturnable : MonoBehaviour
    {
        private float _returnAfterSec = 0f;
        private float _spendTime = 0f;
        private GameObjectsPool _pool = null;

        public static void Attach(GameObjectsPool pool, GameObject gameObject, float returnAfterSec)
        {
            var autoPoolReturnable = gameObject.AddComponent<AutoPoolReturnable>();
            autoPoolReturnable._returnAfterSec = returnAfterSec;
            autoPoolReturnable._pool = pool;
        }
        
        private void Update()
        {
            _pool.LogIfNull(nameof(_pool));
            
            if (_spendTime >= _returnAfterSec)
            {
                _spendTime = 0f;
                _pool.Return(gameObject);
            }

            _spendTime += Time.deltaTime;
        }
    }
}