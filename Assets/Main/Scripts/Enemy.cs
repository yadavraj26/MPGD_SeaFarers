using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent navAgent;
    GameObject player;
    //bool hitFlag=false;
    float coolDownTimer;
    //[SerializeField]
    public PlayerHealth playerHealth;
    public bool debug;
    

    // Start is called before the first frame update
    void Start()
    {
        
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        coolDownTimer -= Time.deltaTime;
        Mathf.Clamp(coolDownTimer, 0, 0.5f);


        if(debug)
            Debug.Log("hit"+navAgent.stoppingDistance+"            "+distance);

        if (distance <= navAgent.stoppingDistance&& coolDownTimer<=0)
        {
            
            playerHealth.DecreaseHealth(20);
            coolDownTimer = 0.5f;
            //hitFlag = true;
            //Debug.Log("Enemy hit");

        }

        if (distance > navAgent.stoppingDistance)
        {
            navAgent.SetDestination(player.transform.position);
        }
    }


}