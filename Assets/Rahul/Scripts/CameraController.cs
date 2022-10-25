using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSenstivity = 100f;
    public GameObject player;

    private float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;
        
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
