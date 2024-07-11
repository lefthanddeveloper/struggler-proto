using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrugglerAttackController : MonoBehaviour, IAnimEventReceiver
{
    [SerializeField] private BodyRef m_StrugglerBody;
    [SerializeField] private ParticleSystem m_SpecialAttackParticle;
    public void OnReceiveAnimationEvent(AnimEventType eventType)
    {
        switch(eventType)
        {
            case AnimEventType.OnAttackA:
                PunchAttack();
                break;

            case AnimEventType.OnAttackB:
                StrongPunchAttack();
                break;

            case AnimEventType.OnAttackD:
                SpecialAttack();
                break;
        }
    }

    private void PunchAttack()
    {
        var handR_Pos = m_StrugglerBody.m_HandR.position;

        float punchRadius = 0.1f;
        
        Collider[] colls = Physics.OverlapSphere(handR_Pos, punchRadius);
        
        foreach(var coll in colls)
        {
            if(coll.TryGetComponentInParent(out IAttackable attackable))
            {
                AttackInfo info = new AttackInfo()
                {
                    hitPoint = handR_Pos,
                    damage = 1,
                };

                attackable.OnAttack(info);
            }
        }
    }

    private void StrongPunchAttack()
    {
        var handR_Pos = m_StrugglerBody.m_HandR.position;

        float punchRadius = 0.125f;

        Collider[] colls = Physics.OverlapSphere(handR_Pos, punchRadius);

        foreach (var coll in colls)
        {
            if (coll.TryGetComponentInParent(out IAttackable attackable))
            {
                AttackInfo info = new AttackInfo()
                {
                    hitPoint = handR_Pos,
                    damage = 2,
                };

                attackable.OnAttack(info);
            }
        }
    }

    private void SpecialAttack()
    {
        Vector3 btwHand = (m_StrugglerBody.m_HandL.position - m_StrugglerBody.m_HandR.position) * 0.5f + m_StrugglerBody.m_HandR.position;

        if(m_SpecialAttackParticle.transform.parent != null)
        {
            m_SpecialAttackParticle.transform.parent = null;
        }
        m_SpecialAttackParticle.transform.position = btwHand;
        m_SpecialAttackParticle.Play();

        float range = 0.25f;
        Collider[] colls = Physics.OverlapSphere(btwHand, range);

        foreach (var coll in colls)
        {
            if (coll.TryGetComponentInParent(out IAttackable attackable))
            {
                AttackInfo info = new AttackInfo()
                {
                    hitPoint =  btwHand,
                    damage = 5,
                };

                attackable.OnAttack(info);
            }
        }
    }
}
