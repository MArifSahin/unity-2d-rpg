using UnityEditor;
using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //make object go up, increase y velocity
        player.SetVelocity(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //if y velocity goes down, character is falling, transfer to fall state
        //We need to be sure we are not in the jump attack state when we transfer to the fall state
        if (rb.linearVelocityY < 0 && stateMachine.currentState != player.jumpAttackState)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
