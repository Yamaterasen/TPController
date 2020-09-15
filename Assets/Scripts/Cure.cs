using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : Ability
{
    int _healAmount = 25;

    public override void Use(Transform origin, Transform target)
    {
        // don't allow us to cast this spell without a target
        if(target == null) { return; }

        Debug.Log("Cast Curel on " + target.gameObject.name);
        //if the target has health, heal it
        target.GetComponent<Health>()?.Heal(_healAmount);
    }
}
