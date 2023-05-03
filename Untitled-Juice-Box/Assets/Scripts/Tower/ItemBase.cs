using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{

    public enum ItemType { PassOver, Grab}
    [SerializeField] public ItemType ThisItemType;


    public virtual void OnPickUp()
    {
        //Do Pick Up Effects
        //Such activate UI or Add Stats etc;
    }

    public virtual void OnPutDown()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (ThisItemType == ItemType.PassOver)
        {
            if (other.gameObject.GetComponent<PlayerStats>() != null)
            {
                OnPickUp();
            }
        }
    }
}
