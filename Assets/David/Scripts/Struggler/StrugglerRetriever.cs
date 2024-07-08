using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrugglerRetriever : MonoBehaviour
{
    [SerializeField] private SelectorUnityEventWrapper m_EventWrapper;

    public static event Action onRetrieveCalled;
    void Start()
    {
        m_EventWrapper.WhenSelected.AddListener(WhenSelected);
    }

    private void WhenSelected()
    {
        onRetrieveCalled?.Invoke();
    }
}
