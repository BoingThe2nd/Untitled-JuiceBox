using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobExplosion : MonoBehaviour
{
    public int Damage;
    bool DamageActivated;

    private void FixedUpdate()
    {
        if (!DamageActivated)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
            foreach (var Enemies in hitColliders)
            {
                if (Enemies.GetComponent<Enemy_Base>() != null)
                {
                    Enemies.GetComponent<Enemy_Base>().ReceiveDamage(Damage);
                }
            }
            DamageActivated = true;
        }

        if (!GetComponent<ParticleSystem>().isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

}
