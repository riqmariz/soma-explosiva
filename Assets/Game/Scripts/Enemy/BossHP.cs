using SharedData.Values;
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
                        () => setCanTakeDamage(false),
                        () => setCanTakeDamage(true)
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
        }

        return false;
    }

    private bool ValidHitByTargetValue(int targetValue)
    {
        return bossTargetValue.Value == targetValue;
    }

    private void setCanTakeDamage(bool value)
    {
        canTakeDamage = value;
    }

    public void KillBoss()
    {
        //todo more complex behaviour on kill
        Destroy(gameObject); // temp destroy
    }
}