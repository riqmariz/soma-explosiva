using SharedData.Values;
using UnityEngine;
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
    public static float _InvulnerableTime = 0;
    
    //todo check if call the initialization on awake or on start
    private void Start()
    {
        _InvulnerableTime = invulnerabilityTimeAfterDamage;
        bossHp.Value = initHp;
        //todo init boss target value later
    }
    public bool TakeDamage(GameObject damager,int targetValue,int damageToApply)
    {
        var destroyTimer = 0f;
        if (canTakeDamage)
        {
            if (ValidHitByTargetValue(targetValue))
            {
                destroyTimer = 0.05f;
                Debug.Log("VALID HIT ON BOSS");
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
                        StartInvulnerability,
                        EndInvulnerability
                    );
                    return true;
                }
            }
            else
            {
                Debug.Log("Invalid hit on boss -> hitted with: "+targetValue+", should be hitted with: "+bossTargetValue.Value);
                onInvalidHit.Raise();
            }
            //temp destroy
            Destroy(damager.gameObject,destroyTimer);
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