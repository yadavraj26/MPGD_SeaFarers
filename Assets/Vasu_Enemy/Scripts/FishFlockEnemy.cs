using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlockEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Player dead");
            Destroy(c.gameObject);
        }

    }
}
