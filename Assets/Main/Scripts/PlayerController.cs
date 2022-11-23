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
    private float dist;
    private float moveX;
    private float moveZ;

    public GameObject torch;

    public bool isJumping;
    public bool isMoving;
    public bool isTorchOn;
    public bool isRunning;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity;

    public GameManager gmRef;
    private PlayerHealth playerHealth;
    public CharacterController characterController;
    public Animator anim;

    public enum States { move, idle }
    private States states;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        currentOxygen = maxOxygen;
        states = States.idle;
        torch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;  // gets position of the player in which direction it can move

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 2f; // increasing the player speed for sprinting
            isRunning = true;
            Debug.Log("Running");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
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


        if (states == States.idle)
        {
            currentOxygen -= 0.2f * Time.deltaTime; // reducing the oxygen at lower level when at idle state
        }


        GameObject pickup = GameObject.FindWithTag("Health");

        if (GameObject.FindWithTag("Health") == true)
        {
            dist = Vector3.Distance(pickup.transform.position, transform.position);
        }

        if (dist < 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerHealth.IncreaseHealth(20);
                Destroy(pickup);
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && !isTorchOn)
        {
            torch.SetActive(true);
            isTorchOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.T) && isTorchOn)
        {
            torch.SetActive(false);
            isTorchOn = false;
        }

        RunAnimations();
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
            playerHealth.DecreaseHealth(20);    // player costs some health when enemy hits
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

    public void RunAnimations()
    {
        anim.SetFloat("yAxis", moveZ);
        anim.SetFloat("xAxis", moveX);
        anim.SetBool("Jump", isJumping);
        anim.SetBool("Sprint", isRunning);
    }
}
