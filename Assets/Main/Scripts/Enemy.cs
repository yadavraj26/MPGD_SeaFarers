using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navAgent;
    GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= navAgent.stoppingDistance)
        {
            playerHealth.DecreaseHealth(20);
            Debug.Log(playerHealth.maxHealth);
        }

        if (distance > navAgent.stoppingDistance)
        {
            navAgent.SetDestination(player.transform.position);
        }
    }


}