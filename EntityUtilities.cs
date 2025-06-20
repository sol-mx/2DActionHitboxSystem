using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class for manipulating entities.
/// </summary>
public class EntityUtilities : MonoBehaviour
{
    // ================================================================================================================ GET HURTBOX

    public static bool TryGetUnitHurtbox(Collider2D collision, References.EntityRef.Type hitbox, bool friendlyFire, out UnitHurtbox hurtbox)
    {
        if ((hitbox == References.EntityRef.Type.PLAYER || hitbox == References.EntityRef.Type.INTERACTABLE || friendlyFire)
            && collision.gameObject.TryGetComponent(out EnemyHurtbox enemyHurtbox))
        {
            hurtbox = enemyHurtbox;
            return true;
        }
        else if ((hitbox == References.EntityRef.Type.ENEMY || hitbox == References.EntityRef.Type.INTERACTABLE || friendlyFire)
            && collision.gameObject.TryGetComponent(out PlayerHurtbox playerHurtbox))
        {
            hurtbox = playerHurtbox;
            return true;
        }
        else
        {
            hurtbox = null;
            return false;
        }
    }

    // ================================================================================================================ APPLY DAMAGE

    public static void Damage(UnitHurtbox target, int amount)
    {
        // damage logic against units
    }

    public static void Knockback(UnitHurtbox target, Vector3 selfPosition, float knockback)
    {
        // knockback logic against units
    }

    public static void DamageAndKnockback(UnitHurtbox target, int amount, Vector3 selfPosition, float knockback)
    {
        Damage(target, amount);
        Knockback(target, selfPosition, knockback);
    }

    public static void Damage(InteractableHurtbox target, int amount)
    {
        // damage logic against interactables
    }

    public static void Knockback(InteractableHurtbox target, Vector3 selfPosition, float knockback)
    {
        // knockback logic against interactables
    }

    public static void DamageAndKnockback(InteractableHurtbox target, int amount, Vector3 selfPosition, float knockback)
    {
        Damage(target, amount);
        Knockback(target, selfPosition, knockback);
    }
}