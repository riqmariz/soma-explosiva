using Common.FSM;
using Data;
using UnityEngine;

public class IdleAfterHittedAction : BaseEnemyAction
{
    private EnemyController _enemyController;
    private Animator _animator;
    private Timer timer;
    
    public IdleAfterHittedAction(FSMState owner, EnemyController enemyController) : base(owner,enemyController)
    {
        this._enemyController = enemyController;
        this._animator = enemyController.GetComponentInChildren<Animator>();
    }

    public override void OnEnter()
    {
        this._animator.SetBool("HitExit",true);
        timer = new Timer();
    }

    public override void OnUpdate()
    {
        if (timer.ElapsedTime() >= _enemyController.IdleAfterHit)
        {
            TryChangeStateTo(_enemyController.WalkStateID);
            timer.Reset();
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        this._animator.SetBool("HitExit",false);
    }
}