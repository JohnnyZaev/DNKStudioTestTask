using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyEventBus
{
    public class EventBus
    {
        private Dictionary<Type, List<CallbackWithPriority>> _signalCallbacks = new Dictionary<Type, List<CallbackWithPriority>>();

        public void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            Type key = typeof(T);
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority>() { new (priority, callback) });
            }
            
            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        public void Invoke<T>(T signal)
        {
            Type key = typeof(T);
            if (_signalCallbacks.TryGetValue(key, out var signalCallback))
            {
                foreach (var obj in signalCallback)
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            Type key = typeof(T);
            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                }
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }
    }
}
