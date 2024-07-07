using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private HandInputModule m_HandInput;

    [Header("[ Joystick Setting ]")]
    [SerializeField] private Joystick m_Joystick;
    [SerializeField] private float m_RadiusOuter = 0.3f;
    [SerializeField] private float m_RadiusInner = 0.05f;
    
    void Start()
    {
        m_HandInput.onPinch_L += OnPinch_L;
        m_HandInput.onRelease_L += OnRelease_L;

        m_Joystick.Init(m_RadiusInner, m_RadiusOuter);
    }

    private void OnDestroy()
    {
        m_HandInput.onPinch_L -= OnPinch_L;
        m_HandInput.onRelease_L -= OnRelease_L;
    }

    private void OnPinch_L(OVRHand hand)
    {
        OVRSkeleton skeleton = hand.GetComponent<OVRSkeleton>();

        if(skeleton.IsValidBone(OVRSkeleton.BoneId.Hand_IndexTip))
        {
            int indexFingerBoneID = (int)OVRSkeleton.BoneId.Hand_IndexTip;
            Transform indexTr = hand.GetComponent<OVRSkeleton>().Bones[indexFingerBoneID].Transform;
            m_Joystick.Show(indexTr);
        }
    }

    private void OnRelease_L(OVRHand hand)
    {
        m_Joystick.Hide();
    }

    
}
