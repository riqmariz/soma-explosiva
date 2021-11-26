
using Common.FSM;
using UnityEngine;

public class BaseEnemyAction : FSMAction
{
    protected EnemyController enemyController;

    public EnemyController EnemyController => enemyController;

    protected GameObject gameObject;
    protected Transform transform;
    
    public BaseEnemyAction(FSMState owner, EnemyController enemyController) : base(owner)
    {
        this.enemyController = enemyController;
        this.gameObject = enemyController.gameObject;
        this.transform = gameObject.transform;
    }

    public virtual FSMAction Initialize(FSMState owner, EnemyController enemyController)
    {
        this.enemyController = enemyController;
        this.gameObject = enemyController.gameObject;
        this.transform = gameObject.transform;
        SetOwner(owner);

        return this;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        if (enemyController.Hp <= 0)
        {
            TryChangeStateTo(EnemyFSM.DeadStateID);
            return;
        }else if (enemyController.hitted)
        {
            TryChangeStateTo(EnemyFSM.HittedStateID);
            return;
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}