using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Transform m_InnerTr;
    [SerializeField] private Transform m_OuterTr;

    private float maxDistFromCenter;
    private Vector3 posOnShow;
    private Transform followTr;

    public bool IsShowing { get; private set; }

    [SerializeField] private LineRenderer m_LineRend;

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

            m_LineRend.SetPosition(1, m_InnerTr.position);
        }
    }

    public void Show(Transform _followTr)
    {
        followTr = _followTr;
        posOnShow = followTr.position;
        this.transform.position = _followTr.position;

        //line
        m_LineRend.positionCount = 2;
        m_LineRend.SetPosition(0, posOnShow);
        m_LineRend.SetPosition(1, posOnShow);
        
        this.gameObject.SetActive(true);
        IsShowing = true;
    }

    public void Hide()
    {
        m_LineRend.positionCount = 0;
        this.gameObject.SetActive(false);
        IsShowing = false;
    }

    public Vector3 GetDirection()
    {
        if (!IsShowing) return Vector3.zero;

        return (m_InnerTr.position - posOnShow).normalized;
    }

    public float GetMagnitude()
    {
        if (!IsShowing) return 0f;

        float mag = (m_InnerTr.position - posOnShow).magnitude;

        return Mathf.Clamp01(mag / maxDistFromCenter);
    }
}
