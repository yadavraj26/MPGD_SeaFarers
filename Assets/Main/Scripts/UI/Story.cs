using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public void OnEnable()
    {
        SceneManager.LoadScene("MainScene");
    }
}
