using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPause;
    private void start()
    {
        isPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Esc press");
            if (isPause == true)
            {
                OnResume();
            }else
            {
                OnPause();
            }
            
        }
    }

    public void OnPause()
    {
        var Menu = PauseMenu.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        isPause = true;
        Time.timeScale = 0;

    }

    public void OnResume()
    {
        var Menu = PauseMenu.GetComponent<Canvas>();
        Menu.enabled = !Menu.enabled;
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public void button02()
    {

    }

    public void button03()
    {
        
    }


}
