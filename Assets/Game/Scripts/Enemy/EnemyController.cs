using JetBrains.Annotations;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Base Enemy Attributes")]
    [SerializeField] 
    protected string enemyName;

    [SerializeField] 
    protected int hp = 100;

    [SerializeField] 
    protected int collisionDamage=20;

    [SerializeField] 
    protected float knockbackForce = 5f;
    public float KnockbackForce => knockbackForce;

    [SerializeField]
    protected float idleAfterHit = 0.25f;
    public float IdleAfterHit => idleAfterHit;

    [SerializeField] 
    protected float hitTime = 0.25f;
    
    public float HitTime => hitTime;

    [HideInInspector] 
    public bool hitted = false;
    public int Hp => hp;
    
    protected EnemyFSM enemyFSM;
    public EnemyFSM EnemyFSM => enemyFSM;

    public string IdleStateID => "IdleState";
    public string WalkStateID => "WalkState";

    public string FollowStateID => "FollowState";
    public string AttackStateID => "AttackState";

    public string DefendStateID => "DefendState";
    
    [CanBeNull] public Spawner Spawner { get; set; }

    private Animator _animator;

    private void Awake()
    {
        InitializeFSM(enemyName);
        //_animator = GetComponentInChildren<Animator>();
    }

    protected abstract void InitializeFSM(string name);

    protected virtual void Update()
    {
        enemyFSM.Update();
    }

    protected virtual void FixedUpdate()
    {
        enemyFSM.FixedUpdate();
    }

    public virtual void TakeDamage(GameObject damager, int damage)
    {
        hp -= damage;
        hitted = true;
        /*string hitParam = "Hit";
        if (_animator && _animator.AnimatorHasParameter(hitParam))
        {
            _animator.SetTrigger(hitParam);      
        }*/
    }

    public virtual void Melt()
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
            spriteRenderer.color = Color.red; //Temporary, change to apply melt effect
    }

    public virtual void Root()
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer)
            spriteRenderer.color = Color.green; //Temporary, change to apply root effect
    }

    public virtual void Freeze()
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(spriteRenderer)
            spriteRenderer.color = Color.blue; //Temporary, change to apply freeze effect
    }
    
    public virtual void Knockback(Rigidbody2D rb,float knockbackForce,Vector2 direction)
    {
        rb.AddForce(direction.normalized * knockbackForce,ForceMode2D.Impulse);
        // Debug.Log("knockback: vector"+direction.normalized);
        
        if (direction.x > 0)
        {
            if (rb.transform.localScale.x > 0)
            {
                rb.transform.localScale = new Vector3(
                    -rb.transform.localScale.x,
                    rb.transform.localScale.y,
                    rb.transform.localScale.z
                    );
            }
        }else if (direction.x < 0)
        {
            if (rb.transform.localScale.x < 0)
            {
                 rb.transform.localScale = new Vector3(
                    -rb.transform.localScale.x,
                    rb.transform.localScale.y,
                    rb.transform.localScale.z
                    );
            }
        } 
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && hp > 0)
        {
            var dmg = other.collider.GetComponent<IDamageable>();
            dmg?.TakeDamage(gameObject,collisionDamage);
        }
    }

}