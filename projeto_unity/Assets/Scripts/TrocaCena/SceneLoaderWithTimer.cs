using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderWithTimer : MonoBehaviour
{
    public float delayTime = 3f;
    public string sceneToLoad = "YourNextSceneName";
    
    public void StartNewScene()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
