using SharedData.Values;
using UnityEngine;
using Utility;

public class BossHP : MonoBehaviour, ITakeDamage
{
    [SerializeField] 
    private int initHp = 3;
    [SerializeField] 
    private IntValue bossHp;
    [SerializeField] 
    private float invulnerabilityTimeAfterDamage = 3f;
    private bool canTakeDamage = true;
    private void Start()
    {
        bossHp.Value = initHp;
    }

    //todo call invulnerability method on take damage or on value change in int value
    public bool TakeDamage(GameObject damager,int damage = 1)
    {
        if (canTakeDamage)
        {
            var hp = bossHp.Value;
            bossHp.Value = Mathf.Max(hp - damage, 0);
            if (bossHp.Value <= 0)
            {
                KillBoss();
            }

            if (hp != bossHp.Value)
            {
                canTakeDamage = false;
                Timers.CreateClock(
                    gameObject, 
                    invulnerabilityTimeAfterDamage,
                    () => setCanTakeDamage(false),
                    () => setCanTakeDamage(true)
                    );
                //put this line on an animation script later
                gameObject.GetComponent<SpriteRenderer>().AlphaFlash(invulnerabilityTimeAfterDamage,0,0.5f);
            }
            return true;
        }

        return false;
    }

    private void setCanTakeDamage(bool value)
    {
        canTakeDamage = value;
    }

    public void KillBoss()
    {
        //todo
        Destroy(gameObject); // temp destroy
    }
}