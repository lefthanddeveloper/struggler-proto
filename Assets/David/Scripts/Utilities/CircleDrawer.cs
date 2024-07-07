using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    public enum CircleAxis { xy, xz, yz}

    [SerializeField] private CircleAxis m_CircleAxis;
    [SerializeField] private LineRenderer m_LineRend;

    [SerializeField] private int m_Segments = 360;
    [SerializeField] private float m_Radius = 0.1f;
    [SerializeField] private float m_LineWidth = 0.01f;


    [Button()]
    public void DrawCircle()
    {
        m_LineRend.positionCount = m_Segments + 1;
        m_LineRend.useWorldSpace = false;
        m_LineRend.startWidth = m_LineWidth;
        m_LineRend.endWidth = m_LineWidth;


        float deltaTheta = (2f * Mathf.PI) / m_Segments;
        float theta = 0f;

        for (int i = 0; i <= m_Segments; i++)
        {
            float x = m_Radius * Mathf.Cos(theta);
            float y = m_Radius * Mathf.Sin(theta);

            if(m_CircleAxis == CircleAxis.xy)
            {
                m_LineRend.SetPosition(i, new Vector3(x, y, 0f));
            }
            else if(m_CircleAxis == CircleAxis.xz)
            {
                m_LineRend.SetPosition(i, new Vector3(x, 0, y));
            }
            else if(m_CircleAxis == CircleAxis.yz) 
            {
                m_LineRend.SetPosition(i, new Vector3(0, x, y));
            }

            theta += deltaTheta;
        }
    }
}
