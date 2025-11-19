using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1;

    protected Collider2D[] EnemiesInRange(Transform t, float radius) 
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }


    protected virtual void OnDrawGizmos() 
    {
        if (targetCheck == null)
            targetCheck = transform;

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);   
    }

    protected void DamageEnemiesInRange(Transform target, float radius) 
    {
        foreach (var enemy in EnemiesInRange(target, radius)) {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if(damageable == null) continue;

            damageable.TakeDamage(1, 1, ElementType.None, transform);
        }
    }
}
