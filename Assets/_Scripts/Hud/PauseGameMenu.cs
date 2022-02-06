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
            var Menu = PauseMenu.GetComponent<Canvas>();
            Menu.enabled = !Menu.enabled;
            if (isPause == true)
            {
                OnResume();
            }else
            {
                OnPause();
            }
            isPause = !isPause;
        }
        
    }

    public void OnPause()
    {
        Time.timeScale = 0;

    }

    public void OnResume()
    {
        Time.timeScale = 1.0f;
    }

    public void button02()
    {

    }

    public void button03()
    {
        
    }


}
