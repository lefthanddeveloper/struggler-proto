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

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       

    public Joystick Joystick => m_Joystick;

    public Action onInputAttack_A;
    public Action onInputAttack_B;
    public Action onInputAttack_C;
    public Action onInputAttack_D;


    void Start()
    {
        m_HandInput.onPinch_L += OnPinch_L;
        m_HandInput.onRelease_L += OnRelease_L;

        m_HandInput.onPinch_R += OnPinch_R;

        m_Joystick.Init(m_RadiusInner, m_RadiusOuter);
    }

    private void OnPinch_R(OVRHand hand, OVRHand.HandFinger finger)
    {
        switch (finger)
        {
            case OVRHand.HandFinger.Index:
                onInputAttack_A?.Invoke();
                break;


            case OVRHand.HandFinger.Middle:
                onInputAttack_B?.Invoke();
                break;

            case OVRHand.HandFinger.Ring:
                onInputAttack_C?.Invoke();
                break;

            case OVRHand.HandFinger.Pinky:
                onInputAttack_D?.Invoke();
                break;
        }
    }

    private void OnDestroy()
    {
        m_HandInput.onPinch_L -= OnPinch_L;
        m_HandInput.onRelease_L -= OnRelease_L;
    }

    private void OnPinch_L(OVRHand hand, OVRHand.HandFinger finger)
    {
        if(finger == OVRHand.HandFinger.Index)
        {
            OVRSkeleton skeleton = hand.GetComponent<OVRSkeleton>();

            if(skeleton.IsValidBone(OVRSkeleton.BoneId.Hand_IndexTip))
            {
                int indexFingerBoneID = (int)OVRSkeleton.BoneId.Hand_IndexTip;
            
                if(skeleton.Bones.Count > indexFingerBoneID)
                {
                    Transform indexTr = skeleton.Bones[indexFingerBoneID].Transform;
                    m_Joystick.Show(indexTr);
                }
            }
        }
    }

    private void OnRelease_L(OVRHand hand, OVRHand.HandFinger finger)
    {
        if(finger == OVRHand.HandFinger.Index)
        {
            m_Joystick.Hide();
        }
    }

    
}
