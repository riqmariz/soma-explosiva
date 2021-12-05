using UnityEngine;

public interface ITakeDamage
{
    bool TakeDamage(GameObject damager,int valueNumber,int damageToApply);
}