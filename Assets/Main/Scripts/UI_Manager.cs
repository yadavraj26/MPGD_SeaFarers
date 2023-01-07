using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    public Canvas UI;
    public TextMeshProUGUI oxygenUI;
    public GameObject restartButton;
    public GameManager gm;
    public PlayerController pc;
    public TextMeshProUGUI gameStatusRef;
    public Image healthBar;
    public Image oxygenBar;
    public PlayerHealth playerHealthRef;
    public TextMeshProUGUI healthUI;
    public RawImage rearViewRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        oxygenUpdate();
        healthUpdate();

        if(playerHealthRef.maxHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene"); // when player dies, LoseScene will load
        }
    }

    void oxygenUpdate()
    {
        oxygenUI.text = pc.currentOxygen.ToString("F0");
        oxygenBar.fillAmount = pc.currentOxygen / 100;
    }

    void healthUpdate()
    {
        healthUI.text = playerHealthRef.maxHealth.ToString("F0");
        healthBar.fillAmount = playerHealthRef.maxHealth / 100;
    }

    public void gameEnd(bool gameStatus)
    {

        Cursor.lockState = CursorLockMode.None;
        //restartButton.SetActive(true);
        if (gameStatus)
        {
            gameStatusRef.text = "Win";
            SceneManager.LoadScene("Assets/Main/Scenes/WinScene.unity");
        }
        else
        {
            gameStatusRef.text = "Try Again";
            SceneManager.LoadScene("Assets/Main/Scenes/LoseScene.unity");
        }

        //gameStatusRef.gameObject.SetActive(true);

        Time.timeScale = 0;

    }

    public void EnableDisableRearView(bool isEnable)
    {
        rearViewRef.gameObject.SetActive(isEnable);
    }
}
