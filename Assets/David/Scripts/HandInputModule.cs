using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandInputModule : MonoBehaviour
{
    public OVRHand m_OvrHand_L;
    public OVRHand m_OvrHand_R;

    public Action<OVRHand, OVRHand.HandFinger> onPinch_L;
    public Action<OVRHand, OVRHand.HandFinger> onRelease_L;

    public Action<OVRHand, OVRHand.HandFinger> onPinch_R;
    public Action<OVRHand, OVRHand.HandFinger> onRelease_R;

    private Dictionary<OVRHand.HandFinger, bool> isPinchingDic_L = new Dictionary<OVRHand.HandFinger, bool>();
    private Dictionary<OVRHand.HandFinger, bool> isPinchingDic_R = new Dictionary<OVRHand.HandFinger, bool>();

    private OVRHand.HandFinger[] fingers_L;
    private OVRHand.HandFinger[] fingers_R;

    void Start()
    {
        isPinchingDic_L = new Dictionary<OVRHand.HandFinger, bool>()
            {
                { OVRHand.HandFinger.Thumb, false },
                { OVRHand.HandFinger.Index, false },
                { OVRHand.HandFinger.Middle, false },
                { OVRHand.HandFinger.Ring, false },
                { OVRHand.HandFinger.Pinky, false },
            };

        

        isPinchingDic_R = new Dictionary<OVRHand.HandFinger, bool>()
            {
                { OVRHand.HandFinger.Thumb, false },
                { OVRHand.HandFinger.Index, false },
                { OVRHand.HandFinger.Middle, false },
                { OVRHand.HandFinger.Ring, false },
                { OVRHand.HandFinger.Pinky, false },
            };


        fingers_L = isPinchingDic_L.Keys.ToArray();
        fingers_R = isPinchingDic_R.Keys.ToArray();
    }

    public bool GetIsPinching_L(OVRHand.HandFinger _finger) => isPinchingDic_L[_finger];
    public bool GetIsPinching_R(OVRHand.HandFinger _finger) => isPinchingDic_R[_finger];



    private void SetIsPinchingState_L(OVRHand.HandFinger _finger, bool _isPinching)
    {
        var cur = isPinchingDic_L[_finger];

        if (cur != _isPinching)
        {
            isPinchingDic_L[_finger] = _isPinching;

            if (_isPinching)
            {
                onPinch_L?.Invoke(m_OvrHand_L, _finger);
            }
            else
            {
                onRelease_L?.Invoke(m_OvrHand_L, _finger);
            }
        }
    }


    private void SetIsPinchingState_R(OVRHand.HandFinger _finger, bool _isPinching)
    {
        var cur = isPinchingDic_R[_finger];

        if(cur != _isPinching)
        {
            isPinchingDic_R[_finger] = _isPinching;

            if (_isPinching)
            {
                onPinch_R?.Invoke(m_OvrHand_R, _finger);
            }
            else
            {
                onRelease_R?.Invoke(m_OvrHand_R, _finger);
            }
        }
    }

    void Update()
    {
        if(m_OvrHand_L.IsTracked)
        {
            foreach(var finger in fingers_L)
            {
                SetIsPinchingState_L(finger, m_OvrHand_L.GetFingerIsPinching(finger));
            }            
        }
        else
        {
            foreach (var finger in fingers_L)
            {
                SetIsPinchingState_L(finger, false);
            }
        }

        if (m_OvrHand_R.IsTracked)
        {
            foreach (var finger in fingers_R)
            {
                SetIsPinchingState_R(finger, m_OvrHand_R.GetFingerIsPinching(finger));
            }
        }
        else
        {
            foreach (var finger in fingers_R)
            {
                SetIsPinchingState_R(finger, false);
            }
        }

    }
}
