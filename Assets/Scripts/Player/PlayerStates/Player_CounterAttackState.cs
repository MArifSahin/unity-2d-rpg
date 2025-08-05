using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat playerCombat;
    private bool counteredSomebody;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        playerCombat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = playerCombat.GetCounterRecoveryDuration();
        counteredSomebody = playerCombat.CounterAttackPerformed();

        anim.SetBool("counterAttackPerformed", counteredSomebody);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocityY);

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (stateTimer <= 0 && !counteredSomebody)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
