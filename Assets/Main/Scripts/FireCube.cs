using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCube : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
    }
}
