using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject newGame;
    public GameObject loadGame;
    public GameObject option;
    public GameObject quit;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadGame()
    {
        playerController.LoadPlayer();
    }

    public void Options()
    {
        option.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
