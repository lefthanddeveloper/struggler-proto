using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputModule : MonoBehaviour
{
    public OVRHand m_OvrHand_L;
    public OVRHand m_OvrHand_R;

    public Action<OVRHand> onPinch_L;
    public Action<OVRHand> onRelease_L;

    private bool _isPinching_L;
    public bool IsPinching_L
    {
        get
        {
            return _isPinching_L;
        }
        private set
        {
            if(_isPinching_L != value)
            {
                _isPinching_L = value;
                if(_isPinching_L)
                {
                    onPinch_L?.Invoke(m_OvrHand_L);
                }
                else
                {
                    onRelease_L?.Invoke(m_OvrHand_L);
                }
            }
        }
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if(m_OvrHand_L.IsTracked)
        {
            IsPinching_L = m_OvrHand_L.GetFingerIsPinching(OVRHand.HandFinger.Index);
        }
        else
        {
            IsPinching_L = false;
        }
    }
}
