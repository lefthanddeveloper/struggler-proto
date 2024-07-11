using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour, IAttackable
{
    [SerializeField] private HighlightEffect m_HighlightFx;

    public void OnAttack(AttackInfo _attackInfo)
    {
        m_HighlightFx.HitFX(_attackInfo.hitPoint);                                                     
    }
} 
