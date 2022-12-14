using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public bool startFollow;
    public GameObject player;
    public float interpSpeed;
    public float coolDownTimer;
    public PlayerController pControllerRef;
    public float catchUpSpeed;
    Vector3 dumy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pControllerRef = player.GetComponent<PlayerController>();
        if(pControllerRef.difficultyLevel==0)
        {
            catchUpSpeed = 1.5f;
        }
        else
        {
            catchUpSpeed = 2.5f;
        }
        //playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFollow)
        {
            Vector3 directionVector = player.transform.position - transform.position;

            
            Quaternion rotation = Quaternion.LookRotation(directionVector, Vector3.up);
            transform.rotation = rotation;
            //To make the enemy follow more closely find the forward position of player and lerp to that pos
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position+(player.transform.forward*1),interpSpeed);
        }


        float distance = Vector3.Distance(player.transform.position, transform.position);
        coolDownTimer -= Time.deltaTime;
        Mathf.Clamp(coolDownTimer, 0, 0.5f);
        
        //Check if player reached
        if (distance <= catchUpSpeed && coolDownTimer <= 0)
        {

            player.GetComponent<PlayerHealth>().DecreaseHealth(100);
            coolDownTimer = 0.5f;
            

        }

    }

    //Control for triggering the shark follow
    public void StartStopFollow(bool isStart)
    {
        gameObject.SetActive(isStart);
        startFollow = isStart;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag=="Player")
        {
            player.GetComponent<PlayerHealth>().DecreaseHealth(100);
            Debug.Log("dead");
        }
    }
}
