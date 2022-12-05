using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager_Individual : MonoBehaviour
{
    public static Vector3 SetRotation = new Vector3(45, 0, 0);


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(SetRotation);
    }
}
