using Common.FSM;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
//using Utility;

public class DeadEnemyAction : FSMAction
{
    private EnemyController _enemyController;
    private Collider2D _boxCollider2D;
    [CanBeNull] private Collider2D _headCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
        [CanBeNull] private Spawner _spawner;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public DeadEnemyAction(FSMState owner, EnemyController enemyController) : base(owner)
    {
        _enemyController = enemyController;
        //_headCollider2D = enemyController.GetComponentInChildren<HittableEnemyHead>()?.GetComponent<Collider2D>();
        _boxCollider2D = enemyController.GetComponent<Collider2D>();
        _rigidbody2D = enemyController.GetComponent<Rigidbody2D>();
        
        _spriteRenderer = enemyController.GetComponentInChildren<SpriteRenderer>();
        _transform = enemyController.transform;
        _spawner = enemyController.Spawner;
        _animator = enemyController.GetComponentInChildren<Animator>();
    }

    public override void OnEnter()
    {
        _animator.SetTrigger("isDead");
        AudioManager.GetInstance().PlayAudio("InimigoMorto");
        if (_headCollider2D != null)
        {
            _headCollider2D.enabled = false;
        }
      //  _spriteRenderer.WhiteFlash(0.2f);
        _enemyController.gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(_spriteRenderer.DOFade(0f,2f).OnComplete( 
                () => Object.Destroy(_enemyController.gameObject)
            )
        );
        sequence.Play();
        _spawner?.KillEnemy();
        //MasterObjectPooler.Instance.GetObject("Explosion",this._transform.position);
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnExit()
    {
    }
}