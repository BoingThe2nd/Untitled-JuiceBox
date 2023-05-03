using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public ItemTower.TowerType m_TowerType;
    public GameManager.Flavour m_TowerFlavour;
    public GameObject CurrentTarget;
    public Queue<GameObject> TargetQueue = new Queue<GameObject>();
    public float TowerRange;
    private void Start()
    {
        //switch (m_TowerType)
        //{
        //    case TowerType.Standard:            
        //        break;
        //    case TowerType.Support:
        //        break;
        //    default:
        //        break;
        //}
    }

    public void SetAllData()
    {

    }


    public void SetStats()
    {

    }

    public void SetTowerType(ItemTower.TowerType towerType)
    {
        m_TowerType = towerType;
    }


    private void Update()
    {
        if (GameManager.m_GameState != GameManager.GameState.Defend)
        {
            return;
        }
        if (CurrentTarget != null)
        {
            transform.LookAt(new Vector3 (CurrentTarget.transform.position.x, transform.position.y, CurrentTarget.transform.position.z));
            float distanceFromTarget = Vector3.Distance(transform.position, CurrentTarget.transform.position);
            if (distanceFromTarget >= TowerRange)
            {
                CurrentTarget = null;
                return;
            }
        }
        else
        {
            if (TargetQueue.Count == 0)
            {
                if (LookForTargets())
                {
                    CurrentTarget = TargetQueue.Dequeue();
                    GetComponent<GeneralShoot>().Shooting = true;
                }
                else
                {
                    CurrentTarget = null;
                    GetComponent<GeneralShoot>().Shooting = false;
                }
            }
            else
            {
                CurrentTarget = TargetQueue.Dequeue();
                GetComponent<GeneralShoot>().Shooting = true;
            }
        } 

    }

    public bool LookForTargets()
    {
        Collider[] NearbyEnemies = Physics.OverlapSphere(transform.position, TowerRange);

        foreach (Collider enemy in NearbyEnemies)
        {
            if (enemy.GetComponent<Enemy_Base>() != null)
            {
                TargetQueue.Enqueue(enemy.gameObject);
            }
        }
        if (TargetQueue.Count != 0)
        {
            //transform.LookAt(new Vector3(CurrentTarget.transform.position.x, transform.position.y, CurrentTarget.transform.position.z));
            return true;
        }
        else
        {
            return false;
        }
    }

}
