using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimEventType
{
    None = 0,
    OnAttackA = 100,
    OnAttackB = 200,
    OnAttackC = 300,
    OnAttackD = 400,
}                       

public class AnimationEventSender : MonoBehaviour
{
    private IAnimEventReceiver receiver;

    private void Start()
    {
        receiver = GetComponentInParent<IAnimEventReceiver>();
    }

    public void OnAttackEvent(AnimEventType eventType)
    {
        if (receiver == null) return;

        receiver.OnReceiveAnimationEvent(eventType);
    }
}
