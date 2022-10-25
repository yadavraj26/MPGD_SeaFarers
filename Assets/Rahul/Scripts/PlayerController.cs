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


    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity;

    private PlayerHealth playerHealth;
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        currentOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = transform.right * x + transform.forward * z;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 4f; 
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Attack();
        OxygenMeter();
        
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
            currentOxygen -= Time.deltaTime;
            Debug.Log(currentOxygen);
        }
        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            Debug.Log("Player is Dead!!");
        }
    }
}
