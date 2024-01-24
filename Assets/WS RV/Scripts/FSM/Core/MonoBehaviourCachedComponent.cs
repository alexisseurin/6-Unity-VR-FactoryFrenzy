using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows caching components to avoid look for it several times per frame
/// </summary>
public class MonoBehaviourCachedComponent : MonoBehaviour
{
    private Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();

    public new T GetComponent<T>() where T : Component
    {
        if (_cachedComponents.ContainsKey(typeof(T)))
            return _cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if (component != null)
        {
            _cachedComponents.Add(typeof(T), component);
        }
        return component;
    }

    public void ClearCachedComponent()
    {
        _cachedComponents.Clear();
    }

}
