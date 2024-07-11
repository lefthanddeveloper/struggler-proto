using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions 
{
    public static bool TryGetComponentInParent<T>(this Component component, out T result) 
    {
        var _result = component.GetComponentInParent<T>();

        if(_result == null)
        {
            result = default(T);
            return false;
        }
        else
        {
            result = _result;
            return true;
        }
    }
}
