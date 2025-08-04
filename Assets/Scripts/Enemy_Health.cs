using UnityEngine;

public class Enemy_Health : Entity_Health
{
    // calling in Awake method is better for performance
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if ( damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }

        base.TakeDamage(damage, damageDealer);
    }
}
