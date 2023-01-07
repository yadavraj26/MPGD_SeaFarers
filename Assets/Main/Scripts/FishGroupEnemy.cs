using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class FishGroupEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;
    public GameObject player;
    private float coolDownTimer;


    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
        float distance = Vector3.Distance(player.transform.position, transform.position);
        coolDownTimer -= Time.deltaTime;
        Mathf.Clamp(coolDownTimer, 0, 0.5f);

        if (distance <= 2.6 && coolDownTimer <= 0)
        {

            player.GetComponent<PlayerHealth>().DecreaseHealth(20);
            coolDownTimer = 0.5f;
            Destroy(gameObject);
            

        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
       

    }

   
        void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Player dead");
            Destroy(c.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player dead");
            Destroy(other.gameObject);
        }
    }
}
