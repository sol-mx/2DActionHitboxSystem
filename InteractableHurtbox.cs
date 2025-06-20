using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Marker class for interactable hurtboxes.
/// </summary>
public class InteractableHurtbox : Hurtbox<Interactable> // define T as Interactable
{
    public override Interactable Owner { get; protected set; }
}
