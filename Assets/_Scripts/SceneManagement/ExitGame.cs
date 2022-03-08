using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void doExitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
