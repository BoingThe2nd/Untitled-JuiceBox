using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{

    [SerializeField]
    LayerMask BuildLayer;
    [SerializeField]
    LayerMask ItemLayer;
    public GameObject HeldObject;

    private bool hitting = false;
    private GameObject hitObject;

    private void Update()
    {
        if (HeldObject != null)
        {
            RaycastHit hit;


            if (Physics.Raycast((transform.position), transform.forward, out hit, 5f, BuildLayer))
            {
                if (hit.collider.gameObject.GetComponent<MapTile>() != null)
                {
                    GameObject go = hit.collider.gameObject;

                    if (hit.collider.gameObject.GetComponent<MapTile>().CheckTile())
                    {
                        if (hitObject == null)
                        {
                            go.SendMessage("OnHitEnter");
                            if (HeldObject.GetComponent<ItemTower>() != null)
                            {
                                HeldObject.GetComponent<ItemTower>().PreviewPlacement(go.transform.position);
                            }
                        }

                        //else if (hitObject.GetInstanceID() == go.GetInstanceID())
                        //{
                        //    hitObject.SendMessage("OnHitStay");
                        //}
                        else
                        {
                            hitObject.SendMessage("OnHitExit");
                            if (HeldObject.GetComponent<ItemTower>() != null)
                            {
                                HeldObject.GetComponent<ItemTower>().DisablePreview();
                            }
                            go.SendMessage("OnHitEnter");
                            if (HeldObject.GetComponent<ItemTower>() != null)
                            {
                                HeldObject.GetComponent<ItemTower>().PreviewPlacement(go.transform.position);
                            }
                        }
                        hitting = true;
                        hitObject = go;
                    }
                }

            }
            else if (hitting)
            {
                hitObject.SendMessage("OnHitExit");
                if (HeldObject.GetComponent<ItemTower>() != null)
                {
                    HeldObject.GetComponent<ItemTower>().DisablePreview();
                }
                hitting = false;
                hitObject = null;
            }
        }
        else if (hitObject != null && hitting)
        {
            hitObject.SendMessage("OnHitExit");
            
            hitting = false;
            hitObject = null;
        }
    }
    public void TryPickPlace()
    {
        if (GameManager.m_GameState != GameManager.GameState.Build)
        {
            return;
        }
        if (HeldObject != null)
        {
            RaycastHit hit;
            if (Physics.Raycast((transform.position), transform.forward, out hit, 5f, BuildLayer))
            {
                if (hit.collider.gameObject.GetComponent<MapTile>() != null)
                {
                    GameObject go = hit.collider.gameObject;

                    if (hit.collider.gameObject.GetComponent<MapTile>().CheckTile())
                    {
                        PutDown(hit.collider.gameObject);
                    }
                }
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast((transform.position), transform.forward, out hit, 5f, ItemLayer))
            {
                if (hit.collider.gameObject.GetComponent<ItemBase>().ThisItemType == ItemBase.ItemType.Grab)
                {
                    PickUp(hit.collider.gameObject);
                }
            }
        }
              
    }

    public void PickUp(GameObject ToHoldObject)
    {
        HeldObject = ToHoldObject;
        HeldObject.GetComponent<Collider>().enabled = false;
        Transform[] children = HeldObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(false);
        }
        HeldObject.SetActive(true);
        HeldObject.GetComponent<ItemBase>().OnPickUp();
        
        //Change Sprite to suit or Something similar
    }

    public void PutDown(GameObject DestinationTile)
    {
        HeldObject.transform.parent = DestinationTile.transform;
        HeldObject.transform.localPosition = Vector3.zero;

        DestinationTile.GetComponent<MapTile>().UpdateTileStatus(MapTile.TileStatus.Obstructed);
        //HeldObject.SetActive(true);
        //Activate Item to Instantiate Turret
        HeldObject.GetComponent<ItemBase>().OnPutDown();
        HeldObject = null;
        
    }

}
