using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance = null;

    public bool IsPassthrough = false;

    public event Action<bool> onIsPassthroughChanged;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
    }

    public void SetIsPassthrough(bool _isPassthrough)
    {
        if (IsPassthrough == _isPassthrough) return;

        IsPassthrough = _isPassthrough;
        onIsPassthroughChanged?.Invoke(IsPassthrough);
    }
    
}
