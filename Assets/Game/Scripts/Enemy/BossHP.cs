using Ball;
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
    [SerializeField] 
    private Event onKillBoss;
    [SerializeField] 
    private Rigidbody rigidbody;
    [SerializeField] 
    private float knockbackForce;
    [SerializeField] 
    private ForceMode forceMode;

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
        if (canTakeDamage)
        {
            if (ValidHitByTargetValue(targetValue))
            {
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
                    var ballDir = damager.GetComponentInParent<MoveToDirection>();
                    Vector3 direction = Vector3.zero;
                    if (ballDir)
                    {
                        direction = ballDir.Direction;
                    }
                    else
                    {
                        Debug.LogWarning("Not found MoveToDirection on damager object");
                        direction = transform.position - damager.transform.position;
                    }
                    rigidbody.AddForce(knockbackForce * direction.normalized,forceMode);
                    return true;
                }
            }
            else
            {
                Debug.Log("Invalid hit on boss -> hitted with: "+targetValue+", should be hitted with: "+bossTargetValue.Value);
                onInvalidHit.Raise();
            }
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
        onKillBoss.Raise();
        Destroy(gameObject); // temp destroy
    }
}