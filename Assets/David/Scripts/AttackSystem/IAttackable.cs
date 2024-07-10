using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo
{
    Vector3 hitPoint;
}

public interface IAttackable 
{
    void OnAttack(AttackInfo _attackInfo);
}
