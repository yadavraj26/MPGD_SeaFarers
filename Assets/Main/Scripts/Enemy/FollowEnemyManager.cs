using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyManager : MonoBehaviour
{
    public FollowEnemy followEnemyRef;
    public bool isStart;
    public UI_Manager uiManagerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(isStart)
            {
                followEnemyRef.StartStopFollow(true);
                uiManagerRef.EnableDisableRearView(true);
            }
            else
            {
                followEnemyRef.StartStopFollow(false);
                uiManagerRef.EnableDisableRearView(false);
            }
        }
    }
    

}

