using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAbility : Ability
{
    [SerializeField] GameObject _liftPulse = null;

    public override void Use(Transform origin, Transform target)
    {
        //spawn projectiles using origin's forwards direction by default
        GameObject projectile = Instantiate
            (_liftPulse, origin.position, origin.rotation);
        //make sure lift is cleaned up
        Destroy(projectile, 1f);
    }
}
