using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocal : MonoBehaviour
{
    public static PlayerLocal Instance = null;

    public OVRCameraRig m_OvrCamRig;
    public Camera m_MainCam;
    public OVRHand m_OvrHand_L;
    public OVRHand m_OvrHand_R;

    public SyntheticHand m_SyntheticHand_L;
    public SyntheticHand m_SyntheticHand_R;

    public Transform m_PalmTr_L;
    public Transform m_PalmTr_R;

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
    
    
}
