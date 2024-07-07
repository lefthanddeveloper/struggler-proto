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
    void Update()
    {
        
    }
}
