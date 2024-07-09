using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughToggle : MonoBehaviour
{
    [SerializeField] private InteractableUnityEventWrapper m_EventWrapper;
    void Start()
    {
        m_EventWrapper.WhenSelect.AddListener(OnToggle);
    }

    private void OnToggle()
    {
        bool current = BackgroundManager.Instance.IsPassthrough;
        BackgroundManager.Instance.SetIsPassthrough(!current);
    }
}
