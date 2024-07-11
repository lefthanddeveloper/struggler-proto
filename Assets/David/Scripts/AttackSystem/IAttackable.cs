using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    public Vector3 hitPoint;
    public float damage;
}

public interface IAttackable 
{
    void OnAttack(AttackInfo _attackInfo);
}
