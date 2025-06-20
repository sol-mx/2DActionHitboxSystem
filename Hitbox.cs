using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hitboxes are attached to triggers that deal damage upon collision with a hurtbox.
/// </summary>
public class Hitbox : MonoBehaviour
{
    // References
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    [Header("Settings | Enable")]
    [SerializeField] private bool enableOnAwake;
    [SerializeField] private bool permanent;

    [Header("Settings | Damage")]
    [SerializeField] protected int damage;
    [SerializeField] protected float knockback;
    [SerializeField] protected bool friendlyFire;

    [Header("Settings | Duration")]
    [SerializeField] protected float preDelay;
    [SerializeField] protected float duration;

    private Coroutine enableRoutine;

    // ================================================================================================================ INITIALIZATION

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (!enableOnAwake)
        {
            col.enabled = false;
            sprite.enabled = false;
        }
        else
        {
            Enable();
        }
    }

    // ================================================================================================================ ENABLE/DISABLE

    public void Enable()
    {
        enableRoutine = StartCoroutine(EnableRoutine());
    }

    protected virtual IEnumerator EnableRoutine()
    {
        yield return new WaitForSeconds(preDelay);

        col.enabled = true;

        if (permanent) yield break;

        yield return new WaitForSeconds(duration);

        col.enabled = false;
    }

    // ================================================================================================================ COLLISION-HANDLING

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get attacker type for friendly fire check
        References.EntityRef.Type hitboxType = this switch
        {
            PlayerHitbox _ => References.EntityRef.Type.PLAYER,
            EnemyHitbox _ => References.EntityRef.Type.ENEMY,
            InteractableHitbox _ => References.EntityRef.Type.INTERACTABLE,
            _ => References.EntityRef.Type.PLAYER,
        };

        // Handling against units
        if (EntityUtilities.TryGetUnitHurtbox(collision, hitboxType, friendlyFire, out var unitHurtbox))
        {
            DamageUnit(unitHurtbox);
        }
        // Handling against interactables
        else if (TryGetInteractableHurtbox(collision, out var interactableHurtbox))
        {
            DamageInteractable(interactableHurtbox);
        }
        // No valid collision
        else
        {
            return;
        }

        // Clean-up
        CleanUpAfterDamageCollision();
    }

    // ================================================================================================================ OVERRIDABLES

    protected virtual void DamageUnit(UnitHurtbox hurtbox)
    {
        EntityUtilities.DamageAndKnockback(hurtbox, damage, transform.position, knockback);
    }

    protected virtual bool TryGetInteractableHurtbox(Collider2D collision, out InteractableHurtbox interactableHurtbox)
    {
        if (collision.TryGetComponent(out interactableHurtbox))
        {
            return true;
        }
        else
        {
            interactableHurtbox = null;
            return false;
        }
    }

    protected virtual void DamageInteractable(InteractableHurtbox hurtbox)
    {
        EntityUtilities.DamageAndKnockback(hurtbox, damage, transform.position, knockback);
    }

    protected virtual void CleanUpAfterDamageCollision()
    {
        if (gameObject != null)
        {
            if (enableRoutine != null)
            {
                StopCoroutine(enableRoutine);
                enableRoutine = null;
            }

            col.enabled = false;
        }
    }
}
