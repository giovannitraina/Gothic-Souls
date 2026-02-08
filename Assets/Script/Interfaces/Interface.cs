using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    void TakeDamage(T dmg);
}

public interface IKnockable<T>
{
    void GetKnocked(T variable);
}
