using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Struggler : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rb;

    private Animator animator;

    private const string Param_MoveSpeed = "MoveSpeed";

    [SerializeField] private float m_ForceMultiplier = 1.0f;

    private float lerpMag = 0f;
    private Vector3 lerpVelocity = Vector3.zero;

    void Start()
    {
        joystick = FindObjectOfType<JoystickController>().Joystick;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        StrugglerRetriever.onRetrieveCalled += OnRetrieveCalled;
    }

    private void OnRetrieveCalled()
    {

        Vector3 teleportPos = PlayerLocal.Instance.m_PalmTr_L.position + Vector3.up * 0.15f;

        Vector3 lookDir = PlayerLocal.Instance.m_MainCam.transform.position - teleportPos;
        lookDir.y = 0f;

        Quaternion teleportRot = Quaternion.LookRotation(lookDir, Vector3.up);

        Teleport(teleportPos, teleportRot);
    }

    private void OnDestroy()
    {
        StrugglerRetriever.onRetrieveCalled -= OnRetrieveCalled;
    }


    private void FixedUpdate()
    {
        float mag = joystick.GetMagnitude();

        if (joystick.IsShowing)
        {
            Vector3 dir = joystick.GetDirection();
            Vector3 target = dir * mag * m_ForceMultiplier;

            lerpVelocity = Vector3.Lerp(lerpVelocity, target, Time.fixedDeltaTime);

            if (mag > 0.2f)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.fixedDeltaTime);
            }

            //rb.AddForce(force);
        }
        else
        {
            lerpVelocity = Vector3.Lerp(lerpVelocity, Vector3.zero, Time.fixedDeltaTime);
        }

        lerpMag = Mathf.Lerp(lerpMag, mag, Time.fixedDeltaTime);

        rb.velocity = lerpVelocity;

        animator.SetFloat(Param_MoveSpeed, lerpMag);
    }

    public void Teleport(Vector3 pos, Quaternion rot)
    {
        rb.position = pos;
        rb.rotation = rot;
    }
   
}
