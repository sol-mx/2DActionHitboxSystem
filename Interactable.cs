using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all interactable objects.
/// </summary>
public class Interactable : Entity
{
    private void Awake()
    {
        foreach (var hurtbox in GetComponentsInChildren<Hurtbox<Interactable>>())
        {
            hurtbox.SetOwner(this);
        }
    }
}
