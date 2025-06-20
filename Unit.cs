using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for player and enemy units.
/// </summary>
public class Unit : Entity
{
    private void Awake()
    {
        foreach (var hurtbox in GetComponentsInChildren<Hurtbox<Unit>>())
        {
            hurtbox.SetOwner(this);
        }
    }
}
