using Common.FSM;
using Data;
using UnityEngine;


public class HittedEnemyAction : FSMAction
{
    private EnemyController _enemyController;
    private Animator _animator;
    private Timer timer;
    private LayerMask _oldLayerMask;
    public HittedEnemyAction(FSMState owner, EnemyController enemyController) : base(owner)
    {
        this._enemyController = enemyController;
        this._animator = enemyController.GetComponentInChildren<Animator>();
    }

    public override void OnEnter()
    {
        this._enemyController.hitted = false;
        this._animator.SetTrigger("Hit");
        _oldLayerMask = this._enemyController.gameObject.layer;
        if (_oldLayerMask == LayerMask.NameToLayer("Boss"))
        {
            this._enemyController.gameObject.layer = LayerMask.NameToLayer("EtherealBoss");
        }
        else
        {
            this._enemyController.gameObject.layer = LayerMask.NameToLayer("Ethereal");
        }
        var spriteRenderer = this._enemyController.GetComponentInChildren<SpriteRenderer>();
        
        timer = new Timer();
    }

    public override void OnUpdate()
    {
        if (timer.ElapsedTime() >= _enemyController.HitTime)
        {
            TryChangeStateTo(EnemyFSM.IdleAfterHit);
            timer.Reset();
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        this._enemyController.gameObject.layer = _oldLayerMask;
    }
}