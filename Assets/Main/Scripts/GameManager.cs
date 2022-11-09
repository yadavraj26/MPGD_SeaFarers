using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public float oxygenDepletionRate;
    public Canvas UI;
    public TextMeshProUGUI oxygenUI;
    public UI_Manager uiManagerRef;
    public PlayerController pcRef;

    public float oxygenVar;
    private int currentCheckPoint;
    //public List<GameObject> checkPointRef;
    // Start is called before the first frame update
    void Start()
    {
        oxygenVar = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        oxygenDepletion();
        //updateUI();

    }

    void oxygenDepletion()
    {
        //oxygenVar = oxygenVar - (Time.deltaTime * oxygenDepletionRate);
        if(pcRef.currentOxygen<=0.0f)
        {

            //Time.timeScale = 0.0f;
            uiManagerRef.gameEnd(false);

        }
    }

    void updateUI()
    {
        oxygenUI.text = oxygenVar.ToString("F0");
    }

    public void checkPoint(SC_CheckPoint checkPointRef)
    {
        if(checkPointRef.checkPointCounter>currentCheckPoint)
        {
            currentCheckPoint = checkPointRef.checkPointCounter;
        }
    }

    public void restart()
    {
        Debug.Log("Restart Clicked");
        SceneManager.LoadScene("Assets/Main/Scenes/MainScene.unity");
    }

    public void win()
    {
        uiManagerRef.gameEnd(true);
    }
}
