using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float liftForce;
    public float liftRadius;
    GameObject liftParticlesSpawned;
    GameObject spellCastParticlesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay(.5f));
    }

    //Delay the lift to match animation
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        Lifting();
    }

    private void Lifting()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, liftRadius);

        foreach (Collider liftedObject in colliders)
        {
            if (liftedObject.CompareTag("Enemy"))
            {
                Rigidbody liftedrigidbody = liftedObject.GetComponent<Rigidbody>();

                liftedrigidbody.AddExplosionForce(liftForce, transform.position, liftRadius, liftForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, liftRadius);
    }
}
