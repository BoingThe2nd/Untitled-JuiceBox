using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehvaiour : MonoBehaviour
{

    [SerializeField] GameObject Blobplosion;
    int BlobDamage;


    public void UpdateStats(int Damage)
    {
        BlobDamage = Damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy_Base>() != null)
        {
            GameObject Explosion = Instantiate(Blobplosion, transform.position, Quaternion.identity);
            Explosion.GetComponent<BlobExplosion>().Damage = BlobDamage;
            //collision.gameObject.GetComponent<Enemy_Base>().ReceiveDamage(BlobDamage);
            Destroy(this.gameObject);
        }
    }



}
