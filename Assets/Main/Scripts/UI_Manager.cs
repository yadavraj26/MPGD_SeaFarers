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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        oxygenUpdate();
    }

    void oxygenUpdate()
    {
        oxygenUI.text = pc.currentOxygen.ToString("F0");
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
    }
}
