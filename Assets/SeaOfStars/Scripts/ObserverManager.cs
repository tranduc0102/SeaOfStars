using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Observer
{
    public static class ObserverManager<T> where T : Enum
    {
        private static readonly Dictionary<T, Action<object>> _events = new();

        /// <summary>
        /// Đăng ký lắng nghe sự kiện
        /// </summary>
        public static void RegisterEvent(T eventID, Action<object> callback)
        {
            if (callback == null)
            {
                Debug.LogWarning($"[Observer] Callback for event {eventID} is NULL.");
                return;
            }

            if (!_events.TryAdd(eventID, callback))
            {
                _events[eventID] += callback;
            }
        }

        /// <summary>
        /// Hủy đăng ký lắng nghe sự kiện
        /// </summary>
        public static void RemoveEvent(T eventID, Action<object> callback)
        {
            if (_events.ContainsKey(eventID))
            {
                _events[eventID] -= callback;
                if (_events[eventID] == null)
                {
                    _events.Remove(eventID);
                }
            }
            else
            {
                Debug.LogWarning($"[Observer] Event '{eventID}' not found in ObserverManager<{typeof(T).Name}>.");
            }
        }

        /// <summary>
        /// Xóa tất cả các listener đã đăng ký
        /// </summary>
        public static void RemoveAllEvents()
        {
            _events.Clear();
        }

        /// <summary>
        /// Gửi sự kiện
        /// </summary>
        public static void PostEvent(T eventID, object param = null)
        {
            if (!_events.TryGetValue(eventID, out var callback))
            {
                Debug.LogWarning($"[Observer] Event '{eventID}' has no listeners.");
                return;
            }

            if (callback == null)
            {
                Debug.LogWarning($"[Observer] Callback for event '{eventID}' is NULL. Removing event.");
                _events.Remove(eventID);
                return;
            }

            callback.Invoke(param);
        }
    }
}
