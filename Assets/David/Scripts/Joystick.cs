using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Transform m_InnerTr;
    [SerializeField] private Transform m_OuterTr;

    private float maxDistFromCenter;
    private Vector3 posOnShow;
    private Transform followTr;

    public bool IsShowing { get; private set; }

    public void Init(float scaleInner, float scaleOuter)
    {
        m_InnerTr.localScale = Vector3.one * scaleInner;
        m_OuterTr.localScale = Vector3.one * scaleOuter;

        maxDistFromCenter = (scaleOuter - scaleInner) * 0.5f;

        Hide();
    }

    private void Update()
    {
        if (IsShowing)
        {
            Vector3 followPos = followTr.position;
            float dist = Vector3.Distance(followPos, posOnShow);
            if(dist < maxDistFromCenter) 
            {
                m_InnerTr.position = followPos;
            }
            else
            {
                Vector3 dir = (followPos - posOnShow).normalized;
                m_InnerTr.position = posOnShow + dir * maxDistFromCenter;
            }
        }
    }

    public void Show(Transform _followTr)
    {
        followTr = _followTr;
        posOnShow = followTr.position;
        this.transform.position = _followTr.position;

        this.gameObject.SetActive(true);
        IsShowing = true;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);   
    }
}
