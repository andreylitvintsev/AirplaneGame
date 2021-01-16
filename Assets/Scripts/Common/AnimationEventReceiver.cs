using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public class AnimationEventReceiver : MonoBehaviour
    {
        [SerializeField] private Receiver[] _receivers;

        private Dictionary<string, StringUnityEvent> _receiversMap = new Dictionary<string, StringUnityEvent>();

        private void Awake()
        {
            foreach (var receiver in _receivers)
            {
                var eventName = receiver.EventName;
                if (!_receiversMap.ContainsKey(eventName))
                {
                    _receiversMap.Add(eventName, receiver.Callbacks);
                }
                else
                {
                    Debug.LogError($"'{nameof(AnimationEventReceiver)}' doesn't contain multiple receivers" +
                                   $" with equals '{nameof(receiver.EventName)}'!");
                }
            }
        }

        // called from animation
        private void OnReceiveAnimationEvent(string eventName)
        {
            if (_receiversMap.TryGetValue(eventName, out var value))
            {
                value.Invoke(eventName);
            }
            else
            {
                Debug.LogError($"Doesn't exist receiver for '{eventName}'");
            }
        }
        
        [Serializable]
        public class Receiver
        {
            public string EventName;
            public StringUnityEvent Callbacks;
        }
    }
}