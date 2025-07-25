using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.G))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
