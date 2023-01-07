using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float currentOxygen;
    public float health;
    public float[] position;
    public int difficulty;

    public PlayerData (PlayerController player, PlayerHealth playerHealth)
    {
        currentOxygen = player.currentOxygen;
        health = playerHealth.maxHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    public PlayerData(int difficultyRef)
    {
        //currentOxygen = player.currentOxygen;
        //health = playerHealth.maxHealth;
        difficulty = difficultyRef;
        //position = new float[3];
        //position[0] = player.transform.position.x;
        //position[1] = player.transform.position.y;
        //position[2] = player.transform.position.z;
    }
}
