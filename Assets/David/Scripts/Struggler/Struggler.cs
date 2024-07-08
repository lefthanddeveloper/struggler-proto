using Oculus.Interaction;
using Oculus.Interaction.PoseDetection.Debug.Editor.Generated;
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
    private const string Param_AttackA = "AttackA";
    
    
    //attack
    private float attackCoolTime = 1.0f;
    private bool isAttacking = false;
    private Coroutine attackCor = null;
    private Coroutine smoothLayerCor = null;

    [SerializeField] private float m_ForceMultiplier = 1.0f;

    private float lerpMag = 0f;
    private Vector3 lerpVelocity = Vector3.zero;

    void Start()
    {
        var joystickController = FindObjectOfType<JoystickController>();
        joystick = joystickController.Joystick;

        joystickController.onInputAttack_A += OnInputAttack_A;


        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        StrugglerRetriever.onRetrieveCalled += OnRetrieveCalled;
    }

    private void OnInputAttack_A()
    {
        if (isAttacking) return;

        attackCor = StartCoroutine(AttackCor());
    }

    private IEnumerator AttackCor()
    {
        isAttacking = true;
        
       SetUpperbodyLayerWeight(1.0f);
        animator.SetTrigger(Param_AttackA);

        float timePassed = 0f;
        while(timePassed < attackCoolTime) 
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

       SetUpperbodyLayerWeight(0f, true);
        isAttacking = false;
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

    private void SetUpperbodyLayerWeight(float _weight, bool smooth = false)
    {
        if (smoothLayerCor != null)
        {
            StopCoroutine(smoothLayerCor);
        }

        if (!smooth)
        {
            animator.SetLayerWeight(1, _weight);
        }
        else
        {
            float curWeight = animator.GetLayerWeight(1);
            smoothLayerCor = StartCoroutine(LayerSmooth(curWeight, _weight));
        }
    }

    private IEnumerator LayerSmooth(float curWeight, float targetWeight)
    {
        float timePassed = 0f;
        float ratio = 0f;
        float smoothTime = 0.5f;

        while(ratio < 1f)
        {
            timePassed += Time.deltaTime;
            ratio = timePassed / smoothTime;
            float weight = Mathf.Lerp(curWeight, targetWeight, ratio);
            SetUpperbodyLayerWeight(weight, false);
            yield return null;
        }
        SetUpperbodyLayerWeight(targetWeight, false);
        smoothLayerCor = null;
    }
   
}
