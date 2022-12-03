using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Base : MonoBehaviour
{
    NavMeshAgent m_Agent;


    public float health;
    public int displayHealth;

    public void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        //Test
       
    }
    private void Update()
    {
        m_Agent.SetDestination(FindObjectOfType<PlayerInputDriver>().transform.position);
    }
}
