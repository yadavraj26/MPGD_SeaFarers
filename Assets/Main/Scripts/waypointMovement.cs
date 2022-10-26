using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed = 10f;
    float wpRadius = 1f;


    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(waypoints[current].transform.position, transform.position)< wpRadius)
        {
            current++;
            if (current >= waypoints.Length)
                current = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
