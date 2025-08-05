using UnityEngine;

public class Enemy_Health : Entity_Health
{

    // calling in Awake method is better for performance
    private Enemy enemy => GetComponent<Enemy>();

    public override bool TakeDamage(float damage, Transform damageDealer)
    {
        bool wasHit = base.TakeDamage(damage, damageDealer);

        if (!wasHit) return false;

        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }
        
        return true;
    }
}
