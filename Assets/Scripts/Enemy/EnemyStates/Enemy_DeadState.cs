using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private Collider2D collider2D;

    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        collider2D = enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        anim.enabled = false;
        collider2D.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 15);

        stateMachine.switchOffStateMachine();
    }
}
