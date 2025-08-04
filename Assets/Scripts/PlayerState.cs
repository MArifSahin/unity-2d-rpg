using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;


    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    
        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        stateMachine.ChangeState(player.dashState);
    }

    private bool CanDash()
    {
        if (player.wallDetected ||
            stateMachine.currentState == player.dashState)
            return false;

        return true;
    }
    
}
