using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevelChoose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Easy()
    {
        LoadSystem.SaveDifficultyLevel(0);
        SceneManager.LoadScene("Assets/Main/Scenes/Level2 1.unity");
    }

    public void Hard()
    {
        LoadSystem.SaveDifficultyLevel(1);
        SceneManager.LoadScene("Assets/Main/Scenes/Level2 1.unity");
    }
}
