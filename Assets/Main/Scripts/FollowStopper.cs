using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] followManagerRef;
    void Start()
    {
        followManagerRef = GameObject.FindGameObjectsWithTag("FollowManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       // Stop all the Sharks from following the player
        foreach (GameObject i in followManagerRef)
        {
            i.GetComponent<FollowEnemyManager>().EnableDisableFollow(false);
        }
       
    }

}
