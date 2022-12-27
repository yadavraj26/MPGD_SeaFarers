using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGroupEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);

    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Player dead");
            Destroy(c.gameObject);
        }

    }
}
