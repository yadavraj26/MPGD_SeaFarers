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

    public bool isJumping;
    public bool isMoving;
    

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

        moveDirection = transform.right * x + transform.forward * z;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 2f; 
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);
        
        if(Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") < 0f || Input.GetAxis("Vertical") < 0f)
        {
            isMoving = true;
            states = States.move;
        }
        else
        {
            isMoving = false;
            states = States.idle;
            Debug.Log(states);
        }

        if (transform.localPosition.y <= 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
                currentOxygen -= 2;
                //Debug.Log(currentOxygen);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Attack();

        if(states == States.idle)
        {
            //isMoving = false;   
            currentOxygen -= 0.2f * Time.deltaTime;
            //Debug.Log("Depleting amount: " + currentOxygen);
            
        }

    }

    private void FixedUpdate()
    {
        if(states == States.move)
        {
            OxygenMeter();
        }

    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.DecreaseHealth(20);
            Debug.Log(playerHealth.maxHealth);
        }

        if(collision.gameObject.CompareTag("Pickup"))
        {
            playerHealth.IncreaseHealth(20);
            Debug.Log(playerHealth.maxHealth);
        }
    }

    private void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attacking!!");
        }
    }

    public void OxygenMeter()
    {
        if (currentOxygen != 0)
        {
            currentOxygen -= 0.6f * Time.deltaTime;
            //Debug.Log(currentOxygen);
        }
        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            //Debug.Log("Player is Dead!!");
        }
    }

}
