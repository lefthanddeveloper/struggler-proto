using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    private CameraClearFlags camDefaultFlag;
    private Color camDefaultBackgroundColor;


    private Camera mainCam;

    [SerializeField] private GameObject[] m_DisableOnPassthrough;
    void Start()
    {
        mainCam = PlayerLocal.Instance.m_MainCam;
        camDefaultBackgroundColor = mainCam.backgroundColor;
        camDefaultFlag = mainCam.clearFlags;

        BackgroundManager.Instance.onIsPassthroughChanged += OnIsPassthroughChanged;
    }

    private void OnIsPassthroughChanged(bool _isPassthrough)
    {
        PlayerLocal.Instance.m_OvrCamRig.GetComponent<OVRManager>().isInsightPassthroughEnabled = _isPassthrough;

        if (_isPassthrough)
        {
            mainCam.clearFlags = CameraClearFlags.SolidColor;
            mainCam.backgroundColor = Color.clear;
        }
        else
        {
            mainCam.clearFlags = camDefaultFlag;
            mainCam.backgroundColor = camDefaultBackgroundColor;
        }

        EnableGameObjs(!_isPassthrough);
    }

    private void EnableGameObjs(bool _enable)
    {
        foreach(var obj in m_DisableOnPassthrough)
        {
            obj.SetActive(_enable);
        }
    }
}
