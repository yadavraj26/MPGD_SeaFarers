using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public bool startFollow;
    public GameObject player;
    public float interpSpeed;
    public float coolDownTimer;
    Vector3 dumy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFollow)
        {
            Vector3 relativePos = player.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position+(player.transform.forward*1),interpSpeed);
        }


        float distance = Vector3.Distance(player.transform.position, transform.position);
        coolDownTimer -= Time.deltaTime;
        Mathf.Clamp(coolDownTimer, 0, 0.5f);

        if (distance <= 1 && coolDownTimer <= 0)
        {

            player.GetComponent<PlayerHealth>().DecreaseHealth(100);
            coolDownTimer = 0.5f;
            //hitFlag = true;
            //Debug.Log("Enemy hit");

        }

    }

    public void StartStopFollow(bool isStart)
    {
        gameObject.SetActive(isStart);
        startFollow = isStart;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag=="Player")
        {
            player.GetComponent<PlayerHealth>().DecreaseHealth(100);
            Debug.Log("dead");
        }
    }
}
