using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionMenu : MonoBehaviour
{
    public Toggle fullScreenToggle, vSyncToggle;

    public List<int> widths = new List<int>() { 568, 960, 1280, 1920 };
    public List<int> heights = new List<int>() { 320, 540, 800, 1080 };
    
    private int index;

    public bool isOption;

    public GameObject optionMenu;
    public GameObject pauseMenu;

    public TextMeshProUGUI res;

    public PauseMenu pause;
    // Start is called before the first frame update
    void Start()
    {
        
        fullScreenToggle.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }

        pause = GetComponent<PauseMenu>();

        /*bool isRes = false;

        for(int i = 0; i < widths.Count; i++)
        {
            if(Screen.width == widths[i] && Screen.height == heights[i])
            {
                isRes = true;
                index = i;


                ResolutionUpdate();
            }
        }

        if(!isRes)
        {
            int w = Screen.width;
            int h = Screen.height;
            widths.Add(w);
            heights.Add(h);
            index = widths.Count - 1;

            ResolutionUpdate();
        }*/

        isOption = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyChanges()
    {
        //Screen.fullScreen = fullScreenToggle.isOn;

        if(vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullScreenToggle.isOn);
    }

    public void ResolutionLeft()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        ResolutionUpdate();
    }
    public void ResolutionRight()
    {
        index++;
        if(index > widths.Count - 1)
        {
            index = widths.Count - 1;
        }
        ResolutionUpdate();
    }

    public void ResolutionUpdate()
    {
        res.text = widths[index].ToString() + " X " + heights[index].ToString();
    }

    public void Close()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
