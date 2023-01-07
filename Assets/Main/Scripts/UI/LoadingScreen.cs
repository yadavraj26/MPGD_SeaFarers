using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneID);
        loadingScreen.SetActive(true);

        while (!oper.isDone)
        {
            float progress = Mathf.Clamp01(oper.progress/0.9f);
            Debug.Log("progress" + progress);

            slider.value = progress;

            yield return null;
        }

    }
}
