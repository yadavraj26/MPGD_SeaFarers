using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public RawImage rearView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        oxygenUpdate();
        healthUpdate();
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
        restartButton.SetActive(true);
        if (gameStatus)
        {
            gameStatusRef.text = "Win";
        }
        else
        {
            gameStatusRef.text = "Try Again";
        }

        gameStatusRef.gameObject.SetActive(true);

        Time.timeScale = 0;

    }

    public void EnableDisableRearView(bool isEnable)
    {
        rearView.gameObject.SetActive(isEnable);
    }
}
