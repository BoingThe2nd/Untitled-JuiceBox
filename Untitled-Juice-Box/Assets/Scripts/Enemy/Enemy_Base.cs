using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Base : MonoBehaviour
{
    NavMeshAgent m_Agent;

    [SerializeField] Enemy_Spawner m_Spawner;

    public float health;
    public int displayHealth;

    public int damageToDeal;
    bool dealtDamage = false;
    public void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        //Adjust Health according to level and etc;
        health = ((GameManager.Level/5) * 5) + 5;

        m_Agent.SetDestination(Farm.TheFarm.transform.position);
       
    }
    private void Update()
    {
       // m_Agent.SetDestination(FindObjectOfType<PlayerInputDriver>().transform.position);
       if (m_Agent.remainingDistance <= 1 && !dealtDamage)
        {
            Farm.TheFarm.TakeDamage(damageToDeal);
            dealtDamage = true;
            ReceiveDamage(health);
        }
    }

    public virtual void ReceiveDamage(float damageToReceive)
    {
        health -= damageToReceive;
        displayHealth = Mathf.RoundToInt(health);
        //Receive Damage Animation or Sprite Flash

        if (health <= 0)
        {
            BeginDeath();
        }
    } 

    public void SetOwner(Enemy_Spawner newOwner)
    {
        m_Spawner = newOwner;
    }

    public virtual void BeginDeath()
    {
        m_Spawner.SpawnedEnemies.Remove(this.gameObject);
        Destroy(this.gameObject);
    }


}
