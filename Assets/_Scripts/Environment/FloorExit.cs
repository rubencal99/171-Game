using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorExit : MonoBehaviour
{
    public static FloorExit instance;
    //public GameObject Player;

    public string sceneName;

    public Camera oldMainCamera;

    public Scene oldScene;



    public SceneManager SceneManager;
private AsyncOperation sceneAsync;

void Awake()
{
   instance = this;
   oldScene = SceneManager.GetActiveScene();
}

void Start() {
  
}
IEnumerator loadScene(string index)
{
    //SceneManager.LoadScene(index, LoadSceneMode.Single);
    AsyncOperation scene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    scene.allowSceneActivation = false;
    sceneAsync = scene;
    PlayerProgressManager.SavePlayer(Player.instance.GetComponentInChildren<PlayerWeapon>(), Player.instance.GetComponent<Player>());

    //Wait until we are done loading the scene
    while (scene.progress < 0.90f)
    {
        Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
        yield return null;
    }
     scene.allowSceneActivation = true;
    //  Wait until the asynchronous scene fully loads
    while (!scene.isDone)
    {
         Debug.Log("still loading...");
        yield return null;
    }

      Debug.Log("Loaded scene " + " [][] Progress: " + scene.progress);
     OnFinishedLoadingAllScene();
}

void enableScene()
{
    //Activate the Scene
    sceneAsync.allowSceneActivation = true;
    //  AudioListener audio = (AudioListener)FindObjectOfType(typeof(AudioListener));
    // if(audio!=null) audio.enabled = false;

    //  oldMainCamera.gameObject.SetActive(false);

    //  foreach (var go in oldScene.GetRootGameObjects())
    //     if(go.tag != "Player" || go.tag != "MainCamera")
    //         Destroy(go);

    Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
    if (sceneToLoad.IsValid())
    {
        Debug.Log("Scene is Valid");
      //  SceneManager.MoveGameObjectToScene(Player, sceneToLoad);
       // SceneManager.UnloadSceneAsync(oldScene);
        SceneManager.SetActiveScene(sceneToLoad);
    }
}

void OnFinishedLoadingAllScene()
{
    Debug.Log("Done Loading Scene");
    enableScene();
    //Debug.Log("Scene Activated!");
}

public void CallLoadScene() {
     StartCoroutine(loadScene(sceneName));
     Debug.Log("Begin Scene load: " + sceneName);
}
    // void OnTriggerEnter(Collider other) {
    //    // if(!guarded) {
    //         if(other.tag == "Player") {
    //            // this.GetComponent<Collider2D>().isTrigger = true;
    //             //this.transform.GetChild(0).gameObject.SetActive(false);
    //             Debug.Log("exited map");
    //              StartCoroutine(loadScene(sceneName));
    //             //this.GetComponent<ExitGame>().doExitGame();
    //         }
    //  //   }
    // }
}
