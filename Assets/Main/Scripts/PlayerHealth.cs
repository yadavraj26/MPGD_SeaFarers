using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseHealth(float health) 
    {
        if (maxHealth < 100)
        {
           // maxHealth += health; // adds health  
        }
    }

    public void DecreaseHealth(float health)
    {
        Debug.Log("Decreased");
        maxHealth = Mathf.Clamp(maxHealth - health, 0, maxHealth); // Subtracts the player health and keep it within a range[0-100]
        Debug.Log("decrease"+maxHealth);
    }
}
