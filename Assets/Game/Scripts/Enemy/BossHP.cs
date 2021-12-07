﻿using SharedData.Values;
using UnityEngine;
using Utility;
using Event = SharedData.Events.Event;

public class BossHP : MonoBehaviour, ITakeDamage
{
    [SerializeField] 
    private int initHp = 3;
    [SerializeField] 
    private IntValue bossHp;
    [SerializeField] 
    private IntValue bossTargetValue;
    [SerializeField] 
    private float invulnerabilityTimeAfterDamage = 3f;
    [SerializeField] 
    private Event onInvalidHit;
    [SerializeField] 
    private Event onValidHit;
    
    private bool canTakeDamage = true;
    private LayerMask _layerMask;
    
    //todo check if call the initialization on awake or on start
    private void Start()
    {
        bossHp.Value = initHp;
        //todo init boss target value later
    }
    public bool TakeDamage(GameObject damager,int targetValue,int damageToApply)
    {
        if (canTakeDamage)
        {
            if (ValidHitByTargetValue(targetValue))
            {
                var hp = bossHp.Value;
                bossHp.Value = Mathf.Max(hp - damageToApply, 0);

                if (bossHp.Value <= 0)
                {
                    KillBoss();
                    return true;
                }
                
                if (hp != bossHp.Value)
                {
                    onValidHit.Raise();
                    Timers.CreateClock(
                        gameObject,
                        invulnerabilityTimeAfterDamage,
                        () => StartInvulnerability(),
                        () => EndInvulnerability()
                        );
                    //put this line on an animation script later
                    gameObject.GetComponent<SpriteRenderer>().AlphaFlash(invulnerabilityTimeAfterDamage, 0, 0.5f);
                    return true;
                }
            }
            else
            {
                onInvalidHit.Raise();
            }
            //temp destroy
            Destroy(damager.gameObject);
        }

        return false;
    }

    private bool ValidHitByTargetValue(int targetValue)
    {
        return bossTargetValue.Value == targetValue;
    }

    private void StartInvulnerability()
    {
        canTakeDamage = false;
        _layerMask = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Invulnerable");
    }
    private void EndInvulnerability()
    {
        canTakeDamage = true;
        gameObject.layer = _layerMask;
    }

    public void KillBoss()
    {
        //todo more complex behaviour on kill
        Destroy(gameObject); // temp destroy
    }
}