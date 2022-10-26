using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygen : MonoBehaviour
{
    //public gameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //gm.increaseoxygen(10);
            gameObject.SetActive(false);
        }
    }
}
