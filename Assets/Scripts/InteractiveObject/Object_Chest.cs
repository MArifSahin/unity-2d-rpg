using UnityEditor.Tilemaps;
using UnityEngine;

public class Object_Chest : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Animator animator => GetComponentInChildren<Animator>();
    private Entity_VFX entityVFX => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    public bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        entityVFX?.PlayOnDamageVFX();
        animator.SetBool("chestOpen", true);
        rb.linearVelocity = knockback; // Simulate a knockback effect
        rb.angularVelocity = Random.Range(-200f, 200f); // Add some random rotation

        return true;
        //Drop items

    }
}
