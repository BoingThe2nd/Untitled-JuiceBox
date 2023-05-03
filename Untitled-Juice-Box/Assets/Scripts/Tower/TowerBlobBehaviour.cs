using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlobBehaviour : BlobBehvaiour
{

    GameObject currentTarget;
    float storedSpeed;
    public void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast((transform.position), transform.forward, out hit, 5f))
        {
            if (hit.collider.GetComponent<Enemy_Base>() != null)
            {
                currentTarget = hit.collider.gameObject;
            }
        }
        storedSpeed = GetComponent<Rigidbody>().velocity.magnitude;

    }

    private void FixedUpdate()
    {
        if (currentTarget != null)
        {
            transform.LookAt(new Vector3 (currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z));
        }
        
        GetComponent<Rigidbody>().velocity = transform.forward * storedSpeed;
    }

}
