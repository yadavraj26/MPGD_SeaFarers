using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyStopper : MonoBehaviour
{
    public GameObject[] followManagerRef;

    // Start is called before the first frame update
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
        foreach (GameObject i in followManagerRef)
        {
            i.GetComponent<FollowEnemyManager>().EnableDisableFollow(false);
        }
    }
}
