using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hurtboxes are attached to triggers that are dealt damage upon collision with a hitbox.
/// </summary>
public abstract class Hurtbox<T> : MonoBehaviour where T : Entity
{
    public abstract T Owner { get; protected set; }

    public void SetOwner(T owner)
    {
        Owner = owner;
    }
}
