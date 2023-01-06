using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyManager : MonoBehaviour
{
    public FollowEnemy followEnemyRef;
    public bool isStart;
    public UI_Manager uiManagerRef;
    private bool isFollowing;
    private float timer=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
        {
            timer = timer + Time.deltaTime;
            Debug.Log("timier"+timer);
            if(timer>18)
            {
                EnableDisableFollow(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        /*Debug.Log("Start");
        if(other.gameObject.CompareTag("Player"))
        {
            if(isStart)
            {*/
        EnableDisableFollow(true);
            /*}
            else
            {
                followEnemyRef.StartStopFollow(false);
                uiManagerRef.EnableDisableRearView(false);
            }
        }*/
    }
    public void EnableDisableFollow(bool isEnable)
    {
        followEnemyRef.StartStopFollow(isEnable);
        isFollowing = isEnable;
        uiManagerRef.EnableDisableRearView(isEnable);
        
    }
    

}

