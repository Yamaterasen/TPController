using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField] GameObject _projectileSpawned = null;

    int _rank = 1;

    public override void Use(Transform origin, Transform target)
    {
        //spawn projectiles using origin's forwards direction by default
        GameObject projectile = Instantiate
            (_projectileSpawned, origin.position, origin.rotation);
        //if we have a target, ortate projectile towards it on spawn
        if(target != null)
        {
            projectile.transform.LookAt(target);
        }
        //make sure fireball doesn't travel infinitely
        Destroy(projectile, 3.5f);
    }
}
