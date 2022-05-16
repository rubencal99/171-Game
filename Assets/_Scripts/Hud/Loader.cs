using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject loadingScreen;

    public void loadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsychronously(sceneIndex));

    }

    IEnumerator LoadAsychronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(operation.progress);

            yield return null;
        }
        
    }

    
}
