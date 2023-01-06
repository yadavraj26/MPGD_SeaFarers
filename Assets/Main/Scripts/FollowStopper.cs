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
        /*Debug.Log("Start");
        if(other.gameObject.CompareTag("Player"))
        {
            if(isStart)
            {*/
        foreach (GameObject i in followManagerRef)
        {
            i.GetComponent<FollowEnemyManager>().EnableDisableFollow(false);
        }
        /*}
        else
        {
            followEnemyRef.StartStopFollow(false);
            uiManagerRef.EnableDisableRearView(false);
        }
    }*/
    }

}
