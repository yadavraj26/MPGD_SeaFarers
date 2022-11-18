using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpHeight = 4f;
    public float gravity = -9.81f;
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDepletionRate = 0.6f;
    public GameManager gmRef;

    public GameObject cube;

    public bool isJumping;
    public bool isMoving;
    public bool isSpawned;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity;

    private PlayerHealth playerHealth;
    public CharacterController characterController;

    public enum States { move, idle }
    private States states;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        currentOxygen = maxOxygen;
        states = States.idle;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = transform.right * x + transform.forward * z;  // gets position of the player in which direction it can move

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 2f; // increasing the player speed for sprinting 
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 2f;
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
        {
            isMoving = true;
            states = States.move;      // checking whether the player is moving or not, then changing the bool value and changing it's state
        }
        else
        {
            isMoving = false;
            states = States.idle;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -gravity); // allow player to jump at the cost of some oxygen 
            currentOxygen -= 2;
            isJumping = true;
        }

   

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Attack();

        if (states == States.idle)
        {
            currentOxygen -= 0.2f * Time.deltaTime; // reducing the oxygen at lower level when at idle state
        }

    }

    private void FixedUpdate()
    {
        if (states == States.move)
        {
            OxygenMeter(); // reducing the oxygen at higher level if player is moving
        }

    }


    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.DecreaseHealth(20);
            Debug.Log(playerHealth.maxHealth); // player costs some health when enemy hits 
        }

        if(collision.gameObject.CompareTag("Health"))
        {
            playerHealth.IncreaseHealth(20);
            Debug.Log(playerHealth.maxHealth); // increasing health after collecting a health pickup
        }

        if (collision.gameObject.CompareTag("Oxygen"))
        {
            currentOxygen += 10; // adds oxygen after collecting oxygen pickup
            if (currentOxygen > maxOxygen)
            {
                currentOxygen = maxOxygen;
            }
        }

        if (collision.gameObject.CompareTag("Test"))
        {
            Debug.Log("Fell into the depths of");
        }

        if (collision.gameObject.CompareTag("Ship"))
        {
            Debug.Log("Win");
            gmRef.gameEnd(true);
        }

    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Platform")
        {
            isJumping = false; // checking if player is on the platform and changing the bool value of isJumping
        }
        if (hit.gameObject.tag == "movePlatform")
        {
            isJumping = false;
            this.transform.SetParent(hit.gameObject.transform);
        }
        else
        {
            this.transform.SetParent(null);
        }
        if (hit.gameObject.tag == "Ship")
        {
            gmRef.gameEnd(true);
        }
        if(hit.gameObject.tag == "MarianaTrench")
        {
            gmRef.gameEnd(false);
            //Debug.Log("Game lost");
        }
    }

    private void Attack()
    {
        if(Input.GetButtonDown("Fire1"))  // attack function
        {
            Debug.Log("Attacking!!");
            SpawnCube();
            isSpawned = true;
        }
    }

    public void OxygenMeter()  
    {
        if (currentOxygen != 0)
        {
            currentOxygen -= (Time.deltaTime * oxygenDepletionRate); // Reducing the player's oxygen over time
            Debug.Log(currentOxygen);
        }
        if (currentOxygen <= 0) // if oxgyen is zero, player dies
        {
            currentOxygen = 0;  
            Debug.Log("Player is Dead!!");
        }
    }

    public void SpawnCube()  // Spawning cubes to attack the enemy 
    {
        Vector3 playerDirec = transform.forward;
        Vector3 playerPos = transform.position;

        Vector3 spawnPos = playerDirec * 3 + playerPos;  

        GameObject block = Instantiate(cube, spawnPos, transform.localRotation);

        Destroy(block, 1.5f);
        isSpawned = false;
    }
}
