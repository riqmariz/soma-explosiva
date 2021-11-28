using Common.FSM;

public class EnemyFSM : FSM
{
    private EnemyController _enemyController;

    private FSMState deadState;

    private FSMState hittedState;

    private FSMState idleAfterHitState;

    public static string DeadStateID => "DeadState";

    public static string HittedStateID => "HittedState";

    public static string IdleAfterHit => "IdleAfterHit";
    
    public EnemyFSM(string name, EnemyController enemyController) : base(name)
    {
        this._enemyController = enemyController;
        var deadState = AddState(DeadStateID);
        var deadAction = new DeadEnemyAction(deadState,_enemyController);
        deadState.AddAction(deadAction);
        this.deadState = deadState;

        var hittedState = AddState(HittedStateID);
        var hittedAction = new HittedEnemyAction(hittedState, _enemyController);
        hittedState.AddAction(hittedAction);
        this.hittedState = hittedState;
        
        var idleAfterHit = AddState(IdleAfterHit);
        var idleAfterHitAction = new IdleAfterHittedAction(idleAfterHit, _enemyController);
        idleAfterHit.AddAction(idleAfterHitAction);
        this.idleAfterHitState = idleAfterHit;
        
        hittedState.AddTransition(this.idleAfterHitState.Name,this.idleAfterHitState);
    }
    
    
    public override FSMState AddState(string name)
    {
        var state = base.AddState(name);

        if (deadState != null)
        {
            state.AddTransition(deadState.Name, deadState);
        }

        if (hittedState != null)
        {
            state.AddTransition(hittedState.Name,hittedState);
        }

        if (idleAfterHitState != null)
        {
            idleAfterHitState.AddTransition(state.Name,state);
        }

        return state;
    }
    
    
    
}